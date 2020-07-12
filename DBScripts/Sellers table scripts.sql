--Creating the Sellars profile table to store information about the sellers
--This table will contain CustomerID as foreign key and it would be unique as well
--All Customers are in Sellars table are sellers and if they login from sallers login they will be
--On Sellers Dashboard and if they login from sites panel then they will be customers by this we can manage
--B to C functionality as well
--We Are using all functionality of existing customer just store sellers info in sellers table.

--Existing Tables
--Customers			-- MainTable
--Address			-- Customer Profile Table where all customers profile data will store 
					-- like firstname, lastname, city, state, country, zipcode etc.
--CustomerAddresses -- Relation of Customer and Address table mapped here

-- For sellers we dont need to do anything with roles we are doing this by functionality.

-- Date 25 Apr - Deleting seller table and all updateds related to sellers we are using Venders instead of sellers


ALter table Sellers 
drop Constraint Fk_customerid
go
Alter table Product
drop Constraint Fk_Customer_CreatedBy


if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='Sellers') 
begin

drop table Sellers

end;

-- Sellers end here
go

-- Creating Alter table for products
 if exists (select 1 from [sys].[columns] c where object_id = object_id(N'[dbo].[Product]') and c.name =N'CreatedBy' )
 BEGIN
	alter table Product
	drop column CreatedBy
 END

go

-- End alter table

-- Foreign Keys of tables
--ALter table Sellers 
--Add Constraint Fk_customerid
--Foreign key(customerid)
--references Customer(ID)

-- Add Foreign key to product table

-- IF you have created constraint with Fk_Customer that was in my last push
-- so please delete that constraint by query drop constrint Fk_Customer, with alter table syntax 
-- and once Fk_Customer is deleted you can run this script and create new constraint 

--Alter table Product
--Add Constraint Fk_Customer_CreatedBy
--Foreign key (CreatedBy)
--references Customer(ID)
