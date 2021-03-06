USE [master]
GO
/****** Object:  Database [DynamicData]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE DATABASE [DynamicData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DynamicData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS2014\MSSQL\DATA\DynamicData.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DynamicData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS2014\MSSQL\DATA\DynamicData_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DynamicData] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DynamicData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DynamicData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DynamicData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DynamicData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DynamicData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DynamicData] SET ARITHABORT OFF 
GO
ALTER DATABASE [DynamicData] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DynamicData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DynamicData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DynamicData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DynamicData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DynamicData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DynamicData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DynamicData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DynamicData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DynamicData] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DynamicData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DynamicData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DynamicData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DynamicData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DynamicData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DynamicData] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DynamicData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DynamicData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DynamicData] SET  MULTI_USER 
GO
ALTER DATABASE [DynamicData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DynamicData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DynamicData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DynamicData] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DynamicData] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DynamicData]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authorization]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authorization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LibraryID] [int] NULL,
	[SecurityGroupID] [int] NULL,
 CONSTRAINT [PK_Authorization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Field]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Editable] [int] NULL,
	[Visible] [int] NULL,
	[ItemID] [int] NULL,
	[LibraryID] [int] NULL,
	[FieldTypeID] [int] NULL,
	[LookupTable] [nvarchar](max) NULL,
	[LookUpId] [nvarchar](max) NULL,
	[LookUpValue] [nvarchar](max) NULL,
	[ActionButonOpenLibraryID] [int] NULL,
	[Deleted] [int] NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldLog]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[DateTime] [datetime2](7) NULL,
	[FieldID] [int] NULL,
	[LibraryID] [int] NULL,
	[EditedByID] [int] NULL,
 CONSTRAINT [PK_FieldLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldType]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_FieldType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldValue]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[FieldID] [int] NULL,
	[Value] [nvarchar](max) NULL,
	[LibraryGuid] [uniqueidentifier] NULL,
	[Created] [datetime2](7) NULL,
	[Updated] [datetime2](7) NULL,
	[ItemGuid] [uniqueidentifier] NULL,
	[ItemID] [int] NULL,
 CONSTRAINT [PK_FieldValue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[LibraryGuid] [uniqueidentifier] NULL,
	[Deleted] [int] NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemFile]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[ItemID] [int] NULL,
	[Filename] [nvarchar](500) NULL,
	[FileLocation] [nvarchar](1000) NULL,
 CONSTRAINT [PK_ItemFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemLog]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[ItemID] [int] NULL,
	[DateTime] [datetime2](7) NULL,
	[EditedByID] [int] NULL,
 CONSTRAINT [PK_ItemLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Library]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Library](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Title] [nvarchar](1000) NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[EditedDate] [datetime2](7) NULL,
	[GroupBy] [nvarchar](max) NULL,
	[OrderBy] [nvarchar](max) NULL,
	[LibraryTypeID] [int] NULL,
	[ParentID] [int] NULL,
	[CreatedByID] [int] NULL,
	[EditedByID] [int] NULL,
	[Deleted] [int] NULL,
	[Visible] [int] NULL,
	[URL] [nvarchar](max) NULL,
	[MenuType] [nvarchar](max) NULL,
 CONSTRAINT [PK_Library] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibraryLog]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[EditedDate] [datetime2](7) NULL,
	[LibraryID] [int] NULL,
	[EditedByID] [int] NULL,
 CONSTRAINT [PK_LibraryLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibraryType]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Controller] [nvarchar](max) NULL,
	[Icon] [nvarchar](50) NULL,
 CONSTRAINT [PK_LibraryType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Create] [int] NULL,
	[Read] [int] NULL,
	[Update] [int] NULL,
	[Delete] [int] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecurityGroup]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PermissionID] [int] NULL,
 CONSTRAINT [PK_SecurityGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[Username] [nvarchar](max) NULL,
	[Firstname] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 8/22/2020 12:57:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200819031719_createdbcontext', N'3.1.7')
GO
SET IDENTITY_INSERT [dbo].[Library] ON 

INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (3, N'e441a4a5-140b-4b5d-89b4-3e53fe39b05d', N'Root', N'Root', N'Root Application Library', NULL, CAST(N'2020-08-19T00:00:00.0000000' AS DateTime2), NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (6, N'5904afa8-f350-4770-9b50-caab91325a6a', N'Contract Container 1', N'Contract Container 1', N'This is the container 1 of the contracts', CAST(N'2020-08-19T01:36:04.4861584' AS DateTime2), NULL, NULL, NULL, 1, 3, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (7, N'19fb351f-20a3-41ba-81e3-885bac5de5cc', N'Contract Container 2', N'Contract Container 22', N'Contract Container 2', NULL, CAST(N'2020-08-22T12:28:15.7761902' AS DateTime2), NULL, NULL, 1, 3, 2, 2, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (8, N'3ebb7998-28a8-4249-a1b1-02195d174fc4', N'Contract Period', N'Contract Period', NULL, CAST(N'2020-08-20T02:36:15.6373853' AS DateTime2), NULL, NULL, NULL, 2, 6, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (9, N'37ed232f-5ee1-4d87-8cf8-0c1f77a0a3e6', N'Task Order', N'Task Order', N'Task Order View', CAST(N'2020-08-21T01:38:12.8054371' AS DateTime2), NULL, NULL, NULL, 2, 6, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (10, N'2e7713aa-29f5-4158-98fc-72658f163242', N'Task Period', N'Task Period', NULL, CAST(N'2020-08-21T01:39:02.9182642' AS DateTime2), NULL, NULL, NULL, 2, 9, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (11, N'0616ad05-ce65-4bdf-9fb6-63971fe4cf6b', N'This is task 1', N'001', N'This is task 1', CAST(N'2020-08-21T02:43:28.5226709' AS DateTime2), NULL, NULL, NULL, 2, 9, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (12, N'a62881f5-9371-415d-b906-25e34ad5c8e9', N'CLIN', N'Contract Line', NULL, CAST(N'2020-08-21T02:47:55.7342696' AS DateTime2), NULL, NULL, NULL, 2, 11, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (13, N'55ec6e3a-1a66-4202-b1d1-5688bc12d8a4', N'SCLIN', N'SCLIN', NULL, CAST(N'2020-08-21T02:52:55.4770449' AS DateTime2), NULL, NULL, NULL, 2, 12, 2, NULL, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (14, N'd6747703-d076-417d-acc4-9c5a9c1465e3', N'Contract Period', N'Contract Period', NULL, NULL, CAST(N'2020-08-22T12:23:56.9188210' AS DateTime2), NULL, NULL, 2, 7, 2, 2, 0, 1, NULL, N'Side Menu')
INSERT [dbo].[Library] ([ID], [GUID], [Name], [Title], [Description], [CreatedDate], [EditedDate], [GroupBy], [OrderBy], [LibraryTypeID], [ParentID], [CreatedByID], [EditedByID], [Deleted], [Visible], [URL], [MenuType]) VALUES (15, N'7b0299a4-471f-4301-a477-0ef72716acd5', N'Contract 10', N'Contract 10', NULL, NULL, CAST(N'2020-08-22T12:17:19.5033545' AS DateTime2), NULL, NULL, 1, 7, 2, 2, 0, 1, NULL, N'Side Menu')
SET IDENTITY_INSERT [dbo].[Library] OFF
GO
SET IDENTITY_INSERT [dbo].[LibraryType] ON 

INSERT [dbo].[LibraryType] ([ID], [Type], [Controller], [Icon]) VALUES (1, N'Container', N'#', N'nav-icon fa fa-desktop')
INSERT [dbo].[LibraryType] ([ID], [Type], [Controller], [Icon]) VALUES (2, N'DataView', N'View', N'nav-icon fa fa-table')
INSERT [dbo].[LibraryType] ([ID], [Type], [Controller], [Icon]) VALUES (3, N'Document', N'Document', N'nav-icon fa fa-folder-tree')
INSERT [dbo].[LibraryType] ([ID], [Type], [Controller], [Icon]) VALUES (4, N'Report', N'Report', N'nav-icon fa fa-chart-line')
INSERT [dbo].[LibraryType] ([ID], [Type], [Controller], [Icon]) VALUES (5, N'Dashboard', N'Dashboard', N'nav-icon fa fa-tachometer-alt')
SET IDENTITY_INSERT [dbo].[LibraryType] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [Name], [UserID]) VALUES (1, N'USER', NULL)
INSERT [dbo].[Role] ([ID], [Name], [UserID]) VALUES (2, N'GLOBAL ADMIN', NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [GUID], [Username], [Firstname], [Lastname], [Email], [Password], [Phone]) VALUES (1, N'61709937-4466-435b-bdae-6e3704f52370', N'sysadmin', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([ID], [GUID], [Username], [Firstname], [Lastname], [Email], [Password], [Phone]) VALUES (2, N'6116edda-fbad-45db-a5f8-40f33a032353', N'admin', N'danny', N'pham', N'email@email.com', N'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([ID], [UserID], [RoleID]) VALUES (1, 2, 1)
INSERT [dbo].[UserRole] ([ID], [UserID], [RoleID]) VALUES (4, 2, 2)
INSERT [dbo].[UserRole] ([ID], [UserID], [RoleID]) VALUES (6, 1, 2)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
/****** Object:  Index [IX_Authorization_LibraryID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Authorization_LibraryID] ON [dbo].[Authorization]
(
	[LibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Authorization_SecurityGroupID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Authorization_SecurityGroupID] ON [dbo].[Authorization]
(
	[SecurityGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Field_ActionButonOpenLibraryID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Field_ActionButonOpenLibraryID] ON [dbo].[Field]
(
	[ActionButonOpenLibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Field_FieldTypeID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Field_FieldTypeID] ON [dbo].[Field]
(
	[FieldTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Field_ItemID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Field_ItemID] ON [dbo].[Field]
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Field_LibraryID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Field_LibraryID] ON [dbo].[Field]
(
	[LibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldLog_EditedByID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldLog_EditedByID] ON [dbo].[FieldLog]
(
	[EditedByID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldLog_FieldID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldLog_FieldID] ON [dbo].[FieldLog]
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldLog_LibraryID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldLog_LibraryID] ON [dbo].[FieldLog]
(
	[LibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldValue_FieldID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldValue_FieldID] ON [dbo].[FieldValue]
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldValue_ItemID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_FieldValue_ItemID] ON [dbo].[FieldValue]
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ItemFile_ItemID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ItemFile_ItemID] ON [dbo].[ItemFile]
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ItemLog_EditedByID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ItemLog_EditedByID] ON [dbo].[ItemLog]
(
	[EditedByID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ItemLog_ItemID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ItemLog_ItemID] ON [dbo].[ItemLog]
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Library_CreatedByID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Library_CreatedByID] ON [dbo].[Library]
(
	[CreatedByID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Library_EditedByID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Library_EditedByID] ON [dbo].[Library]
(
	[EditedByID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Library_LibraryTypeID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Library_LibraryTypeID] ON [dbo].[Library]
(
	[LibraryTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Library_ParentID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Library_ParentID] ON [dbo].[Library]
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LibraryLog_EditedByID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_LibraryLog_EditedByID] ON [dbo].[LibraryLog]
(
	[EditedByID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LibraryLog_LibraryID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_LibraryLog_LibraryID] ON [dbo].[LibraryLog]
(
	[LibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SecurityGroup_PermissionID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityGroup_PermissionID] ON [dbo].[SecurityGroup]
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_RoleID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleID] ON [dbo].[UserRole]
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_UserID]    Script Date: 8/22/2020 12:57:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_UserID] ON [dbo].[UserRole]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GUID]  DEFAULT (newid()) FOR [GUID]
GO
ALTER TABLE [dbo].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_Library_LibraryID] FOREIGN KEY([LibraryID])
REFERENCES [dbo].[Library] ([ID])
GO
ALTER TABLE [dbo].[Authorization] CHECK CONSTRAINT [FK_Authorization_Library_LibraryID]
GO
ALTER TABLE [dbo].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_SecurityGroup_SecurityGroupID] FOREIGN KEY([SecurityGroupID])
REFERENCES [dbo].[SecurityGroup] ([ID])
GO
ALTER TABLE [dbo].[Authorization] CHECK CONSTRAINT [FK_Authorization_SecurityGroup_SecurityGroupID]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_FieldType_FieldTypeID] FOREIGN KEY([FieldTypeID])
REFERENCES [dbo].[FieldType] ([ID])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_FieldType_FieldTypeID]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Item_ItemID] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ID])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Item_ItemID]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Library_ActionButonOpenLibraryID] FOREIGN KEY([ActionButonOpenLibraryID])
REFERENCES [dbo].[Library] ([ID])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Library_ActionButonOpenLibraryID]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Library_LibraryID] FOREIGN KEY([LibraryID])
REFERENCES [dbo].[Library] ([ID])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Library_LibraryID]
GO
ALTER TABLE [dbo].[FieldLog]  WITH CHECK ADD  CONSTRAINT [FK_FieldLog_Field_FieldID] FOREIGN KEY([FieldID])
REFERENCES [dbo].[Field] ([ID])
GO
ALTER TABLE [dbo].[FieldLog] CHECK CONSTRAINT [FK_FieldLog_Field_FieldID]
GO
ALTER TABLE [dbo].[FieldLog]  WITH CHECK ADD  CONSTRAINT [FK_FieldLog_Library_LibraryID] FOREIGN KEY([LibraryID])
REFERENCES [dbo].[Library] ([ID])
GO
ALTER TABLE [dbo].[FieldLog] CHECK CONSTRAINT [FK_FieldLog_Library_LibraryID]
GO
ALTER TABLE [dbo].[FieldLog]  WITH CHECK ADD  CONSTRAINT [FK_FieldLog_User_EditedByID] FOREIGN KEY([EditedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[FieldLog] CHECK CONSTRAINT [FK_FieldLog_User_EditedByID]
GO
ALTER TABLE [dbo].[FieldValue]  WITH CHECK ADD  CONSTRAINT [FK_FieldValue_Field_FieldID] FOREIGN KEY([FieldID])
REFERENCES [dbo].[Field] ([ID])
GO
ALTER TABLE [dbo].[FieldValue] CHECK CONSTRAINT [FK_FieldValue_Field_FieldID]
GO
ALTER TABLE [dbo].[FieldValue]  WITH CHECK ADD  CONSTRAINT [FK_FieldValue_Item_ItemID] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FieldValue] CHECK CONSTRAINT [FK_FieldValue_Item_ItemID]
GO
ALTER TABLE [dbo].[ItemFile]  WITH CHECK ADD  CONSTRAINT [FK_ItemFile_Item_ItemID] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ItemFile] CHECK CONSTRAINT [FK_ItemFile_Item_ItemID]
GO
ALTER TABLE [dbo].[ItemLog]  WITH CHECK ADD  CONSTRAINT [FK_ItemLog_Item_ItemID] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ID])
GO
ALTER TABLE [dbo].[ItemLog] CHECK CONSTRAINT [FK_ItemLog_Item_ItemID]
GO
ALTER TABLE [dbo].[ItemLog]  WITH CHECK ADD  CONSTRAINT [FK_ItemLog_User_EditedByID] FOREIGN KEY([EditedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[ItemLog] CHECK CONSTRAINT [FK_ItemLog_User_EditedByID]
GO
ALTER TABLE [dbo].[Library]  WITH CHECK ADD  CONSTRAINT [FK_Library_Library_ParentID] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Library] ([ID])
GO
ALTER TABLE [dbo].[Library] CHECK CONSTRAINT [FK_Library_Library_ParentID]
GO
ALTER TABLE [dbo].[Library]  WITH CHECK ADD  CONSTRAINT [FK_Library_LibraryType_LibraryTypeID] FOREIGN KEY([LibraryTypeID])
REFERENCES [dbo].[LibraryType] ([ID])
GO
ALTER TABLE [dbo].[Library] CHECK CONSTRAINT [FK_Library_LibraryType_LibraryTypeID]
GO
ALTER TABLE [dbo].[Library]  WITH CHECK ADD  CONSTRAINT [FK_Library_User_CreatedByID] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Library] CHECK CONSTRAINT [FK_Library_User_CreatedByID]
GO
ALTER TABLE [dbo].[Library]  WITH CHECK ADD  CONSTRAINT [FK_Library_User_EditedByID] FOREIGN KEY([EditedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Library] CHECK CONSTRAINT [FK_Library_User_EditedByID]
GO
ALTER TABLE [dbo].[LibraryLog]  WITH CHECK ADD  CONSTRAINT [FK_LibraryLog_Library_LibraryID] FOREIGN KEY([LibraryID])
REFERENCES [dbo].[Library] ([ID])
GO
ALTER TABLE [dbo].[LibraryLog] CHECK CONSTRAINT [FK_LibraryLog_Library_LibraryID]
GO
ALTER TABLE [dbo].[LibraryLog]  WITH CHECK ADD  CONSTRAINT [FK_LibraryLog_User_EditedByID] FOREIGN KEY([EditedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[LibraryLog] CHECK CONSTRAINT [FK_LibraryLog_User_EditedByID]
GO
ALTER TABLE [dbo].[SecurityGroup]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroup_Permission_PermissionID] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([ID])
GO
ALTER TABLE [dbo].[SecurityGroup] CHECK CONSTRAINT [FK_SecurityGroup_Permission_PermissionID]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role_RoleID]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User_UserID]
GO
USE [master]
GO
ALTER DATABASE [DynamicData] SET  READ_WRITE 
GO
