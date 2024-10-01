--[4] �Խ���(DotNetNote)�� ���� �亯 : ReplyNote
Create Proc dbo.ReplyNote
	@Name		NVARCHAR(25),
	@Email		NVARCHAR(100),
	@Title		NVARCHAR(150),
	@PostIp		NVARCHAR(15),
	@Content	NTEXT,
	@Password	NVARCHAR(20),
	@Encoding	NVARCHAR(10),
	@Homepage	NVARCHAR(100),
	@ParentNum	Int,			--	�θ���� ������ȣ(Id)
	@FileName	NVARCHAR(255),
	@FileSize	Int
as
	--[0]��������
	Declare	@MaxRefOrder		Int,
			@MaxRefAnswerNum	int,
			@ParentRef			Int,
			@ParentStep			Int,
			@ParentRefOrder		Int
	--[1] �θ���� �亯 �� (AnswerNum) 1 ����
	Update Notes Set AnswerNum = AnswerNum + 1 Where Id = @ParentNum

	--[2] �����ۿ� ���ؼ� �亯�� �ι� �̻��ϸ� ���� �亯�Ѱ� ���� ��Ÿ���� �Ѵ�.
	Select	@MaxRefOrder = RefOrder
			,@MaxRefAnswerNum = AnswerNum
	from	Notes
	where	ParentNum = @ParentNum
			and RefOrder = (Select	MAX(RefOrder)
							From	Notes
							where	ParentNum = @ParentNum)

	If @MaxRefOrder is null
	begin
		select	@MaxRefOrder = RefOrder
		from	Notes
		where	Id = @ParentNum
		
		set		@MaxRefAnswerNum = 0
	end

	--[3] �߰��� �亯�� �� (������ �� �ڸ� ����)
	select	@ParentRef = Ref
			,@ParentStep = Step
	from	Notes
	where	Id = @ParentNum

	update	Notes
	Set		RefOrder = RefOrder + 1
	where	Ref = @ParentRef 
			And RefOrder > (@MaxRefOrder + @MaxRefAnswerNum)

	--[4] ������
	Insert Notes
	(
		Name, Email, Title, PostIp, Content, Password, Encoding,
		Homepage, Ref, Step, RefOrder, ParentNum, FileName, FileSize
	)
	values
	(
		@Name, @Email, @Title, @PostIp, @Content, @Password, @Encoding
		,@Homepage, @ParentRef, @ParentStep + 1, @MaxRefOrder + @MaxRefAnswerNum + 1
		,@ParentNum, @FileName, @FileSize
	)
Go
	