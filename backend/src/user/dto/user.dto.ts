import { Member } from "src/db/entity/member";

export class UserDto {
    nickname: string;

    accountId: string;

    static fromEntity(entity: Member): UserDto {
        return {
            nickname: entity.nickname,
            accountId: entity.accountId
        }
    }
}