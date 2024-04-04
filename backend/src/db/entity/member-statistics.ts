import { Column, Entity, JoinColumn, OneToOne } from "typeorm";
import { CreatedAt, Id } from "../typeorm-utils";
import { Member } from "./member";
import { CommonCode } from "./common-code";

@Entity({ comment: '사용자 통계'})
export class MemberStatistics {
    @Id('사용자 ID')
    memberId: string;

    @Id('통계 분야 코드 ID')
    fieldCodeId: string;

    @Column({
        type: 'int',
        nullable: true,
        comment: '통계 수치'
    })
    value: number;

    
    @OneToOne(() => Member)
    @JoinColumn({ name: 'member_id'})
    member: Member;

    @OneToOne(() => CommonCode)
    @JoinColumn({ name: 'field_code_id'})
    fieldCode: CommonCode;
}