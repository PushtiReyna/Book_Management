CREATE DATABASE BookManagementDB

CREATE TABLE BookMst(
	BookId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BookName [Varchar](100) NOT NULL,
	CategoryId Int Not Null,
	SubcategoryId Int Not Null,
	AuthorName  varchar(100) Not Null,
	BookPages Int NOT NULL,
	Publisher  varchar(100) NOT NULL,
	PublishDate datetime NOT NULL,
	Edition  varchar(100) NOT NULL,
	Description nvarchar(300) NOT NULL,
	Price nvarchar(300) NOT NULL,
	CoverImagePath nvarchar(200) Not Null,
	PdfPath nvarchar(200) Not Null,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
)

CREATE TABLE CategoryMst(
	CategoryId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CategoryName [Varchar](100) NOT NULL,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
)

	CREATE TABLE SubcategoryMst(
	SubcategoryId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	SubcategoryName [Varchar](100) NOT NULL,
	CategoryId Int Not Null,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
	)

	CREATE TABLE UserMst(
	UserId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FullName varchar(200) NOT NULL,
	Email varchar(300) Not Null,
	UserName varchar(100) Not Null,
	Address nvarchar(300) not null,
	ContactNumber nvarchar(50) not null,
	Password nvarchar(100) not null,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
	)

	Insert into UserMst values('ABC','abc@gmail.com','ABC','Vadodara','1234567890','ABC','1','0','1','2023-11-06 11:33:59.127','0','2023-11-06 11:33:59.127')