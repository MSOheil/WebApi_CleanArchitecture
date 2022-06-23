CREATE TABLE [dbo].[Visit]
(
	[VisitId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateAt] DATETIME NOT NULL, 
    [VisitedAt] DATETIME NULL, 
    [IsActive] BIT NOT NULL, 
    [UserId] NVARCHAR(50) NOT NULL, 
    [DoctorId] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Visit_ToTable] FOREIGN KEY (PatientId) REFERENCES Patient([PatientId])
)
