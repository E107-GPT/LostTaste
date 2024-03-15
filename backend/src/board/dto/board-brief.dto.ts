import { Board } from "src/db/entity/board";

export class BoardBriefDto {
    id: string;

    categoryCode: string;

    title: string;

    createdAt: Date;

    isReplied: boolean;

    static fromEntity(entity: Board): BoardBriefDto {
        return {
            id: entity.id,
            categoryCode: entity.category.id,
            title: entity.title,
            createdAt: entity.createdAt,
            isReplied: entity.repliedAt !== undefined
        };
    }
}