import { CommonCode } from "src/db/entity/common-code";
import { CommonCodeType } from "src/db/entity/common-code-type";
import { UserDto } from "./user.dto";

export class UserProfileDto extends UserDto {
    jelly: number;
    lastCustom: Map<typeof CommonCodeType.prototype.id, typeof CommonCode.prototype.id>;
}