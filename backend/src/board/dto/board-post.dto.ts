import { IsNotEmpty, Length } from "class-validator";
import { CommonCode } from "src/db/entity/common-code";
import { IsCommonCode } from "src/validation/is-common-code.validator";

export class BoardPostDto {
    @IsCommonCode(['BCT'])
    categoryCode: typeof CommonCode.prototype.id;

    @Length(1, 16)
    nickname: string;

    @Length(4, 32)
    password: string;

    @Length(1, 64)
    title: string;

    @IsNotEmpty()
    content: string;
}