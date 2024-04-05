import { Body, Controller, Post, UseGuards } from '@nestjs/common';
import { AdventureService } from './adventure.service';
import { AuthGuard } from 'src/auth/auth.guard';
import { AuthUser } from 'src/auth/auth-util';
import { UserDto } from 'src/user/dto/user.dto';
import { AdventurePostDto } from './dto/adventure-post-dto';

@Controller('adventure')
export class AdventureController {
    constructor (
        private readonly adventureService: AdventureService
    ) {}

    @Post()
    @UseGuards(AuthGuard)
    async post(@AuthUser() user: UserDto, @Body() dto: AdventurePostDto) {
        return await this.adventureService.save(user, dto);
    }
}
