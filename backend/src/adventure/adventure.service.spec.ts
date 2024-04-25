import { Test, TestingModule } from '@nestjs/testing';
import { AdventureService } from './adventure.service';

describe('AdventureService', () => {
  let service: AdventureService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [AdventureService],
    }).compile();

    service = module.get<AdventureService>(AdventureService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
