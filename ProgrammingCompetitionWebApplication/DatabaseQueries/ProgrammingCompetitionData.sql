CREATE DATABASE [ProgrammingCompetitionData];
GO

USE [ProgrammingCompetitionData];
GO

CREATE TABLE [ProgrammingTask]
(
	[TaskId] INT IDENTITY (1, 1) NOT NULL,
	[TaskName] NVARCHAR (100) NOT NULL,
	[Result] TEXT NOT NULL,
	PRIMARY KEY CLUSTERED ([TaskId] ASC)
);
GO

INSERT INTO [ProgrammingTask] (TaskName, Result) VALUES
	('Hello World', 'Hello World'), 
	('2 + 2', '4'), 
	('Find seventh Fibonacci number', '13'),
	('2 times 17', '34'),
	('2 ^ 34', '1717986918'),
	('Find all alphabet letters (uppercase)', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')
GO

CREATE TABLE [UserSubmission]
(
	[SubmissionId] INT IDENTITY (1, 1) NOT NULL,
	[Nickname] NVARCHAR (50) NOT NULL,
	[TaskId] INT NOT NULL,
	[Result] TEXT NOT NULL,
	[IsCorrect] BIT NOT NULL,
	[Code] TEXT NULL, 
    [SubmissionDateTime] DATETIME NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([SubmissionId] ASC),
    CONSTRAINT [FK_TaskId] FOREIGN KEY ([TaskId]) 
        REFERENCES [ProgrammingTask] ([TaskId]) ON DELETE CASCADE,
	CONSTRAINT [Unique_Nickname_Task_UserSubmissions] UNIQUE (Nickname, TaskId)
);
GO

INSERT INTO [UserSubmission] (Nickname, TaskId, Result, IsCorrect, SubmissionDateTime, IsDeleted) VALUES
		('John', 1, 'Goodbye world', 0, GETUTCDATE(), 0),
		('John', 2, '4', 1, GETUTCDATE(), 0),
		('John', 3, '13', 1, GETUTCDATE(), 0),
		('John', 4, '34', 1, GETUTCDATE(), 0),
		('John', 5, '1717986918', 1, GETUTCDATE(), 0),
		('John', 6, 'CDEFGHIJKLMNOPQRSTUVWXYZ', 0, GETUTCDATE(), 0),
		('Frank', 1, 'Hello world', 1, GETUTCDATE(), 0),
		('Frank', 2, '4', 1, GETUTCDATE(), 0),
		('Frank', 3, '13', 1, GETUTCDATE(), 0),
		('Frank', 4, '34', 1, GETUTCDATE(), 0),
		('Frank', 5, '1717986918', 1, GETUTCDATE(), 0),
		('Frank', 6, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 1, GETUTCDATE(), 0),
		('Elisabeth', 1, 'Hello world', 1, GETUTCDATE(), 0),
		('Elisabeth', 2, '5', 0, GETUTCDATE(), 0),
		('Elisabeth', 3, '13', 1, GETUTCDATE(), 0),
		('Elisabeth', 4, '44', 0, GETUTCDATE(), 0),
		('Elisabeth', 5, '0', 0, GETUTCDATE(), 0),
		('Elisabeth', 6, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 1, GETUTCDATE(), 0),
		('Mark', 1, 'Hello World', 1, GETUTCDATE(), 0),
		('Mark', 2, '4', 1, GETUTCDATE(), 0),
		('Mark', 3, '13', 1, GETUTCDATE(), 0),
		('Mark', 4, '34', 1, GETUTCDATE(), 0),
		('Jack', 1, 'Hello World', 1, GETUTCDATE(), 0),
		('Jack', 3, '12', 0, GETUTCDATE(), 0),
		('Jack', 5, '1717986918', 1, GETUTCDATE(), 0),
		('Smith', 3, '13', 1, GETUTCDATE(), 0),
		('Smith', 4, '34', 1, GETUTCDATE(), 0),
		('Eleanor', 1, 'Hello World', 1, GETUTCDATE(), 0),
		('Claus', 6, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 1, GETUTCDATE(), 0)
GO