--[7] �ش���� ����� ���� ���ν��� : �亯 ���� ������ ������Ʈ�ϰ� ������ �����.
Create Proc dbo.DeleteNote
	@Id Int,
	@Password NVARCHAR(30)	--	��ȣ �Ű� ���� �߰�
As
	Declare @cnt Int
	Select	@cnt = Count(*)
	From	Notes
	Where	Id = @Id
			and Password = @Password

	if @cnt = 0
	begin
		Return 0 -- ��ȣ�� ��ȣ�� �´°� ������ 0�� ��ȯ
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
			,Title = N'(������ ���Դϴ�.)'
			,Content = N'(������ ���Դϴ�. ' + N'����亯�� ���ԵǾ� �ֱ� ������ ���븸 �����Ǿ����ϴ�.)'
			,ModifyIp = N'((DELETED))'
			,FileName = ''
			,FileSize = 0
			,CommentCount = 0
		where	Id = @Id
	end
go