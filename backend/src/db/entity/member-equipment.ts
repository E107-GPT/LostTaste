import { Entity, JoinColumn, ManyToOne, PrimaryColumn } from "typeorm";
import { CodeColumn, CreatedAt, Id } from "../typeorm-utils";
import { CommonCode } from "./common-code";
import { Member } from "./member";

@Entity({ comment: '사용자 착용 정보' })
export class MemberEquipment {

    @PrimaryColumn({
        type: 'char',
        length: 3,
        comment: '커스텀 코드 타입 ID'
    })
    customCodeTypeId: string;

    @Id('사용자 ID')
    memberId: string;

    @CodeColumn('custom_code_id', false)
    customCode: CommonCode;

    @ManyToOne(() => Member)
    @JoinColumn({ name: 'member_id' })
    member: Member;

    @CreatedAt('사용자 착용 정보 생성 시간')
    createdAt: Date;

}