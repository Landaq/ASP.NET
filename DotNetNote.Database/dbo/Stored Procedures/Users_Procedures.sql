--[1] �Է� ���� ���ν���

Create Proc dbo.WriteUsers
	@UserID	NVARCHAR(25)
	,@Password NVARCHAR(20)
as
	Insert into Users Values(@UserID, @Password)
go

--[2] ��� ���� ���ν���
Create Proc dbo.ListUsers
as
	select	UID
			,UserID
			,Password
	from	dbo.Users
	order by UID desc	
go

--[3] �� ���� ���ν���
Create Proc dbo.ViewUsers
	@UID int
as
	select	UID
			,UserID
			,Password
	From	dbo.Users
	where	UID = @UID
go

--[4] ���� ���� ���ν���
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

--[5] ���� ���� ���ν���
Create Proc dbo.DeleteUsers
	@UID int
as
	Delete dbo.Users
	where	UID = @UID
go

--[6] �˻� ���� ���ν���
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