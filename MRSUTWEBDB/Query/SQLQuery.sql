/*
use MVCMRSUTWEBDB

create table RoleMaster
(
	RoleId int identity(9001,1) primary key,
	RoleDescription varchar(20),
	CreatedOn DateTime 
)
insert into RoleMaster(RoleDescription, CreatedOn)values
('Admin',GETDATE()),
('User',GETDATE()),
('Artist',GETDATE())

select * from RoleMaster

insert into  UserMaster(UserId, FirstName, LastName, UserName, EmailAddress, Password, isActive, Role, LoginIp)values
('RANDOM','1','1','admin','admin@gmail.com','123','1','9001','0.0.0.0'),
('RANDOM','1','1','user','user@gmail.com','123','1','9002','0.0.0.0'),
('RANDOM','1','1','Artist','artist@gmail.com','123','1','9003','0.0.0.0')

create table UserMaster
(
	UId int identity primary key,
	UserId varchar(50),
	FirstName varchar(50),
	LastName varchar(50),
	UserName varchar(50),
	EmailAddress varchar(50),
	Password varchar(50),
	isActive bit,
	CreatedOn DateTime default getdate(),
)


ALTER TABLE UserMaster
ADD Role int REFERENCES RoleMaster(RoleId);

select * from UserMaster
select * from RoleMaster

*/