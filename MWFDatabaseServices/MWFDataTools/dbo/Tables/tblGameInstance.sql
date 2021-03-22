CREATE TABLE [dbo].[tblGameInstance] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [Game]           INT          NOT NULL,
    [Port]           VARCHAR (5)  NOT NULL,
    [Args]           VARCHAR (64) NOT NULL,
    [HostId]	INT FOREIGN KEY REFERENCES tblHost(Id) NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE INDEX UIX_HostID_Port
ON [dbo].[tblGameInstance] ([HostId], [Port]);
