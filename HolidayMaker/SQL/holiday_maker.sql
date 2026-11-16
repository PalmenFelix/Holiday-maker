-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: Holiday_maker
-- ------------------------------------------------------
-- Server version	8.4.13

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
-- Table structure for table `Booking`
--

/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Booking` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `RoomID` int NOT NULL,
  `CustomerID` int NOT NULL,
  `CheckIn` date NOT NULL,
  `CheckOut` date NOT NULL,
  `People` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Booking_Rooms_FK` (`RoomID`),
  KEY `Booking_Customer_FK` (`CustomerID`),
  CONSTRAINT `Booking_Customer_FK` FOREIGN KEY (`CustomerID`) REFERENCES `Customer` (`ID`),
  CONSTRAINT `Booking_Rooms_FK` FOREIGN KEY (`RoomID`) REFERENCES `Rooms` (`ID`),
  CONSTRAINT `chk_booking_dates` CHECK ((`CheckOut` > `CheckIn`)),
  CONSTRAINT `chk_booking_people` CHECK ((`People` > 0))
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Booking`
--

LOCK TABLES `Booking` WRITE;
/*!40000 ALTER TABLE `Booking` DISABLE KEYS */;
INSERT INTO `Booking` VALUES (1,1,1,'2026-12-01','2026-12-04',1),(3,3,3,'2026-12-02','2026-12-06',4),(4,4,4,'2026-12-10','2026-12-14',2),(5,1,5,'2026-12-08','2026-12-11',1),(6,5,2,'2026-12-01','2026-12-03',1),(7,6,3,'2026-12-04','2026-12-08',2),(8,7,4,'2026-12-10','2026-12-15',4),(9,8,5,'2026-12-12','2026-12-16',2),(10,5,1,'2026-12-15','2026-12-18',1),(11,9,3,'2026-12-01','2026-12-05',1),(12,10,4,'2026-12-03','2026-12-07',2),(13,11,5,'2026-12-08','2026-12-12',4),(14,12,1,'2026-12-15','2026-12-19',4),(15,9,2,'2026-12-10','2026-12-13',1),(16,13,4,'2026-12-02','2026-12-06',1),(17,14,5,'2026-12-05','2026-12-09',1),(18,15,1,'2026-12-10','2026-12-14',4),(19,16,2,'2026-12-15','2026-12-20',2),(20,13,3,'2026-12-12','2026-12-16',1),(21,17,5,'2026-12-01','2026-12-04',1),(22,18,1,'2026-12-05','2026-12-09',2),(23,19,2,'2026-12-10','2026-12-15',4),(24,20,3,'2026-12-16','2026-12-20',2),(25,17,4,'2026-12-08','2026-12-11',1),(26,3,2,'2026-12-12','2026-12-14',3);
/*!40000 ALTER TABLE `Booking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Customer`
--

/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Customer` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PhoneNumber` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Customer`
--

LOCK TABLES `Customer` WRITE;
/*!40000 ALTER TABLE `Customer` DISABLE KEYS */;
INSERT INTO `Customer` VALUES (1,'Ted Mosby','ted@hotmail.com','0701234567'),(2,'Barney Stinson','barney@gmail.com','0702345678'),(3,'Phil Dunphy','phil@gmail.com','0723456789'),(4,'Rachel Green','rachel@live.com','0704567890'),(5,'Michael Scott','michael@icloud.com','0705678901'),(6,'Bert','Bert@live.com','0725689898');
/*!40000 ALTER TABLE `Customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Hotel`
--

/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Hotel` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Location` varchar(100) NOT NULL,
  `Pool` tinyint(1) NOT NULL,
  `Restaurant` tinyint(1) NOT NULL,
  `DistanceBeach` int NOT NULL,
  `DistanceCenter` int NOT NULL,
  `Rating` int NOT NULL,
  PRIMARY KEY (`ID`),
  CONSTRAINT `chk_hotel_beach` CHECK ((`DistanceBeach` >= 0)),
  CONSTRAINT `chk_hotel_center` CHECK ((`DistanceCenter` >= 0)),
  CONSTRAINT `chk_hotel_rating` CHECK ((`Rating` between 1 and 5))
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Hotel`
--

LOCK TABLES `Hotel` WRITE;
/*!40000 ALTER TABLE `Hotel` DISABLE KEYS */;
INSERT INTO `Hotel` VALUES (1,'Seaside Hotel','Barcelona',1,1,200,800,5),(2,'Athens Resort','Athens',1,1,400,1000,3),(3,'Gladiator Palace','Rome',1,1,1000,500,5),(4,'Stockholm Hotel','Stockholm',1,1,2000,300,4),(5,'Buenos Aires Resort','Buenos Aires',1,1,500,500,5);
/*!40000 ALTER TABLE `Hotel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Rooms`
--

/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Rooms` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `HotelID` int NOT NULL,
  `RoomNumber` int NOT NULL,
  `MaxPersons` int NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_hotel_room` (`HotelID`,`RoomNumber`),
  CONSTRAINT `Rooms_Hotel_FK` FOREIGN KEY (`HotelID`) REFERENCES `Hotel` (`ID`),
  CONSTRAINT `chk_room_capacity` CHECK ((`MaxPersons` > 0)),
  CONSTRAINT `chk_room_price` CHECK ((`Price` >= 0))
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Rooms`
--

LOCK TABLES `Rooms` WRITE;
/*!40000 ALTER TABLE `Rooms` DISABLE KEYS */;
INSERT INTO `Rooms` VALUES (1,1,101,1,600),(2,1,102,2,1100),(3,1,103,4,1500),(4,1,104,2,1100),(5,2,101,1,500),(6,2,102,2,800),(7,2,103,4,1300),(8,2,104,2,800),(9,3,101,1,700),(10,3,102,2,1000),(11,3,103,4,1800),(12,3,104,4,1800),(13,4,101,1,800),(14,4,102,1,800),(15,4,103,4,2000),(16,4,104,2,1400),(17,5,101,1,500),(18,5,102,2,900),(19,5,103,4,1400),(20,5,104,2,900);
/*!40000 ALTER TABLE `Rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'Holiday_maker'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-11-16 21:35:00
