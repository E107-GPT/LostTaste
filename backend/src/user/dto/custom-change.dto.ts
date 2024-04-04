import { IsEnum } from "class-validator";
import { CustomType } from "src/common/enums";
import { IsCustomCode } from "src/validation/is-common-code.validator";

export class CustomChangeDto {
    @IsEnum(CustomType, {
        message: '유효하지 않은 커스텀 코드 타입입니다!'
    })
    customType: CustomType;

    @IsCustomCode({
        message: '커스텀 코드가 잘못되었습니다!'
    })
    code: string;
}