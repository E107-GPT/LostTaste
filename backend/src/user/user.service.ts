import { Injectable, Logger } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';

import * as bcrypt from 'bcrypt';
import { Member } from 'src/db/entity/member';
import { SignupDto } from 'src/user/dto/signup.dto';
import { Repository } from 'typeorm';
import { UserProfileDto } from './dto/user-profile.dto';
import { UserDto } from './dto/user.dto';

@Injectable()
export class UserService {
    private readonly HASH_SALT_ROUND = 10;

    constructor (
        @InjectRepository(Member)
        private readonly memberRepository: Repository<Member>,
    ) {}

    async findByAccountId(username: string): Promise<Member | undefined> {
        return this.memberRepository.findOne({ where: { accountId: username } });
    }

    async findByDto(dto: UserDto): Promise<Member | undefined> {
        return this.findByAccountId(dto.accountId);
    }

    async signup(dto: SignupDto): Promise<void> {
        this.memberRepository.save({
            accountId: dto.accountId,
            password: await this.hash(dto.password),
            nickname: dto.nickname
        });
    }

    async hash(plaintext: string) {
        return await bcrypt.hash(plaintext, this.HASH_SALT_ROUND);
    }

    async getProfile(user: UserDto): Promise<UserProfileDto> {
        const entity: Member = await this.findByDto(user);
        const equipments = entity.equipments;

        const customMap = new Map<string, string>();
        equipments.forEach(equipment => {
            const typeId = equipment.customCodeTypeId;
            const id = equipment.customCode.id;

            customMap.set(typeId, id);
        });

        return {
            ...user,
            lastCustom: customMap
        };
    }
}
