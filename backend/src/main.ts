import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import { INestApplication, ValidationPipe } from '@nestjs/common';
import * as fs from 'fs';
<<<<<<< HEAD
=======
import * as path from 'path';
import { BusinessExceptionFilter } from './exception/exception-filter';
>>>>>>> 7352ca8f72583167efc8ca3fe09206d5a54789e3

async function bootstrap() {
  const app = await NestFactory.create(AppModule, {
    httpsOptions: {
      key: fs.readFileSync(process.cwd() + '/resources/cert.key'),
      cert: fs.readFileSync(process.cwd() + '/resources/cert.crt'),
    }
  });

  initializeSwagger(app);
  app.useGlobalPipes(new ValidationPipe({
    transform: true,
    transformOptions: {
      enableImplicitConversion: true
    }
  }));
  app.useGlobalFilters(new BusinessExceptionFilter());

  await app.listen(process.env.SERVER_PORT);
}
bootstrap();

function initializeSwagger(app: INestApplication) {
  const config = new DocumentBuilder()
    .setTitle('Cats example')
    .setDescription('The cats API description')
    .setVersion('1.0')
    .addTag('cats')
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);
}