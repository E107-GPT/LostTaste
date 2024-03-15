import { Injectable } from '@nestjs/common';
import { Board } from 'src/db/entity/board';
import { LessThan, Repository } from 'typeorm';
import { BoardBriefDto } from './dto/board-brief.dto';

@Injectable()
export class BoardService {
    constructor(
        private readonly boardRepository: Repository<Board>
    ) {}

    async loadBelowId(limit: number, lastSeenId?: typeof Board.prototype.id): Promise<BoardBriefDto[]> {
        const boards: Board[] = await this.boardRepository.find({
            where: lastSeenId ? { id: LessThan(lastSeenId) } : undefined,
            take: limit,
            order: {id: 'DESC'}
        });

        return boards.map(BoardBriefDto.fromEntity);
    }
}
