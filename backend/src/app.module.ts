<<<<<<< HEAD
import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { AuthModule } from './auth/auth.module';
import { UserModule } from './user/user.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MongooseModule } from '@nestjs/mongoose';
import { ConfigModule } from '@nestjs/config';
import { CacheModule } from '@nestjs/cache-manager';
import { redisStore } from 'cache-manager-redis-yet';
=======
import { CacheModule } from '@nestjs/cache-manager';
import { ClassProvider, Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { APP_INTERCEPTOR } from '@nestjs/core';
import { TypeOrmModule } from '@nestjs/typeorm';
import { redisStore } from 'cache-manager-redis-yet';
import * as path from 'path';
import { SnakeNamingStrategy } from 'typeorm-naming-strategies';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { AuthModule } from './auth/auth.module';
import { CodeModule } from './code/code.module';
import { MapToObjectInterceptor } from './common/map-to-object.interceptor';
import { UserModule } from './user/user.module';
import { BoardModule } from './board/board.module';
import { PasswordModule } from './password/password.module';
>>>>>>> 7352ca8f72583167efc8ca3fe09206d5a54789e3

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
<<<<<<< HEAD
  autoLoadEntities: true,
  synchronize: process.env.NODE_ENV === 'dev'
=======
  entities: [ path.join(__dirname, '/db/entity/*.{js, ts}')],
  namingStrategy: new SnakeNamingStrategy(),
  synchronize: process.env.NODE_ENV === 'dev',
  logging: process.env.NODE_ENV === 'dev',
>>>>>>> 7352ca8f72583167efc8ca3fe09206d5a54789e3
});

const redisModule = CacheModule.registerAsync({
  useFactory: async () => ({
    store: () => redisStore({
      url: process.env.REDIS_HOST
    })
  })
});

const mapToObjectProvider: ClassProvider = {
  provide: APP_INTERCEPTOR,
  useClass: MapToObjectInterceptor
}

@Module({
<<<<<<< HEAD
  imports: [configModule, typeOrmModule, mongooseModule, redisModule, AuthModule, UserModule],
=======
  imports: [configModule, typeOrmModule, redisModule, AuthModule, UserModule, CodeModule, BoardModule, PasswordModule],
>>>>>>> 7352ca8f72583167efc8ca3fe09206d5a54789e3
  controllers: [AppController],
  providers: [AppService, mapToObjectProvider],
})
export class AppModule { }
