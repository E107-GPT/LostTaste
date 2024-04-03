-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: j10e107.p.ssafy.io    Database: losttaste
-- ------------------------------------------------------
-- Server version	8.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `adventure`
--

DROP TABLE IF EXISTS `adventure`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adventure` (
  `adventure_id` bigint NOT NULL AUTO_INCREMENT COMMENT '모험 ID',
  `play_time` time NOT NULL COMMENT '플레이 시간',
  `rng_seed` int NOT NULL COMMENT 'RNG 시드값',
  `stage_count` tinyint NOT NULL COMMENT '클리어 스테이지 수',
  `path` json NOT NULL COMMENT '던전 탐색 경로',
  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '생성 시간',
  PRIMARY KEY (`adventure_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='모험';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adventure`
--

LOCK TABLES `adventure` WRITE;
/*!40000 ALTER TABLE `adventure` DISABLE KEYS */;
/*!40000 ALTER TABLE `adventure` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adventure_item_log`
--

DROP TABLE IF EXISTS `adventure_item_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adventure_item_log` (
  `member_id` bigint NOT NULL COMMENT '사용자 ID',
  `adventure_id` bigint NOT NULL COMMENT '모험 ID',
  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '로그 생성 시간',
  `item_code_id` char(1) NOT NULL COMMENT '아이템 코드',
  PRIMARY KEY (`member_id`,`adventure_id`,`item_code_id`),
  KEY `FK_64c1f74e6051c23df69c503a165` (`adventure_id`,`member_id`),
  KEY `FK_c184fbafa4b4ea47b19ff27a823` (`item_code_id`),
  CONSTRAINT `FK_64c1f74e6051c23df69c503a165` FOREIGN KEY (`adventure_id`, `member_id`) REFERENCES `party_member` (`adventure_id`, `member_id`),
  CONSTRAINT `FK_c184fbafa4b4ea47b19ff27a823` FOREIGN KEY (`item_code_id`) REFERENCES `common_code` (`common_code_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='모험 아이템 기록';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adventure_item_log`
--

LOCK TABLES `adventure_item_log` WRITE;
/*!40000 ALTER TABLE `adventure_item_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `adventure_item_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `board`
--

DROP TABLE IF EXISTS `board`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `board` (
  `nickname` varchar(16) NOT NULL COMMENT '작성자 닉네임',
  `password` char(64) DEFAULT NULL COMMENT '게시글 비밀번호',
  `title` varchar(64) NOT NULL COMMENT '게시글 제목',
  `content` text NOT NULL COMMENT '게시글 내용',
  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '게시글 작성 시간',
  `reply` text COMMENT '게시글 답글 내용',
  `replied_at` datetime DEFAULT NULL COMMENT '게시글 답글 단 시간',
  `category_id` char(8) NOT NULL COMMENT '공통 코드 ID',
  `board_id` bigint NOT NULL AUTO_INCREMENT COMMENT '게시판 ID',
  PRIMARY KEY (`board_id`),
  KEY `FK_42f1dad0993489f574bfad5d3ef` (`category_id`),
  CONSTRAINT `FK_42f1dad0993489f574bfad5d3ef` FOREIGN KEY (`category_id`) REFERENCES `common_code` (`common_code_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='게시판';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `board`
--

LOCK TABLES `board` WRITE;
/*!40000 ALTER TABLE `board` DISABLE KEYS */;
INSERT INTO `board` VALUES ('hihi','$2b$10$wh2YJgelherMH/AzFMybOert5zsYHn9CLMdhg82grfrDGVK6vipPq','쒸프트끼가안빠져요','쒸쁘뜨','2024-03-18 16:07:58.217600',NULL,NULL,'BCT_0001',1),('test','$2b$10$B0ZNv6nc.7M27uNwLbd17OZtdmhdczzwYBynfjfKe6U42lkuk3Yfe','쒸프트끼가안빠져요123','ttttt','2024-03-30 21:40:56.063296',NULL,NULL,'BCT_0001',2);
/*!40000 ALTER TABLE `board` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `common_code`
--

DROP TABLE IF EXISTS `common_code`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `common_code` (
  `description` varchar(16) NOT NULL COMMENT '공통 코드 설명',
  `common_code_id` char(8) NOT NULL COMMENT '공통 코드 ID',
  `type_id` char(3) DEFAULT NULL COMMENT '공통 코드 타입 ID',
  PRIMARY KEY (`common_code_id`),
  KEY `FK_298fdd2df3364ff90b5842c72a3` (`type_id`),
  CONSTRAINT `FK_298fdd2df3364ff90b5842c72a3` FOREIGN KEY (`type_id`) REFERENCES `common_code_type` (`common_code_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='공통 코드';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `common_code`
--

LOCK TABLES `common_code` WRITE;
/*!40000 ALTER TABLE `common_code` DISABLE KEYS */;
INSERT INTO `common_code` VALUES ('건의 게시판','BCT_0001','BCT'),('버그 리포트','BCT_0002','BCT'),('기본 캠프','CSK_0001','CSK'),('평범한 검','ITM_0001','ITM'),('모험가','JOB_0001','JOB'),('펫 없음','PET_0001','PET'),('기본 스킨','SKN_0001','SKN');
/*!40000 ALTER TABLE `common_code` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `common_code_type`
--

DROP TABLE IF EXISTS `common_code_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `common_code_type` (
  `common_code_type_id` char(3) NOT NULL COMMENT '공통 코드 타입 ID',
  `description` varchar(16) DEFAULT NULL COMMENT '공통 코드 타입 설명',
  PRIMARY KEY (`common_code_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='공통 코드 타입';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `common_code_type`
--

LOCK TABLES `common_code_type` WRITE;
/*!40000 ALTER TABLE `common_code_type` DISABLE KEYS */;
INSERT INTO `common_code_type` VALUES ('BCT','게시판 카테고리'),('CSK','캠프 스킨'),('ITM','아이템'),('JOB','직업'),('MSF','사용자 통계 분야'),('PET','펫'),('SKN','스킨');
/*!40000 ALTER TABLE `common_code_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jelly_log`
--

DROP TABLE IF EXISTS `jelly_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jelly_log` (
  `jelly_log_id` bigint NOT NULL COMMENT '젤리 변동 기록 ID',
  `delta` int NOT NULL COMMENT '젤리 변동량',
  `reason` varchar(16) NOT NULL COMMENT '젤리 변동 사유',
  `member_id` bigint NOT NULL COMMENT '사용자 아이디',
  PRIMARY KEY (`jelly_log_id`),
  KEY `FK_84665f63ec644554b06e89d42e4` (`member_id`),
  CONSTRAINT `FK_84665f63ec644554b06e89d42e4` FOREIGN KEY (`member_id`) REFERENCES `member` (`member_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='젤리 변동 기록';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jelly_log`
--

LOCK TABLES `jelly_log` WRITE;
/*!40000 ALTER TABLE `jelly_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `jelly_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `member`
--

DROP TABLE IF EXISTS `member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `member` (
  `member_id` bigint NOT NULL AUTO_INCREMENT COMMENT '사용자 아이디',
  `nickname` varchar(16) NOT NULL COMMENT '사용자 닉네임',
  `account_id` varchar(16) NOT NULL COMMENT '로그인 시 사용할 ID',
  `password` char(64) DEFAULT NULL COMMENT '사용자 비밀번호 해시',
  `jelly` int NOT NULL DEFAULT '0' COMMENT '사용자 보유 젤리',
  `is_deleted` tinyint NOT NULL DEFAULT '0' COMMENT '사용자 회원 탈퇴 여부',
  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '사용자 회원가입 시간',
  `deleted_at` datetime(6) DEFAULT NULL COMMENT '사용자 회원탈퇴 시간',
  PRIMARY KEY (`member_id`)
) ENGINE=InnoDB AUTO_INCREMENT=228 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='사용자';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `member`
--

LOCK TABLES `member` WRITE;
/*!40000 ALTER TABLE `member` DISABLE KEYS */;
INSERT INTO `member` VALUES (4,'테스트3','test3','$2b$10$acUDO7/Mx4fPZx3nYo6o3.CsrdGrLsiB2ZI0Mxh7Lo8/CFQIky/4W',0,0,'2024-03-14 10:16:03.628406',NULL),(5,'테스트4','test4','$2b$10$bJ2yjCAITf7XFsaobPo/KOld2z/gUztIHiZqx1gIaAqHwx6mQq/oW',0,0,'2024-03-14 12:17:42.507458',NULL),(6,'테스트5','test5','$2b$10$HdeMYHXXl4YuKUgYbwUgAeHHmvK/sKJDAQOqDy5zeYgmqEiMt3lhm',0,0,'2024-03-14 12:22:55.535766',NULL),(7,'테스트0','test','$2b$10$M4u2.0V4inxPSOWVsi32Pu1Vy5RHh85QDWrfX57OF1f1V1aCsthAG',0,0,'2024-03-14 17:12:13.342911',NULL),(8,'helloworld','helloworld','$2b$10$CDc9qHc8ip3xQh1kyMuxweN5T/zvrjinGS22vnvi2EjTcjXpSIvXK',0,0,'2024-03-18 09:16:36.947423',NULL),(9,'newworld','newworld','$2b$10$SKdXHd6UqjctOZdH.Uxa3O19enody7f1tFR/xWngOdwbkyPjbu1Vm',0,0,'2024-03-18 09:23:24.650238',NULL),(10,'헬로','hellossafy','$2b$10$bilRTmgcdmgeipcyBehB3OGVzan0bEnhRunHhsHRKWHrQiJUeErwu',0,0,'2024-03-18 12:20:10.365292',NULL),(11,'헬로','hellossafy','$2b$10$lwaD2ED4jOQD.zAc4usN7ux7uFa7Vq56MG78iuK0GnfuCwh8UFc4O',0,0,'2024-03-18 12:20:10.371125',NULL),(12,'HHC','hocheoltest','$2b$10$IfT9w8wC1vtBi6.R7.RvVuTyMcndt9Qi0IY8P.vnMb9uLVuDmyrce',0,0,'2024-03-18 17:33:31.880608',NULL),(13,'q','qqqq','$2b$10$Eh3GOpRxr6lDoqZKNY5jbOiB30AVyy07rh14.z40J4UM08vSuJ0w6',0,0,'2024-03-19 12:10:57.631863',NULL),(14,'q','qqqq','$2b$10$IoS61ROfo1iefY3Cja5dued9jHHqZbNoJO8Osy9WXxj95fXDxqiTm',0,0,'2024-03-19 12:10:57.635360',NULL),(15,'insik','coach1','$2b$10$UYzXvgdK8bO0IAgnaFuqSOsRDl8GwSTfS72dXq5lc0w83NP4e6/im',0,0,'2024-03-20 11:45:47.957035',NULL),(16,'ye','yecoach','$2b$10$I1YsN5mIQayTAQzh5YWdxuuDkZ5CRADT7XY8PXA5XzUhSZRJZylTC',0,0,'2024-03-20 11:46:48.297562',NULL),(17,'su','suyoung998','$2b$10$wgdBHAQvZ5ej4PK9wFgM/eH27/P3oVakZjkfUU33m7J9FFIm2S2hq',0,0,'2024-03-20 12:03:13.762395',NULL),(18,'GM구본웅','thesmartist','$2b$10$JM81tP9L8JskJn7Md0wJX.u7tYEPP2UBq59bx5YOa9PKEYmY/uweS',0,0,'2024-03-20 12:03:16.231023',NULL),(19,'김치맨','kdw123','$2b$10$ViWI9AiZNkDelmh2z4JlL.F7WzF4UccosZuqz25wSmDN4wKGtq32S',0,0,'2024-03-20 12:08:32.629508',NULL),(20,'죠죠','moonwlgh','$2b$10$zCGNy2YGau4KE8DhVPGWhenYLLxHS9Y88BvusqQkj9l0FCghgoMei',0,0,'2024-03-20 12:13:03.916570',NULL),(21,'sjh','sjh96','$2b$10$YOFkQ2NDWL4LDEcA8Zj2W./3mUkC2ijKHz47LHHRrVKbecgr4K4kC',0,0,'2024-03-20 12:15:18.989120',NULL),(22,'지비집','edward','$2b$10$O4tMR9rDeWXKiMYlIyDMZ.Nu3FnN7ENJHu5n24mynsJSNxsXX92gG',0,0,'2024-03-20 12:17:38.591317',NULL),(23,'ssafykim','ssafykim','$2b$10$yibA8AeXKnJB84vPp7HtH.1sPZ4DycOiTDGBsr1SDICov8xPT1hT2',0,0,'2024-03-20 12:20:11.437042',NULL),(24,'못키','rudgns9334','$2b$10$rjDAmLww1Zv1fYFh.opmQOZhwHEDZqJEV/WpryNbrcEXru3y4aBx6',0,0,'2024-03-20 12:20:15.043495',NULL),(25,'qweqwe','test123','$2b$10$6ls2R3IGF0ZVq6DDBCfiveCmD3ynMroYcElaWwIF9BFLU1Gvm1hne',0,0,'2024-03-20 12:20:36.220083',NULL),(26,'강컨','mong','$2b$10$WZwBlaB77MgY4xz7ENoadeA8UFRrL3u1CQwzduM/wqAmvbQcTI7U6',0,0,'2024-03-20 12:22:42.871257',NULL),(27,'단맛','dalsu','$2b$10$inZ1tCQ/niAR3COo3uYm.OX7BIE2Yup1TLhnPam6B05JPsWW3q87q',0,0,'2024-03-20 12:28:43.643276',NULL),(28,'이재명','LeeJaeMyoung','$2b$10$eQLXyt65sGVBXVvQbb2WtuyVhr5lxTihAcMHPNDNAUl47Rbow4X9m',0,0,'2024-03-20 12:29:44.251288',NULL),(29,'우주최강부울경대표박박박수빈','subin123','$2b$10$48ay6Lua0v0gMbmK7O508uoKUHxN1/73HQgq6NmH9M28cju51d0m.',0,0,'2024-03-20 12:30:38.830816',NULL),(30,'Sonny','kain9101','$2b$10$ASMRicZn3/3U1XyvXWwkTeY1gaFo3xdWC.TJ9EYSEWacRUaurZsou',0,0,'2024-03-20 12:30:57.581306',NULL),(31,'윤석열','YunSeokYeol','$2b$10$elAhNUnkGDbHn1Osf0uy6uG59HwJb9t3iXOU7higkHhU1hnYH2nPi',0,0,'2024-03-20 12:31:39.034325',NULL),(32,'테스트1','losttastetest1','$2b$10$HeVBG2Y6akY8CkLAzeEMfeXr9k19k6Qw4AqDIDeParzNWQprTXVba',0,0,'2024-03-20 12:32:20.931893',NULL),(33,'우주최강깜찍장지민','jangjimin','$2b$10$S2eJ34PARbIx1klOocfmwO9glv0AR0REU7asgfZhBMHZM3e9EQuRm',0,0,'2024-03-20 12:33:23.358787',NULL),(34,'호철이','hhc123','$2b$10$uEYtW530/R85VVUX5ZfHRuz/JeVu1XsieAgKtCF1ysfLzZym8oDia',0,0,'2024-03-20 12:34:28.475990',NULL),(35,'test2','test2','$2b$10$43tzxdwgungLmXrxlHen.Oqpvw0z2R7uXbR6y2vwgAHnU3ERn4tS.',0,0,'2024-03-20 12:36:36.834144',NULL),(36,'따봉태호','ice980','$2b$10$vulEcSkPX1OdMHAw0M0cw.GQUFQr2T7OVD0PFA.AqKpTCjMnhhVhO',0,0,'2024-03-20 12:36:47.359314',NULL),(37,'dahee','dahee0525','$2b$10$X3CHL30jbjb2Ye6wwcktE.diKK4spHEOwmmjiiTUTE8E1wzbOX.RO',0,0,'2024-03-20 12:43:10.725544',NULL),(38,'지켜보고있다','sonyou','$2b$10$4v5VS8icAd/O0M4AksXm1uV2ZGTFABCwJGhbjcUdiv9cl0G0DWp5q',0,0,'2024-03-20 13:21:41.408641',NULL),(39,'나나','xw2nana','$2b$10$LzkLOr0LOfM.FgqQFBC0nuwj84qUDLbPuDao6xxHTgk4wK8T6Ery.',0,0,'2024-03-20 13:37:47.791312',NULL),(40,'losttaste','losttaste','$2b$10$U7dM75zcCOZpG4RiCnk1jOdu3lSaG8AoImv5kM5YR/YfQ/1q8.ZGC',0,0,'2024-03-20 13:38:10.646321',NULL),(41,'qwerty','qwerty','$2b$10$Kjna0mrVm1BrxGGJC4ctNez9AN.6fKG69LOQfCEbrtJ4okSffSbyO',0,0,'2024-03-20 13:38:38.158444',NULL),(42,'LikeBear','wjdtpwls32','$2b$10$T6dm3U6vH3aFrrMTk5fuQeelgL8KpcXmtjJzD/lwsTZEiUUwZOwuW',0,0,'2024-03-20 13:46:11.646075',NULL),(43,'hoont','hoontp','$2b$10$5588HmhgLrlt6VtaRyOaA.FIDhsCllYeVrp7cki7pGrE7wgO1t8ry',0,0,'2024-03-20 13:49:48.635836',NULL),(44,'sjh','ssafy1031','$2b$10$fNlaUFgEDajBtshEBDUFfeyCb2UWaBkb2X61I/aPsmeUqxUUeX.5K',0,0,'2024-03-20 13:55:02.763630',NULL),(45,'jongkookE','johncookie2222','$2b$10$EJhjmnMoQ5KcIrA4blOybOpGOcZKHh39uqrsGUsDng9JFkB4H.M6u',0,0,'2024-03-20 14:04:08.546457',NULL),(46,'이정원','sorrow4468','$2b$10$briaYeUsMQMakh/Yn5Ecp.Bm7vqtVq5R4P69MlaT7JCPSHGzunwJi',0,0,'2024-03-20 15:16:20.013236',NULL),(47,'123qwe','wewe','$2b$10$rWW9VMq44DpWMZw9.0EJKeq5MV1yiGSk7N346tT6udT9AsvZkkr1.',0,0,'2024-03-20 15:34:01.239864',NULL),(48,'바다','didue35','$2b$10$Z1YXrPa1AAXsue1cMi8Sm.7yH433P7HpvRrFKS/GtxNVvID/OoI/q',0,0,'2024-03-20 17:01:36.799477',NULL),(49,'소스','ssj0187','$2b$10$0YJ5Yl8SKj4hizh.PTPbA.VAv7kPg.NiuDHVEbQuOkbeinSiCFOI.',0,0,'2024-03-20 17:02:30.187302',NULL),(50,'하이하하이하','botboy0441','$2b$10$r7WZwneurrODJ1PwhwB.F.U0oeSdqr0JeQhCIcPvZWgAOmoZQwop6',0,0,'2024-03-20 17:11:23.902723',NULL),(51,'sadfadf','sdfa','$2b$10$r2QefTH4UVjsDudZO99FDeQ3bBuSocinwoxYPORzE68dHwf75nr9q',0,0,'2024-03-20 20:45:35.668641',NULL),(52,'asdf','asdf','$2b$10$PvC2MyNTVwV6bc1SkIeFZeW0IZ4Hlrp5pFxw9MHzqbemjCPp2dOTS',0,0,'2024-03-20 20:45:45.108661',NULL),(53,'asd1','asd1','$2b$10$qLyiGyZ4Fjo71bu1zy/Iy.7Bq4mlu6Z3jHi2w5ObIugqbRFM/lqHm',0,0,'2024-03-20 23:00:31.407666',NULL),(54,'지비지비집','test0101','$2b$10$Me6puytNizTIp9t2k73fgeQqzuI/ZFhPrNUmIf6yYM9LheEb.4n.S',0,0,'2024-03-21 09:08:34.340099',NULL),(55,'엉루','lure','$2b$10$rfc5En8rBqUXDcbI.Av1o.0a5avkcqrJLCGSfMLP2hlHfihJSNkHS',0,0,'2024-03-21 16:38:56.644951',NULL),(56,'k','kkkk','$2b$10$UWAa2ev8QE6parh/DDe7luyIMxHFgufAUBn7MkIdQ/xOVyRN7Jtz2',0,0,'2024-03-21 19:41:49.323534',NULL),(57,'YD','kyd1126','$2b$10$LBRoYzS7G1T1nXkZ00RN5u1tKnl8.Dra2j1/kqHLjwX/Hgjmj4hH.',0,0,'2024-03-22 13:47:10.718429',NULL),(58,'양모모','yoonmo','$2b$10$zuuY.iKb3wyjGRbUeQ5ouOXYcd.j36GhfXLgZzU2JvXrnOgUukV5i',0,0,'2024-03-22 14:14:12.441576',NULL),(59,'123123','wewe33','$2b$10$3nOIcY73HV8py0/P9o6REOco6CCQBj9TcydDaqueH4AUkkEuHGslm',0,0,'2024-03-24 16:44:17.928462',NULL),(60,'wqeqwe','asdfasdf','$2b$10$b4zngf4b6mQpM/3LMx4aQ.elpOzH434RY9O3GSyYN1DdfYlBinW8G',0,0,'2024-03-24 16:44:46.235206',NULL),(61,'sjh','ssafy96','$2b$10$k7ZMxRuKFj9AvcvezPNDtes.ZmHUUqP.yApqAwf5YBN9cC3ospRKq',0,0,'2024-03-26 14:43:11.699916',NULL),(62,'Lement','lement3864','$2b$10$yqXvcZGzE/LAGFX40EAKGeNUvUaXf.997zJ8.Yckq2Fzn7IIe7wyy',0,0,'2024-03-28 11:35:50.244563',NULL),(63,'kdh97','kdh97','$2b$10$ledzWJ4R1Sz81Ooko7QYLeCWLpY/4v3wGJ/J0WE22DM38S5i8sfka',0,0,'2024-03-29 11:43:47.550722',NULL),(64,'지비지비집','edward626kr','$2b$10$2fQB.DifCH1ptVuDholRhOlw40c6bpPwuwAiDwDztqmdU162FL2ny',0,0,'2024-03-29 12:47:49.581793',NULL),(65,'qkrwngjs','741u741','$2b$10$uQg6b5ttV.O6aiO.KT3qgeyzPY3L9e0hu.AJM940KhsJVCtOMXf1u',0,0,'2024-03-29 12:50:01.249545',NULL),(66,'울산상큼이','entp1618','$2b$10$G9Bg37byMU8.aubqzff9We4s1tRidzIB8VZiqk1Ya67e4KxVVxJ8W',0,0,'2024-03-29 13:19:15.157569',NULL),(67,'김치파워맨','hide321','$2b$10$RabbZlASnKb3iBGLaRIoruTRuXEGpJ.cj6Dq3C8MHyqISc8XPgmWK',0,0,'2024-03-29 13:52:46.632996',NULL),(68,'abcd','abcd1234','$2b$10$/.I1V098hfhwJ5eVE3e8m.KCUOgHxQCvJb3iy7MwPuq2VHCsqjHiO',0,0,'2024-03-29 13:57:17.115816',NULL),(69,'wewewewe','asdfasdf1','$2b$10$/2qC01FJoCWtV0hUSeaIg.bAzlkhFTm84DXWGNyPQ3dqMmdkt8Rcq',0,0,'2024-03-29 14:02:22.593099',NULL),(70,'싸울아비','soulabby','$2b$10$jrxjp1PZuZ7mHONuTM3xSOLKox9vT1rCsbHLywM1EFdVz5Ff5Pi22',0,0,'2024-03-29 14:06:11.821521',NULL),(71,'영은코치','yecoach1','$2b$10$ktXzJA1AK6xHLO42ISf.xedfsxUllE/hP/FkcD87KDmBWCun7OEPO',0,0,'2024-03-29 14:06:37.864764',NULL),(72,'경코','rudgnscoach','$2b$10$kEEwEAXCo8TslyPtDSVj2.MJ3vXoE4gJ57jkn/oXlL.Fl2IOEzEKa',0,0,'2024-03-29 14:08:36.527404',NULL),(73,'소코','sojeongcoach','$2b$10$.scdlVhSZVO0nuFQvO9mWeX4jEjwMxwj7vSZpIDwUWp/rLgHKCLk6',0,0,'2024-03-29 14:18:21.554593',NULL),(74,'dahee123','dahee123','$2b$10$yA3ORsQHityg0pgQDd0DWOAhg0PN4VkJyenatTWvXt42uk8wl/dr6',0,0,'2024-03-29 14:19:47.118508',NULL),(75,'peng','peng','$2b$10$jT6m9cGaw1tfOltLTwAgCec0OeOskcHyjtV2F0TbDm1xqlbsLpxgu',0,0,'2024-03-29 14:21:08.472205',NULL),(76,'momo','momo','$2b$10$MSm.PfvE7AqUhj1fVQqmK.13kCar.4zXHbeSsvJ1BzN7GcQp5VfGW',0,0,'2024-03-29 14:22:05.771421',NULL),(77,'조현수','ssafy406','$2b$10$MVBskJlTeF16WsPc7IgN6OwxTshC4Ptx2g7J75KSPJmMQZLUAKZW.',0,0,'2024-03-29 15:09:14.758133',NULL),(78,'seoulking','seoulking','$2b$10$BgQjBLD4AS0adcyLXYBN2egYER78.LMB7N8gZ9rjTLjQb6RL/cABq',0,0,'2024-03-29 15:09:30.673361',NULL),(79,'kingssafy','kingssafy','$2b$10$qV5wUw/Lw6RMOKFRLL5.pOPinPJtpqkoIl9Q8wPMOShaSvpUNObLG',0,0,'2024-03-29 15:10:03.247139',NULL),(80,'seobeomsuk','seoul','$2b$10$tYd3poj98sI1y5EXEJiXweSrvLH.rYUf/EPrR.X7RWrprrI.lZtCa',0,0,'2024-03-29 15:10:11.218723',NULL),(81,'ssafy','ssafy','$2b$10$HnFsGyW6kMjZomEGuE5B..0vdn96JYJiooSWNwjWyOX7yQ9Atn0h2',0,0,'2024-03-29 15:10:11.432921',NULL),(82,'백골부대 병장 서울대표 송윤재','hscho','$2b$10$1BmGbmjcIeOk1KbW6WnEaecHdw87ggdZmY7z3RbGhxGTE/Om6.1u6',0,0,'2024-03-29 15:10:49.083745',NULL),(83,'ihateregister','ihateregister','$2b$10$9fIjekqXImG6mpwNObtAnetCZT15W6hHBUYUtQru//9MiG.JY1Cpi',0,0,'2024-03-29 15:11:26.646991',NULL),(84,'rlaclwhdk','card0923','$2b$10$qEf64kv5sUkrXMKkacGSeupNHZOzQwu1pQNN7D2hyqWK8J9YC0Vru',0,0,'2024-03-29 15:12:19.454615',NULL),(85,'test1234','test1234','$2b$10$mJ4pV9tFFQdRfVD47GBIweQvs8YZGlu9wYb2UKbPkLM/oj75gcAsG',0,0,'2024-03-29 15:12:19.699502',NULL),(86,'주홍111','kangchanwoo','$2b$10$v5m1CKZhKnTsZqHsqrzNaOOKLHWVcy/x.QCTiU5Da9ZM11m8fcMcy',0,0,'2024-03-29 15:12:39.403336',NULL),(87,'asd','qwerasdfzxcv','$2b$10$DNVqEnT1qTaU4gF.DU43FeLhtxFBuDRXGxTtjGdyhrF7eTR3KRaje',0,0,'2024-03-29 15:13:05.306812',NULL),(88,'asdf','qwer123','$2b$10$cooeXmcZ18bIAYG0qiWTBu8IDICkTlDBbZCtrTU5YUC9rV/aV4jvq',0,0,'2024-03-29 15:13:14.421912',NULL),(89,'도리','dori723','$2b$10$mghJlvL1FYNP0xYLkEnQmOarDbNrrAe9csqQDJs6AD6ly.d034XCO',0,0,'2024-03-29 15:13:23.297284',NULL),(90,'ddd','oo5439','$2b$10$6hPoW.cSFCzi3CVhLh/VROpMrD2ioX8oe5zsn1heXLLirKlTaGQVm',0,0,'2024-03-29 15:13:27.717180',NULL),(91,'개빡친햄스터','dndbekfrl1','$2b$10$AuKuXCyh9X7rK.vupvokMuVE.0yfgNmo.boPqRCTQUOWn0J/nzlui',0,0,'2024-03-29 15:13:41.279311',NULL),(92,'qwer1234','qwe1234','$2b$10$jZcvew1uzNEwUa3hpcmZ0u0tiKkcurfncst8ta.AZ4bniygPgWoz.',0,0,'2024-03-29 15:13:48.268552',NULL),(93,'qqqq','aaaa','$2b$10$u2JzYAPj6hOc8G1piHM.muFO.smTVLh7hd/B.64wqXlJfPqxkBagq',0,0,'2024-03-29 15:13:51.027649',NULL),(94,'telsam','telsam','$2b$10$lVMIlWGO6MaV6yt3KrYsCOTcHc.4JqKOx.2AZC1b24cVI/psMk6fu',0,0,'2024-03-29 15:14:37.466010',NULL),(95,'신문영','ztrl','$2b$10$dlo0kAZf55wceTMZ0C2jo..9Ps0xzqlhBoLC5u0dT.40347VXGPJy',0,0,'2024-03-29 15:16:43.450873',NULL),(96,'ssaktory','ssaktory','$2b$10$no9VvCb6gxHooedtcoPIP.eunG3dj44mVDhRLlsT758HvneEHokCe',0,0,'2024-03-29 15:26:08.716182',NULL),(97,'찜콩코코','hyejin','$2b$10$TqPywKo4y3d09tHmIihlx.z5x9PH1rbnSysSRY5ZpITNyPaZPq..i',0,0,'2024-03-29 15:26:49.169727',NULL),(98,'121212','asd123','$2b$10$Kc9LkOIBPgGNSIdFo.eAOuhs6YWQCKRln10l.NgV2vM3VFxmhblQC',0,0,'2024-03-29 15:28:22.070030',NULL),(99,'sibel','DMZanimal','$2b$10$3lSEbBJAvS1EPZQs1mEIqel353a/i6OR7qkNXbeKjIs4rVDcktlbm',0,0,'2024-03-29 15:33:19.768664',NULL),(100,'oilater','oilater','$2b$10$7gBHkXAx4LYTpHvyM4xJ..82VlmuxaRuvgh1RYCtr0es9iJMGvxYG',0,0,'2024-03-29 15:33:24.082580',NULL),(101,'manomotion','hello','$2b$10$yMRgOVb3UjtbBDAkND2AIetD.LJ.BDePvZuLG66MMsq2yqDnmIcyW',0,0,'2024-03-29 15:38:04.987896',NULL),(102,'AZARATH','DOHOON122','$2b$10$yVFzz4kPlodMY61gUx3ATeBkasNrK8KXqoa.PLBpKZzZ/BVydFJva',0,0,'2024-03-29 15:40:11.106686',NULL),(103,'wngjsqkr','741o741','$2b$10$b2ZrP2i4RHbVL6p4sPihAOdoc5bMiCwnL28iNPebZD/jZjLbZndEK',0,0,'2024-03-29 15:47:41.561524',NULL),(104,'qkrwngjs','741p741','$2b$10$ZSEd9b5G5PU65e7PU61v3usDQ35WNCponL7XYPShBmiakgcE7CaPO',0,0,'2024-03-29 15:48:50.664692',NULL),(105,'huni','jihun','$2b$10$1BNlddPqtND16xP4i88A6.PF.7dIkUOezZu18wVVFg8ZocD0/pGgi',0,0,'2024-03-29 15:53:08.444092',NULL),(106,'언리얼엔진5','tkdgur0811','$2b$10$AlNAMW1bcKQYmXmB2HZkEO14kVQqBeqxNJW4XwVzuxXo6cG.UTDfO',0,0,'2024-03-29 15:56:20.574874',NULL),(107,'fire','fire','$2b$10$oMHCUVH0Q3U5vpUz4u0xPuM25nxg9lTrNxceKvThyO6A355dxBN7O',0,0,'2024-03-29 16:01:11.915330',NULL),(108,'ssafy321','ssafy321','$2b$10$3K.tuWd0r8/x4Lg3IqCoUOjkkqVxslvUe9w/NiEVLlTqiioZTx.72',0,0,'2024-03-29 16:03:26.165495',NULL),(109,'너 뭔데','qwer','$2b$10$9OIInwgERBiObsf2xhowUuOh.tUV4E813YeoBbJhiBLO8MxQb/L4i',0,0,'2024-03-29 16:06:42.796906',NULL),(110,'당근마켓네고전사','ktb5057','$2b$10$CJjPoI1MB3Rl8R7jixaCSuGftm.fT.72J7XZPoC2u9GKD1AmMZWGi',0,0,'2024-03-29 16:17:26.894722',NULL),(111,'권기용','badacura','$2b$10$Xm.q9q9BnWSj3oY.6dRPleJ2KTthAnzMFSAdOw0W57pCAIafGZEzS',0,0,'2024-03-29 16:20:27.998612',NULL),(112,'해안짬타','kgh2120','$2b$10$fSc6prPkYegQcVrPR0l3qusQ6h.3hOJJ1XE9WlaZ9XIlfL/F4SMpO',0,0,'2024-03-29 16:21:16.816488',NULL),(113,'null','null','$2b$10$umigtb04AlouFEbNyJqfN.1SVN0QrmUCJFePuLSC0cY5Kten3U5oy',0,0,'2024-03-29 16:22:22.669486',NULL),(114,'admin','admin','$2b$10$ppTVqiYK.bNU0bgJiz3qy.EnJZ936KAd8OwASbSc5F1mi90BP0sRK',0,0,'2024-03-29 16:22:55.029548',NULL),(115,'Soul','gardener','$2b$10$I2/q4dNhvITXmP4vnVSLVeKSzHlUr4xaObjutdlRVl76A95E/qv82',0,0,'2024-03-29 16:22:59.075800',NULL),(116,'hye','hyeryeong','$2b$10$PQMbQzrQhIVJgXdpm0cB0OVn0eCVfqLbe5V8Wgdm776nyxLwMlqre',0,0,'2024-03-29 16:41:53.398772',NULL),(117,'gpgpgp','gpgpgp','$2b$10$t.aimOs/pIrHBgYNs8YKDeriZaDbLkzvLZOp6gOVLK4OAFFRqVgV2',0,0,'2024-03-29 16:45:14.956670',NULL),(118,'qweqwew','qqqq1234','$2b$10$SqeD2GECd3CqdL7BmHHVqOngkmJBr51QMz3fc458.5AoVxSy7HQuu',0,0,'2024-03-29 16:45:58.229665',NULL),(119,'뚱이','patrick123','$2b$10$WBHghvlimPgiATcC/ZdJoOcNtvAxaTKmKHU0Z4hPlq0uqKrGxzGJ6',0,0,'2024-03-29 16:46:26.036523',NULL),(120,'캉코','kangco','$2b$10$tWC3SSKWpwRasM8C40l.wuayvWPBShKqgYy3XJiexdaJA39PPX/x2',0,0,'2024-03-29 16:50:06.757387',NULL),(121,'ssuya','treamor','$2b$10$WjsgT/Zifh.NbNTdLuotS.ox3m7vXPOFzoHVM3nex0hzlXU4.5isq',0,0,'2024-03-29 16:53:55.364756',NULL),(122,'나','uiopu0','$2b$10$WyQENyS4LBgrwEtke6Dq0.K4l39TCnud0mikAlsM7d4PkfYH46AWW',0,0,'2024-03-29 16:54:40.711244',NULL),(123,'김덕배','boolbool','$2b$10$iP7TnMCJ5T1VAwgopm7jmelcM.tr3pM7e2aNGdpQLYBM9TTtFGqRO',0,0,'2024-03-29 16:59:46.982380',NULL),(124,'미남','ssafy102','$2b$10$F1YUwxIRBi.S1cixFd4ndOxt4WwmzHoJYuV94./jJdrN2KqR94Ety',0,0,'2024-03-29 17:00:18.527812',NULL),(125,'불굴의김해청년','teddybears56','$2b$10$e99uLwoAlxKtekwQ5VS5N.yGiMf5w//HygdgCNsKvefY5R1LHz2Zq',0,0,'2024-03-29 17:02:42.676463',NULL),(126,'cch2','ch2125','$2b$10$p9GgUq3HMbkvWx10yrmAou1/n5mFscOFV8v17bphY7Fz7ggEX6Nb2',0,0,'2024-03-29 17:34:30.324494',NULL),(127,'다라락','dararac123','$2b$10$EzyZhYEhjNrcbi0Ht6pae.PolNCGHVUrdPrgMCHjNZOxnAxLf1cq.',0,0,'2024-03-29 17:48:40.779122',NULL),(128,'재원킹','jww5555','$2b$10$06.La4sEA8ykZMRGgfal/OLRMSRthdBJVxvXedRKlfGZaB5ssH9Qe',0,0,'2024-03-29 20:36:45.882875',NULL),(129,'펭슈','rlackdgml97','$2b$10$s3.4vYmWbvq52/ODoQtPnewJ3gMTlMs1Bsah6PnCqy.zXonqHjcAu',0,0,'2024-03-29 20:38:51.366406',NULL),(130,'삽고수','ehdgoa123','$2b$10$1CnzO3ogQdaDDi/.Hh8LeuOACIg6CMqHAWlDZ7PZy.QBAG8FKh.IG',0,0,'2024-03-29 20:54:14.860097',NULL),(131,'아나까나깐나리','smrmaak','$2b$10$cflEzlfZcWfxh/IzJptI/uq5w0eKbtOyugVcG9skDn58.n87a4C52',0,0,'2024-03-29 21:39:29.590562',NULL),(132,'가자','sijak55','$2b$10$Jr1Y3/4SxPrEUaxUWTutdOwF/pQ19d2B6LnAyVjxl/KgI7cUjDcD.',0,0,'2024-03-29 21:59:37.883897',NULL),(133,'ㅂㅂㅂㅂ','qweqwe','$2b$10$V1kqcx2WcXLwNjQh2TVVSOXZdS3X0pEyHesY.v8xO5vuqHmd3V1ia',0,0,'2024-03-29 22:19:47.673303',NULL),(134,'chanyeong','chanyeong','$2b$10$UP8KxyE2cowI99SJt5Y4vuTsbtKZJbE8sdGXvnA9PqKQW.yNa7GVe',0,0,'2024-03-29 23:08:26.936713',NULL),(135,'띠드','cnhug3','$2b$10$Icr8ttBfwZVcG9QZcEb1ROz2IiODJjdanbsT/nAhGDoGm4AAi/1B2',0,0,'2024-03-30 11:06:52.212712',NULL),(136,'띠드123','cnhug33','$2b$10$3wNjGxhEln.sJfDlX1bBQ.KRQ.16ukxOrbUzxaLuXx9mwXoD.uuFS',0,0,'2024-03-30 11:08:11.320821',NULL),(137,'Lement1111','lement38641','$2b$10$13XOIzGrCc0uBllac/hbrOtZMVnrd8NWO/yXvoqndylW24NPgNmGy',0,0,'2024-03-31 01:12:33.622169',NULL),(138,'GM구본웅2','thesmartist2','$2b$10$JLexjqU/Rb1oDwv5lax6juYOIL3JWtJnSUGb0w2Ye3DVSn5VItcNa',0,0,'2024-03-31 02:30:43.174746',NULL),(139,'RoothLCP','jakl207','$2b$10$DnThmsgZBg/cQPBdDnDa0eubrpvdKEae9MbqEfSPnaYiXqmOAAAS.',0,0,'2024-03-31 18:18:10.273518',NULL),(140,'dongggle','dongggle','$2b$10$VvThLzD9d9pHRjE5CIoGlu3qDWuhOvuRLz9ZN.iz1dLec/njDdWIq',0,0,'2024-03-31 18:19:17.220162',NULL),(141,'qwe','ssafy123','$2b$10$3BQaNEXV/TYw6r3wPPzrV.5iD.PFA3x3uDwMlY//LHgUNFG7PuJTa',0,0,'2024-03-31 23:55:11.712463',NULL),(142,'오렌지좋아','orange123','$2b$10$dMm541gt/moeeVEg3uese.lXIA77JCQ1I0uTGe5SlLVO3cPXxKx.m',0,0,'2024-04-01 01:35:32.041641',NULL),(143,'test97','test97','$2b$10$YJRjDc6l.wgPNeEjKrA5S.8R59MIkJD6orenCphumHYTaO1ObWAnu',0,0,'2024-04-01 09:15:12.220005',NULL),(144,'changchang','changchang','$2b$10$.08gKDcvDXw/6EUWQPhvs.Psv.cFSjNjMfBRJbVm89HMmH5XIfBuK',0,0,'2024-04-01 12:08:30.226813',NULL),(145,'서울메타버스','myid1234','$2b$10$PaIJa7QAUmTb5Vai/sRpmupHR4T14ebZuF3j.9Ipm6hFXnMZ1qvL6',0,0,'2024-04-01 22:18:16.102657',NULL),(146,'서울촌놈','myid12345','$2b$10$m4EFBnlk2whcGxdBI5RecezdqR79ZmS15JhEMh7ILuKIx4pRMw/1W',0,0,'2024-04-01 22:19:08.261434',NULL),(147,'hoontp2','hoon','$2b$10$9wla7JbGPrJgSjlxZIR75erJBpjhzMh5QuwIZnHW05rPp/.YUcNE.',0,0,'2024-04-01 22:20:15.573318',NULL),(148,'aaaaaaaa','aaaaaaaa','$2b$10$pRn58sHbOO47t/HVpEjkz.0ChKKYt667UsILodbghN5uqVRx9UWLm',0,0,'2024-04-01 22:31:52.997755',NULL),(149,'현미이','hyeonmin','$2b$10$dCXGzfj.Q62fSKvqMduED.Z88ArN6izrPgHVlzXLF6fjUrCNvmRQK',0,0,'2024-04-01 22:32:43.167090',NULL),(150,'hhc2','hhc2','$2b$10$kxKqrMYBG4swTW8mL9rXw.CAbgmVoFa0DvLyMN.cXv6cTwQBvFDJW',0,0,'2024-04-01 23:22:04.845080',NULL),(151,'따호','ice98','$2b$10$L0ag7khqT3kb53DAclmP/uu..pvSE.VBGAgVSZF4up48/rkzb/Nia',0,0,'2024-04-01 23:26:30.459486',NULL),(152,'ssafy','qwe123','$2b$10$KvXWpxwOH4zcuKt9QLLNP.Qv/Y.uLicL5XmqWPCosIdsIGrgRxvmO',0,0,'2024-04-01 23:37:36.579517',NULL),(153,'준찬','cksghkd123','$2b$10$nV/TpT5jnOdJ0/hWh7/EuuRNUrgss7S768KJpkLB1ILuklAzNmODK',0,0,'2024-04-01 23:39:26.783613',NULL),(154,'역체원데프트','deftdeft','$2b$10$89dTnuk4WZoyqNDf2L4sB.2owkNC5arPFXooFwiVzOnFQXCjDdIB2',0,0,'2024-04-01 23:55:43.886146',NULL),(155,'asdfqwer12','asdfqwer12','$2b$10$b.7Mku0c/9WTzejwYCBE9eU5wXss/TY/AxWHOuBIHsBeLCre85UmO',0,0,'2024-04-01 23:56:00.092290',NULL),(156,'woonchoi','woonchoi','$2b$10$b5CLF43JJm185RToAVYlpuy0jx85RJVT4N.XeNkd4XHVxIJ7yRFlq',0,0,'2024-04-02 08:22:26.743479',NULL),(157,'1040126','ssafy1040126','$2b$10$un58tbuNctPz8HmfNQjifepUp6raCbtgVXgyG.FaIZASDY1a9p5KC',0,0,'2024-04-02 08:31:59.636821',NULL),(158,'윤석열','sengmin14','$2b$10$H7SBQRaJZ1LIWfJnnk7TeuHLDn365p5Os2hed/2lEWgFiI9UFcxVu',0,0,'2024-04-02 08:38:52.831249',NULL),(159,'이재명','sengmin114','$2b$10$LYEA1hKutfiiyWYmdKbwwuTWvcM.8Jg4f/.JM618ARnxcBBpc1.dO',0,0,'2024-04-02 08:43:39.713251',NULL),(160,'홍준표','sengmin144','$2b$10$7EYgQZuR2BQTxy2piQeiZOaTShOMaF0G9Xb8KvoG22r2H02jsfEaK',0,0,'2024-04-02 08:44:02.864337',NULL),(161,'이낙연','sengmin31','$2b$10$wnNRohBzGbnzKmTId6tX/u.mxqhFdq/rkpFRKCAERrNmNyglHqi6G',0,0,'2024-04-02 08:44:16.378458',NULL),(162,'안철수','sengmin311','$2b$10$bHCM0VTmU/9jokrf9KZD4.prQKuU1OaZ98BHyQxNTm6bOPgesknUG',0,0,'2024-04-02 08:44:40.566541',NULL),(163,'조국','sengmin123','$2b$10$oNkSbi4sgHYj8At8nqLKYuUIZmRni3Fc5LuH.e1fFzj2Vnt7mQxvy',0,0,'2024-04-02 08:45:14.482140',NULL),(164,'하태경','sengmin1234','$2b$10$c2tCDT/H0z7l7.INkq3DnesPFvB5OJZFMMHakJ39Jke6ZeC.yKczK',0,0,'2024-04-02 08:45:25.982192',NULL),(165,'김무성','sengmin12345','$2b$10$Si8.Kn1rHO2K/xLyxRGJ2unyI7CL3ijZgpblZ.ojItYVaLs7UQK7W',0,0,'2024-04-02 08:45:42.829519',NULL),(166,'김도읍','sengmin1111','$2b$10$19ycEPOgsilUiBul2zFRvO3F73J9x6FS5r2Bx45uoBFFdSFa4eiuu',0,0,'2024-04-02 08:46:01.579266',NULL),(167,'노무현','sengmin11124','$2b$10$aWHR6lLXLarkC6JOQ6.nM.EMdMx0dr6g0IWLX7qxxNDSenIBE3S4C',0,0,'2024-04-02 08:46:21.061442',NULL),(168,'gd','chan9784','$2b$10$vvg3tRgnmrSajUES199lbu/g5pCPANLjqp5SzUf7M6R.KGgQfmybK',0,0,'2024-04-02 08:49:45.478964',NULL),(169,'sweetpotato','sweetpotato','$2b$10$ljA1ocpwe31n3SiMAfA/Xe9kQlpYEte4nrKekAY.Z2.FQ/UcdC2HG',0,0,'2024-04-02 08:57:51.103302',NULL),(170,'재환','jw1234','$2b$10$oll4sTWpFx21TuBLkQkMSuFJPwx1wAHtJXYE3ZIEuAEDlu.ufKGP2',0,0,'2024-04-02 08:59:55.301684',NULL),(171,'1234','12344321','$2b$10$P1bS6UCrOMCZzbjkZMG2U.W5iEklHtqzWrX4uzB8MvUIqZYzeriu.',0,0,'2024-04-02 09:02:04.152301',NULL),(172,'싸피','nhs5036','$2b$10$eZ7IRL4wKa.5a2Z.0w9Ae.TySX2seI3LtrB1mk5arLrZXwaWlJTWa',0,0,'2024-04-02 09:30:35.050871',NULL),(173,'1233','12312','$2b$10$LnhHEV/E.Ar1kCLnMjDT0uHS9PG0Xtm3pyQoTZfFF.ayVyH.VFHxS',0,0,'2024-04-02 10:19:08.782538',NULL),(174,'1234','1234','$2b$10$QfC3O1gnLujCvXM74Cu1vuNYfeeWLBVZIEqsGifzIQfAp/ceFLDn.',0,0,'2024-04-02 10:19:26.180875',NULL),(175,'qhdrnak','qhdrnak','$2b$10$1C769irSvmIq16aMeoU3Qu1FbTSwO5q/eFGe3MfcirOjqtTjnAqnK',0,0,'2024-04-02 11:09:02.916434',NULL),(176,'김치','qwer1234','$2b$10$suafBcREYpFpIzCEI9Nh9OTQyo7MHei3asLZyvdlgDkod5N6nqAW6',0,0,'2024-04-02 11:15:41.105269',NULL),(177,'11','sssaa','$2b$10$wAFUHELJriB69mnEF5OyYeU4Jg1GKPjg.0xKGALAvH1SlDS/1uyl6',0,0,'2024-04-02 12:26:28.114582',NULL),(178,'ssa','sssa','$2b$10$adR4sN7CjItMRk/FmAq3N.jmu92.A7X5an35KN.sirR0FKmiemDOy',0,0,'2024-04-02 12:50:23.099143',NULL),(179,'dawn','dawn','$2b$10$ea1S7wTgZzZlYY1JnTUfLu/FuezlBFbT/UISzLHgywa8aDyMjzhDW',0,0,'2024-04-02 12:57:05.047754',NULL),(180,'gogwang','gogwang','$2b$10$27xdKm5KgbqJcgzCgOp6jeILxHUXKszsl9IfVruGLfGwVz3KC2h5m',0,0,'2024-04-02 13:01:41.156635',NULL),(181,'jungjung','ssafyjung','$2b$10$Dsx92SNQjG/n/eaOtoCrCeRHfO3CHLhE.kaQsza0E3BOHKyo6n86q',0,0,'2024-04-02 13:05:14.988192',NULL),(182,'게부라','cheuora','$2b$10$aHWqqy9imH5Al6lTYJsvDuO0joT.Uru4j/Tr1lBrtiq1l9T/Rb3v6',0,0,'2024-04-02 13:10:20.823852',NULL),(183,'asdasd','asdasd','$2b$10$2K/WuwQRvOJXMr49V3CkSeEEUbmjURgNgiBnBNano4ID64/luNplq',0,0,'2024-04-02 13:12:04.189462',NULL),(184,'공백','zsa332','$2b$10$54W2BM0iNU6jPn4ck5NeE.qF9RMWGqFenVpemm2FYZUSZV1Q67lAW',0,0,'2024-04-02 13:14:12.911250',NULL),(185,'happy','happy12','$2b$10$vZGV2wuHBahe7npIUk2eNejZjLAHJrz0nVorCQf8UKdSEhDccWgG6',0,0,'2024-04-02 13:14:28.074210',NULL),(186,'불코치','qwert','$2b$10$BSrhuPpuE6E09Lnc7stjFefO100.rI0GPzt8ZI8WcYmmmsXwQ34y2',0,0,'2024-04-02 13:39:01.317448',NULL),(187,'qweadsz','ssafy00','$2b$10$k13jJpjVaK1rEmaz6gghUOhRopZIg3TbEdrnsvL7yqMFjDdcY932S',0,0,'2024-04-02 13:39:46.520785',NULL),(188,'1234','hgoa','$2b$10$cGIrI7ZSDMhgD8iRKN1IE.z0EXlp/ALD/ev6UDrXGaZk8Bi8.nsN6',0,0,'2024-04-02 14:13:11.339214',NULL),(189,'소스','asdf1234','$2b$10$EWSijnlNQ/KBqQFV6IJ5t.aPSGI7vqYSWrc5PbNzZIgw2lGPK/t1y',0,0,'2024-04-02 14:37:45.492651',NULL),(190,'코치','coach','$2b$10$Z1FTu5DYTvxKVELNewkFG..UWJ2Kucwfq.KZNbQTbeQTfvL4u5kNy',0,0,'2024-04-02 14:53:42.812407',NULL),(191,'파개왕','yg5260','$2b$10$j.8Ukm5HERtwYnDeCfe3iutuvwK2Og.lmJjSg0N2mAT6BEy3EbhUK',0,0,'2024-04-02 15:03:50.465534',NULL),(192,'빵일','sugo123','$2b$10$QB8tTx2h0s0t91ZhmBlUxeeVladT38jbaC3d1V3f2v/l0sm.nHGsS',0,0,'2024-04-02 15:59:02.401428',NULL),(193,'김똑딱','kkkkbs1111','$2b$10$6wGovXK5ntBa.XxXjrJVl.OATUlTA98Bv8LH13s9fleCrwp9iyudS',0,0,'2024-04-02 16:13:37.920285',NULL),(194,'김똑딱','kkkk1234','$2b$10$MQ.0bT468frm.urc6w7yS.t/D2B3sywyvUlOxhouaCr7V7dYnkVaK',0,0,'2024-04-02 16:14:10.704100',NULL),(195,'aaaaa','aaaaa','$2b$10$Xzw7NKGE6YWJlAVTCNh0ouGH5W3eIs.GNJbcpU7XtMEz4dXghWKVi',0,0,'2024-04-02 17:22:21.632767',NULL),(196,'띠드','cnhug1','$2b$10$CE.9LATX4GgrSP2eaxvKiOS1KyonClzZdy/NXwz3rCgz2dXJUAI02',0,0,'2024-04-02 17:25:26.609047',NULL),(197,'seobo','seobo','$2b$10$c2pZOu1BYhEkVdNSpRKB2OTJU/cB3WaFjjZizMXBO4F5ryakZe7v6',0,0,'2024-04-02 17:35:14.672083',NULL),(198,'asdf12345','asdf12345','$2b$10$4KPQSscxoh7rQ.UIghaDIeyqvb4RyPlP0thaUtOaz.xIGMF/ABGq6',0,0,'2024-04-02 17:36:17.680268',NULL),(199,'riri0912','hslee0912','$2b$10$3NuI7NlYRFaf2zqi.EBrYuTvhwRAsHtHaP5z8xKUaCJ20F0dNtPa.',0,0,'2024-04-02 17:52:10.302746',NULL),(200,'qwe123123','qwe123123','$2b$10$E4MNkWG/7HiffRglg/eqmu9amdFeR0ZxuQ5x6314bLTVFpCuUwL.y',0,0,'2024-04-02 17:59:56.251642',NULL),(201,'져니','tjwjdgus83','$2b$10$N57cBSgJUSEc2XA2399rMOijhNTuGxX06dDBGVnEfNtS6BReLow46',0,0,'2024-04-02 21:14:32.908014',NULL),(202,'gfsd2397','ssafy01','$2b$10$CpUL55hrmyNLkafAUNThOOXqfMScYDlbilfexBU7pBr8U6Rh2bnfm',0,0,'2024-04-03 04:05:05.764190',NULL),(203,'에푸테스터','ftest','$2b$10$bDncU2vrX/bBWrjF6/hodOfTbRx1osmMc8Xf4zwU5..j6auOTYQN6',0,0,'2024-04-03 10:00:38.878948',NULL),(204,'z109','ssafyc102','$2b$10$lqMsFRGnBs/AMyyoipfzJeVT8p4z7irbrvOLv1F/U7c3FtRXlCfy.',0,0,'2024-04-03 11:27:14.560247',NULL),(205,'hagni','hangi','$2b$10$9zUVlyCd90VC3SZx1F81P.QLWQHVwE/u0w73o0x83yXC9lDTCEoqG',0,0,'2024-04-03 11:38:36.297189',NULL),(206,'서울촌놈','ssafyA608','$2b$10$.gpd564KQ9K2crGSjSHr7udnkundbZ8QaaXWn.Mc0JwB5JQm9GX92',0,0,'2024-04-03 11:38:55.409485',NULL),(207,'sonhangi','sonhangi','$2b$10$Vmh6zRo0SPO0d0jRp31pY.ASgUTzDOnGfft3b9Msr21eIni/nbDT2',0,0,'2024-04-03 11:39:01.550107',NULL),(208,'beefLost','lostTample','$2b$10$DxXreRe6gAKus8N7/qfa1.e/GNvTLHksVK4yI6J9/UeffgXYq7hHK',0,0,'2024-04-03 11:52:17.478518',NULL),(209,'hihi','hihi5252','$2b$10$tAIRWkA54jr0VIPtd8Zm3.IfES8QEVJozCK.Bs7sRQTCoHBVg8F.W',0,0,'2024-04-03 11:52:47.912360',NULL),(210,'강컨','mstkang1','$2b$10$t5eYpsjFEn8ZzvA8NS25WOUSv6NZf.M4Z3NGq443/A3cdUEBVy37W',0,0,'2024-04-03 11:53:09.256113',NULL),(211,'카라멜','test123123','$2b$10$KLuK4xz8KYOt.rzWnGPYWeIdPvlRvC6n/dNiaXgtD9DhjQyMsETxm',0,0,'2024-04-03 11:59:39.805502',NULL),(212,'qq','qqqq1111','$2b$10$ldmzmVeV.cjMw.IFkhjpzeMWQYXMTV/Ve5.4q0jLubaNNCFm6ITOe',0,0,'2024-04-03 12:02:19.126103',NULL),(213,'_워리어_','testwarrior','$2b$10$IPX.ZhIjbzDGTBdKn9B6q.sk4C1pN3yPH.ggcqbyC0WiSMbbDGRmW',0,0,'2024-04-03 12:20:15.766733',NULL),(214,'123123','dudwls95','$2b$10$ODRUvg3gnfzDV4uEzdwvY.g.fb.N2/ot6sUrNR/ylJAZsdnlvaMkq',0,0,'2024-04-03 12:21:01.460988',NULL),(215,'kkc','kimkichan','$2b$10$VUjYdgu.RK2gSeWFCSBxN.ci2SAsoF1CFa6YFh.8hy.jOkFebExs.',0,0,'2024-04-03 12:59:14.457604',NULL),(216,'asd0584','asd0584','$2b$10$HZLmktlQQ/kGX2x0wvbu3edGgGD8lVrHEkQhZ1bWT71zwRXMDC2TS',0,0,'2024-04-03 13:25:03.535333',NULL),(217,'damin','damin','$2b$10$6qrr3BPGbR/rJy00867Truweuu42yGcjJ1UAfDPdP4GCuxlnTV3li',0,0,'2024-04-03 13:39:48.037606',NULL),(218,'kaka','kakaya12','$2b$10$EKGA3TqMvZcWXieVHPOjROFBJbCSISJ9y4LlpqDHZQ9LJEk5Q6dle',0,0,'2024-04-03 13:48:02.400390',NULL),(219,'윤모','momo123','$2b$10$btUsgPsUj90hahyN8APAQeHkMljSwbpJYqFt.uOl9x3tQIRlzhKCO',0,0,'2024-04-03 13:58:25.543100',NULL),(220,'fefejz','asdf6543','$2b$10$lRSBokVPY2CuiHlRtmsZN.h4Yilwvz1rQHnWIn6PUJ1DlCuo0Bt92',0,0,'2024-04-03 13:59:12.328041',NULL),(221,'changchangchang','changchangchang','$2b$10$EP33Hnyjf8zbR6pMHgDdh.xPbCqPHg3cyRXbGmMQXmb39C/G5j2yK',0,0,'2024-04-03 14:00:49.082561',NULL),(222,'arimu','arim','$2b$10$xNdwBqZCv458mvjNeRZfKODueDANxYZl5HMEWdcp2rKOjm.D04uBS',0,0,'2024-04-03 14:01:25.111619',NULL),(223,'_메이지_','testmage','$2b$10$IYzCCpNDxoIyYDKX91.AxuEhFlmT/3HGYNVbUTCKGHsU00ZSB/3uC',0,0,'2024-04-03 14:05:18.606702',NULL),(224,'ymmmiin','ymin0216','$2b$10$nELzBUA9bvcGO4T0ychGEeWVYA6uOLkLdkiy5ywuC2mFke6JbPZ82',0,0,'2024-04-03 14:06:02.295255',NULL),(225,'_프리스트_','testpriest','$2b$10$2F5IvdoWIM4RLvYA5hnafewzj3Wcuh0eHSqNjKxNkHwC6obeJSJ5W',0,0,'2024-04-03 14:17:34.680830',NULL),(226,'ssafygumi','ssafy1234','$2b$10$ZM5BiePADz1Ob4PbsRee5uz4rIK4ew62BgYdcnez3Q5ZRXcPcsv7m',0,0,'2024-04-03 14:27:44.728366',NULL),(227,'한성현','sunghyun','$2b$10$PKro22FggQfgZNGxdqUCQucZ2kzbpRbR7J..PKvsJT1jwzqAnf0Vq',0,0,'2024-04-03 14:41:28.623229',NULL);
/*!40000 ALTER TABLE `member` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `member_equipment`
--

DROP TABLE IF EXISTS `member_equipment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `member_equipment` (
  `custom_code_type_id` char(3) NOT NULL COMMENT '커스텀 코드 타입 ID',
  `member_id` bigint NOT NULL COMMENT '사용자 ID',
  `custom_code_id` char(8) NOT NULL COMMENT '공통 코드 ID',
  PRIMARY KEY (`custom_code_type_id`,`member_id`),
  KEY `FK_d0508e95f5dbbd01e1aa42025cf` (`member_id`),
  KEY `FK_921dd32a025f0804f0091161aee` (`custom_code_id`),
  CONSTRAINT `FK_921dd32a025f0804f0091161aee` FOREIGN KEY (`custom_code_id`) REFERENCES `common_code` (`common_code_id`),
  CONSTRAINT `FK_d0508e95f5dbbd01e1aa42025cf` FOREIGN KEY (`member_id`) REFERENCES `member` (`member_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='사용자 착용 정보';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `member_equipment`
--

LOCK TABLES `member_equipment` WRITE;
/*!40000 ALTER TABLE `member_equipment` DISABLE KEYS */;
INSERT INTO `member_equipment` VALUES ('CSK',4,'CSK_0001'),('CSK',5,'CSK_0001'),('CSK',6,'CSK_0001'),('CSK',7,'CSK_0001'),('CSK',8,'CSK_0001'),('CSK',9,'CSK_0001'),('CSK',10,'CSK_0001'),('CSK',11,'CSK_0001'),('CSK',12,'CSK_0001'),('CSK',13,'CSK_0001'),('CSK',14,'CSK_0001'),('CSK',15,'CSK_0001'),('CSK',16,'CSK_0001'),('CSK',17,'CSK_0001'),('CSK',18,'CSK_0001'),('CSK',19,'CSK_0001'),('CSK',20,'CSK_0001'),('CSK',21,'CSK_0001'),('CSK',22,'CSK_0001'),('CSK',23,'CSK_0001'),('CSK',24,'CSK_0001'),('CSK',25,'CSK_0001'),('CSK',26,'CSK_0001'),('CSK',27,'CSK_0001'),('CSK',28,'CSK_0001'),('CSK',29,'CSK_0001'),('CSK',30,'CSK_0001'),('CSK',31,'CSK_0001'),('CSK',32,'CSK_0001'),('CSK',33,'CSK_0001'),('CSK',34,'CSK_0001'),('CSK',35,'CSK_0001'),('CSK',36,'CSK_0001'),('CSK',37,'CSK_0001'),('CSK',38,'CSK_0001'),('CSK',39,'CSK_0001'),('CSK',40,'CSK_0001'),('CSK',41,'CSK_0001'),('CSK',42,'CSK_0001'),('CSK',43,'CSK_0001'),('CSK',44,'CSK_0001'),('CSK',45,'CSK_0001'),('CSK',46,'CSK_0001'),('CSK',47,'CSK_0001'),('CSK',48,'CSK_0001'),('CSK',49,'CSK_0001'),('CSK',50,'CSK_0001'),('CSK',51,'CSK_0001'),('CSK',52,'CSK_0001'),('CSK',53,'CSK_0001'),('CSK',54,'CSK_0001'),('CSK',55,'CSK_0001'),('CSK',56,'CSK_0001'),('CSK',57,'CSK_0001'),('CSK',58,'CSK_0001'),('CSK',59,'CSK_0001'),('CSK',60,'CSK_0001'),('CSK',61,'CSK_0001'),('CSK',62,'CSK_0001'),('CSK',63,'CSK_0001'),('CSK',64,'CSK_0001'),('CSK',65,'CSK_0001'),('CSK',66,'CSK_0001'),('CSK',67,'CSK_0001'),('CSK',68,'CSK_0001'),('CSK',69,'CSK_0001'),('CSK',70,'CSK_0001'),('CSK',71,'CSK_0001'),('CSK',72,'CSK_0001'),('CSK',73,'CSK_0001'),('CSK',74,'CSK_0001'),('CSK',75,'CSK_0001'),('CSK',76,'CSK_0001'),('CSK',77,'CSK_0001'),('CSK',78,'CSK_0001'),('CSK',79,'CSK_0001'),('CSK',80,'CSK_0001'),('CSK',81,'CSK_0001'),('CSK',82,'CSK_0001'),('CSK',83,'CSK_0001'),('CSK',84,'CSK_0001'),('CSK',85,'CSK_0001'),('CSK',86,'CSK_0001'),('CSK',87,'CSK_0001'),('CSK',88,'CSK_0001'),('CSK',89,'CSK_0001'),('CSK',90,'CSK_0001'),('CSK',91,'CSK_0001'),('CSK',92,'CSK_0001'),('CSK',93,'CSK_0001'),('CSK',94,'CSK_0001'),('CSK',95,'CSK_0001'),('CSK',96,'CSK_0001'),('CSK',97,'CSK_0001'),('CSK',98,'CSK_0001'),('CSK',99,'CSK_0001'),('CSK',100,'CSK_0001'),('CSK',101,'CSK_0001'),('CSK',102,'CSK_0001'),('CSK',103,'CSK_0001'),('CSK',104,'CSK_0001'),('CSK',105,'CSK_0001'),('CSK',106,'CSK_0001'),('CSK',107,'CSK_0001'),('CSK',108,'CSK_0001'),('CSK',109,'CSK_0001'),('CSK',110,'CSK_0001'),('CSK',111,'CSK_0001'),('CSK',112,'CSK_0001'),('CSK',113,'CSK_0001'),('CSK',114,'CSK_0001'),('CSK',115,'CSK_0001'),('CSK',116,'CSK_0001'),('CSK',117,'CSK_0001'),('CSK',118,'CSK_0001'),('CSK',119,'CSK_0001'),('CSK',120,'CSK_0001'),('CSK',121,'CSK_0001'),('CSK',122,'CSK_0001'),('CSK',123,'CSK_0001'),('CSK',124,'CSK_0001'),('CSK',125,'CSK_0001'),('CSK',126,'CSK_0001'),('CSK',127,'CSK_0001'),('CSK',128,'CSK_0001'),('CSK',129,'CSK_0001'),('CSK',130,'CSK_0001'),('CSK',131,'CSK_0001'),('CSK',132,'CSK_0001'),('CSK',133,'CSK_0001'),('CSK',134,'CSK_0001'),('CSK',135,'CSK_0001'),('CSK',136,'CSK_0001'),('CSK',137,'CSK_0001'),('CSK',138,'CSK_0001'),('CSK',139,'CSK_0001'),('CSK',140,'CSK_0001'),('CSK',141,'CSK_0001'),('CSK',142,'CSK_0001'),('CSK',143,'CSK_0001'),('CSK',144,'CSK_0001'),('CSK',145,'CSK_0001'),('CSK',146,'CSK_0001'),('CSK',147,'CSK_0001'),('CSK',148,'CSK_0001'),('CSK',149,'CSK_0001'),('CSK',150,'CSK_0001'),('CSK',151,'CSK_0001'),('CSK',152,'CSK_0001'),('CSK',153,'CSK_0001'),('CSK',154,'CSK_0001'),('CSK',155,'CSK_0001'),('CSK',156,'CSK_0001'),('CSK',157,'CSK_0001'),('CSK',158,'CSK_0001'),('CSK',159,'CSK_0001'),('CSK',160,'CSK_0001'),('CSK',161,'CSK_0001'),('CSK',162,'CSK_0001'),('CSK',163,'CSK_0001'),('CSK',164,'CSK_0001'),('CSK',165,'CSK_0001'),('CSK',166,'CSK_0001'),('CSK',167,'CSK_0001'),('CSK',168,'CSK_0001'),('CSK',169,'CSK_0001'),('CSK',170,'CSK_0001'),('CSK',171,'CSK_0001'),('CSK',172,'CSK_0001'),('CSK',173,'CSK_0001'),('CSK',174,'CSK_0001'),('CSK',175,'CSK_0001'),('CSK',176,'CSK_0001'),('CSK',177,'CSK_0001'),('CSK',178,'CSK_0001'),('CSK',179,'CSK_0001'),('CSK',180,'CSK_0001'),('CSK',181,'CSK_0001'),('CSK',182,'CSK_0001'),('CSK',183,'CSK_0001'),('CSK',184,'CSK_0001'),('CSK',185,'CSK_0001'),('CSK',186,'CSK_0001'),('CSK',187,'CSK_0001'),('CSK',188,'CSK_0001'),('CSK',189,'CSK_0001'),('CSK',190,'CSK_0001'),('CSK',191,'CSK_0001'),('CSK',192,'CSK_0001'),('CSK',193,'CSK_0001'),('CSK',194,'CSK_0001'),('CSK',195,'CSK_0001'),('CSK',196,'CSK_0001'),('CSK',197,'CSK_0001'),('CSK',198,'CSK_0001'),('CSK',199,'CSK_0001'),('CSK',200,'CSK_0001'),('CSK',201,'CSK_0001'),('CSK',202,'CSK_0001'),('CSK',203,'CSK_0001'),('CSK',204,'CSK_0001'),('CSK',205,'CSK_0001'),('CSK',206,'CSK_0001'),('CSK',207,'CSK_0001'),('CSK',208,'CSK_0001'),('CSK',209,'CSK_0001'),('CSK',210,'CSK_0001'),('CSK',211,'CSK_0001'),('CSK',212,'CSK_0001'),('CSK',213,'CSK_0001'),('CSK',214,'CSK_0001'),('CSK',215,'CSK_0001'),('CSK',216,'CSK_0001'),('CSK',217,'CSK_0001'),('CSK',218,'CSK_0001'),('CSK',219,'CSK_0001'),('CSK',220,'CSK_0001'),('CSK',221,'CSK_0001'),('CSK',222,'CSK_0001'),('CSK',223,'CSK_0001'),('CSK',224,'CSK_0001'),('CSK',225,'CSK_0001'),('CSK',226,'CSK_0001'),('CSK',227,'CSK_0001'),('JOB',4,'JOB_0001'),('JOB',5,'JOB_0001'),('JOB',6,'JOB_0001'),('JOB',7,'JOB_0001'),('JOB',8,'JOB_0001'),('JOB',9,'JOB_0001'),('JOB',10,'JOB_0001'),('JOB',11,'JOB_0001'),('JOB',12,'JOB_0001'),('JOB',13,'JOB_0001'),('JOB',14,'JOB_0001'),('JOB',15,'JOB_0001'),('JOB',16,'JOB_0001'),('JOB',17,'JOB_0001'),('JOB',18,'JOB_0001'),('JOB',19,'JOB_0001'),('JOB',20,'JOB_0001'),('JOB',21,'JOB_0001'),('JOB',22,'JOB_0001'),('JOB',23,'JOB_0001'),('JOB',24,'JOB_0001'),('JOB',25,'JOB_0001'),('JOB',26,'JOB_0001'),('JOB',27,'JOB_0001'),('JOB',28,'JOB_0001'),('JOB',29,'JOB_0001'),('JOB',30,'JOB_0001'),('JOB',31,'JOB_0001'),('JOB',32,'JOB_0001'),('JOB',33,'JOB_0001'),('JOB',34,'JOB_0001'),('JOB',35,'JOB_0001'),('JOB',36,'JOB_0001'),('JOB',37,'JOB_0001'),('JOB',38,'JOB_0001'),('JOB',39,'JOB_0001'),('JOB',40,'JOB_0001'),('JOB',41,'JOB_0001'),('JOB',42,'JOB_0001'),('JOB',43,'JOB_0001'),('JOB',44,'JOB_0001'),('JOB',45,'JOB_0001'),('JOB',46,'JOB_0001'),('JOB',47,'JOB_0001'),('JOB',48,'JOB_0001'),('JOB',49,'JOB_0001'),('JOB',50,'JOB_0001'),('JOB',51,'JOB_0001'),('JOB',52,'JOB_0001'),('JOB',53,'JOB_0001'),('JOB',54,'JOB_0001'),('JOB',55,'JOB_0001'),('JOB',56,'JOB_0001'),('JOB',57,'JOB_0001'),('JOB',58,'JOB_0001'),('JOB',59,'JOB_0001'),('JOB',60,'JOB_0001'),('JOB',61,'JOB_0001'),('JOB',62,'JOB_0001'),('JOB',63,'JOB_0001'),('JOB',64,'JOB_0001'),('JOB',65,'JOB_0001'),('JOB',66,'JOB_0001'),('JOB',67,'JOB_0001'),('JOB',68,'JOB_0001'),('JOB',69,'JOB_0001'),('JOB',70,'JOB_0001'),('JOB',71,'JOB_0001'),('JOB',72,'JOB_0001'),('JOB',73,'JOB_0001'),('JOB',74,'JOB_0001'),('JOB',75,'JOB_0001'),('JOB',76,'JOB_0001'),('JOB',77,'JOB_0001'),('JOB',78,'JOB_0001'),('JOB',79,'JOB_0001'),('JOB',80,'JOB_0001'),('JOB',81,'JOB_0001'),('JOB',82,'JOB_0001'),('JOB',83,'JOB_0001'),('JOB',84,'JOB_0001'),('JOB',85,'JOB_0001'),('JOB',86,'JOB_0001'),('JOB',87,'JOB_0001'),('JOB',88,'JOB_0001'),('JOB',89,'JOB_0001'),('JOB',90,'JOB_0001'),('JOB',91,'JOB_0001'),('JOB',92,'JOB_0001'),('JOB',93,'JOB_0001'),('JOB',94,'JOB_0001'),('JOB',95,'JOB_0001'),('JOB',96,'JOB_0001'),('JOB',97,'JOB_0001'),('JOB',98,'JOB_0001'),('JOB',99,'JOB_0001'),('JOB',100,'JOB_0001'),('JOB',101,'JOB_0001'),('JOB',102,'JOB_0001'),('JOB',103,'JOB_0001'),('JOB',104,'JOB_0001'),('JOB',105,'JOB_0001'),('JOB',106,'JOB_0001'),('JOB',107,'JOB_0001'),('JOB',108,'JOB_0001'),('JOB',109,'JOB_0001'),('JOB',110,'JOB_0001'),('JOB',111,'JOB_0001'),('JOB',112,'JOB_0001'),('JOB',113,'JOB_0001'),('JOB',114,'JOB_0001'),('JOB',115,'JOB_0001'),('JOB',116,'JOB_0001'),('JOB',117,'JOB_0001'),('JOB',118,'JOB_0001'),('JOB',119,'JOB_0001'),('JOB',120,'JOB_0001'),('JOB',121,'JOB_0001'),('JOB',122,'JOB_0001'),('JOB',123,'JOB_0001'),('JOB',124,'JOB_0001'),('JOB',125,'JOB_0001'),('JOB',126,'JOB_0001'),('JOB',127,'JOB_0001'),('JOB',128,'JOB_0001'),('JOB',129,'JOB_0001'),('JOB',130,'JOB_0001'),('JOB',131,'JOB_0001'),('JOB',132,'JOB_0001'),('JOB',133,'JOB_0001'),('JOB',134,'JOB_0001'),('JOB',135,'JOB_0001'),('JOB',136,'JOB_0001'),('JOB',137,'JOB_0001'),('JOB',138,'JOB_0001'),('JOB',139,'JOB_0001'),('JOB',140,'JOB_0001'),('JOB',141,'JOB_0001'),('JOB',142,'JOB_0001'),('JOB',143,'JOB_0001'),('JOB',144,'JOB_0001'),('JOB',145,'JOB_0001'),('JOB',146,'JOB_0001'),('JOB',147,'JOB_0001'),('JOB',148,'JOB_0001'),('JOB',149,'JOB_0001'),('JOB',150,'JOB_0001'),('JOB',151,'JOB_0001'),('JOB',152,'JOB_0001'),('JOB',153,'JOB_0001'),('JOB',154,'JOB_0001'),('JOB',155,'JOB_0001'),('JOB',156,'JOB_0001'),('JOB',157,'JOB_0001'),('JOB',158,'JOB_0001'),('JOB',159,'JOB_0001'),('JOB',160,'JOB_0001'),('JOB',161,'JOB_0001'),('JOB',162,'JOB_0001'),('JOB',163,'JOB_0001'),('JOB',164,'JOB_0001'),('JOB',165,'JOB_0001'),('JOB',166,'JOB_0001'),('JOB',167,'JOB_0001'),('JOB',168,'JOB_0001'),('JOB',169,'JOB_0001'),('JOB',170,'JOB_0001'),('JOB',171,'JOB_0001'),('JOB',172,'JOB_0001'),('JOB',173,'JOB_0001'),('JOB',174,'JOB_0001'),('JOB',175,'JOB_0001'),('JOB',176,'JOB_0001'),('JOB',177,'JOB_0001'),('JOB',178,'JOB_0001'),('JOB',179,'JOB_0001'),('JOB',180,'JOB_0001'),('JOB',181,'JOB_0001'),('JOB',182,'JOB_0001'),('JOB',183,'JOB_0001'),('JOB',184,'JOB_0001'),('JOB',185,'JOB_0001'),('JOB',186,'JOB_0001'),('JOB',187,'JOB_0001'),('JOB',188,'JOB_0001'),('JOB',189,'JOB_0001'),('JOB',190,'JOB_0001'),('JOB',191,'JOB_0001'),('JOB',192,'JOB_0001'),('JOB',193,'JOB_0001'),('JOB',194,'JOB_0001'),('JOB',195,'JOB_0001'),('JOB',196,'JOB_0001'),('JOB',197,'JOB_0001'),('JOB',198,'JOB_0001'),('JOB',199,'JOB_0001'),('JOB',200,'JOB_0001'),('JOB',201,'JOB_0001'),('JOB',202,'JOB_0001'),('JOB',203,'JOB_0001'),('JOB',204,'JOB_0001'),('JOB',205,'JOB_0001'),('JOB',206,'JOB_0001'),('JOB',207,'JOB_0001'),('JOB',208,'JOB_0001'),('JOB',209,'JOB_0001'),('JOB',210,'JOB_0001'),('JOB',211,'JOB_0001'),('JOB',212,'JOB_0001'),('JOB',213,'JOB_0001'),('JOB',214,'JOB_0001'),('JOB',215,'JOB_0001'),('JOB',216,'JOB_0001'),('JOB',217,'JOB_0001'),('JOB',218,'JOB_0001'),('JOB',219,'JOB_0001'),('JOB',220,'JOB_0001'),('JOB',221,'JOB_0001'),('JOB',222,'JOB_0001'),('JOB',223,'JOB_0001'),('JOB',224,'JOB_0001'),('JOB',225,'JOB_0001'),('JOB',226,'JOB_0001'),('JOB',227,'JOB_0001'),('PET',4,'PET_0001'),('PET',5,'PET_0001'),('PET',6,'PET_0001'),('PET',7,'PET_0001'),('PET',8,'PET_0001'),('PET',9,'PET_0001'),('PET',10,'PET_0001'),('PET',11,'PET_0001'),('PET',12,'PET_0001'),('PET',13,'PET_0001'),('PET',14,'PET_0001'),('PET',15,'PET_0001'),('PET',16,'PET_0001'),('PET',17,'PET_0001'),('PET',18,'PET_0001'),('PET',19,'PET_0001'),('PET',20,'PET_0001'),('PET',21,'PET_0001'),('PET',22,'PET_0001'),('PET',23,'PET_0001'),('PET',24,'PET_0001'),('PET',25,'PET_0001'),('PET',26,'PET_0001'),('PET',27,'PET_0001'),('PET',28,'PET_0001'),('PET',29,'PET_0001'),('PET',30,'PET_0001'),('PET',31,'PET_0001'),('PET',32,'PET_0001'),('PET',33,'PET_0001'),('PET',34,'PET_0001'),('PET',35,'PET_0001'),('PET',36,'PET_0001'),('PET',37,'PET_0001'),('PET',38,'PET_0001'),('PET',39,'PET_0001'),('PET',40,'PET_0001'),('PET',41,'PET_0001'),('PET',42,'PET_0001'),('PET',43,'PET_0001'),('PET',44,'PET_0001'),('PET',45,'PET_0001'),('PET',46,'PET_0001'),('PET',47,'PET_0001'),('PET',48,'PET_0001'),('PET',49,'PET_0001'),('PET',50,'PET_0001'),('PET',51,'PET_0001'),('PET',52,'PET_0001'),('PET',53,'PET_0001'),('PET',54,'PET_0001'),('PET',55,'PET_0001'),('PET',56,'PET_0001'),('PET',57,'PET_0001'),('PET',58,'PET_0001'),('PET',59,'PET_0001'),('PET',60,'PET_0001'),('PET',61,'PET_0001'),('PET',62,'PET_0001'),('PET',63,'PET_0001'),('PET',64,'PET_0001'),('PET',65,'PET_0001'),('PET',66,'PET_0001'),('PET',67,'PET_0001'),('PET',68,'PET_0001'),('PET',69,'PET_0001'),('PET',70,'PET_0001'),('PET',71,'PET_0001'),('PET',72,'PET_0001'),('PET',73,'PET_0001'),('PET',74,'PET_0001'),('PET',75,'PET_0001'),('PET',76,'PET_0001'),('PET',77,'PET_0001'),('PET',78,'PET_0001'),('PET',79,'PET_0001'),('PET',80,'PET_0001'),('PET',81,'PET_0001'),('PET',82,'PET_0001'),('PET',83,'PET_0001'),('PET',84,'PET_0001'),('PET',85,'PET_0001'),('PET',86,'PET_0001'),('PET',87,'PET_0001'),('PET',88,'PET_0001'),('PET',89,'PET_0001'),('PET',90,'PET_0001'),('PET',91,'PET_0001'),('PET',92,'PET_0001'),('PET',93,'PET_0001'),('PET',94,'PET_0001'),('PET',95,'PET_0001'),('PET',96,'PET_0001'),('PET',97,'PET_0001'),('PET',98,'PET_0001'),('PET',99,'PET_0001'),('PET',100,'PET_0001'),('PET',101,'PET_0001'),('PET',102,'PET_0001'),('PET',103,'PET_0001'),('PET',104,'PET_0001'),('PET',105,'PET_0001'),('PET',106,'PET_0001'),('PET',107,'PET_0001'),('PET',108,'PET_0001'),('PET',109,'PET_0001'),('PET',110,'PET_0001'),('PET',111,'PET_0001'),('PET',112,'PET_0001'),('PET',113,'PET_0001'),('PET',114,'PET_0001'),('PET',115,'PET_0001'),('PET',116,'PET_0001'),('PET',117,'PET_0001'),('PET',118,'PET_0001'),('PET',119,'PET_0001'),('PET',120,'PET_0001'),('PET',121,'PET_0001'),('PET',122,'PET_0001'),('PET',123,'PET_0001'),('PET',124,'PET_0001'),('PET',125,'PET_0001'),('PET',126,'PET_0001'),('PET',127,'PET_0001'),('PET',128,'PET_0001'),('PET',129,'PET_0001'),('PET',130,'PET_0001'),('PET',131,'PET_0001'),('PET',132,'PET_0001'),('PET',133,'PET_0001'),('PET',134,'PET_0001'),('PET',135,'PET_0001'),('PET',136,'PET_0001'),('PET',137,'PET_0001'),('PET',138,'PET_0001'),('PET',139,'PET_0001'),('PET',140,'PET_0001'),('PET',141,'PET_0001'),('PET',142,'PET_0001'),('PET',143,'PET_0001'),('PET',144,'PET_0001'),('PET',145,'PET_0001'),('PET',146,'PET_0001'),('PET',147,'PET_0001'),('PET',148,'PET_0001'),('PET',149,'PET_0001'),('PET',150,'PET_0001'),('PET',151,'PET_0001'),('PET',152,'PET_0001'),('PET',153,'PET_0001'),('PET',154,'PET_0001'),('PET',155,'PET_0001'),('PET',156,'PET_0001'),('PET',157,'PET_0001'),('PET',158,'PET_0001'),('PET',159,'PET_0001'),('PET',160,'PET_0001'),('PET',161,'PET_0001'),('PET',162,'PET_0001'),('PET',163,'PET_0001'),('PET',164,'PET_0001'),('PET',165,'PET_0001'),('PET',166,'PET_0001'),('PET',167,'PET_0001'),('PET',168,'PET_0001'),('PET',169,'PET_0001'),('PET',170,'PET_0001'),('PET',171,'PET_0001'),('PET',172,'PET_0001'),('PET',173,'PET_0001'),('PET',174,'PET_0001'),('PET',175,'PET_0001'),('PET',176,'PET_0001'),('PET',177,'PET_0001'),('PET',178,'PET_0001'),('PET',179,'PET_0001'),('PET',180,'PET_0001'),('PET',181,'PET_0001'),('PET',182,'PET_0001'),('PET',183,'PET_0001'),('PET',184,'PET_0001'),('PET',185,'PET_0001'),('PET',186,'PET_0001'),('PET',187,'PET_0001'),('PET',188,'PET_0001'),('PET',189,'PET_0001'),('PET',190,'PET_0001'),('PET',191,'PET_0001'),('PET',192,'PET_0001'),('PET',193,'PET_0001'),('PET',194,'PET_0001'),('PET',195,'PET_0001'),('PET',196,'PET_0001'),('PET',197,'PET_0001'),('PET',198,'PET_0001'),('PET',199,'PET_0001'),('PET',200,'PET_0001'),('PET',201,'PET_0001'),('PET',202,'PET_0001'),('PET',203,'PET_0001'),('PET',204,'PET_0001'),('PET',205,'PET_0001'),('PET',206,'PET_0001'),('PET',207,'PET_0001'),('PET',208,'PET_0001'),('PET',209,'PET_0001'),('PET',210,'PET_0001'),('PET',211,'PET_0001'),('PET',212,'PET_0001'),('PET',213,'PET_0001'),('PET',214,'PET_0001'),('PET',215,'PET_0001'),('PET',216,'PET_0001'),('PET',217,'PET_0001'),('PET',218,'PET_0001'),('PET',219,'PET_0001'),('PET',220,'PET_0001'),('PET',221,'PET_0001'),('PET',222,'PET_0001'),('PET',223,'PET_0001'),('PET',224,'PET_0001'),('PET',225,'PET_0001'),('PET',226,'PET_0001'),('PET',227,'PET_0001'),('SKN',4,'SKN_0001'),('SKN',5,'SKN_0001'),('SKN',6,'SKN_0001'),('SKN',7,'SKN_0001'),('SKN',8,'SKN_0001'),('SKN',9,'SKN_0001'),('SKN',10,'SKN_0001'),('SKN',11,'SKN_0001'),('SKN',12,'SKN_0001'),('SKN',13,'SKN_0001'),('SKN',14,'SKN_0001'),('SKN',15,'SKN_0001'),('SKN',16,'SKN_0001'),('SKN',17,'SKN_0001'),('SKN',18,'SKN_0001'),('SKN',19,'SKN_0001'),('SKN',20,'SKN_0001'),('SKN',21,'SKN_0001'),('SKN',22,'SKN_0001'),('SKN',23,'SKN_0001'),('SKN',24,'SKN_0001'),('SKN',25,'SKN_0001'),('SKN',26,'SKN_0001'),('SKN',27,'SKN_0001'),('SKN',28,'SKN_0001'),('SKN',29,'SKN_0001'),('SKN',30,'SKN_0001'),('SKN',31,'SKN_0001'),('SKN',32,'SKN_0001'),('SKN',33,'SKN_0001'),('SKN',34,'SKN_0001'),('SKN',35,'SKN_0001'),('SKN',36,'SKN_0001'),('SKN',37,'SKN_0001'),('SKN',38,'SKN_0001'),('SKN',39,'SKN_0001'),('SKN',40,'SKN_0001'),('SKN',41,'SKN_0001'),('SKN',42,'SKN_0001'),('SKN',43,'SKN_0001'),('SKN',44,'SKN_0001'),('SKN',45,'SKN_0001'),('SKN',46,'SKN_0001'),('SKN',47,'SKN_0001'),('SKN',48,'SKN_0001'),('SKN',49,'SKN_0001'),('SKN',50,'SKN_0001'),('SKN',51,'SKN_0001'),('SKN',52,'SKN_0001'),('SKN',53,'SKN_0001'),('SKN',54,'SKN_0001'),('SKN',55,'SKN_0001'),('SKN',56,'SKN_0001'),('SKN',57,'SKN_0001'),('SKN',58,'SKN_0001'),('SKN',59,'SKN_0001'),('SKN',60,'SKN_0001'),('SKN',61,'SKN_0001'),('SKN',62,'SKN_0001'),('SKN',63,'SKN_0001'),('SKN',64,'SKN_0001'),('SKN',65,'SKN_0001'),('SKN',66,'SKN_0001'),('SKN',67,'SKN_0001'),('SKN',68,'SKN_0001'),('SKN',69,'SKN_0001'),('SKN',70,'SKN_0001'),('SKN',71,'SKN_0001'),('SKN',72,'SKN_0001'),('SKN',73,'SKN_0001'),('SKN',74,'SKN_0001'),('SKN',75,'SKN_0001'),('SKN',76,'SKN_0001'),('SKN',77,'SKN_0001'),('SKN',78,'SKN_0001'),('SKN',79,'SKN_0001'),('SKN',80,'SKN_0001'),('SKN',81,'SKN_0001'),('SKN',82,'SKN_0001'),('SKN',83,'SKN_0001'),('SKN',84,'SKN_0001'),('SKN',85,'SKN_0001'),('SKN',86,'SKN_0001'),('SKN',87,'SKN_0001'),('SKN',88,'SKN_0001'),('SKN',89,'SKN_0001'),('SKN',90,'SKN_0001'),('SKN',91,'SKN_0001'),('SKN',92,'SKN_0001'),('SKN',93,'SKN_0001'),('SKN',94,'SKN_0001'),('SKN',95,'SKN_0001'),('SKN',96,'SKN_0001'),('SKN',97,'SKN_0001'),('SKN',98,'SKN_0001'),('SKN',99,'SKN_0001'),('SKN',100,'SKN_0001'),('SKN',101,'SKN_0001'),('SKN',102,'SKN_0001'),('SKN',103,'SKN_0001'),('SKN',104,'SKN_0001'),('SKN',105,'SKN_0001'),('SKN',106,'SKN_0001'),('SKN',107,'SKN_0001'),('SKN',108,'SKN_0001'),('SKN',109,'SKN_0001'),('SKN',110,'SKN_0001'),('SKN',111,'SKN_0001'),('SKN',112,'SKN_0001'),('SKN',113,'SKN_0001'),('SKN',114,'SKN_0001'),('SKN',115,'SKN_0001'),('SKN',116,'SKN_0001'),('SKN',117,'SKN_0001'),('SKN',118,'SKN_0001'),('SKN',119,'SKN_0001'),('SKN',120,'SKN_0001'),('SKN',121,'SKN_0001'),('SKN',122,'SKN_0001'),('SKN',123,'SKN_0001'),('SKN',124,'SKN_0001'),('SKN',125,'SKN_0001'),('SKN',126,'SKN_0001'),('SKN',127,'SKN_0001'),('SKN',128,'SKN_0001'),('SKN',129,'SKN_0001'),('SKN',130,'SKN_0001'),('SKN',131,'SKN_0001'),('SKN',132,'SKN_0001'),('SKN',133,'SKN_0001'),('SKN',134,'SKN_0001'),('SKN',135,'SKN_0001'),('SKN',136,'SKN_0001'),('SKN',137,'SKN_0001'),('SKN',138,'SKN_0001'),('SKN',139,'SKN_0001'),('SKN',140,'SKN_0001'),('SKN',141,'SKN_0001'),('SKN',142,'SKN_0001'),('SKN',143,'SKN_0001'),('SKN',144,'SKN_0001'),('SKN',145,'SKN_0001'),('SKN',146,'SKN_0001'),('SKN',147,'SKN_0001'),('SKN',148,'SKN_0001'),('SKN',149,'SKN_0001'),('SKN',150,'SKN_0001'),('SKN',151,'SKN_0001'),('SKN',152,'SKN_0001'),('SKN',153,'SKN_0001'),('SKN',154,'SKN_0001'),('SKN',155,'SKN_0001'),('SKN',156,'SKN_0001'),('SKN',157,'SKN_0001'),('SKN',158,'SKN_0001'),('SKN',159,'SKN_0001'),('SKN',160,'SKN_0001'),('SKN',161,'SKN_0001'),('SKN',162,'SKN_0001'),('SKN',163,'SKN_0001'),('SKN',164,'SKN_0001'),('SKN',165,'SKN_0001'),('SKN',166,'SKN_0001'),('SKN',167,'SKN_0001'),('SKN',168,'SKN_0001'),('SKN',169,'SKN_0001'),('SKN',170,'SKN_0001'),('SKN',171,'SKN_0001'),('SKN',172,'SKN_0001'),('SKN',173,'SKN_0001'),('SKN',174,'SKN_0001'),('SKN',175,'SKN_0001'),('SKN',176,'SKN_0001'),('SKN',177,'SKN_0001'),('SKN',178,'SKN_0001'),('SKN',179,'SKN_0001'),('SKN',180,'SKN_0001'),('SKN',181,'SKN_0001'),('SKN',182,'SKN_0001'),('SKN',183,'SKN_0001'),('SKN',184,'SKN_0001'),('SKN',185,'SKN_0001'),('SKN',186,'SKN_0001'),('SKN',187,'SKN_0001'),('SKN',188,'SKN_0001'),('SKN',189,'SKN_0001'),('SKN',190,'SKN_0001'),('SKN',191,'SKN_0001'),('SKN',192,'SKN_0001'),('SKN',193,'SKN_0001'),('SKN',194,'SKN_0001'),('SKN',195,'SKN_0001'),('SKN',196,'SKN_0001'),('SKN',197,'SKN_0001'),('SKN',198,'SKN_0001'),('SKN',199,'SKN_0001'),('SKN',200,'SKN_0001'),('SKN',201,'SKN_0001'),('SKN',202,'SKN_0001'),('SKN',203,'SKN_0001'),('SKN',204,'SKN_0001'),('SKN',205,'SKN_0001'),('SKN',206,'SKN_0001'),('SKN',207,'SKN_0001'),('SKN',208,'SKN_0001'),('SKN',209,'SKN_0001'),('SKN',210,'SKN_0001'),('SKN',211,'SKN_0001'),('SKN',212,'SKN_0001'),('SKN',213,'SKN_0001'),('SKN',214,'SKN_0001'),('SKN',215,'SKN_0001'),('SKN',216,'SKN_0001'),('SKN',217,'SKN_0001'),('SKN',218,'SKN_0001'),('SKN',219,'SKN_0001'),('SKN',220,'SKN_0001'),('SKN',221,'SKN_0001'),('SKN',222,'SKN_0001'),('SKN',223,'SKN_0001'),('SKN',224,'SKN_0001'),('SKN',225,'SKN_0001'),('SKN',226,'SKN_0001'),('SKN',227,'SKN_0001');
/*!40000 ALTER TABLE `member_equipment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `member_statistics`
--

DROP TABLE IF EXISTS `member_statistics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `member_statistics` (
  `member_id` bigint NOT NULL COMMENT '사용자 ID',
  `field_code_id` char(1) NOT NULL COMMENT '통계 분야 코드 ID',
  `value` int DEFAULT NULL COMMENT '통계 수치',
  PRIMARY KEY (`member_id`,`field_code_id`),
  KEY `FK_68414414fe1ad7cd3c52a46075a` (`field_code_id`),
  CONSTRAINT `FK_68414414fe1ad7cd3c52a46075a` FOREIGN KEY (`field_code_id`) REFERENCES `common_code` (`common_code_id`),
  CONSTRAINT `FK_9d160ad55384d7bb6a28fd8321a` FOREIGN KEY (`member_id`) REFERENCES `member` (`member_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='사용자 통계';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `member_statistics`
--

LOCK TABLES `member_statistics` WRITE;
/*!40000 ALTER TABLE `member_statistics` DISABLE KEYS */;
/*!40000 ALTER TABLE `member_statistics` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `party_member`
--

DROP TABLE IF EXISTS `party_member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `party_member` (
  `member_id` bigint NOT NULL COMMENT '사용자 ID',
  `adventure_id` bigint NOT NULL COMMENT '모험 ID',
  `is_captain` tinyint NOT NULL COMMENT '파티장 여부',
  PRIMARY KEY (`member_id`,`adventure_id`),
  KEY `FK_c96f6a6e2a954b0bda2e0a6b596` (`adventure_id`),
  CONSTRAINT `FK_ba519459d05923c284c1777b684` FOREIGN KEY (`member_id`) REFERENCES `member` (`member_id`),
  CONSTRAINT `FK_c96f6a6e2a954b0bda2e0a6b596` FOREIGN KEY (`adventure_id`) REFERENCES `adventure` (`adventure_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='파티원';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `party_member`
--

LOCK TABLES `party_member` WRITE;
/*!40000 ALTER TABLE `party_member` DISABLE KEYS */;
/*!40000 ALTER TABLE `party_member` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `party_member_custom`
--

DROP TABLE IF EXISTS `party_member_custom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `party_member_custom` (
  `custom_code_type_id` char(1) NOT NULL COMMENT '커스텀 코드 타입',
  `member_id` bigint NOT NULL COMMENT '사용자 ID',
  `adventure_id` bigint NOT NULL COMMENT '모험 ID',
  `custom_code_id` char(8) DEFAULT NULL COMMENT '공통 코드 ID',
  PRIMARY KEY (`custom_code_type_id`,`member_id`,`adventure_id`),
  KEY `FK_5e60216a50008dcf276b602afad` (`adventure_id`,`member_id`),
  KEY `FK_bb6e04466a61ff05e08b8c4253f` (`custom_code_id`),
  CONSTRAINT `FK_5e60216a50008dcf276b602afad` FOREIGN KEY (`adventure_id`, `member_id`) REFERENCES `party_member` (`adventure_id`, `member_id`),
  CONSTRAINT `FK_9a0ebe8e503d5b5cb957abcc698` FOREIGN KEY (`custom_code_type_id`) REFERENCES `common_code_type` (`common_code_type_id`),
  CONSTRAINT `FK_bb6e04466a61ff05e08b8c4253f` FOREIGN KEY (`custom_code_id`) REFERENCES `common_code` (`common_code_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='파티원 커스텀';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `party_member_custom`
--

LOCK TABLES `party_member_custom` WRITE;
/*!40000 ALTER TABLE `party_member_custom` DISABLE KEYS */;
/*!40000 ALTER TABLE `party_member_custom` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unlock`
--

DROP TABLE IF EXISTS `unlock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `unlock` (
  `unlock_id` bigint NOT NULL AUTO_INCREMENT COMMENT '해금 ID',
  `price` int NOT NULL COMMENT '사용한 젤리',
  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '해금 시간',
  `member_id` bigint NOT NULL COMMENT '사용자 아이디',
  `item_code_id` char(8) NOT NULL COMMENT '공통 코드 ID',
  PRIMARY KEY (`unlock_id`),
  KEY `FK_32c932f6fa613b8f38e4bb73df2` (`member_id`),
  KEY `FK_c5e9219968bfae06e728275c2e8` (`item_code_id`),
  CONSTRAINT `FK_32c932f6fa613b8f38e4bb73df2` FOREIGN KEY (`member_id`) REFERENCES `member` (`member_id`),
  CONSTRAINT `FK_c5e9219968bfae06e728275c2e8` FOREIGN KEY (`item_code_id`) REFERENCES `common_code` (`common_code_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='해금';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unlock`
--

LOCK TABLES `unlock` WRITE;
/*!40000 ALTER TABLE `unlock` DISABLE KEYS */;
/*!40000 ALTER TABLE `unlock` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-03 14:47:01
