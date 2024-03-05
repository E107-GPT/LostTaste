import { JobCode } from "src/types/codes";
import { Entity, JoinColumn, ManyToOne } from "typeorm";
import { Code, GeneratedId } from "../decorators";
import { Member } from "./member";

@Entity()
export class JobUnlock {
    @GeneratedId('직업 해금 ID')
    id: number;

    @ManyToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member

    @Code('직업 코드')
    jobCode: keyof JobCode
}