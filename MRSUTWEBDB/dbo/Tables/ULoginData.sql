CREATE TABLE [dbo].[UserMaster] (
    [UId]          INT          IDENTITY (1, 1) NOT NULL,
    [UserId]       VARCHAR (50) NULL,
    [FirstName]    VARCHAR (50) NULL,
    [LastName]     VARCHAR (50) NULL,
    [UserName]     VARCHAR (50) NULL,
    [EmailAddress] VARCHAR (50) NULL,
    [Password]     VARCHAR (50) NULL,
    [isActive]     BIT          NULL,
    [CreatedOn]    DATETIME     DEFAULT (getdate()) NULL,
    [Role]         INT          NULL,
    [LoginIp] NVARCHAR(20) NULL, 
    PRIMARY KEY CLUSTERED ([UId] ASC),
    FOREIGN KEY ([Role]) REFERENCES [dbo].[RoleMaster] ([RoleId])
);