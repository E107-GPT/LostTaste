import { AdventureLog } from "src/db/entity/adventure-log";

export class RankingRecordsDto {
    records: RankingRecordDto[]

    static fromEntities(entities: AdventureLog[]): RankingRecordsDto {
        return {
            records: entities.map(entity => ({
                partyName: entity.partyName,
                playTimeSec: entity.playTime / 1000
            }))
        };
    }
}

class RankingRecordDto {
    partyName: string;
    playTimeSec: number;   // 소숫점 허용
}