import { GeneratedId, Jelly, Nickname, Password, Username } from 'src/db/decorators';
import { Entity, OneToMany, OneToOne } from 'typeorm';
import { CampSkinUnlock } from './camp-skin-unlock';
import { JobUnlock } from './job-unlock';
import { MemberStatistics } from './member-statistics';
import { PetUnlock } from './pet-unlock';
import { SkinUnlock } from './skin-unlock';
import { EntityBase } from '../entity-base';

@Entity()
export class Member extends EntityBase {
  @GeneratedId('사용자 일련번호')
  id: number;

  @Nickname('사용자 닉네임')
  nickname: string;

  @Username('로그인 시 사용자 이름')
  username: string;

  @Password('사용자 비밀번호')
  password: string;

  @Jelly('사용자 보유 젤리')
  jelly: number;


  @OneToMany(() => SkinUnlock, skinUnlock => skinUnlock.member)
  skinUnlocks: SkinUnlock[];

  @OneToMany(() => JobUnlock, jobUnlock => jobUnlock.member)
  jobUnlocks: JobUnlock[];

  @OneToMany(() => CampSkinUnlock, campSkinCode => campSkinCode.member)
  campSkinUnlocks: CampSkinUnlock[];

  @OneToMany(() => PetUnlock, petUnlock => petUnlock.member)
  petUnlocks: PetUnlock[];

  @OneToOne(() => MemberStatistics, memberStatistics => memberStatistics.member)
  memberStatistics: MemberStatistics;
}