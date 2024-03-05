import { Module } from '@nestjs/common';
import { UserService } from './user.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Member } from '../db/entity/member';

@Module({
  imports: [],
  providers: [UserService],
  exports: [UserService]
})
export class UserModule { }
