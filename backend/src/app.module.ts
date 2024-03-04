import { Module, Provider } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { AuthModule } from './auth/auth.module';
import { UserModule } from './user/user.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MongooseModule } from '@nestjs/mongoose';
import { RedisClientOptions } from 'redis';
import { ConfigModule } from '@nestjs/config';
import * as path from 'path';
import { CacheModule } from '@nestjs/cache-manager';
import { redisStore } from 'cache-manager-redis-yet';

const configModule = ConfigModule.forRoot({
  isGlobal: true,
  envFilePath: {
    'dev': '.dev.env',
    'prod': '.prod.env'
  }[process.env.NODE_ENV] ?? '.dev.env'
});

const typeOrmModule = TypeOrmModule.forRoot({
  type: 'mysql',
  url: process.env.MYSQL_URL,
  entities: [path.join(__dirname, '/**/*.entity{.ts, .js}')],
  synchronize: process.env.NODE_ENV === 'dev'
});

const mongooseModule = MongooseModule.forRoot(process.env.MONGODB_URL);

const redisModule = CacheModule.registerAsync({
  useFactory: async () => ({
    store: () => redisStore({
      url: process.env.REDIS_HOST
    })
  })
});

@Module({
  imports: [configModule, typeOrmModule, mongooseModule, redisModule, AuthModule, UserModule],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
