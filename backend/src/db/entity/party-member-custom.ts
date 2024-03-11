import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { CodeColumn, Id } from "../typeorm-utils";
import { CommonCodeType } from "./common-code-type";
import { CommonCode } from "./common-code";
import { PartyMember } from "./party-member";

@Entity({ comment: '파티원 커스텀' })
export class PartyMemberCustom {
    @Id('커스텀 코드 타입')
    customCodeTypeId: string;

    @Id('사용자 ID', 'member_id')
    memberId: string;

    @Id('모험 ID', 'adventure_id')
    adventureId: string;

    @ManyToOne(() => CommonCodeType)
    customCodeType: CommonCodeType;
    
    @ManyToOne(() => PartyMember)
    @JoinColumn({ name: 'member_id', referencedColumnName: 'memberId' })
    @JoinColumn({ name: 'adventure_id', referencedColumnName: 'adventureId' })
    partyMember: PartyMember;

    @CodeColumn('custom_code_id', true)
    customCode: CommonCode;
}