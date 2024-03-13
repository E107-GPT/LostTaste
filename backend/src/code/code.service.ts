import { Injectable, Logger, OnApplicationBootstrap } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CommonCode } from 'src/db/entity/common-code';
import { CommonCodeType } from 'src/db/entity/common-code-type';
import { Repository } from 'typeorm';
import { CodeMap, PrefixAndCode } from './code.type';

@Injectable()
export class CodeService implements OnApplicationBootstrap {
    private readonly logger: Logger = new Logger(CodeService.name);

    constructor (
        @InjectRepository(CommonCodeType)
        private commonCodeTypeRepository: Repository<CommonCodeType>,

        @InjectRepository(CommonCode)
        private commonCodeRepository: Repository<CommonCode>,
    ) {}
    
    private codeMap: CodeMap = new Map();

    private readonly CODE_PATTERN: RegExp = /^\[A-Za-z]{3}_\d{4}$/g;

    /**
     * Nest 부팅 시 실행되는 함수
     */
    async onApplicationBootstrap(): Promise<void> {
        const customCodes: CommonCode[] = await this.commonCodeRepository.find();

        customCodes.forEach((code) => {
            const prefix = code.type.id;
            if (!this.codeMap.has(prefix)) {
                this.codeMap.set(prefix, []);
            }
            this.codeMap.get(prefix).push({
                code: code.id,
                description: code.description
            });
        });

        this.logger.log('공통 코드 로딩 완료!');
    }

    async getCommonCodeTypeEntity(prefix: string): Promise<CommonCodeType | undefined> {
        return await this.commonCodeTypeRepository.findOne({where: {id: prefix} });
    }

    async getCommonCodeEntity(fullCode: string | PrefixAndCode): Promise<CommonCode | undefined> {
        let prefix = '';
        let code = '';
        if (typeof fullCode === 'string') {
            if (!this.CODE_PATTERN.test(fullCode)) {
                throw new Error('잘못된 양식의 커스텀 코드입니다.');
            }
    
            const matchArray: RegExpMatchArray = fullCode.match(this.CODE_PATTERN);
            prefix = matchArray[1];
            code = matchArray[2];
        }
        else {
            prefix = fullCode.prefix;
            code = fullCode.code;
        }
        
        return await this.commonCodeRepository.findOne({
            where: {type: {id: prefix}, id: code}
        });
    }
}
