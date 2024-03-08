import { Module } from '@nestjs/common';
import { CodeService } from './code.service';
import { CodeController } from './code.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { CustomCode } from 'src/db/entity/custom-code';
import { ItemCode } from 'src/db/entity/item-code';
import { CustomCodeType } from 'src/db/entity/custom-code-type';

@Module({
  imports: [TypeOrmModule.forFeature([
    CustomCodeType,
    CustomCode,
    ItemCode,
  ])],
  providers: [CodeService],
  controllers: [CodeController]
})
export class CodeModule {}
