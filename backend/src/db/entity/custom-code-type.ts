import { Column, Entity, PrimaryColumn } from "typeorm";

@Entity()
export class CustomCodeType {
    @PrimaryColumn({
        type: 'char',
        length: 3,
        nullable: false,
        comment: '커스텀 코드 타입 접두사'
    })
    prefix: string;

    @Column({
        type: 'varchar',
        length: 16,
        nullable: true,
        comment: '커스텀 코드 타입 설명'
    })
    description: string;
}