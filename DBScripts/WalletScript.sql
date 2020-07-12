if not exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='Wallet') 
begin

Create table Wallet
(
	ID int primary key identity(1,1),
	Amount decimal,
	DateCreated datetime,
	DateModified datetime null,
	CustomerID int,
	IsActive bit
)

end;

go

if not exists (select 1 from [sys].[columns] c where object_id = object_id(N'[dbo].[Order]') and c.name =N'WalletID' )
 BEGIN
	alter table [dbo].[Order]
	add WalletID int null
 END

go

Alter Table [dbo].[Order]
Add Constraint FK_Order_WalletID
Foreign Key(WalletID)
References Wallet(ID)

go

alter table Wallet
Add Constraint FK_Wallet_CustomerID
Foreign Key(CustomerID)
References Customer(ID)

go

if not exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='WalletDetail') 
begin

Create Table WalletDetail
(
	Id int primary key identity(1,1),
	WalletID int,
	Description nvarchar(1000),
	DateCreated datetime,
	Amount decimal(10,4),
	AmountAdded bit,
	AmountDeducted bit,
	IsActive bit
)

end;

go

Alter Table WalletDetail
Add Constraint FK_WalletDetails_WalletID
Foreign Key(WalletID)
References Wallet(Id)

