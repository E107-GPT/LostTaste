import { IsNotEmpty, Length } from "class-validator";
import { CommonCode } from "src/db/entity/common-code";
import { IsCommonCode } from "src/validation/is-common-code.validator";

export class BoardPostDto {
    @IsCommonCode(['BCT'], {
        message: '게시판 카테고리 코드가 잘못되었습니다!'
    })
    categoryCode: typeof CommonCode.prototype.id;

    @Length(1, 16, {
        message: '닉네임은 1~16자 이내여야 합니다!'
    })
    nickname: string;

    @Length(4, 32, {
        message: '비밀번호는 4~32자 이내여야 합니다!'
    })
    password: string;

    @Length(1, 64, {
        message: '제목은 1~64자 이내여야 합니다!'
    })
    title: string;

    @IsNotEmpty({
        message: '내용은 필수입니다!'
    })
    content: string;
}