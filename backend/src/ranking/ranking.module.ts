import { Module } from '@nestjs/common';
import { RankingController } from './ranking.controller';
import { RankingService } from './ranking.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AdventureLog } from 'src/db/entity/adventure-log';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      AdventureLog
    ])
  ],
  controllers: [RankingController],
  providers: [RankingService]
})
export class RankingModule {}
