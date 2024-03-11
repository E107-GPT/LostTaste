import { Column, Entity } from "typeorm";
import { CodeTableEntity, Id } from "../typeorm-utils";

@Entity({ comment: '통계 분야 코드' })
export class StatisticsFieldCode implements CodeTableEntity {
    @Id('통계 분야 코드 ID', 'statistics_field_code_id')
    id: string;

    @Column({
        type: 'varchar',
        length: 16,
        nullable: true,
        comment: '통계 분야 이름'
    })
    name: string;

}