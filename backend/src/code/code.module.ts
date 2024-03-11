import { Module } from '@nestjs/common';
import { CodeService } from './code.service';
import { CodeController } from './code.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { CommonCode } from 'src/db/entity/common-code';
import { ItemCode } from 'src/db/entity/item-code';
import { CommonCodeType } from 'src/db/entity/common-code-type';

@Module({
  imports: [TypeOrmModule.forFeature([
    CommonCodeType,
    CommonCode,
    ItemCode,
  ])],
  providers: [CodeService],
  controllers: [CodeController]
})
export class CodeModule {}
