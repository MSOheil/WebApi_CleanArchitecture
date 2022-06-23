CREATE TABLE [dbo].[Patient]
(
	[PatientId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [NationalCode] FLOAT NOT NULL, 
    [Birthday] DATETIME NOT NULL, 
    [IsActive] BIT NOT NULL, 
)
