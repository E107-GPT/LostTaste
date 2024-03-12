import { Module } from '@nestjs/common';
import { UserService } from './user.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Member } from 'src/db/entity/member';

@Module({
  imports: [TypeOrmModule.forFeature([
    Member
  ])],
  providers: [UserService],
  exports: [UserService]
})
export class UserModule { }
