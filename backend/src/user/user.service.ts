import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';

import * as bcrypt from 'bcrypt';
import { SignupDto } from 'src/auth/dto/signup.dto';
import { Member } from 'src/db/entity/member';
import { Repository } from 'typeorm';

@Injectable()
export class UserService {
    private readonly HASH_SALT_ROUND = 10;

    constructor (
        @InjectRepository(Member)
        private memberRepository: Repository<Member>
    ) {}

    async findByAccountId(username: string): Promise<Member | undefined> {
        return this.memberRepository.findOne({ where: { accountId: username } });
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
}
