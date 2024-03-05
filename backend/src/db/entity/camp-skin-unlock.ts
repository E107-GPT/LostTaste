import { CampSkinCode } from "src/types/codes";
import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { Code, GeneratedId } from "../decorators";
import { Member } from "./user";

@Entity()
export class CampSkinUnlock {
    @GeneratedId('캠프 스킨 해금 ID')
    id: number;

    @ManyToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member

    @Code('캠프 코드')
    jobCode: keyof CampSkinCode
}