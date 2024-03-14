import { IsEnum } from "class-validator";
import { CustomType } from "src/common/enums";
import { IsCommonCode } from "src/validation/is-common-code.validator";

export class CustomChangeDto {
    @IsEnum(CustomType)
    customType: CustomType;

    @IsCommonCode()
    code: string;
}