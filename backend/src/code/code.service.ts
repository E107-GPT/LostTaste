import { Injectable, Logger, OnApplicationBootstrap } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CommonCode } from 'src/db/entity/common-code';
import { CommonCodeType } from 'src/db/entity/common-code-type';
import { Repository } from 'typeorm';
import { CodeCache, CodeCacheHierarchyTypeData } from './code-util';

@Injectable()
export class CodeService implements OnApplicationBootstrap {
    private readonly logger: Logger = new Logger(CodeService.name);

    constructor (
        @InjectRepository(CommonCode)
        private commonCodeRepository: Repository<CommonCode>,
    ) {}
    
    private codeCache: CodeCache = new CodeCache();

    private readonly CODE_PATTERN: RegExp = /^[A-Za-z]{3}_\d{4}$/;

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

            if (!hierarchical.has(type.id)) {
                hierarchical.set(type.id, new CodeCacheHierarchyTypeData(type));
            }
            hierarchical.get(type.id).codes.set(code.id, code);
            horizontal.set(code.id, code);
        });

        this.logger.log('공통 코드 로딩 완료!');
    }

    getCommonCodeTypeEntity(prefix: string): CommonCodeType | undefined {
        return this.codeCache.hierarchical.get(prefix).type;
    }
    
    getCommonCodeEntity(fullCode: string): CommonCode | undefined {
        if (!this.CODE_PATTERN.test(fullCode)) {
            throw new Error('잘못된 양식의 커스텀 코드입니다.');
        }
        
        return this.codeCache.horizontal.get(fullCode);
    }
}
