import { Column, Entity } from "typeorm";
import { CodeColumn, CodeTableEntity, Id } from "../typeorm-utils";
import { CustomCodeType } from "./custom-code-type";

@Entity()
export class CustomCode implements CodeTableEntity {
    @Id('커스텀 코드')
    id: string;

    @CodeColumn(CustomCodeType, 'prefix')
    type: CustomCodeType;

    @Column({ type: 'varchar', length: 16})
    description: string;
}