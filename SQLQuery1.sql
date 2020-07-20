Create Database CarCosmeticSalon;
use CarCosmeticSalon;
-- drop table OrdersTemplateOrdersSet;
-- go
-- drop table OrdersTemplateSet;
-- go

-- drop table Users;
-- go
-- drop table Images;
-- go
-- drop table OrdersTemplateImages;
-- go
-- drop table Orders;
-- go

-- drop table OrdersTemplate;
-- go
-- drop table OrdersInformation;
-- go
-- drop table OrdersTemplateInformation;
-- go
-- drop table UserInformations;
-- go
-- drop table SmallImages;
-- go
-- drop table UserImages;
-- go
-- drop table UserType;
-- go

Create Table UserType(
UserTypeId int not null IDENTITY    PRIMARY KEY,
AccessRights int not null, -- 1 temporary user,2 user,3 employee,4 admin
Name nvarchar(255) not null
);

 Create Table Users (
 UserId int NOT NULL IDENTITY    PRIMARY KEY,
 Login nvarchar(254) NOT NULL,
 Password nvarchar(254) NOT NULL,
 Email nvarchar(254) NOT NULL,
 FirstName nvarchar(254) NOT NULL,
 Surname nvarchar(254) NOT NULL,
 PhoneNumber varchar(15) NOT NULL,
 AccoutCreateDate DATETIME NOT NULL,
 UserTypeId int foreign key references UserType(UserTypeID));


Create Table OrdersTemplate(
OrderTemplateId int not null identity primary key,
MaxCost money not null,
MinCost money not null,
Name nvarchar(500) not null,
AdditionalInformation nvarchar(3000),
ExpectedTime time);


Create Table OrdersTemplateSet(
OrderSetId int not null identity primary key,
Name nvarchar(500) not null);

Create Table OrdersTemplateOrdersSet(
OrderTemplateId  int NOT NULL foreign key references OrdersTemplate(OrderTemplateId),
OrderSetId int not null foreign key references OrdersTemplateSet(OrderSetId));




Create Table Orders(
OrderId int NOT NULL IDENTITY    PRIMARY KEY,
UserId  int NOT NULL foreign key references Users(UserId),
CreateOrderUserId  int NOT NULL foreign key references Users(UserId),
OrderTemplateId  int NOT NULL foreign key references OrdersTemplate(OrderTemplateId),
OrderDate DATETIME NOT NULL,
ExpectedStartOfOrder DATETIME NULL,
CompletedOrderDate DATETIME NULL,
StartOfOrder DATETIME NULL,
delays int NULL,
Cost money null,
IsOrderCompleted bit not null,
IsOrderStarted bit not null,
IsPaid bit not null);




Create Table UserInformations(
InformationId int not null identity primary key,
UserId  int foreign key references Users(UserId),
TypeOfInformation nvarchar(254) not null, 
Information nvarchar(max));


Create Table OrdersInformation(
InformationId int not null identity primary key,
OrderId int not null foreign key references Orders(OrderId),
TypeOfInformation nvarchar(254) not null, 
Information nvarchar(max));


Create Table OrdersTemplateInformation(
InformationId int not null identity primary key,
OrderTemplateId int not null foreign key references OrdersTemplate(OrderTemplateId),
TypeOfInformation nvarchar(254) not null, 
Information nvarchar(max));


create table UserImages(
ImageId int not null identity primary key,
UserId int not null foreign key references Users(UserId),
AdditionalInformation nvarchar(254),
Image image)

--bo mo¿e byæ parê image id do templatea

create table OrdersTemplateImages(
ImageId int not null identity primary key,
OrderTemplateId int not null foreign key references OrdersTemplate(OrderTemplateId),
AdditionalInformation nvarchar(254),
Image image)

Create Table Blog(
BlogId int not null identity primary key,
content nvarchar(MAX));

Create Table BlogImages(
ImageId int not null identity primary key,
BlogId int not null foreign key references Blog(BlogId),
Image image);

Create Table SocialMedia(
SocialMediaId int not null identity primary key,
ShortDescripption nvarchar(254),
TypeOfSocialmedia nvarchar(100),
link nvarchar(MAX) not null);

create table DayInfo(
	DayId int not null identity primary key,
	Name nvarchar(200) not null ,
	IsOpen bit not null,
    OpenHour nvarchar(10) null,
    CloseHour nvarchar(10) null);

create table DiffrentDayInfo(
	DiffDayInfo int not null identity primary key,
	DayId int not null foreign key references DayInfo(DayId),
	IsOpen bit not null,
	OpenHour nvarchar(10) null,
	CloseHour nvarchar(10) null,
	ExactChangeDate DATETIME NOT NULL,
	);
Insert into DayInfo(Name,IsOpen,OpenHour,CloseHour) values ('pon',1,'09:30:00', '16:30:00');
Insert into DayInfo(Name,IsOpen,OpenHour,CloseHour) values ('wto',1,'09:30:00', '17:30:00');
Insert into DayInfo(Name,IsOpen,OpenHour,CloseHour) values ('œro',1,'08:00:00', '16:00:00');
Insert into DayInfo(Name,IsOpen,OpenHour,CloseHour) values ('czw',1,'09:00:00', '17:30:00');
Insert into DayInfo(Name,IsOpen,OpenHour,CloseHour) values ('pi¹',1,'09:00:00', '17:00:00');
Insert into DayInfo(Name,IsOpen) values ('sob',0);
Insert into DayInfo(Name,IsOpen) values ('nie',0);


Insert into OrdersTemplate(MaxCost,MinCost,Name,AdditionalInformation,ExpectedTime) values (500,300,'test 1', 'information for test1','20:30:00');
Insert into OrdersTemplate(MaxCost,MinCost,Name,AdditionalInformation,ExpectedTime) values (500,300,'test 2', 'information for test2','00:30:00');
Insert into OrdersTemplate(MaxCost,MinCost,Name,AdditionalInformation,ExpectedTime) values (100,50,'test 3', 'information for test3','00:01:00');

Insert Into UserType(AccessRights,Name) values (1,'Temporary User');
Insert Into UserType(AccessRights,Name) values (2,'Normal User');
Insert Into UserType(AccessRights,Name) values (3,'Employee');
Insert Into UserType(AccessRights,Name) values (4,'Admin');

select * from UserType;

--Insert Into Users(
--Login,Password,Email,FirstName,Surname,PhoneNumber,AccoutCreateDate,UserTypeId) 
--values ('Admin','Admin','p.trzeciak666@gmail.com','Patryk','Trzeciak',
--'790821390',GETDATE(),4);
--Insert Into Users(
--Login,Password,Email,FirstName,Surname,PhoneNumber,AccoutCreateDate,UserTypeId ) 
--values ('User1','User1','p.trzeciak666@gmail.com','Patryk1','Trzeciak1',
--'790821391',GETDATE(),2);
--Insert Into Users(
--Login,Password,Email,FirstName,Surname,PhoneNumber,AccoutCreateDate,UserTypeId ) 
--values ('Employee1','Employee1','p.trzeciak666@gmail.com','Patryk1','Trzeciak1',
--'790821391',GETDATE(),3);
--select * from users;


--Insert Into Orders
--(UserId,CreateOrderUserId,OrderTemplateId,OrderDate,Cost,IsOrderCompleted,IsOrderStarted, IsPaid)
--Values(2,2,3,GETDATE(),100,0,0,0);
--Insert Into Orders
--(UserId,CreateOrderUserId,OrderTemplateId,OrderDate,Cost,IsOrderCompleted,IsOrderStarted,IsPaid)
--Values(3,1,2,GETDATE(),100,0,0,0);
--Insert Into Orders
--(UserId,CreateOrderUserId,OrderTemplateId,OrderDate,Cost,IsOrderCompleted,IsOrderStarted,IsPaid)
--Values(2,1,1,GETDATE(),100,0,0,0);


select * from Orders;

