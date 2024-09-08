--[1] �Խ���(DotNetNote)�� ���� �ۼ� : WriteNote
Create Proc dbo.WriteNote
	@Name			NVarChar(25),
	@Email			NVarChar(100),
	@Title			NVARCHAR(150),
	@PostIp			NVARCHAR(15),
	@Content		NTEXT,
	@Password		NVARCHAR(20),
	@Encoding		NVARCHAR(10),
	@Hompage		NVARCHAR(100),
	@FileName		NVARCHAR(255),
	@FileSize		Int
as
	Declare @MaxRef Int
	Select @MaxRef = Max(Ref) from Notes

	If @MaxRef is Null
		Set @MaxRef = 1 -- ���̺� ���� �� ó���� ��
	Else
		Set @MaxRef = @MaxRef + 1

	Insert Notes
	(
		Name,Email,Title,PostIp,Content,Password, Encoding,
		Homepage,Ref,FileName,FileSize
	)
	Values
	(
		@Name,@Email,@Title, @PostIp, @Content, @Password, @Encoding,
		@Hompage, @MaxRef, @FileName, @FileSize
	)
Go
