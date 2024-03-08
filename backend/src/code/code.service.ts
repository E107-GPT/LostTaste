import { Injectable, Logger, OnApplicationBootstrap } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { CustomCode } from 'src/db/entity/custom-code';
import { Repository } from 'typeorm';
import { CodeMap, CustomCodeMap } from './code.type';

@Injectable()
export class CodeService implements OnApplicationBootstrap {
    private readonly logger: Logger = new Logger(CodeService.name);

    constructor (
        @InjectRepository(CustomCode)
        private customCodeRepository: Repository<CustomCode>,
    ) {}
    
    private codeMap: CodeMap = {
        custom: new Map(),
        item: new Map()
    };

    // Nest 부팅 시 실행
    async onApplicationBootstrap(): Promise<void> {
        const custom = this.codeMap.custom;

        const customCodes: CustomCode[] = await this.customCodeRepository.find();

        customCodes.forEach((customCode) => {
            const prefix = customCode.type.prefix;
            if (custom.has(prefix)) {
                custom.set(prefix, new Map() as CustomCodeMap);
            }
            else {
                custom.get(prefix).set(customCode.id, customCode.description)
            }
        });
        
        // TODO: itemCode

        this.logger.log('공통 코드 로딩 완료!');
    }

}
