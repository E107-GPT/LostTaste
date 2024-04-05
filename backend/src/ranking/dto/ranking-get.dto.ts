import { IsNotEmpty, Min } from "class-validator";

export class RankingGetDto {
    @IsNotEmpty({
        message: '가져올 개수(limit)가 있어야 합니다!'
    })
    @Min(1, {
        message: '가져올 개수(limit)는 1 이상이어야 합니다!'
    })
    limit: number
}