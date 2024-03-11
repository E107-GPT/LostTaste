import { Entity, PrimaryColumn } from "typeorm";
import { CodeColumn, CreatedAt, Id } from "../typeorm-utils";
import { CustomCode } from "./custom-code";

@Entity()
export class MemberEquipment {

    @PrimaryColumn({
        type: 'char',
        length: 3,
        comment: '커스텀 코드 타입 ID'
    })
    customCodeTypeId: string;

    @Id('사용자 ID')
    memberId: string;

    @CodeColumn(CustomCode, 'custom_code_id', false)
    customCode: CustomCode;

    @CreatedAt('사용자 착용 정보 생성 시간')
    createdAt: Date;

}