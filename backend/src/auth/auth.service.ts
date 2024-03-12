import { Injectable, NotFoundException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import * as bcrypt from 'bcrypt';
import { UserService } from 'src/user/user.service';
import { LoginDto } from './dto/login.dto';

@Injectable()
export class AuthService {
    constructor(
        private userService: UserService,
        private jwtService: JwtService,
    ){}

    async login(dto: LoginDto): Promise<{accessToken: string}> {
        const user = await this.userService.findByAccountId(dto.username);
        if (bcrypt.compareSync(dto.password, user?.password)) {
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
