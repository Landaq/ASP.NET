--[2] ��� ���̺� ����
Create Table dbo.NoteComments
(
    Id          Int Identity(1, 1) 
                Not Null Primary Key,               -- �Ϸù�ȣ
    BoardName   NVarChar(50) Null,                  -- �Խ����̸�(Ȯ��): Notice
    BoardId     Int Not Null,                       -- �ش� �Խ����� �Խù� ��ȣ
    Name        NVarChar(25) Not Null,              -- �ۼ���
    Opinion     NVarChar(4000) Not Null,            -- ��� ����
    PostDate    SmallDateTime Default(GetDate()),   -- �ۼ���
    Password    NVarChar(20) Not Null               -- ��� ������ ��ȣ
)
Go
