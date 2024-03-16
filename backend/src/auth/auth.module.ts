import { Module } from '@nestjs/common';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { UserModule } from 'src/user/user.module';
import { JwtModule } from '@nestjs/jwt';
import { ConfigService } from '@nestjs/config';
import { PasswordModule } from 'src/password/password.module';

// 환경 변수를 동적으로 가져오기 위해 비동기를 사용
const jwtModule = JwtModule.registerAsync({
  inject: [ConfigService],
  global: true,
  useFactory: (configService: ConfigService) => ({
    secret: configService.get<string>('JWT_SECRET'),
    signOptions: {
      algorithm: 'HS256',
      expiresIn: configService.get<string>('JWT_EXPIRES_IN')
    }
  })
});

@Module({
  imports: [UserModule, jwtModule, PasswordModule],
  controllers: [AuthController],
  providers: [AuthService],
})
export class AuthModule { }