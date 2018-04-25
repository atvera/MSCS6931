
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/24/2018 23:21:05
-- Generated from EDMX file: C:\Users\anat_\Documents\GitHub\MSCS6931\RiverKeeperDAL\RiverKeeper.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [riverkeeper];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FK_SurveyQuestion_Surveys]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_SurveyQuestion] DROP CONSTRAINT [FK_FK_SurveyQuestion_Surveys];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_SurveyQuestion_Questions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_SurveyQuestion] DROP CONSTRAINT [FK_FK_SurveyQuestion_Questions];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSurvey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Surveys] DROP CONSTRAINT [FK_UserSurvey];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_AnswerQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerSurvey]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_AnswerSurvey];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[Surveys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Surveys];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveyQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FK_SurveyQuestion];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [QuestionId] int IDENTITY(1,1) NOT NULL,
    [Type] smallint  NOT NULL,
    [Wording] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Surveys'
CREATE TABLE [dbo].[Surveys] (
    [SurveyId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [isTemplate] bit  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [ZipCode] nchar(10)  NULL
);
GO

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [AnswerId] int IDENTITY(1,1) NOT NULL,
    [Response] nvarchar(max)  NOT NULL,
    [Question_QuestionId] int  NOT NULL,
    [Survey_SurveyId] int  NOT NULL
);
GO

-- Creating table 'FK_SurveyQuestion'
CREATE TABLE [dbo].[FK_SurveyQuestion] (
    [FK_SurveyQuestion_Questions_SurveyId] int  NOT NULL,
    [Questions_QuestionId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [QuestionId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([QuestionId] ASC);
GO

-- Creating primary key on [SurveyId] in table 'Surveys'
ALTER TABLE [dbo].[Surveys]
ADD CONSTRAINT [PK_Surveys]
    PRIMARY KEY CLUSTERED ([SurveyId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [AnswerId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([AnswerId] ASC);
GO

-- Creating primary key on [FK_SurveyQuestion_Questions_SurveyId], [Questions_QuestionId] in table 'FK_SurveyQuestion'
ALTER TABLE [dbo].[FK_SurveyQuestion]
ADD CONSTRAINT [PK_FK_SurveyQuestion]
    PRIMARY KEY CLUSTERED ([FK_SurveyQuestion_Questions_SurveyId], [Questions_QuestionId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FK_SurveyQuestion_Questions_SurveyId] in table 'FK_SurveyQuestion'
ALTER TABLE [dbo].[FK_SurveyQuestion]
ADD CONSTRAINT [FK_FK_SurveyQuestion_Surveys]
    FOREIGN KEY ([FK_SurveyQuestion_Questions_SurveyId])
    REFERENCES [dbo].[Surveys]
        ([SurveyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Questions_QuestionId] in table 'FK_SurveyQuestion'
ALTER TABLE [dbo].[FK_SurveyQuestion]
ADD CONSTRAINT [FK_FK_SurveyQuestion_Questions]
    FOREIGN KEY ([Questions_QuestionId])
    REFERENCES [dbo].[Questions]
        ([QuestionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FK_SurveyQuestion_Questions'
CREATE INDEX [IX_FK_FK_SurveyQuestion_Questions]
ON [dbo].[FK_SurveyQuestion]
    ([Questions_QuestionId]);
GO

-- Creating foreign key on [UserId] in table 'Surveys'
ALTER TABLE [dbo].[Surveys]
ADD CONSTRAINT [FK_UserSurvey]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSurvey'
CREATE INDEX [IX_FK_UserSurvey]
ON [dbo].[Surveys]
    ([UserId]);
GO

-- Creating foreign key on [Question_QuestionId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_AnswerQuestion]
    FOREIGN KEY ([Question_QuestionId])
    REFERENCES [dbo].[Questions]
        ([QuestionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerQuestion'
CREATE INDEX [IX_FK_AnswerQuestion]
ON [dbo].[Answers]
    ([Question_QuestionId]);
GO

-- Creating foreign key on [Survey_SurveyId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_AnswerSurvey]
    FOREIGN KEY ([Survey_SurveyId])
    REFERENCES [dbo].[Surveys]
        ([SurveyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerSurvey'
CREATE INDEX [IX_FK_AnswerSurvey]
ON [dbo].[Answers]
    ([Survey_SurveyId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------