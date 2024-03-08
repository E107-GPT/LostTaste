import { Column, Entity, ManyToOne } from "typeorm";
import { CodeColumn, CreatedAt, Id } from "../typeorm-utils";
import { PartyMember } from "./party-member";
import { ItemCode } from "./item-code";

@Entity()
export class AdventureItemLog {
    @Id('사용자 ID')
    memberId: string;

    @Id('모험 ID')
    adventureId: string;

    @Id('아이템 코드')
    itemCodeId: string;

    @ManyToOne(() => PartyMember)
    partyMember: PartyMember;

    @ManyToOne(() => ItemCode)
    itemCode: ItemCode;

    @CreatedAt('로그 생성 시간')
    createdAt: Date;
}