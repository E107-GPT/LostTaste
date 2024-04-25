import { Injectable } from '@nestjs/common';
import { RankingGetDto } from './dto/ranking-get.dto';
import { RankingRecordsDto } from './dto/ranking-records.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { AdventureLog } from 'src/db/entity/adventure-log';
import { Repository } from 'typeorm';

@Injectable()
export class RankingService {
    constructor (
        @InjectRepository(AdventureLog)
        private readonly adventureLogRepository: Repository<AdventureLog>
    ) {}

    async load(dto: RankingGetDto): Promise<RankingRecordsDto> {
        let entities: AdventureLog[] = await this.adventureLogRepository.find({
            take: dto.limit,
            order: { playTime: 'ASC' }
        });

        return RankingRecordsDto.fromEntities(entities);
    }
}
