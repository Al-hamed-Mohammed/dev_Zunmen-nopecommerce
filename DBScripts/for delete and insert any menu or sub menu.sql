
--this script for Deleting any permission or menu or submenu with is not availabble in Access control list in admin menu

If exists (select ID from PermissionRecord where Name ='Admin area. Manage Vendors')
Begin
print 'delete script'
--Delete from PermissionRecord_Role_Mapping
-- where PermissionRecord_Id in (select ID from PermissionRecord where Name ='Admin area. Manage Vendors')
-- And CustomerRole_Id in (select id from CustomerRole where Name='Administrators')
End



--this script for Insert  any permission or menu or submenu with is not availabble in Access control list in admin menu

--Script without merge query

--Insert into PermissionRecord_Role_Mapping ([PermissionRecord_Id],[CustomerRole_Id]) 
--values ((select ID from PermissionRecord where Name ='Admin area. Manage Vendors'),(select id from CustomerRole where Name='Administrators'))



--Script with merge query

--merge into [dbo].PermissionRecord_Role_Mapping as trg
--using
--(
--select
--		 (select ID from PermissionRecord where Name ='Admin area. Manage Vendors') as PermissionRecord_Id
--		 ,(select id from CustomerRole where Name='Administrators') as CustomerRole_Id
		
--FROM  PermissionRecord_Role_Mapping
--) as src
--on trg.PermissionRecord_Id = src.PermissionRecord_Id and  trg.CustomerRole_Id = src.CustomerRole_Id
--when not matched by target then insert (PermissionRecord_Id,CustomerRole_Id)
--Values (src.PermissionRecord_Id,src.CustomerRole_Id);