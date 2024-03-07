import { Entity } from "typeorm";
import { CodeColumn, CodeTableEntity, Id } from "../typeorm-utils";
import { CustomCodeType } from "./custom-code-type";

@Entity()
export class CustomCode implements CodeTableEntity {
    @Id('커스텀 코드')
    id: number;

    @CodeColumn(CustomCodeType, 'prefix')
    prefix: CustomCodeType;
}