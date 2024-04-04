import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Member } from 'src/db/entity/member';
import { UserController } from './user.controller';
import { UserService } from './user.service';
<<<<<<< HEAD
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from './user.entity';

@Module({
  imports: [TypeOrmModule.forFeature([User])],
=======
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
>>>>>>> 7352ca8f72583167efc8ca3fe09206d5a54789e3
  providers: [UserService],
  exports: [UserService]
})
export class UserModule { }
