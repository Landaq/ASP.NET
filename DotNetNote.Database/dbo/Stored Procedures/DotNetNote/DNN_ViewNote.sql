--[3] 해당 글을 세부적으로 일어 오는 저장 프로시져 : ViewNote
Create Proc dbo.ViewNote
	@Id Int
As
	--조회수 카운트 1 증가
	Update Notes set ReadCount = ReadCount + 1 where Id = @Id

	--모든 항목 조회
	Select * From Notes where Id = @Id
Go