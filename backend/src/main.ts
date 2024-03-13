import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import { INestApplication, ValidationPipe } from '@nestjs/common';
import * as fs from 'fs';
import * as path from 'path';

async function bootstrap() {
  const app = await NestFactory.create(AppModule, {
    httpsOptions: {
      key: fs.readFileSync(path.join(process.cwd(), process.env.HTTPS_KEY_PATH)),
      cert: fs.readFileSync(path.join(process.cwd(), process.env.HTTPS_CERT_PATH)),
    }
  });

  initializeSwagger(app);
  app.useGlobalPipes(new ValidationPipe());

  await app.listen(443);
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