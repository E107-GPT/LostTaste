import { Body, Controller, Delete, Get, Param, Post, Query } from '@nestjs/common';
import { BoardService } from './board.service';
import { BoardPostDto } from './dto/board-post.dto';
import { BoardDeleteDto } from './dto/board-delete.dto';
import { BoardGetDto } from './dto/board-get.dto';

@Controller('board')
export class BoardController {
  constructor(
    private readonly boardService: BoardService
  ) { }

  @Get()
  async get(@Query() dto: BoardGetDto) {
    return await this.boardService.loadBelowId(dto);
  }

  @Get(':boardId')
  async getBoardId(@Param('boardId') boardId: string) {
    return await this.boardService.loadDetail(boardId);
  }

  @Post()
  async post(@Body() dto: BoardPostDto) {
    this.boardService.post(dto);
  }

  @Delete(':boardId')
  async deleteBoardId(
    @Param('boardId') boardId: string,
    @Body() dto: BoardDeleteDto
  ) {
    this.boardService.delete(boardId, dto);
  }
}
