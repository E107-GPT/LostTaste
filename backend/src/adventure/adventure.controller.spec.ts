import { Test, TestingModule } from '@nestjs/testing';
import { AdventureController } from './adventure.controller';

describe('AdventureController', () => {
  let controller: AdventureController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [AdventureController],
    }).compile();

    controller = module.get<AdventureController>(AdventureController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
