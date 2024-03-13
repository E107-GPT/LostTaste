import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { CommonCode } from 'src/db/entity/common-code';
import { CommonCodeType } from 'src/db/entity/common-code-type';
import { CodeController } from './code.controller';
import { CodeService } from './code.service';

@Module({
  imports: [TypeOrmModule.forFeature([
    CommonCodeType,
    CommonCode,
  ])],
  providers: [CodeService],
  controllers: [CodeController],
  exports: [CodeService]
})
export class CodeModule {}
