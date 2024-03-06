import { CampSkinCode } from "src/types/codes";
import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { Code, GeneratedId } from "../decorators";
import { Member } from "./member";
import { EntityBase } from "../entity-base";

@Entity()
export class CampSkinUnlock extends EntityBase {
    @GeneratedId('캠프 스킨 해금 ID')
    id: number;

    @ManyToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member

    @Code('캠프 코드')
    jobCode: CampSkinCode
}