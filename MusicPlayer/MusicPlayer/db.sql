CREATE TABLE [dbo].[Musics] (
    [Index]    INT            IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (MAX) NOT NULL,
    [FilePath] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Musics] PRIMARY KEY CLUSTERED ([Index] ASC)
);
