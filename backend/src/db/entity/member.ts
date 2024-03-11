import { Column, Entity } from "typeorm";
import { CreatedAt, GeneratedId } from "../typeorm-utils";

@Entity({ comment: '사용자' })
export class Member {
    @GeneratedId('사용자 아이디', 'member_id')
    id: string

    @Column({
        type: 'varchar',
        length: 16,
        nullable: false,
        comment: '사용자 닉네임'
    })
    nickname: string;

    @Column({
        type: 'varchar',
        length: 16,
        nullable: false,
        comment: '로그인 시 사용할 ID'
    })
    accountId: string;

    @Column({
        type: 'char',
        length: 64,
        nullable: true,
        comment: '로그인 비밀번호 해시'
    })
    password: string;

    @Column({
        type: 'int',
        nullable: false,
        default: 0,
        comment: '사용자 보유 젤리'
    })
    jelly: number;

    @Column({
        type: 'boolean',
        nullable: false,
        default: false,
        comment: '사용자 회원 탈퇴 여부',
    })
    isDeleted: boolean;

    @CreatedAt('사용자 회원가입 시간')
    createdAt: Date;
}