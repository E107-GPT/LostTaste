import { Column, Entity } from "typeorm";
import { CreatedAt, GeneratedId } from "../typeorm-utils";

@Entity({ comment: '게임 클리어 기록' })
export class GameClearLog {
  @GeneratedId('게임 클리어 기록 ID', 'game_clear_log_id')
  id: string;

  @Column({
    type: 'varchar',
    nullable: false,
    length: 16,
    comment: '파티 이름'
  })
  partyName: string;

  @Column({
    type: 'tinyint',
    nullable: false,
    comment: '파티 인원수'
  })
  memberCount: number;

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

  @CreatedAt('생성 시간')
  createdAt: Date;
}