--[9] 게시판(DotNetNote)에서 데이터 검색 리스트 : SearchNotes
Create Procedure dbo.SearchNotes
	@Page	Int,
	@SearchField	NVARCHAR(25),
	@SearchQuery	NVARCHAR(25)
As
	with DotNetNoteOrderedLists
	As
	(
		Select	[Id]
				,[Name]
				,[Email]
				,[Title]
				,[PostDate]
				,[ReadCount], [Ref], [Step], [RefOrder], [AnswerNum], [ParentNum]
				,[CommentCount]
				,[FileName]
				,[FileSize]
				,[DownCount]
				,Row_Number() over (order By Ref Desc, RefOrder Asc) As 'RowNumber'
		from	Notes
		where	(
					CASE @SearchField
						WHEN 'Name' Then [Name]
						When 'Title' Then Title
						When 'Content' Then Content
						Else
							@SearchQuery
					End
				) Like '%' + @SearchQuery + '%'
	)
	Select	[Id]
			,[Name]
			,[Email]
			,[Title]
			,[PostDate]
			,[ReadCount], [Ref], [Step], [RefOrder], [AnswerNum], [ParentNum]
			,[CommentCount]
			,[FileName]
			,[FileSize]
			,[DownCount]
			,[RowNumber]
	from	DotNetNoteOrderedLists
	where	RowNumber between @Page * 10 + 1 and (@Page  + 1) * 10
	Order By Id Desc
Go
