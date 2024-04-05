import { Injectable } from '@nestjs/common';
import { UserDto } from 'src/user/dto/user.dto';
import { AdventurePostDto } from './dto/adventure-post-dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { AdventureLog } from 'src/db/entity/adventure-log';

@Injectable()
export class AdventureService {
    constructor (
        @InjectRepository(AdventureLog)
        private readonly gameClearLogRepository: Repository<AdventureLog>
    ) {}

    async save(user: UserDto, dto: AdventurePostDto): Promise<void> {

    }
}
