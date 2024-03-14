import { ValidationArguments, ValidationOptions, ValidatorConstraint, ValidatorConstraintInterface, registerDecorator } from "class-validator";
import { CodeService } from "src/code/code.service";

@ValidatorConstraint({ async: true })
export class IsCommonCodeConstraint implements ValidatorConstraintInterface {
    async validate(value: any, validationArguments?: ValidationArguments): Promise<boolean> {
        const codes = CodeService.codeCache.horizontal.keys();
        for (const code of codes) {
            if (code === value) return true;
        }
        return false;
    }
    defaultMessage?(validationArguments?: ValidationArguments): string {
        return validationArguments.property + '은(는) 존재하는 공통 코드여야 합니다!';
    }
}

export function IsCommonCode(validationOptions?: ValidationOptions) {
    return function (object: Object, propertyName: string) {
        registerDecorator({
            target: object.constructor,
            propertyName: propertyName,
            options: validationOptions,
            constraints: [],
            validator: IsCommonCodeConstraint
        });
    }
}