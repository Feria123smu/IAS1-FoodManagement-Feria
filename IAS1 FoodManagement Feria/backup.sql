-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: dbs_lab1
-- ------------------------------------------------------
-- Server version	8.0.42

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
-- Current Database: `dbs_lab1`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `dbs_lab1` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `dbs_lab1`;

--
-- Table structure for table `audit_tbl`
--

DROP TABLE IF EXISTS `audit_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `audit_tbl` (
  `userid` int DEFAULT NULL,
  `username` varchar(45) DEFAULT NULL,
  `role` varchar(45) DEFAULT NULL,
  `logDate` datetime DEFAULT NULL,
  `auditActivity` varchar(45) DEFAULT NULL,
  `details` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `audit_tbl`
--

LOCK TABLES `audit_tbl` WRITE;
/*!40000 ALTER TABLE `audit_tbl` DISABLE KEYS */;
INSERT INTO `audit_tbl` VALUES (-1,'','','2025-12-04 21:46:10','New receipt','Receipt \n[payment: \"831\",\npay_change: \"1\",\ncustomerName: \"Try lang\",\ndeliver address: \"Palasyo ni San Pedro Jr.\"\nItems:\n[\n        receipt_id: \"26\"\n    food_item: \"BaconCheese\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"26\"\n    food_item: \"Barbecue\"\n    quantity: \"1\"\n]\n'),(-1,'','','2025-12-04 21:49:38','New receipt','Receipt \n[payment: \"5000\",\npay_change: \"3850\",\ncustomerName: \"gh\",\ndeliver address: \"gfh6\"\nItems:\n[\n        receipt_id: \"27\"\n    food_item: \"Barbecue\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"27\"\n    food_item: \"CookiesCream\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"27\"\n    food_item: \"ChocolateHeaven\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"27\"\n    food_item: \"BaconCheese\"\n    quantity: \"1\"\n]\n'),(-1,'','','2025-12-04 22:32:00','New receipt','Receipt \n[payment: \"2000\",\npay_change: \"850\",\ncustomerName: \"dfh\",\ndeliver address: \"sgh\"\nItems:\n[\n        receipt_id: \"25\"\n    food_item: \"Barbecue\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"25\"\n    food_item: \"BaconCheese\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"25\"\n    food_item: \"ChocolateHeaven\"\n    quantity: \"1\"\n]\n[\n        receipt_id: \"25\"\n    food_item: \"CookiesCream\"\n    quantity: \"1\"\n]\n'),(-1,'','','2025-12-04 23:27:27','Receipt deleted successfully','id=\"-1\"'),(-1,'','','2025-12-04 23:27:45','Receipt deleted successfully','id=\"2\"'),(-1,'','','2025-12-04 23:28:27','Receipt deleted successfully','id=\"8\"'),(-1,'','','2025-12-04 23:28:36','Receipt deleted successfully','id=\"16\"');
/*!40000 ALTER TABLE `audit_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receipt_item_tbl`
--

DROP TABLE IF EXISTS `receipt_item_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receipt_item_tbl` (
  `receipt_id` int DEFAULT NULL,
  `food_item` varchar(100) DEFAULT NULL,
  `quantity` decimal(10,0) DEFAULT NULL,
  KEY `receipt_id` (`receipt_id`),
  CONSTRAINT `receipt_item_tbl_ibfk_1` FOREIGN KEY (`receipt_id`) REFERENCES `receipts_tbl` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receipt_item_tbl`
--

LOCK TABLES `receipt_item_tbl` WRITE;
/*!40000 ALTER TABLE `receipt_item_tbl` DISABLE KEYS */;
INSERT INTO `receipt_item_tbl` VALUES (16,'Barbecue',1),(16,'All Meaty',1),(18,'BaconCheese',1),(18,'Barbecue',2),(18,'StrawberryCheesecake',1),(18,'CookiesCream',1),(19,'BaconCheese',1),(19,'CookiesCream',2),(19,'Barbecue',1),(19,'All Meaty',1),(20,'BaconCheese',1),(20,'CookiesCream',2),(20,'Barbecue',1),(21,'ChocolateHeaven',1),(21,'BaconCheese',1),(21,'CookiesCream',1),(22,'ChocolateHeaven',1),(22,'BaconCheese',1),(22,'CookiesCream',2),(23,'All Meaty',1),(23,'StrawberryCheesecake',1),(24,'All Meaty',1),(24,'StrawberryCheesecake',1);
/*!40000 ALTER TABLE `receipt_item_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receipts_tbl`
--

DROP TABLE IF EXISTS `receipts_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receipts_tbl` (
  `payment` decimal(10,0) DEFAULT NULL,
  `pay_change` decimal(10,0) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `customerName` varchar(100) DEFAULT NULL,
  `deliveryAddress` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receipts_tbl`
--

LOCK TABLES `receipts_tbl` WRITE;
/*!40000 ALTER TABLE `receipts_tbl` DISABLE KEYS */;
INSERT INTO `receipts_tbl` VALUES (15000000,15000,1,'Richboy','Solano Mansion'),(15000000,15000,2,'Richboy','Solano Mansion'),(15000000,15000,3,'Richboy','Solano Mansion'),(120000,118500,7,'Please','Tae'),(120000,118820,8,'ff','dff'),(120000,118650,9,'s','s'),(120000,119400,10,'s','s'),(120000,119250,11,'s','s'),(120000,119230,12,'u','u'),(120000,119400,13,'ss','66'),(120000,119420,14,'y','42'),(120000,119020,15,'p','pp'),(120000,119150,16,'rr','1r'),(120000,118400,17,'TTe','fefe'),(120000,118460,18,'sfa','ssf'),(120000,118420,19,'Jejegirl','CR na mabantot'),(120000,118870,20,'nababaliw sa yo','langit ng mga tanga'),(120000,119250,21,'kulangot boy','sipon river'),(120000,119100,22,'dirty taong grasa','kabkab rain land ni Austine'),(700,90,23,'ostin','hsjhfjsd'),(700,90,24,'ostin','hsjhfjsd');
/*!40000 ALTER TABLE `receipts_tbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_user`
--

DROP TABLE IF EXISTS `tbl_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` longtext NOT NULL,
  `role` enum('Admin','Staff','User') NOT NULL DEFAULT 'User',
  `status` enum('Active','Pending','Inactive') NOT NULL DEFAULT 'Pending',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_user`
--

LOCK TABLES `tbl_user` WRITE;
/*!40000 ALTER TABLE `tbl_user` DISABLE KEYS */;
INSERT INTO `tbl_user` VALUES (5,'jose','jose1234','Admin','Pending'),(6,'pedro','pedro1234','User','Active'),(7,'prily','hahahahshshshshshshs','User','Inactive'),(9,'admin','9cdca6289d90c1b87395bfcb2a07e1b407710d11141d6c5080fbdfba5360cdff','Admin','Active'),(10,'sssd','1153268148783bc42dba5e62ee638485','User','Pending'),(11,'dd','9306dd2bd7a22ee6691624490d78f866','User','Pending'),(12,'fffas','4f6be48299762969396d8def780070b1','User','Pending'),(22,'reggreer','9306dd2bd7a22ee6691624490d78f866','User','Pending');
/*!40000 ALTER TABLE `tbl_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-12-05  1:09:17
