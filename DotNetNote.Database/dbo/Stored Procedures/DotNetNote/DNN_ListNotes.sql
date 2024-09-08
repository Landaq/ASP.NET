--[2] 게시판(DotNetNote)에서 데이터 출력 : ListNotes
Create Procedure dbo.ListNotes
	@Page Int
as
	with DotNetNoteOrderedLists
	as
	(
		select
			[Id], [Name], [Title], PostDate, ReadCount,
			Ref, Step, RefOrder, AnswerNum, ParentNum,
			CommentCount, FileName, FileSize, DownCount,
			ROW_NUMBER() over (Order By Ref Desc, RefOrder Asc) as 'RowNumber'
		from	Notes
	)
	select	*	From DotNetNoteOrderedLists
	where	RowNumber Between @Page * 10 + 1 And (@Page +1) * 10
Go