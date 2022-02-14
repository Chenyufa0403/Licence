use master
go
if exists(select * from sys.databases where name='Test')
drop database Test
go
create database Test
go
use Test
go
create table UserInfo 
(
ID	int	primary key identity(1,1),
UserName	varchar(50)		not null,
Pwd	varchar(50)	not null
) 
go
create table Licence 
(
ID	int primary key identity(1,1),
Name	varchar(50) not null,
IssueDate	datetime	 not null,
VaildDate	datetime,
IsReview	bit default(0),	--0表示不需要，1表示需要   0false  1true
ReviewDate	datetime,
State	varchar(20) not null,
LendUserID	int references UserInfo(ID)
)
GO
insert into UserInfo values('admin','123456'),('aa','123456'),('bb','123456')
insert into Licence values('计算机技术与软件技术资格','2020-06-06','2021-06-06','1','2021-06-06','借出',1)
insert into Licence values('项目管理专业人士认证','2020-06-06',null,'0',null,'借出',2)
insert into Licence values('税务登记证','2020-06-06','2021-06-06','1','2021-06-06','借出',3)
insert into Licence values('AA级信用企业','2020-06-06','2021-06-06','1','2021-06-06','在库',null)

select * from UserInfo
select * from Licence