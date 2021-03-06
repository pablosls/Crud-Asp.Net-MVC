USE [AVALIACAO]
GO

/****** Object:  Table [dbo].[CadAlunoPABLO]    Script Date: 28/09/2015 10:35:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CadAlunoPABLO](
	[ID_ALUNO] [int] IDENTITY(1,1) NOT NULL,
	[COD_MATRICULA] [nchar](10) NOT NULL,
	[NOME] [varchar](100) NOT NULL,
	[DATA_NASCIMENTO] [date] NOT NULL,
 CONSTRAINT [PK_TB_ALUNO] PRIMARY KEY CLUSTERED 
(
	[ID_ALUNO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


