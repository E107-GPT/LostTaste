import { Controller, Get, Param, Post } from '@nestjs/common';
import { BoardService } from './board.service';

@Controller('board')
export class BoardController {
  constructor(
    private readonly boardService: BoardService
  ) { }

  @Get()
  async get() {

  }

  @Get(':boardId')
  async getBoardId(@Param('boardId') boardId: string) {

  }

  @Post()
  async post() {

  }
}
