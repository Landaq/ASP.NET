--[3] �ش� ���� ���������� �Ͼ� ���� ���� ���ν��� : ViewNote
Create Proc dbo.ViewNote
	@Id Int
As
	--��ȸ�� ī��Ʈ 1 ����
	Update Notes set ReadCount = ReadCount + 1 where Id = @Id

	--��� �׸� ��ȸ
	Select * From Notes where Id = @Id
Go