--[8] �ش� ���� �����ϴ� ���� ���ν��� : ModifyNote
Create Proc dbo.ModifyNote
	@Name		NVARCHAR(25),
	@Email		NVARCHAR(100),
	@Title		NVARCHAR(150),
	@ModifyIp	NVARCHAR(15),
	@Content	NText,
	@Password	NVARCHAR(30),
	@Encoding	NVARCHAR(10),
	@Hompage	NVARCHAR(100),
	@FileName	NVARCHAR(255),
	@FilSize	Int,
	@Id			Int
As
	Declare	@cnt Int

	select	@cnt = Count(*)
	From	Notes
	where	Id = @Id
			and Password = @Password

	if @cnt > 0		--��ȣ�� ��ȣ�� �´°� �ִٸ�...
	begin
		update Notes
		SET
			Name = @Name
			,Email = @Email
			,Title = @Title
			,ModifyIp = @ModifyIp
			,ModifyDate = GETDATE()
			,Content = @Content
			,Encoding = @Encoding
			,Homepage = @Hompage
			,FileName = @FileName
			,@FilSize = @FilSize
		where	Id = @Id
	end
	Else
		select '0'
go