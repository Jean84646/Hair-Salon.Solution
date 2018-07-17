# Hair Salon
##### A Hair Salon Application

#### By Jean Jia, 07.13.2018

## Description

A database application to hold a list of stylists and their clients.

## Setup

Install Hair Salon by downloading the folder.

## Technologies Used

Application: CSharp, netcoreapp1.1, Razor, MAMP, MySQL

## Support and Contact

For any questions, concerns, or support details, please email:
jean84646@gmail.com

## Spec

* Create a database to hold list of stylists, stylist's description, and their clients.
* Allow user to select a stylist to view the stylist's description and a list of all clients that belong to that stylist.
* Allow user to add new stylists.
* Allow user to add new clients to specific stylist.
* User should not be able to add a client if no stylists have been added.

## Setup Database
Using MySQL command:
* CREATE DATABASE jean_jia;
* USE jean_jia;
* CREATE TABLE stylists (name VARCHAR (255), description TEXT, id serial PRIMARY KEY);
* CREATE TABLE clients (client VARCHAR (255), stylist_id VARCHAR (255));

### Legal

Copyright (c) 2018 **Jean Jia**

This software is licensed under the MIT license.
