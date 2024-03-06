import { Entity, JoinColumn, OneToOne } from "typeorm";
import { Code, Id } from "../decorators";
import { Member } from "./member";
import { CampSkinCode, JobCode, PetCode, SkinCode } from "src/types/codes";
import { EntityBase } from "../entity-base";

@Entity()
export class MemberEquipment extends EntityBase {
    @Id('사용자 ID')
    id: number;

    @Code('착용 중 스킨 코드')
    skinCode: SkinCode;
    
    @Code('착용 중 캠프 스킨 코드')
    campSkinCode: CampSkinCode;

    @Code('착용 중 직업 코드')
    jobCode: JobCode;

    @Code('착용 중 펫 코드')
    petCode: PetCode;


    @OneToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member;
}