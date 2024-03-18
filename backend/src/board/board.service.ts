import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Board } from 'src/db/entity/board';
import { LessThan, Repository } from 'typeorm';
import { BoardBriefDto } from './dto/board-brief.dto';
import { BoardDetailDto } from './dto/board-detail.dto';
import { BoardPostDto } from './dto/board-post.dto';
import { CodeService } from 'src/code/code.service';
import { PasswordService } from 'src/password/password.service';
import { BoardDeleteDto } from './dto/board-delete.dto';

@Injectable()
export class BoardService {
    constructor(
        @InjectRepository(Board)
        private readonly boardRepository: Repository<Board>,

        private readonly codeService: CodeService,
        private readonly passwordService: PasswordService
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

    async post(dto: BoardPostDto) {
        this.boardRepository.save({
            category: this.codeService.getCommonCodeEntity(dto.categoryCode),
            nickname: dto.nickname,
            password: await this.passwordService.hash(dto.password),
            title: dto.title,
            content: dto.content
        })
    }

    async delete(dto: BoardDeleteDto) {
    }
}
