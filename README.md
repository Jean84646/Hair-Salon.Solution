# Hair Salon
##### A Hair Salon Application

#### By Jean Jia, 07.20.2018

## Description

A database application to hold a list of stylists, stylist's specialties, and clients list.

## Setup

Install Hair Salon by downloading the folder.

## Technologies Used

Application: CSharp, netcoreapp1.1, Razor, MAMP, MySQL

## Support and Contact

For any questions, concerns, or support details, please email:
jean84646@gmail.com

## Spec

* The user is able to see a list of all our stylists.
* The User is able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* The User is able to add new stylists to our system when they are hired.
* The User is able to edit JUST the name of a stylist.
* The User is able to delete stylists (all and single).

* The User is able to add new clients to a specific stylist. User should not be able to add a client if no stylists have been added.
* The User is able to view clients (all and single).
* The User is able to edit ALL of the information for a client.
* The User is able to delete clients (all and single).

* The User is able to add a specialty and view all specialties that have been added.
* The User is able to add a specialty to a stylist.
* The User is able to click on a specialty and see all of the stylists that have that specialty.
* The User is to see the stylist's specialties on the stylist's details page.
* The User is able to add a stylist to a specialty.

## Setup Database
Using MySQL command:
* CREATE DATABASE jean_jia;
* USE jean_jia;
* CREATE TABLE `jean_jia`.`stylists` ( `id` INT NOT NULL AUTO_INCREMENT , `stylist_name` VARCHAR(255) NOT NULL , `stylist_description` TEXT NULL , PRIMARY KEY (`id `)) ENGINE = InnoDB;
* CREATE TABLE `jean_jia`.`clients` ( `id` INT NOT NULL AUTO_INCREMENT , `client_name` VARCHAR(255) NOT NULL , `stylist_id` INT NOT NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;
* CREATE TABLE `jean_jia`.`specialties` ( `id` INT NOT NULL AUTO_INCREMENT , `specialties` VARCHAR(255) NOT NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;
* CREATE TABLE `jean_jia`.`stylist_specialties` ( `id` INT NOT NULL AUTO_INCREMENT , `stylist_id` INT NOT NULL , `specialties_id` INT NOT NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;

### Legal

Copyright (c) 2018 **Jean Jia**

This software is licensed under the MIT license.
