import { PetCode } from "src/types/codes";
import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { Code, GeneratedId } from "../decorators";
import { Member } from "./member";
import { EntityBase } from "../entity-base";

@Entity()
export class PetUnlock extends EntityBase {
    @GeneratedId('펫 해금 ID')
    id: number;

    @ManyToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member

    @Code('펫 코드')
    jobCode: PetCode
}