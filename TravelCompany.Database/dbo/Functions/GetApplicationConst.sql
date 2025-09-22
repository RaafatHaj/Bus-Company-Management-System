CREATE FUNCTION GetApplicationConst(@Name NVARCHAR(100))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Value NVARCHAR(MAX);
    SELECT @Value = a.Value FROM ApplicationConsts a WHERE a.Name = @Name;
    RETURN @Value;
END