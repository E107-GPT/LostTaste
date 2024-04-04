import { Injectable } from '@nestjs/common';

@Injectable()
export class AppService {
  getHello(): string {
    return '안녕하세요 김피탕 전설 API 서버입니다.';
  }
}
