import { Body, Controller, Get, Post, Req, UseGuards } from '@nestjs/common';
import { AuthGuard } from 'src/auth/auth.guard';
import { SignupDto } from 'src/user/dto/signup.dto';
import { UserService } from './user.service';

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
    getProfile(@Req() req) {
        return req.user;
    }
}
