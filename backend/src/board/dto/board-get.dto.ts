import { IsNotEmpty } from "class-validator";
import { Board } from "src/db/entity/board";

export class BoardGetDto {
    @IsNotEmpty()
    limit: number;

    before?: typeof Board.prototype.id | undefined;
}