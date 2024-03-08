import { Column, Entity } from "typeorm";
import { CreatedAt, GeneratedId } from "../typeorm-utils";

@Entity()
export class Adventure {
    @GeneratedId('모험 ID')
    id: string;

    /**
     * 밀리초 단위
     */
    @Column({
        type: 'time',
        nullable: false,
        comment: '플레이 시간'
    })
    playTime: number;
    
    @Column({
        type: 'int',
        nullable: false,
        comment: 'RNG 시드값'
    })
    rngSeed: number;

    @Column({
        type: 'tinyint',
        nullable: false,
        comment: '진행 방 수'
    })
    roomCount: number;

    @Column({
        type: 'json',
        nullable: false,
        comment: '던전 탐색 경로'
    })
    path: AdventurePath;

    @CreatedAt('생성 시간')
    createdAt: Date;
}

export type AdventurePath = Array<Array<number>>;