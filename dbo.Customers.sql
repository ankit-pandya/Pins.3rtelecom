CREATE TABLE [dbo].[Customers] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [MerchantID]  INT           NOT NULL,
    [OldMID]      INT           NOT NULL,
    [Address1]    NVARCHAR (50) NOT NULL,
    [Address2]    NVARCHAR (50) NULL,
    [Address3]    NVARCHAR (50) NULL,
    [Address4]    NVARCHAR (50) NULL,
    [Postcode]    NVARCHAR (50) NOT NULL,
    [TerminalID]  NVARCHAR (50) NOT NULL,
    [IPAddress]   NVARCHAR (50) NULL,
    [Status]      NVARCHAR (50) NULL,
    [Balance]     DECIMAL (18)  NULL,
    [CreditLimit] DECIMAL (18)  NULL,
    [BalWarning]  DECIMAL (18)  NULL,
    [Type]        NVARCHAR (50) NULL,
    [PinMode]     NVARCHAR (50) NULL,
    [Telephone]   INT           NULL,
    [Email]       NVARCHAR (50) NULL,
    [ContactName] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

