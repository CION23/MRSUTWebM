CREATE TABLE [dbo].[RoleMaster] (
    [RoleId]          INT          IDENTITY (9001, 1) NOT NULL,
    [RoleDescription] VARCHAR (20) NULL,
    [CreatedOn]       DATETIME     NULL,
    CONSTRAINT [PK_RoleMaster_RoleId] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

