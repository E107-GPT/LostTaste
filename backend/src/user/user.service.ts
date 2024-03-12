import { Injectable } from '@nestjs/common';

export type User = {
    id: number,
    username: string,
    password: string,
}

@Injectable()
export class UserService {
    private readonly testUsers: User[] = [
        {
            id: 1,
            username: 'ssafy1',
            password: 'q1w2e3r4!'
        },
        {
            id: 2,
            username: 'ssafy2',
            password: 'asdfasdf!'
        },
    ];

    async findOne(username: string): Promise<User | undefined> {
        return this.testUsers.find(user => user.username === username);
    }
}
