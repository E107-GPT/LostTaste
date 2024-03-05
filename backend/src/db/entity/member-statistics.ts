import { Entity, JoinColumn, OneToOne, PrimaryColumn } from "typeorm";
import { Id } from "../decorators";
import { Member } from "./user";

@Entity()
export class MemberStatistics {
    @Id('사용자 ID')
    id: number;


    @OneToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member;
}
