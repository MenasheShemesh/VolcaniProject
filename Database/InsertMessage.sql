ALTER PROCEDURE [dbo].[InsertMessage]
	  @RowsAffected int output,
	  @FirstName nvarchar(50),
      @LastName nvarchar(50),
      @City nvarchar(50),
      @Street nvarchar(50),
      @HouseNumber int,
      @PhonePrefix nvarchar(3),
      @PhoneNumber nvarchar(7),
      @Content nvarchar(250),
      @Id bigint output
AS
BEGIN
DECLARE @CUR_WORKER_ID bigint;
   SET @CUR_WORKER_ID = NULL;
   
   SELECT @CUR_WORKER_ID = w.WORKER_ID
   FROM    dbo.WORKERS w 
   WHERE   w.WORKER_FIRST_NAME = @FirstName
   AND     w.WORKER_LAST_NAME = @LastName
   AND		w.WORKER_CITY = @City
   AND		w.WORKER_STREET = @Street
   AND		w.WORKER_HOUSE_NUMBER = @HouseNumber;
   
   IF @CUR_WORKER_ID IS NULL
    BEGIN
		SELECT @CUR_WORKER_ID = SEQ_WORKERS.NEXTVAL;
		
        INSERT INTO dbo.WORKERS 
        VALUES (@CUR_WORKER_ID, @FirstName, @LastName, @City, @Street, @HouseNumber);
    END;
    
   SELECT @ID = SEQ_MESSAGES.NEXTVAL;
   INSERT INTO dbo.MESSAGES
        VALUES (@ID, @CUR_WORKER_ID, @PhonePrefix, @PhoneNumber, @Content, 0, CURRENT_TIMESTAMP, NULL);
   SET @RowsAffected = @@ROWCOUNT;   
END;