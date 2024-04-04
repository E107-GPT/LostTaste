import { Column, Entity, PrimaryColumn } from "typeorm";

@Entity({ comment: '공통 코드 타입' })
export class CommonCodeType {
    @PrimaryColumn({
        name: 'common_code_type_id',
        type: 'char',
        length: 3,
        nullable: false,
        comment: '공통 코드 타입 ID'
    })
    id: string;

    @Column({
        type: 'varchar',
        length: 16,
        nullable: true,
        comment: '공통 코드 타입 설명'
    })
    description: string;
}