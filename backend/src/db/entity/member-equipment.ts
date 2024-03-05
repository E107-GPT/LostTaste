import { Entity, JoinColumn, OneToOne } from "typeorm";
import { Code, Id } from "../decorators";
import { Member } from "./user";
import { CampSkinCode, JobCode, PetCode, SkinCode } from "src/types/codes";

@Entity()
export class MemberEquipment {
    @Id('사용자 ID')
    id: number;

    @Code('착용 중 스킨 코드')
    skinCode: keyof SkinCode;
    
    @Code('착용 중 캠프 스킨 코드')
    campSkinCode: keyof CampSkinCode;

    @Code('착용 중 직업 코드')
    jobCode: keyof JobCode;

    @Code('착용 중 펫 코드')
    petCode: keyof PetCode;

    @OneToOne(() => Member)
    @JoinColumn({ name: 'memberId' })
    member: Member;
}