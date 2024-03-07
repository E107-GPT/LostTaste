import { Entity, JoinColumn, OneToOne } from "typeorm";
import { Id } from "../decorators";
import { CustomCodeType } from "./custom-code-type";

@Entity()
export class CustomCode {
    @Id('커스텀 코드')
    id: number;

    // @Code(CustomCodeType)
    @JoinColumn({ name: 'prefix' })
    @OneToOne(() => CustomCodeType)
    prefix: CustomCodeType;
}