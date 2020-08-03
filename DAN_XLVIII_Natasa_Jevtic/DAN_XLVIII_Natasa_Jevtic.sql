IF DB_ID('Pizzeria') IS NULL
    create database Pizzeria;
GO	
use Pizzeria
--Deleting tables and views, if they exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblOrderItem')
	drop table tblOrderItem;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblOrder')
	drop table tblOrder;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblMenu')
	drop table tblMenu;
IF EXISTS(select * FROM sys.views where name = 'vwOrderItem')
	drop view vwOrderItem;
IF EXISTS(select * FROM sys.views where name = 'vwOrder')
	drop view vwOrder;
IF EXISTS(select * FROM sys.views where name = 'vwMenu')
	drop view vwMenu;
GO
--Creating a table with list of pizzas
create table tblMenu(
FoodID int IDENTITY(1,1) PRIMARY KEY,
FoodName varchar(20) NOT NULL,
Price int NOT NULL
);
--Creating a table of orders
create table tblOrder(
OrderID int IDENTITY(1,1) PRIMARY KEY,
DateAndTimeOfOrder smalldatetime NOT NULL,
TotalPrice int NOT NULL,
CustomerJMBG varchar(13) NOT NULL,
OrderStatus varchar(10) NOT NULL,
CONSTRAINT CHK_JMBG CHECK(LEN(CustomerJMBG)=13 AND ISNUMERIC(CustomerJMBG)=1),
);
--Creating a table with list of ordered items
create table tblOrderItem(
ID int identity(1,1) PRIMARY KEY,
FoodID int FOREIGN KEY REFERENCES tblMenu(FoodID) NOT NULL,
Quantity int NOT NULL,
OrderID int FOREIGN KEY REFERENCES tblOrder(OrderID) NOT NULL
);
--Inserting information about menu
insert into tblMenu values
('Margharita',750),
('White Pizza',790),
('Artichoke',860),
('Capricciosa',850),
('Pepperoni',890),
('Hawaii',890),
('Prosciutto',980),
('Coca Cola',160),
('Haineken',240),
('San Pellegrino',210);
--Creating a view of menu
GO
create view vwMenu as
select m.*
from tblMenu m;
--Creating a view of orders item
GO
create view vwOrderItem as
select i.*, m.FoodName, m.Price, o.TotalPrice, o.CustomerJMBG
from tblOrderItem i
INNER JOIN tblMenu m
ON i.FoodID = m.FoodID
INNER JOIN tblOrder o
ON i.OrderID = o.OrderID;
--Creating a view of orders
GO
create view vwOrder as
select *
from tblOrder;