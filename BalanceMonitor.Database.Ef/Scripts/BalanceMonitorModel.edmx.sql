
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/05/2016 01:52:30
-- Generated from EDMX file: C:\Users\Think\Documents\Visual Studio 2015\Projects\C#\Design Patterns\Cqrs\BalanceMonitorCqrs\BalanceMonitor.Database.Ef\BalanceMonitorModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BalanceMonitor];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountBalance_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountBalance] DROP CONSTRAINT [FK_AccountBalance_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountLedger_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountLedger] DROP CONSTRAINT [FK_AccountLedger_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountLedger_Transaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountLedger] DROP CONSTRAINT [FK_AccountLedger_Transaction];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountLedger_TransactionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountLedger] DROP CONSTRAINT [FK_AccountLedger_TransactionType];
GO
IF OBJECT_ID(N'[dbo].[FK_Transaction_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transaction] DROP CONSTRAINT [FK_Transaction_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_Transaction_TransactionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transaction] DROP CONSTRAINT [FK_Transaction_TransactionType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[AccountBalance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountBalance];
GO
IF OBJECT_ID(N'[dbo].[AccountLedger]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountLedger];
GO
IF OBJECT_ID(N'[dbo].[Transaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transaction];
GO
IF OBJECT_ID(N'[dbo].[TransactionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Account] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AccountId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Created] datetime  NOT NULL
);
GO

-- Creating table 'AccountBalances'
CREATE TABLE [dbo].[AccountBalance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AccountId] uniqueidentifier  NOT NULL,
    [Currency] nchar(3)  NOT NULL,
    [Amount] decimal(18,6)  NOT NULL
);
GO

-- Creating table 'AccountLedgers'
CREATE TABLE [dbo].[AccountLedger] (
    [Id] int  NOT NULL,
    [Comments] nvarchar(100)  NOT NULL,
    [AccountName] nvarchar(50)  NOT NULL,
    [Time] datetimeoffset  NOT NULL,
    [Amount] decimal(18,6)  NOT NULL,
    [Currency] nvarchar(3)  NOT NULL,
    [Account_AccountId] uniqueidentifier  NOT NULL,
    [Transaction_TransactionId] uniqueidentifier  NOT NULL,
    [TransactionType_TransactionTypeId] int  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transaction] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TransactionId] uniqueidentifier  NOT NULL,
    [TransactionTimeUtc] datetimeoffset  NOT NULL,
    [Amount] decimal(18,6)  NOT NULL,
    [Currency] nvarchar(3)  NOT NULL,
    [Account_AccountId] uniqueidentifier  NOT NULL,
    [TransactionType_TransactionTypeId] int  NOT NULL
);
GO

-- Creating table 'TransactionTypes'
CREATE TABLE [dbo].[TransactionType] (
    [TransactionTypeId] int IDENTITY(1,1) NOT NULL,
    [TransactionTypeName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Aggregates'
CREATE TABLE [dbo].[Aggregate] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AggregateId] uniqueidentifier  NOT NULL,
    [AggregateType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AggregateId] uniqueidentifier  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Version] int  NOT NULL,
    [Payload] nvarchar(max)  NOT NULL,
    [Aggregate_AggregateId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AccountId] in table 'Accounts'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [PK_Account]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- Creating primary key on [AccountId], [Currency] in table 'AccountBalances'
ALTER TABLE [dbo].[AccountBalance]
ADD CONSTRAINT [PK_AccountBalance]
    PRIMARY KEY CLUSTERED ([AccountId], [Currency] ASC);
GO

-- Creating primary key on [Id] in table 'AccountLedgers'
ALTER TABLE [dbo].[AccountLedger]
ADD CONSTRAINT [PK_AccountLedger]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TransactionId] in table 'Transactions'
ALTER TABLE [dbo].[Transaction]
ADD CONSTRAINT [PK_Transaction]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [TransactionTypeId] in table 'TransactionTypes'
ALTER TABLE [dbo].[TransactionType]
ADD CONSTRAINT [PK_TransactionType]
    PRIMARY KEY CLUSTERED ([TransactionTypeId] ASC);
GO

-- Creating primary key on [AggregateId] in table 'Aggregates'
ALTER TABLE [dbo].[Aggregate]
ADD CONSTRAINT [PK_Aggregate]
    PRIMARY KEY CLUSTERED ([AggregateId] ASC);
GO

-- Creating primary key on [AggregateId], [Version] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([AggregateId], [Version] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AccountId] in table 'AccountBalances'
ALTER TABLE [dbo].[AccountBalance]
ADD CONSTRAINT [FK_AccountBalance_Account]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Account]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Account_AccountId] in table 'AccountLedgers'
ALTER TABLE [dbo].[AccountLedger]
ADD CONSTRAINT [FK_AccountLedger_Account]
    FOREIGN KEY ([Account_AccountId])
    REFERENCES [dbo].[Account]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountLedger_Account'
CREATE INDEX [IX_FK_AccountLedger_Account]
ON [dbo].[AccountLedger]
    ([Account_AccountId]);
GO

-- Creating foreign key on [Account_AccountId] in table 'Transactions'
ALTER TABLE [dbo].[Transaction]
ADD CONSTRAINT [FK_Transaction_Account]
    FOREIGN KEY ([Account_AccountId])
    REFERENCES [dbo].[Account]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transaction_Account'
CREATE INDEX [IX_FK_Transaction_Account]
ON [dbo].[Transaction]
    ([Account_AccountId]);
GO

-- Creating foreign key on [Transaction_TransactionId] in table 'AccountLedgers'
ALTER TABLE [dbo].[AccountLedger]
ADD CONSTRAINT [FK_AccountLedger_Transaction]
    FOREIGN KEY ([Transaction_TransactionId])
    REFERENCES [dbo].[Transaction]
        ([TransactionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountLedger_Transaction'
CREATE INDEX [IX_FK_AccountLedger_Transaction]
ON [dbo].[AccountLedger]
    ([Transaction_TransactionId]);
GO

-- Creating foreign key on [TransactionType_TransactionTypeId] in table 'AccountLedgers'
ALTER TABLE [dbo].[AccountLedger]
ADD CONSTRAINT [FK_AccountLedger_TransactionType]
    FOREIGN KEY ([TransactionType_TransactionTypeId])
    REFERENCES [dbo].[TransactionType]
        ([TransactionTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountLedger_TransactionType'
CREATE INDEX [IX_FK_AccountLedger_TransactionType]
ON [dbo].[AccountLedger]
    ([TransactionType_TransactionTypeId]);
GO

-- Creating foreign key on [TransactionType_TransactionTypeId] in table 'Transactions'
ALTER TABLE [dbo].[Transaction]
ADD CONSTRAINT [FK_Transaction_TransactionType]
    FOREIGN KEY ([TransactionType_TransactionTypeId])
    REFERENCES [dbo].[TransactionType]
        ([TransactionTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transaction_TransactionType'
CREATE INDEX [IX_FK_Transaction_TransactionType]
ON [dbo].[Transaction]
    ([TransactionType_TransactionTypeId]);
GO

-- Creating foreign key on [Aggregate_AggregateId] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_AggregateEvent]
    FOREIGN KEY ([Aggregate_AggregateId])
    REFERENCES [dbo].[Aggregate]
        ([AggregateId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AggregateEvent'
CREATE INDEX [IX_FK_AggregateEvent]
ON [dbo].[Event]
    ([Aggregate_AggregateId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------