SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `zera_levi` ;
CREATE SCHEMA IF NOT EXISTS `zera_levi` DEFAULT CHARACTER SET utf8 ;
USE `zera_levi` ;

-- -----------------------------------------------------
-- Table `zera_levi`.`t_people`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_people` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_people` (
  `_id` INT NOT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `given_name` VARCHAR(45) NULL DEFAULT NULL,
  `family_name` VARCHAR(45) NULL DEFAULT NULL,
  `address` VARCHAR(300) NULL DEFAULT NULL,
  `member` TINYINT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_yahrtziehts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_yahrtziehts` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_yahrtziehts` (
  `_id` INT NOT NULL,
  `person_id` INT NOT NULL,
  `relation` VARCHAR(45) NULL DEFAULT NULL,
  `date` DATETIME NOT NULL,
  `deceaseds_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`_id`),
  INDEX `fk_yahrtziehts_people1_idx` (`person_id` ASC),
  CONSTRAINT `fk_yahrtziehts_people1`
    FOREIGN KEY (`person_id`)
    REFERENCES `zera_levi`.`t_people` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_accounts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_accounts` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_accounts` (
  `_id` INT NOT NULL,
  `person_id` INT NOT NULL,
  `monthly_total` INT NULL DEFAULT NULL,
  `last_month_paid` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`_id`),
  INDEX `fk_accounts_people1_idx` (`person_id` ASC),
  CONSTRAINT `fk_accounts_people1`
    FOREIGN KEY (`person_id`)
    REFERENCES `zera_levi`.`t_people` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_donations`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_donations` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_donations` (
  `_id` INT NOT NULL,
  `account_id` INT NOT NULL,
  `reason` VARCHAR(100) NOT NULL,
  `amount` DOUBLE NOT NULL,
  `date_donated` DATETIME NOT NULL,
  `date_paid` DATETIME NULL DEFAULT NULL,
  `paid` TINYINT(1) NOT NULL,
  `comments` VARCHAR(300) NULL DEFAULT NULL,
  PRIMARY KEY (`_id`),
  INDEX `fk_donations_accounts_idx` (`account_id` ASC),
  CONSTRAINT `fk_donations_accounts`
    FOREIGN KEY (`account_id`)
    REFERENCES `zera_levi`.`t_accounts` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_phone_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_phone_types` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_phone_types` (
  `_id` INT NOT NULL,
  `type_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_phone_numbers`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_phone_numbers` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_phone_numbers` (
  `person_id` INT NOT NULL,
  `number` VARCHAR(45) NOT NULL,
  `number_type` INT NOT NULL,
  `_id` INT NOT NULL,
  INDEX `fk_phone_numbers_people1_idx` (`person_id` ASC),
  INDEX `fk_phone_numbers_phone_types1_idx` (`number_type` ASC),
  PRIMARY KEY (`_id`),
  CONSTRAINT `fk_phone_numbers_people1`
    FOREIGN KEY (`person_id`)
    REFERENCES `zera_levi`.`t_people` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_phone_numbers_phone_types1`
    FOREIGN KEY (`number_type`)
    REFERENCES `zera_levi`.`t_phone_types` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_privilege_groups`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_privilege_groups` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_privilege_groups` (
  `_id` INT NOT NULL,
  `group_name` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_users` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_users` (
  `_id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `privileges_group` INT NOT NULL,
  PRIMARY KEY (`_id`),
  INDEX `fk_users_privilege_groups1_idx` (`privileges_group` ASC),
  CONSTRAINT `fk_users_privilege_groups1`
    FOREIGN KEY (`privileges_group`)
    REFERENCES `zera_levi`.`t_privilege_groups` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_privileges`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_privileges` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_privileges` (
  `_id` INT(11) NOT NULL,
  `privilege_name` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `zera_levi`.`t_privileges_per_group`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `zera_levi`.`t_privileges_per_group` ;

CREATE TABLE IF NOT EXISTS `zera_levi`.`t_privileges_per_group` (
  `group_id` INT NOT NULL,
  `privilege_id` INT NOT NULL,
  INDEX `group_connection_idx` (`group_id` ASC),
  INDEX `privilege_connection_idx` (`privilege_id` ASC),
  PRIMARY KEY (`group_id`, `privilege_id`),
  CONSTRAINT `group_connection`
    FOREIGN KEY (`group_id`)
    REFERENCES `zera_levi`.`t_privilege_groups` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `privilege_connection`
    FOREIGN KEY (`privilege_id`)
    REFERENCES `zera_levi`.`t_privileges` (`_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

USE `zera_levi` ;

-- -----------------------------------------------------
-- procedure clear_database
-- -----------------------------------------------------

USE `zera_levi`;
DROP procedure IF EXISTS `zera_levi`.`clear_database`;

DELIMITER $$
USE `zera_levi`$$
CREATE PROCEDURE `clear_database` ()
BEGIN
SET FOREIGN_KEY_CHECKS=0;
TRUNCATE TABLE t_yahrtziehts;
TRUNCATE TABLE t_donations;
TRUNCATE TABLE t_accounts;
TRUNCATE TABLE t_phone_numbers;
TRUNCATE TABLE t_phone_types;
TRUNCATE TABLE t_people;
TRUNCATE TABLE t_privileges_per_group;
TRUNCATE TABLE t_privilege_groups;
TRUNCATE TABLE t_privileges;
TRUNCATE TABLE t_users;
SET FOREIGN_KEY_CHECKS=1;
END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
