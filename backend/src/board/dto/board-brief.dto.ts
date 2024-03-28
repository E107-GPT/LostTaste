import { Board } from "src/db/entity/board";
import { CommonCode } from "src/db/entity/common-code";

export class BoardBriefDto {
    id: string;

    categoryCode: typeof CommonCode.prototype.id;

    title: string;

    createdAt: Date;

    isReplied: boolean;

    static fromEntity(entity: Board): BoardBriefDto {
        return {
            id: entity.id,
            categoryCode: entity.category.id,
            title: entity.title,
            createdAt: entity.createdAt,
            isReplied: entity.repliedAt !== null && entity.repliedAt !== undefined
        };
    }
}