import { Controller, Get, Query } from '@nestjs/common';
import { RankingService } from './ranking.service';
import { RankingGetDto } from './dto/ranking-get.dto';

@Controller('ranking')
export class RankingController {
    constructor (
        private readonly rankingService: RankingService
    ) {}

    @Get()
    async get(@Query() dto: RankingGetDto) {
        return await this.rankingService.load(dto);
    }
}
