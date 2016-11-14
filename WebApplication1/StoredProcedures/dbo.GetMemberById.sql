CREATE PROCEDURE [dbo].[GetMemberById]
	@memberId bigint = 0
AS
BEGIN
	SELECT * FROM Members
	WHERE MemberId = @memberId
END
