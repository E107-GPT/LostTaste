import { Board } from "src/db/entity/board";
import { CommonCode } from "src/db/entity/common-code"

export class BoardDetailDto {
    id: string;

    categoryCode: typeof CommonCode.prototype.id;

    title: string;

    nickname: string;

    createdAt: Date;

    reply: string;

    repliedAt: Date;

    static fromEntity(entity: Board): BoardDetailDto {
        return {
            id: entity.id,
            categoryCode: entity.category.id,
            title: entity.title,
            nickname: entity.nickname,
            createdAt: entity.createdAt,
            reply: entity.reply,
            repliedAt: entity.repliedAt
        };
    }
}