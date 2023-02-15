CREATE PROCEDURE [dbo].[GetProducts] 
AS
BEGIN
	SELECT 
        [InstanceId]
        ,[Name]
        ,[Description]
        ,[ProductImageUris]
        ,[ValidSkus]
        ,[CreatedTimestamp]  from [Instances].[Products]
END

	 
