import { Injectable, UnauthorizedException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UserService } from 'src/user/user.service';
import { LoginDto } from './dto/login.dto';
import * as bcrypt from 'bcrypt';

@Injectable()
export class AuthService {
    constructor(
        private userService: UserService,
        private jwtService: JwtService,
    ){}

    async login(dto: LoginDto): Promise<{accessToken: string}> {
        const user = await this.userService.findByAccountId(dto.username);
        if (bcrypt.compareSync(dto.password, user?.password)) {
            throw new UnauthorizedException();
        }

        return {
            accessToken: await this.jwtService.signAsync({
                sub: user.accountId,
                username: user.nickname
            })
        };
    }
}
