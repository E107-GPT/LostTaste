import { Injectable } from '@nestjs/common';
import { UserDto } from 'src/user/dto/user.dto';
import { AdventurePostDto } from './dto/adventure-post-dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { GameClearLog } from 'src/db/entity/game-clear-log';

@Injectable()
export class AdventureService {
    constructor (
        @InjectRepository(GameClearLog)
        private readonly gameClearLogRepository: Repository<GameClearLog>
    ) {}

    async save(user: UserDto, dto: AdventurePostDto): Promise<void> {

    }
}
