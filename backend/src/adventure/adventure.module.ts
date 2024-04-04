import { Module } from '@nestjs/common';
import { AdventureController } from './adventure.controller';
import { AdventureService } from './adventure.service';

@Module({
  controllers: [AdventureController],
  providers: [AdventureService]
})
export class AdventureModule {}
