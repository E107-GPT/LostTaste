import { Injectable, NotFoundException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import * as bcrypt from 'bcrypt';
import { UserService } from 'src/user/user.service';
import { LoginDto } from './dto/login.dto';
import { PasswordService } from 'src/password/password.service';

@Injectable()
export class AuthService {
    constructor(
        private readonly userService: UserService,
        private readonly jwtService: JwtService,
        private readonly passwordService: PasswordService,
    ){}

    async login(dto: LoginDto): Promise<{accessToken: string}> {
        const user = await this.userService.findByAccountId(dto.accountId);
        if (!user || !await this.passwordService.compare(dto.password, user.password)) {
            throw new NotFoundException();
        }

        return {
            accessToken: await this.jwtService.signAsync({
                sub: user.accountId,
                username: user.nickname
            })
        };
    }
}
