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
IsReview	bit default(0),	--0��ʾ����Ҫ��1��ʾ��Ҫ   0false  1true
ReviewDate	datetime,
State	varchar(20) not null,
LendUserID	int references UserInfo(ID)
)
GO
insert into UserInfo values('admin','123456'),('aa','123456'),('bb','123456')
insert into Licence values('�������������������ʸ�','2020-06-06','2021-06-06','1','2021-06-06','���',1)
insert into Licence values('��Ŀ����רҵ��ʿ��֤','2020-06-06',null,'0',null,'���',2)
insert into Licence values('˰��Ǽ�֤','2020-06-06','2021-06-06','1','2021-06-06','���',3)
insert into Licence values('AA��������ҵ','2020-06-06','2021-06-06','1','2021-06-06','�ڿ�',null)

select * from UserInfo
select * from Licence