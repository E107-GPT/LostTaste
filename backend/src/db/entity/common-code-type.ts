import { Column, Entity, PrimaryColumn } from "typeorm";
import { CodeTableEntity } from "../typeorm-utils";

@Entity({ comment: '커스텀 코드 타입' })
export class CommonCodeType implements CodeTableEntity {
    @PrimaryColumn({
        name: 'custom_code_type_id',
        type: 'char',
        length: 3,
        nullable: false,
        comment: '커스텀 코드 타입 ID'
    })
    id: string;

    @Column({
        type: 'varchar',
        length: 16,
        nullable: true,
        comment: '커스텀 코드 타입 설명'
    })
    description: string;
}