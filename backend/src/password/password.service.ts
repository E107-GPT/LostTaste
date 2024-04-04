import { Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';

@Injectable()
export class PasswordService {
    private readonly HASH_SALT_ROUND = 10;

    async hash(plaintext: string) {
        return await bcrypt.hash(plaintext, this.HASH_SALT_ROUND);
    }

    async compare(plaintext: string, hashed: string) {
        return await bcrypt.compare(plaintext, hashed);
    }
}
