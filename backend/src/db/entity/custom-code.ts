import { Column, Entity } from "typeorm";
import { CodeColumn, CodeTableEntity, Id } from "../typeorm-utils";
import { CustomCodeType } from "./custom-code-type";

@Entity({ comment: '커스텀 코드' })
export class CustomCode implements CodeTableEntity {
    @Id('커스텀 코드 ID', 'custom_code_id')
    id: string;

    @CodeColumn(CustomCodeType, 'type_id')
    type: CustomCodeType;

    @Column({ type: 'varchar', length: 16})
    description: string;
}