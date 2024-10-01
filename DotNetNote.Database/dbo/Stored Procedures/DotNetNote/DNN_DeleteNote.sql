--[7] 해당글을 지우는 저장 프로시저 : 답변 글이 있으면 업데이트하고 없으면 지운다.
Create Proc dbo.DeleteNote
	@Id Int,
	@Password NVARCHAR(30)	--	암호 매개 변수 추가
As
	Declare @cnt Int
	Select	@cnt = Count(*)
	From	Notes
	Where	Id = @Id
			and Password = @Password

	if @cnt = 0
	begin
		Return 0 -- 번호와 암호가 맞는게 없으면 0을 반환
	end

	Declare	@AnswerNum	Int
			,@RefOrder	Int
			,@Ref		Int
			,@ParentNum	Int
	select	@AnswerNum = AnswerNum
			,@RefOrder = RefOrder
			,@Ref = Ref
			,@ParentNum = ParentNum
	from	Notes
	where	Id = @Id

	if @AnswerNum = 0
	begin
		If @RefOrder > 0
		Begin
			Update Notes
			Set		RefOrder = RefOrder -1
			where	Ref = @Ref
					and RefOrder > @RefOrder
			
			update	Notes
			set		AnswerNum = AnswerNum - 1
			where	Id = @ParentNum
		End
		Delete Notes
		where	Id=@Id
		
		delete	Notes
		where	Id = @ParentNum
				and ModifyIp = N'((DELETED))'
				and AnswerNum = 0
	End
	Else
	begin
		update Notes
		set
			Name = N'(Unknown)'
			,Email = ''
			,Password = ''
			,Title = N'(삭제된 글입니다.)'
			,Content = N'(삭제된 글입니다. ' + N'현재답변이 포함되어 있기 때문에 내용만 삭제되었습니다.)'
			,ModifyIp = N'((DELETED))'
			,FileName = ''
			,FileSize = 0
			,CommentCount = 0
		where	Id = @Id
	end
go