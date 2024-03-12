import { CommonCode } from "src/db/entity/common-code";
import { CommonCodeType } from "src/db/entity/common-code-type";

export class UserProfileDto {
    username: string;
    nickname: string;
    lastCustom: Map<typeof CommonCodeType.prototype.id, typeof CommonCode.prototype.id>;
}