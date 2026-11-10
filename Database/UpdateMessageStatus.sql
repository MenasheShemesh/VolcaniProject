ALTER PROCEDURE [dbo].[UpdateMessageStatus] 
	  @RowsAffected int OUTPUT,
	  @Id bigint,
	  @Status int
AS
BEGIN
	UPDATE dbo.MESSAGES 
	SET MESSAGE_STATUS = @Status,
		MESSAGE_UPDATE_TIME = CURRENT_TIMESTAMP
    WHERE MESSAGE_ID = @Id;
    SET @RowsAffected = @@ROWCOUNT;
END