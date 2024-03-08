import { Column, Entity } from "typeorm";
import { CodeTableEntity, Id } from "../typeorm-utils";

@Entity()
export class ItemCode implements CodeTableEntity {
    @Id('아이템 코드', 'item_code_id')
    id: string;

    @Column({
        type: 'varchar',
        length: 16,
        nullable: true,
        comment: '아이템 이름'
    })
    itemName: string;
}