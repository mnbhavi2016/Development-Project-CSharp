CREATE PROCEDURE [dbo].[usp_SearchProducts]
	@name varchar(256) 
AS
BEGIN
	SELECT 
        [InstanceId]
        ,[Name]
        ,[Description]
        ,[ProductImageUris]
        ,[ValidSkus]
        ,[CreatedTimestamp]  from [Instances].[Products] where [Name] = @name
           
END

 



