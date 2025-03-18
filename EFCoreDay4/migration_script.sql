IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Departments] (
    [DepartmentId] int NOT NULL IDENTITY,
    [DepartmentName] nvarchar(15) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
);

CREATE TABLE [Projects] (
    [ProjectId] int NOT NULL IDENTITY,
    [ProjectName] nvarchar(50) NOT NULL,
    [StartDate] date NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectId])
);

CREATE TABLE [Employees] (
    [EmployeeId] int NOT NULL IDENTITY,
    [Name] nvarchar(30) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [DepartmentId] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId]),
    CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId]) ON DELETE CASCADE
);

CREATE TABLE [EmployeeProjects] (
    [EmployeeId] int NOT NULL,
    [ProjectId] int NOT NULL,
    [Role] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_EmployeeProjects] PRIMARY KEY ([EmployeeId], [ProjectId]),
    CONSTRAINT [FK_EmployeeProjects_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeProjects_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DepartmentId', N'DepartmentName') AND [object_id] = OBJECT_ID(N'[Departments]'))
    SET IDENTITY_INSERT [Departments] ON;
INSERT INTO [Departments] ([DepartmentId], [DepartmentName])
VALUES (1, N'IT'),
(2, N'HR'),
(3, N'Finance'),
(4, N'Marketing'),
(5, N'Operations');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DepartmentId', N'DepartmentName') AND [object_id] = OBJECT_ID(N'[Departments]'))
    SET IDENTITY_INSERT [Departments] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProjectId', N'ProjectName', N'StartDate') AND [object_id] = OBJECT_ID(N'[Projects]'))
    SET IDENTITY_INSERT [Projects] ON;
INSERT INTO [Projects] ([ProjectId], [ProjectName], [StartDate])
VALUES (1, N'Cloud Migration', '2023-06-01'),
(2, N'E-commerce Platform', '2023-07-15'),
(3, N'AI Chatbot', '2023-09-10'),
(4, N'Data Analytics Dashboard', '2023-10-05'),
(5, N'CRM System', '2024-01-20'),
(6, N'ERP Upgrade', '2024-03-12');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProjectId', N'ProjectName', N'StartDate') AND [object_id] = OBJECT_ID(N'[Projects]'))
    SET IDENTITY_INSERT [Projects] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeId', N'DepartmentId', N'Email', N'Name') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] ON;
INSERT INTO [Employees] ([EmployeeId], [DepartmentId], [Email], [Name])
VALUES (1, 1, N'alice@company.com', N'Alice Johnson'),
(2, 1, N'bob@company.com', N'Bob Smith'),
(3, 2, N'charlie@company.com', N'Charlie Brown'),
(4, 3, N'david@company.com', N'David Wilson'),
(5, 2, N'eve@company.com', N'Eve Adams'),
(6, 4, N'frank@company.com', N'Frank Thomas'),
(7, 5, N'grace@company.com', N'Grace Lee'),
(8, 3, N'henry@company.com', N'Henry Clark'),
(9, 1, N'isabella@company.com', N'Isabella Moore'),
(10, 4, N'jack@company.com', N'Jack White');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeId', N'DepartmentId', N'Email', N'Name') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeId', N'ProjectId', N'Role') AND [object_id] = OBJECT_ID(N'[EmployeeProjects]'))
    SET IDENTITY_INSERT [EmployeeProjects] ON;
INSERT INTO [EmployeeProjects] ([EmployeeId], [ProjectId], [Role])
VALUES (1, 1, N'Lead Developer'),
(1, 2, N'Full Stack Developer'),
(2, 1, N'DevOps Engineer'),
(2, 3, N'Backend Engineer'),
(3, 2, N'Project Manager'),
(3, 3, N'Data Scientist'),
(4, 1, N'Tester'),
(4, 3, N'Financial Analyst'),
(5, 2, N'HR Consultant'),
(5, 6, N'Scrum Master'),
(6, 4, N'Marketing Specialist'),
(7, 5, N'Operations Manager'),
(8, 6, N'Business Analyst'),
(9, 4, N'Frontend Developer'),
(10, 5, N'UI/UX Designer');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeId', N'ProjectId', N'Role') AND [object_id] = OBJECT_ID(N'[EmployeeProjects]'))
    SET IDENTITY_INSERT [EmployeeProjects] OFF;

CREATE INDEX [IX_EmployeeProjects_ProjectId] ON [EmployeeProjects] ([ProjectId]);

CREATE INDEX [IX_Employees_DepartmentId] ON [Employees] ([DepartmentId]);

CREATE UNIQUE INDEX [IX_Employees_Email] ON [Employees] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250313085517_Initial_Migration', N'9.0.2');

COMMIT;
GO

