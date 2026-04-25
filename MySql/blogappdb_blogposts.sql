-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: blogappdb
-- ------------------------------------------------------
-- Server version	8.0.38

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
-- Table structure for table `blogposts`
--

DROP TABLE IF EXISTS `blogposts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `blogposts` (
  `Id` char(36) NOT NULL,
  `Title` varchar(200) NOT NULL,
  `Slug` varchar(220) NOT NULL,
  `Excerpt` varchar(500) DEFAULT NULL,
  `Content` longtext,
  `ImageUrl` varchar(500) DEFAULT NULL,
  `PublishedAt` datetime DEFAULT NULL,
  `CreatedAt` datetime NOT NULL,
  `UpdatedAt` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UX_BlogPosts_Slug` (`Slug`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blogposts`
--

LOCK TABLES `blogposts` WRITE;
/*!40000 ALTER TABLE `blogposts` DISABLE KEYS */;
INSERT INTO `blogposts` VALUES ('5842b4e9-c512-4970-9667-d393ba6a18f2','Test2 Post','test2-post','Test 2 Post For check','Test 2  Post checking using React','/uploads/907f2968ca31fc6365492c5d7c464b68.jpg','2026-04-25 10:58:39','2026-04-25 10:58:39','2026-04-25 10:58:39'),('e3969a2b-6e95-4ee2-b4dc-f7ae0b45337e','Test Post','test-post','Testing for Blog Project','Testing comment it showing on UI Or not','/uploads/02a2a8d8239d16998e556353fe72fc10.jpg','2026-04-25 10:41:17','2026-04-25 10:41:17','2026-04-25 10:49:33');
/*!40000 ALTER TABLE `blogposts` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-04-25 17:53:00
