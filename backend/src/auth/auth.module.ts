import { Module } from '@nestjs/common';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { UserModule } from 'src/user/user.module';
import { JwtModule } from '@nestjs/jwt';

const jwtModule = JwtModule.register({
  global: true,
  secret: 'jwtsecret',                // JWT Secret
  signOptions: { expiresIn: '3600s' },  // 만료 시간
});

@Module({
  imports: [UserModule, jwtModule],
  controllers: [AuthController],
  providers: [AuthService],
})
export class AuthModule { }