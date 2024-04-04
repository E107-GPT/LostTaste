import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Member } from 'src/db/entity/member';
import { UserController } from './user.controller';
import { UserService } from './user.service';
import { CodeModule } from 'src/code/code.module';
import { MemberEquipment } from 'src/db/entity/member-equipment';
import { PasswordModule } from 'src/password/password.module';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      Member,
      MemberEquipment
    ]),
    CodeModule,
    PasswordModule
  ],
  controllers: [UserController],
  providers: [UserService],
  exports: [UserService]
})
export class UserModule { }
