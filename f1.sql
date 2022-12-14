-- MySQL dump 10.13  Distrib 8.0.31, for Linux (x86_64)
--
-- Host: imysko.ru    Database: F1_cars
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Cars`
--

DROP TABLE IF EXISTS `Cars`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Cars` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `information` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `team_id` int DEFAULT NULL,
  `image_url` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`id`),
  KEY `team_id` (`team_id`),
  CONSTRAINT `Cars_team_id` FOREIGN KEY (`team_id`) REFERENCES `Teams` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1014 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Cars`
--

LOCK TABLES `Cars` WRITE;
/*!40000 ALTER TABLE `Cars` DISABLE KEYS */;
INSERT INTO `Cars` VALUES (2,'Renault R25','болид Формулы - 1, представленным Renault в сезоне 2005 года. На протяжении нескольих лет Ferrari совместно с Михаэлем Шумахером доминировали в Формуле-1. Это был последний сезон, когда команды использовали трехлитровые моторы V10, но ихменения в регламенте сильно коснулись аэродинамики, это и помогло Renoult отыграть все то преимущество, которое Ferrari накопила за предыдущие сезоны. Как итог Ferrari не могла бороться даже за победы, а главным соперником Алонсо был Кими Райкконен, выступавший на болиде McLaren MP4-20. В данном сезоне Фернандо Алонсо смог завоевать свой первый титул чемпиона мира и победить, как на то время казалось, непобедимый тандем Ferrari и Михаэля Шумахера.',2,'/media/r25.jpeg',NULL),(3,'RB16B','гоночный болид Формулы-1, разработанный и построенный Red Bull Racing для участия в чемпионате мира Формулы - 1 2021 года. Данный болид мало чем отличается от предшественника 2020 года, так как всего лишь был адаптирован под изменения в регламенте на 2021 год. Он были оснащены силовым агрегатом Honda. RB16B стал первым автомобилем, не принадлежащим Mercedes, выигравшим любой чемпионат с 2013 года, а также Honda стала первым японским мотористом - чемпионом мира в эпоху турбогибридов. Ещё в начале сезона Honda заявила что это последний сезон в котором она будет выступать поставщиком двигателей и им было необходимо уйти \"громко хлопнув дверью\". Так и вышло, Макс Ферстаппен на болиде RB16B завоевал свой первый титул чемпиона мира, и прервал доминирование Mercedes, которые на протяжении 7 лет побеждали как в личном зачёте, так и в кубке конструкторов.',3,'/media/rb-16b.jpg',NULL),(4,'RB18','Drivers\' Championships	Max Verstappen, 2022',3,'/media/rb-18.jpg',NULL),(1008,'W11','Drivers\' Championships	Lewis Hamilton, 2020',4,'/media/638064864708640180.png',NULL),(1010,'W10','Drivers\' Championships	Lewis Hamilton, 2019',4,'/media/638065744461637423.png',NULL),(1012,'McLaren MP4/4','один из самых успешных гоночных автомобилей в истории Формулы-1. Был разработан командой McLaren под руководством конструкторов Гордона Марри и Стива Николза. До сезона 1988 года самым доминирующим автомобилем, участвовавшим в гонках Формулы-1, был Lotus 79, однако успехи MP4/4 превзошли все результаты, показанные до этого. Сезон стал лёгкой прогулкой для команды McLaren: команда выиграла 15 из 16 гонок, завоевав 10 дублей и 25 подиумов из 32. Доминирование прервалось только однажды, в Монце, когда лидировавший Сенна за полтора круга до финиша попал в аварию, обгоняя отстававшего на круг Жана-Луи Шлессера, а Прост сошёл после редкого в том году отказа двигателя Honda. В конце сезона McLaren легко выиграла Кубок Конструкторов, а Сенна стал чемпионом.',1,'/media/mp4-4.jpg',NULL);
/*!40000 ALTER TABLE `Cars` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Roles`
--

DROP TABLE IF EXISTS `Roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Roles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `role` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Roles`
--

LOCK TABLES `Roles` WRITE;
/*!40000 ALTER TABLE `Roles` DISABLE KEYS */;
INSERT INTO `Roles` VALUES (1,'User'),(2,'Editor'),(3,'Superuser');
/*!40000 ALTER TABLE `Roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Sessions`
--

DROP TABLE IF EXISTS `Sessions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Sessions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `Sessions_Users_null_fk` (`user_id`),
  CONSTRAINT `Sessions_Users_null_fk` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1670986458 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Sessions`
--

LOCK TABLES `Sessions` WRITE;
/*!40000 ALTER TABLE `Sessions` DISABLE KEYS */;
INSERT INTO `Sessions` VALUES (1670981505,4);
/*!40000 ALTER TABLE `Sessions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Teams`
--

DROP TABLE IF EXISTS `Teams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Teams` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(70) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `url` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Teams`
--

LOCK TABLES `Teams` WRITE;
/*!40000 ALTER TABLE `Teams` DISABLE KEYS */;
INSERT INTO `Teams` VALUES (1,'McLaren','mclaren'),(2,'Renault','renault'),(3,'Oracle Red Bull Racing','red-bull-racing'),(4,'Mercedes','mercedes');
/*!40000 ALTER TABLE `Teams` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` text NOT NULL,
  `password_hash` text NOT NULL,
  `password_key` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (4,'ilia','1/iC0QmJtE00DE+gkv5DqSynJfDRVBuuAHYJNdlcSI2tFnvNKlnxPgqSB7WclNxO1M4SRsfn2HDpkoj3kIYAqA==','/gzm4L4oDjN0EQCix4tpD1xUYlIqZSSCNlxyXyXetw5D9Vki0ddKMwxHostS+jedbYSfB9EebCtRkhDI50waWXwgO3DOJQM73SdElVACD2LsUcSAYRwDwAdan0dQ+rbWS0Aj3ZCYbyQyCKwWvHmhQvJsnOwKenuwnuilQm7kc30='),(5,'user','CaFWoBRwMrbtkQirMjll63/9qQ9atyJcgRm4c7G+dviWgJLeTwFPofbN9csiWOAHZrz1A/cXB580rwj5IIaWyA==','UNLVHWDaKnmCarBv5mTJyXLO5qm9nxmS6vTNp40F0DDQ2h0mEOXutFIReptRL7oWclIeuX2/6vjIFbtKplwu8UpB1OrmTx7DUhoPGyvL2jX3nQSgT3WKWVPMDhYAeAMwvHBlK3sf/bCf9+jhufx+IQfLAClP9VXmWbeyCGOZVXs='),(9,'slava','HXDVE9zCY7FUV6bpWYP68QZ5jErXMvi+lfvTCsPFiiXjEwsZf+OzsePeqYtB+/EvrfHjyrqLePnuHJlJY04oYg==','/IgHPb3bqrShORCZI17ZNXByctd+BJBW5que+eOQ/kyZnJIe1mu8Z7gF9pdV1uvljoWrXq2wYbV3lQ732fYTfSuVmpMILaea4Tnuez6VbGTyMYuib5HduF9oO8AJiS2Y+DsQqt9OnsqG8U1VrIKmZgbLrKTfrwdZdNhZZOXN8Cg='),(10,'prod','ZFhIXul+vHcuUPsusCLQtvBTchUGasMyAtO0MQurhaL8BBXUVy7U3KMEXunxZI8cV82BdOVlGZTj19pOugE+1w==','+ua6fM9iqYwCM96z9V/RdiBCruM9vlJEpiDDT+/3j6n4nUgqxDWxos3pI0CvFDHci1nBCUObK8Xdab3Tur7He/8idt1RxU084k/DcDRoZR9evAOlbs/PNV5huPHHB5BTqNtl3DRxUQLymUdXaFdNPMhqNzLlKzeh38imtMrUwgM=');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users_Roles`
--

DROP TABLE IF EXISTS `Users_Roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users_Roles` (
  `user_id` int DEFAULT NULL,
  `role_id` int DEFAULT NULL,
  KEY `user_id` (`user_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `role_id` FOREIGN KEY (`role_id`) REFERENCES `Roles` (`id`),
  CONSTRAINT `user_id` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users_Roles`
--

LOCK TABLES `Users_Roles` WRITE;
/*!40000 ALTER TABLE `Users_Roles` DISABLE KEYS */;
INSERT INTO `Users_Roles` VALUES (4,3),(5,1),(5,2),(9,1),(10,1);
/*!40000 ALTER TABLE `Users_Roles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-14 11:52:41
