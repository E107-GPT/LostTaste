import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Board } from 'src/db/entity/board';
import { LessThan, Repository } from 'typeorm';
import { BoardBriefDto } from './dto/board-brief.dto';
import { BoardDetailDto } from './dto/board-detail.dto';

@Injectable()
export class BoardService {
    constructor(
        @InjectRepository(Board)
        private readonly boardRepository: Repository<Board>
    ) {}

    async loadBelowId(limit: number, before?: typeof Board.prototype.id): Promise<BoardBriefDto[]> {
        const boards: Board[] = await this.boardRepository.find({
            where: before ? { id: LessThan(before) } : undefined,
            take: limit,
            order: {id: 'DESC'}
        });

        return boards.map(BoardBriefDto.fromEntity);
    }

    async loadDetail(id: string): Promise<BoardDetailDto> {
        const board = await this.boardRepository.findOneBy({ id });
        return BoardDetailDto.fromEntity(board);
    }
}
