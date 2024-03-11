import { Column, Entity, JoinColumn, ManyToOne } from "typeorm";
import { CodeColumn, CreatedAt, GeneratedId } from "../typeorm-utils";
import { Member } from "./member";
import { ItemCode } from "./item-code";

@Entity({ comment: '해금' })
export class Unlock {
    @GeneratedId('해금 ID', 'unlock_id')
    id: string;

    @ManyToOne(() => Member, { nullable: false })
    @JoinColumn()
    member: Member;

    @CodeColumn(ItemCode)
    itemCode: ItemCode;

    @Column({
        type: 'int',
        nullable: false,
        comment: '사용한 젤리'
    })
    price: number;

    @CreatedAt('해금 시간')
    createdAt: Date;
}