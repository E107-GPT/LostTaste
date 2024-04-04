import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { CreatedAt, Id } from "../typeorm-utils";
import { CommonCode } from "./common-code";
import { PartyMember } from "./party-member";

@Entity({ comment: '모험 아이템 기록' })
export class AdventureItemLog {
    @Id('사용자 ID', 'member_id')
    memberId: string;

    @Id('모험 ID', 'adventure_id')
    adventureId: string;

    @Id('아이템 코드')
    itemCodeId: string;

    @ManyToOne(() => PartyMember)
    @JoinColumn({ name: 'member_id', referencedColumnName: 'memberId' })
    @JoinColumn({ name: 'adventure_id', referencedColumnName: 'adventureId' })
    partyMember: PartyMember;

    @ManyToOne(() => CommonCode)
    itemCode: CommonCode;

    @CreatedAt('로그 생성 시간')
    createdAt: Date;
}