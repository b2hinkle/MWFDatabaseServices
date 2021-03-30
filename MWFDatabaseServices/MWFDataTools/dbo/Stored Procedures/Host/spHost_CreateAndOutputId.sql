CREATE PROCEDURE [dbo].[spHost_CreateAndOutputId]
	@inHost udtHost READONLY,
	@outId int OUT	--Our out parameter so we can keep track of it in C# for when we want to destroy it
AS
BEGIN
	SET NOCOUNT ON;		--Don't give how many rows affected

	INSERT INTO tblHost([HostIp], [HostServicesAPISocketAddress], [IsActive])
	SELECT [HostIp], [HostServicesAPISocketAddress], [IsActive] FROM @inHost;
	SELECT @outId = SCOPE_IDENTITY();	--SCOPE_IDENTITY() returns the most recent modified Id within the scope of this procedure (last identity created in the same session and the same scope)
END
RETURN 0
