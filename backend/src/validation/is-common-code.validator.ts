import { ValidationArguments, ValidationOptions, ValidatorConstraint, ValidatorConstraintInterface, registerDecorator } from "class-validator";
import { CodeService } from "src/code/code.service";
import { CustomType } from "src/common/enums";

@ValidatorConstraint({ async: true })
export class IsCommonCodeConstraint implements ValidatorConstraintInterface {
    async validate(value: any, validationArguments?: ValidationArguments): Promise<boolean> {
        const [typeIds] = validationArguments.constraints;

        const hierarchical = CodeService.codeCache.hierarchical;
        for (const typeId of typeIds) {
            const codeIds = hierarchical.get(typeId).codes.keys();
            if (!codeIds) continue;
            for (const codeId of codeIds) {
                if (value === codeId) return true;
            }
        }
        return false;
    }
    defaultMessage?(validationArguments?: ValidationArguments): string {
        const [typeIds] = validationArguments.constraints;
        return validationArguments.property + '은(는) 다음 타입 내 유효한 공통 코드여야 합니다! [' + typeIds + ']';
    }
}

/**
 * 프로퍼티가 특정 타입의 공통 코드인지 검사합니다.
 * @param commonCodeTypes 공통 코드 타입 ID (접두사)
 * @param validationOptions 유효성 검사 옵션
 * @returns 공통 코드라면 true, 아니면 false
 */
export function IsCommonCode(commonCodeTypes: string[], validationOptions?: ValidationOptions) {
    return function (object: Object, propertyName: string) {
        registerDecorator({
            target: object.constructor,
            propertyName: propertyName,
            options: validationOptions,
            constraints: [commonCodeTypes],
            validator: IsCommonCodeConstraint,
        });
    }
}

/**
 * 프로퍼티가 커스텀 타입의 공통 코드인지 검사합니다.
 * @param validationOptions 유효성 검사 옵션
 * @returns 공통 코드라면 true, 아니면 false
 */
export const IsCustomCode = (validationOptions?: ValidationOptions) => IsCommonCode(Object.values(CustomType), validationOptions);