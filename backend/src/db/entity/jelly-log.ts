import { Column, Entity, JoinColumn, ManyToOne } from "typeorm";
import { Id } from "../typeorm-utils";
import { Member } from "./member";

@Entity()
export class JellyLog {
    @Id('젤리 변동 기록 ID', 'jelly_log_id')
    id: string;

    @ManyToOne(() => Member, { nullable: false })
    @JoinColumn({ name: 'member_id' })
    member: Member;

    @Column({
        type: 'int',
        name: 'delta',
        nullable: false,
        comment: '젤리 변동량'
    })
    delta: number;

    @Column({
        type: 'varchar',
        length: '16',
        nullable: false,
        comment: '젤리 변동 사유'
    })
    reason: string;
}