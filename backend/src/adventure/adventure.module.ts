import { Module } from '@nestjs/common';
import { AdventureController } from './adventure.controller';
import { AdventureService } from './adventure.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AdventureLog } from 'src/db/entity/adventure-log';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      AdventureLog
    ])
  ],
  controllers: [AdventureController],
  providers: [AdventureService]
})
export class AdventureModule {}
