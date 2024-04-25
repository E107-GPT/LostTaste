import { Injectable } from '@nestjs/common';
import { UserDto } from 'src/user/dto/user.dto';
import { AdventurePostDto } from './dto/adventure-post-dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { AdventureLog } from 'src/db/entity/adventure-log';
import { UserService } from 'src/user/user.service';

@Injectable()
export class AdventureService {
    constructor (
        @InjectRepository(AdventureLog)
        private readonly adventureLogRepository: Repository<AdventureLog>,

        private readonly userService: UserService,
    ) {}

    async save(user: UserDto, dto: AdventurePostDto): Promise<void> {
        this.adventureLogRepository.save({
            captain: await this.userService.findByDto(user),
            partyName: dto.partyName,
            memberCount: dto.memberCount,
            playTime: dto.playTimeSec * 1000,
            rngSeed: dto.rngSeed,
        });
    }
}
