import { Body, Controller, Get, Post, Req, UseGuards } from '@nestjs/common';
import { AuthGuard } from 'src/auth/auth.guard';
import { SignupDto } from 'src/user/dto/signup.dto';
import { UserService } from './user.service';
import { AuthUser } from 'src/auth/auth-util';
import { UserDto } from './dto/user.dto';

@Controller('user')
export class UserController {
    constructor (
        private readonly userService: UserService
    ) {}

    @Post()
    post(@Body() dto: SignupDto) {
        return this.userService.signup(dto);
    }

    @Get('profile')
    @UseGuards(AuthGuard)
    getProfile(@AuthUser() user: UserDto) {
        return this.userService.getProfile(user);
    }
}
