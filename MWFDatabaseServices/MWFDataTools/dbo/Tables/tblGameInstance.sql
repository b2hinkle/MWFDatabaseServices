CREATE TABLE [dbo].[tblGameInstance] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [ProcessId]      INT          NOT NULL,
    [Game]           INT          NOT NULL,
    [Port]           VARCHAR (5)  NOT NULL,
    [Args]           VARCHAR (64),
    [HostId]	INT FOREIGN KEY REFERENCES tblHost(Id) NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Create unique non clusterted index for tblGameInstance. No two game instances on the same host can have the same port number.
CREATE UNIQUE INDEX UIX_HostID_Port
ON [dbo].[tblGameInstance] ([HostId], [Port]);
