import { Body, Controller, Delete, Get, Param, Post, Query } from '@nestjs/common';
import { BoardService } from './board.service';
import { BoardPostDto } from './dto/board-post.dto';
import { BoardDeleteDto } from './dto/board-delete.dto';

@Controller('board')
export class BoardController {
  constructor(
    private readonly boardService: BoardService
  ) { }

  @Get()
  async get(
    @Query('limit') limit: number,
    @Query('before') before: string | undefined
  ) {
    return await this.boardService.loadBelowId(limit, before);
  }

  @Get(':boardId')
  async getBoardId(@Param('boardId') boardId: string) {
    return await this.boardService.loadDetail(boardId);
  }

  @Post()
  async post(dto: BoardPostDto) {
    this.boardService.post(dto);
  }

  @Delete(':boardId')
  async deleteBoardId(
    @Param('boardId') boardId: string,
    @Body() dto: BoardDeleteDto
  ) {
    this.boardService.delete(dto);
  }
}
