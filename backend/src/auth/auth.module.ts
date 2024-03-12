import { Module } from '@nestjs/common';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { UserModule } from 'src/user/user.module';
import { JwtModule } from '@nestjs/jwt';

const jwtModule = JwtModule.register({
  global: true,
  secret: process.env.JWT_SECRET,
  signOptions: {
    algorithm: 'HS256',
    expiresIn: process.env.JWT_EXPIRES_IN
  }
});

@Module({
  imports: [UserModule, jwtModule],
  controllers: [AuthController],
  providers: [AuthService],
})
export class AuthModule { }