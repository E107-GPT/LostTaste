import { IsEnum } from "class-validator";
import { CustomType } from "src/common/enums";
import { IsCustomCode } from "src/validation/is-common-code.validator";

export class CustomChangeDto {
    @IsEnum(CustomType)
    customType: CustomType;

    @IsCustomCode()
    code: string;
}