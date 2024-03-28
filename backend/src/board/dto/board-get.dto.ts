import { IsNotEmpty, IsNumberString, IsOptional, Min } from "class-validator";
import { Board } from "src/db/entity/board";

export class BoardGetDto {
    @IsNotEmpty({
        message: '가져올 개수(limit)가 있어야 합니다!'
    })
    @Min(1, {
        message: '가져올 개수(limit)는 1 이상이어야 합니다!'
    })
    limit: number;

    @IsOptional()
    @IsNumberString({ no_symbols: true }, {
        message: '이전 번호는 숫자여야 합니다!'
    })
    before?: typeof Board.prototype.id | undefined;
}