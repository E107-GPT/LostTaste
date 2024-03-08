import { Id, Jelly, Nickname, Password, Username } from 'src/orm/decorators';
import { Entity } from 'typeorm'

@Entity('Member')
export class User {
  @Id('사용자 일련번호')
  id: number;

  @Nickname('사용자 닉네임')
  nickname: string;

  @Username('사용자 이름')
  username: string;

  @Password('사용자 비밀번호')
  password: string;

  @Jelly('사용자 보유 젤리')
  jelly: number;
}