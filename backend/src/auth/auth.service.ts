import { Injectable, UnauthorizedException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UserService } from 'src/user/user.service';

@Injectable()
export class AuthService {
    constructor(
        private userService: UserService,
        private jwtService: JwtService
    ){}

    async login(username: string, password: string)
        : Promise<{accessToken: string}>
    {
        const user = await this.userService.findOne(username);
        if (user?.password !== password) {
            throw new UnauthorizedException();
        }

        return {
            accessToken: await this.jwtService.signAsync({
                sub: user.id,
                username: user.username
            })
        };
    }

}
