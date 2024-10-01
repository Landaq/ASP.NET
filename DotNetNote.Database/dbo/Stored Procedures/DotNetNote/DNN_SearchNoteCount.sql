--[6] �˻� ����� ���ڵ� �� ��ȯ
Create Proc dbo.SearchNoteCount
	@SearchField	NVARCHAR(25),
	@SearchQuery	NVARCHAR(25)
As
	Set	@SearchQuery = '%'+@SearchQuery+'%'

	select	Count(*)
	From	Notes
	where
		(
			Case @SearchField
				when 'Name' Then [Name]
				when 'Title' Then Title
				when 'Content' Then Content
				Else @SearchQuery
			End
		)
	Like	@SearchQuery
Go