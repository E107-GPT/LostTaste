import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AdventureLog } from 'src/db/entity/adventure-log';
import { UserModule } from 'src/user/user.module';
import { AdventureController } from './adventure.controller';
import { AdventureService } from './adventure.service';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      AdventureLog
    ]),
    UserModule
  ],
  controllers: [AdventureController],
  providers: [AdventureService]
})
export class AdventureModule {}
