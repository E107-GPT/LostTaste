import { Module, Provider } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { AuthModule } from './auth/auth.module';
import { UserModule } from './user/user.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MongooseModule } from '@nestjs/mongoose';
import { createClient } from 'redis';
import { ConfigModule } from '@nestjs/config';
import * as path from 'path';

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
  entities: [path.join(__dirname, '/**/*.entity{.ts, .js}')],
=======
  autoLoadEntities: true,
>>>>>>> 017f98c6f8f0baa077e35a97a1f41f0eaf7df3bd
  synchronize: process.env.NODE_ENV === 'dev'
});

const mongooseModule = MongooseModule.forRoot(process.env.MONGODB_URL);

const redisProvider: Provider = {
  provide: 'REDIS_CLIENT',
  useFactory: async () => {
    const client = createClient({
<<<<<<< HEAD
        url: process.env.REDIS_URL
=======
      url: process.env.REDIS_URL
>>>>>>> 017f98c6f8f0baa077e35a97a1f41f0eaf7df3bd
    });
    await client.connect();
    return client;
  }
}

@Module({
  imports: [configModule, typeOrmModule, mongooseModule, AuthModule, UserModule],
  controllers: [AppController],
  providers: [redisProvider, AppService],
})
<<<<<<< HEAD
export class AppModule {}
=======
export class AppModule { }
>>>>>>> 017f98c6f8f0baa077e35a97a1f41f0eaf7df3bd
