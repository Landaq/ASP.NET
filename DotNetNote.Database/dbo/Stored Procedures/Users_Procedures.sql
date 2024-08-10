--[1] 입력 저장 프로시져

Create Proc dbo.WriteUsers
	@UserID	NVARCHAR(25)
	,@Password NVARCHAR(20)
as
	Insert into Users Values(@UserID, @Password)
go

--[2] 출력 저장 프로시져
Create Proc dbo.ListUsers
as
	select	UID
			,UserID
			,Password
	from	dbo.Users
	order by UID desc	
go

--[3] 상세 저장 프로시져
Create Proc dbo.ViewUsers
	@UID int
as
	select	UID
			,UserID
			,Password
	From	dbo.Users
	where	UID = @UID
go

--[4] 수정 저장 프로시져
Create Proc dbo.ModifyUsers
	@UserID NVARCHAR(25)
	,@Password NVARCHAR(20)
	,@UID INT
as
	Begin	Tran
		Update dbo.Users
		SET
			UserID = @UserID
			,Password = @Password
		where	UID = @UID
	Commit Tran
go

--[5] 삭제 저장 프로시져
Create Proc dbo.DeleteUsers
	@UID int
as
	Delete dbo.Users
	where	UID = @UID
go

--[6] 검색 저장 프로시져
Create Proc dbo.SearchUsers
	@SearchField NVARCHAR(25)
	,@SearchQuery NVARCHAR(25)
as
	Declare @strSQL NVARCHAR(255)
	Set	@strSQL = '
		Select	*
		from	dbo.Users
		Where
			' + @SearchField + ' like ''%' + @SearchQuery + '%''
			'
	Exec(@strSQL)
go