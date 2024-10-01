--[4] 게시판(DotNetNote)에 글을 답변 : ReplyNote
Create Proc dbo.ReplyNote
	@Name		NVARCHAR(25),
	@Email		NVARCHAR(100),
	@Title		NVARCHAR(150),
	@PostIp		NVARCHAR(15),
	@Content	NTEXT,
	@Password	NVARCHAR(20),
	@Encoding	NVARCHAR(10),
	@Homepage	NVARCHAR(100),
	@ParentNum	Int,			--	부모글의 고유번호(Id)
	@FileName	NVARCHAR(255),
	@FileSize	Int
as
	--[0]변수선언
	Declare	@MaxRefOrder		Int,
			@MaxRefAnswerNum	int,
			@ParentRef			Int,
			@ParentStep			Int,
			@ParentRefOrder		Int
	--[1] 부모글의 답변 수 (AnswerNum) 1 증가
	Update Notes Set AnswerNum = AnswerNum + 1 Where Id = @ParentNum

	--[2] 같은글에 대해서 답변을 두번 이상하면 먼저 답변한게 위에 나타나게 한다.
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

	--[3] 중간에 답변달 때 (비집고 들어갈 자리 마련)
	select	@ParentRef = Ref
			,@ParentStep = Step
	from	Notes
	where	Id = @ParentNum

	update	Notes
	Set		RefOrder = RefOrder + 1
	where	Ref = @ParentRef 
			And RefOrder > (@MaxRefOrder + @MaxRefAnswerNum)

	--[4] 최종장
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
	