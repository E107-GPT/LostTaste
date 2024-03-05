import { SkinCode } from "src/types/codes";
import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { Code, GeneratedId } from "../decorators";
import { Member } from "./member";

@Entity()
export class SkinUnlock {
    @GeneratedId('스킨 해금 ID')
    id: number;

    @ManyToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member

    @Code('스킨 코드')
    skinCode: keyof SkinCode
}