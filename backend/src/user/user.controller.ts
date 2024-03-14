import { Body, Controller, Get, Patch, Post, Req, UseGuards } from '@nestjs/common';
import { AuthGuard } from 'src/auth/auth.guard';
import { SignupDto } from 'src/user/dto/signup.dto';
import { UserService } from './user.service';
import { AuthUser } from 'src/auth/auth-util';
import { UserDto } from './dto/user.dto';
import { CustomChangeDto } from './dto/custom-change.dto';

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

    @Patch('custom')
    @UseGuards(AuthGuard)
    patchCustom(@AuthUser() user: UserDto, @Body() dto: CustomChangeDto) {
        this.userService.changeCustom(user, dto);
    }
}
