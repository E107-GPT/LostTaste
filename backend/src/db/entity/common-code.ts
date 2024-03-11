import { Column, Entity, JoinColumn, ManyToOne } from "typeorm";
import { Id } from "../typeorm-utils";
import { CommonCodeType } from "./common-code-type";

@Entity({ comment: '커스텀 코드' })
export class CommonCode {
    @Id('커스텀 코드 ID', 'custom_code_id')
    id: string;

    @ManyToOne(() => CommonCodeType)
    @JoinColumn({
        name: 'type_id'
    })
    type: CommonCodeType;

    @Column({
        type: 'varchar',
        length: 16,
        comment: '커스텀 코드 설명'
    })
    description: string;
}