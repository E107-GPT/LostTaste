import { Column } from "typeorm";
import { CodeColumn, CreatedAt, Id, Password } from "../typeorm-utils";
import { CommonCode } from "./common-code";

export class Board {
    @Id('게시판 ID', 'board_id')
    id: string;

    @CodeColumn()
    category: CommonCode

    @Column({
        type: 'varchar',
        length: 16,
        nullable: false,
        comment: '작성자 닉네임'
    })
    nickname: string;

    @Password('게시글 비밀번호')
    password: string;

    @Column({
        type: 'varchar',
        length: 64,
        nullable: false,
        comment: '게시글 제목'
    })
    title: string;

    @Column({
        type: 'text',
        nullable: false,
        comment: '게시글 내용'
    })
    content: string;

    @CreatedAt('게시글 작성 시간')
    createdAt: Date;

    @Column({
        type: 'text',
        nullable: true,
        comment: '게시글 답글 내용'
    })
    reply: string;

    @Column({
        type: 'datetime',
        nullable: true,
        comment: '게시글 답글 단 시간'
    })
    repliedAt: Date;
}