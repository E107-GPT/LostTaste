# Unity

### Version

- **게임 엔진**: Unity `2022.3.20f1`
- **실시간 통신**: PUN `2.45 - FREE`

### 실행 방법

- 유니티 Editor에서 빌드
    - Login, Loading, Dungeon, Ending Scene 선택해서 빌드

# 인프라

- **웹 서버**: Nginx
- **가상화** : Docker
- **CI/CD** : Jenkins

# Back-end

### Version

- **런타임 환경:** Node.js `20.11.0`
- **언어**: TypeScript
- **패키지 매니저**: npm
- **프레임워크**: Nest.js
- **DB 모델링**
    - **ORM 라이브러리**: TypeORM
    - **Redis 클라이언트**: ioredis

### 실행 방법

```jsx
npm install
npm run start:prod
```

```jsx
//.prod.env
MYSQL_URL=jdbc:mysql://j107:j107j1234@j10e107.p.ssafy.io/losttaste?serverTimezone=UTC
REDIS_URL=redis://j123j1234@j10e107.p.ssafy.io:6379

HTTPS_KEY_PATH=/keys/j10e107.p.ssafy.io/privkey.pem
HTTPS_CERT_PATH=/keys/j10e107.p.ssafy.io/cert.pem

JWT_SECRET=i107gptlegendjwtsecret1233
JWT_EXPIRES_IN=24h

SERVER_PORT=3000
```

# DB

- **게임 데이터 DB**: MySQL
- **캐시 DB**: Redis

# Front-end

### Version

- **런타임 환경:** Node.js `20.11.0`
- **프레임워크**: React.js `18.2.0`

### 실행 방법

```csharp
npm install
npm run build
```

# Photon Server Setting

1. https://www.photonengine.com/
2. 로그인 후 Create New Application
3. Multiplayer Game을 선택하고 프로젝트 생성 AppId 복사
4. 유니티 -> 포톤 유니티 네트워킹 -> PUN Wizard -> Setup Project -> 복사한 AppId 입력 후 Setup Project

# 시연 시나리오

1. 로그인 및 회원가입


![image.png](./img/image.png)
2. 오프닝 씬 영상

![image.png](./img/image10.png)
3. 캠프 입장


![image-1.png](./img/image-1.png)
4. 파티 모집

![image-2.png](./img/image-2.png)
5. 던전 입장

![image-3.png](./img/image-3.png)
6. 직업 선택

![image-4.png](./img/image-4.png)
7. 던전

![image-5.png](./img/image-5.png)
8. 엔딩

![image-6.png](./img/image-6.png)
