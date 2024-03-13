import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Member } from 'src/db/entity/member';
import { UserController } from './user.controller';
import { UserService } from './user.service';
import { CodeModule } from 'src/code/code.module';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      Member
    ]),
    CodeModule
  ],
  controllers: [UserController],
  providers: [UserService],
  exports: [UserService]
})
export class UserModule { }
