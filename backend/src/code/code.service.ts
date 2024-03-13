import { Injectable, Logger, OnApplicationBootstrap } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CommonCode } from 'src/db/entity/common-code';
import { CommonCodeType } from 'src/db/entity/common-code-type';
import { Repository } from 'typeorm';
import { CodeCache, CodeCacheHierarchyTypeData, PrefixAndCode } from './code-util';

@Injectable()
export class CodeService implements OnApplicationBootstrap {
    private readonly logger: Logger = new Logger(CodeService.name);

    constructor (
        @InjectRepository(CommonCode)
        private commonCodeRepository: Repository<CommonCode>,
    ) {}
    
    private codeCache: CodeCache = new CodeCache();

    private readonly CODE_PATTERN: RegExp = /^\[A-Za-z]{3}_\d{4}$/g;

    /**
     * Nest 부팅 시 실행되는 함수
     */
    async onApplicationBootstrap(): Promise<void> {
        this.cacheAllCodes();
    }

    async cacheAllCodes(): Promise<void> {
        const customCodes: CommonCode[] = await this.commonCodeRepository.find();

        const hierarchical = this.codeCache.hierarchical;
        const horizontal = this.codeCache.horizontal;
        customCodes.forEach((code) => {
            const type = code.type;
            const typeId = type.id;
            const codeId = code.id;
            const fullCode = this.toFullCode({ prefix: typeId, code: codeId });

            if (!hierarchical.has(typeId)) {
                hierarchical.set(typeId, new CodeCacheHierarchyTypeData(type));
            }
            hierarchical.get(typeId).codes.set(code.id, code);
            horizontal.set(fullCode, code);
        });

        this.logger.log('공통 코드 로딩 완료!');
    }

    /**
     * 코드 접두사와 ID를 받아 풀 코드를 구성합니다.
     * 예) "SKN_0001", "PET_0002", "ITM_1234"
     * @param prefixAndCode 코드 접두사와 코드 ID
     * @returns 채번된 풀 코드
     */
    toFullCode(prefixAndCode: PrefixAndCode) {
        return prefixAndCode.prefix + '_' + prefixAndCode.code.padStart(4, '0'); 
    }

    getCommonCodeTypeEntity(prefix: string): CommonCodeType | undefined {
        return this.codeCache.hierarchical.get(prefix).type;
    }
    
    getCommonCodeEntity(fullCode: string | PrefixAndCode): CommonCode | undefined {
        if (typeof fullCode === 'string') {
            if (!this.CODE_PATTERN.test(fullCode)) {
                throw new Error('잘못된 양식의 커스텀 코드입니다.');
            }
        }
        else {
            fullCode = this.toFullCode(fullCode);
        }
        
        return this.codeCache.horizontal.get(fullCode);
    }
}
