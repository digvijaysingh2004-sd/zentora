USE [Zentora]
GO
/****** Object:  UserDefinedTableType [dbo].[RolePermissionTVP]    Script Date: 03-07-2026 11:07:52 AM ******/
CREATE TYPE [dbo].[RolePermissionTVP] AS TABLE(
	[ParentModuleId] [int] NULL,
	[ChildModuleId] [int] NULL,
	[SubChildModuleId] [int] NULL,
	[ActionId] [int] NOT NULL
)
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[BranchName] [nvarchar](200) NOT NULL,
	[BranchCode] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[AddressLine1] [nvarchar](200) NOT NULL,
	[AddressLine2] [nvarchar](200) NULL,
	[City] [nvarchar](100) NOT NULL,
	[State] [nvarchar](100) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](20) NOT NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[Website] [nvarchar](200) NULL,
	[HeadOfBranch] [nvarchar](150) NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChildModules]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChildModules](
	[ChildModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ParentModuleId] [int] NOT NULL,
	[ModuleName] [nvarchar](100) NOT NULL,
	[RouteUrl] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChildModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](200) NULL,
	[CompanyCode] [nvarchar](50) NULL,
	[RegistrationNumber] [nvarchar](100) NULL,
	[CompanyType] [nvarchar](100) NULL,
	[IndustryType] [nvarchar](100) NULL,
	[TaxId] [nvarchar](100) NULL,
	[PrimaryEmail] [nvarchar](200) NULL,
	[PrimaryPhone] [nvarchar](20) NULL,
	[SecondaryPhone] [nvarchar](20) NULL,
	[AddressLine1] [nvarchar](250) NULL,
	[AddressLine2] [nvarchar](250) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[WorkingDaysPerWeek] [int] NULL,
	[PrimaryHRName] [nvarchar](200) NULL,
	[PrimaryHREmail] [nvarchar](200) NULL,
	[PrimaryHRPhone] [nvarchar](20) NULL,
	[DefaultCurrency] [nvarchar](50) NULL,
	[DefaultTimeZone] [nvarchar](100) NULL,
	[FinancialYearStart] [nvarchar](50) NULL,
	[FinancialYearEnd] [nvarchar](50) NULL,
	[PayrollCycle] [nvarchar](100) NULL,
	[DefaultLeavePolicy] [nvarchar](200) NULL,
	[ShiftTimings] [nvarchar](200) NULL,
	[LogoUrl] [nvarchar](500) NULL,
	[TagLine] [nvarchar](200) NULL,
	[BrandingColor] [nvarchar](100) NULL,
	[Website] [nvarchar](200) NULL,
	[LinkedInUrl] [nvarchar](200) NULL,
	[TwitterUrl] [nvarchar](200) NULL,
	[FacebookUrl] [nvarchar](200) NULL,
	[InstagramUrl] [nvarchar](200) NULL,
	[ParentCompanyId] [int] NULL,
	[NumberOfEmployees] [int] NULL,
	[Certifications] [nvarchar](500) NULL,
	[InsuranceDetails] [nvarchar](500) NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [int] NOT NULL,
	[DepartmentName] [nvarchar](200) NOT NULL,
	[DepartmentCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[HeadOfDepartment] [nvarchar](150) NULL,
	[HeadEmail] [nvarchar](150) NULL,
	[HeadPhone] [nvarchar](20) NULL,
	[NumberOfEmployees] [int] NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeDetails]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpCode] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PhoneCode] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[MaritalStatus] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Company] [nvarchar](250) NOT NULL,
	[Branch] [nvarchar](250) NOT NULL,
	[Department] [nvarchar](250) NOT NULL,
	[SubDepartment] [nvarchar](250) NULL,
	[OfficeShift] [nvarchar](250) NOT NULL,
	[OfficeLocation] [nvarchar](250) NOT NULL,
	[DOJ] [date] NOT NULL,
	[DOL] [date] NULL,
	[DOB] [date] NOT NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[State] [nvarchar](250) NOT NULL,
	[City] [nvarchar](250) NOT NULL,
	[Country] [nvarchar](250) NOT NULL,
	[ZipCode] [nvarchar](250) NOT NULL,
	[RoleType] [bigint] NOT NULL,
	[LeaveCategory] [nvarchar](250) NULL,
	[HolidayCategory] [nvarchar](250) NULL,
	[ProjectRole] [nvarchar](250) NULL,
	[ReportingManager] [bigint] NOT NULL,
	[AssociateReportingManager] [bigint] NULL,
	[IsActive] [bigint] NOT NULL,
	[IsDeleted] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[SystemAddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginDetails]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[RoleType] [bigint] NOT NULL,
	[Token] [nvarchar](max) NULL,
	[ExpiryToken] [datetime] NULL,
	[LastLogin] [datetime] NOT NULL,
	[IsActive] [bigint] NOT NULL,
	[IsDeleted] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[SystemAddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_LoginDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleActions]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleActions](
	[ActionId] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [nvarchar](50) NOT NULL,
	[ActionCode] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParentModules]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentModules](
	[ParentModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](100) NOT NULL,
	[IconClass] [nvarchar](50) NULL,
	[RouteUrl] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ParentModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ParentModuleId] [int] NULL,
	[ChildModuleId] [int] NULL,
	[SubChildModuleId] [int] NULL,
	[ActionId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[RoleDescription] [text] NULL,
	[IsActive] [bigint] NOT NULL,
	[IsDeleted] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[SystemAddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubChildModules]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubChildModules](
	[SubChildModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ChildModuleId] [int] NOT NULL,
	[ModuleName] [nvarchar](100) NOT NULL,
	[RouteUrl] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubChildModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubDepartments]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubDepartments](
	[SubDepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[SubDepartmentName] [nvarchar](200) NOT NULL,
	[SubDepartmentCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[HeadOfSubDept] [nvarchar](150) NULL,
	[HeadEmail] [nvarchar](150) NULL,
	[HeadPhone] [nvarchar](20) NULL,
	[NumberOfEmployees] [int] NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__SubDepar__17B42F96016C3A0E] PRIMARY KEY CLUSTERED 
(
	[SubDepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (2, 1, N'Head Office Delhi', N'BR001', N'delhi@company.com', N'9876543210', N'123 Main St', N'Connaught Place', N'New Delhi', N'Delhi', N'India', N'110001', N'0111234567', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (3, 1, N'Mumbai Branch', N'BR002', N'mumbai@company.com', N'9876543211', N'45 Marine Drive', N'Churchgate', N'Mumbai', N'Maharashtra', N'India', N'400001', N'0222345678', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (4, 1, N'Bangalore Branch', N'BR003', N'bangalore@company.com', N'9876543212', N'12 MG Road', N'Indiranagar', N'Bangalore', N'Karnataka', N'India', N'560001', N'0803456789', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (5, 1, N'Chennai Branch', N'BR004', N'chennai@company.com', N'9876543213', N'78 Anna Salai', N'T Nagar', N'Chennai', N'Tamil Nadu', N'India', N'600001', N'0444567890', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (6, 1, N'Hyderabad Branch', N'BR005', N'hyderabad@company.com', N'9876543214', N'56 Banjara Hills', N'Jubilee Hills', N'Hyderabad', N'Telangana', N'India', N'500001', N'0405678901', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (7, 1, N'Pune Branch', N'BR006', N'pune@company.com', N'9876543215', N'23 FC Road', N'Shivaji Nagar', N'Pune', N'Maharashtra', N'India', N'411001', N'0206789012', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (8, 1, N'Kolkata Branch', N'BR007', N'kolkata@company.com', N'9876543216', N'89 Park Street', N'Salt Lake', N'Kolkata', N'West Bengal', N'India', N'700001', N'0337890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (9, 1, N'Ahmedabad Branch', N'BR008', N'ahmedabad@company.com', N'9876543217', N'34 CG Road', N'Navrangpura', N'Ahmedabad', N'Gujarat', N'India', N'380001', N'0798901234', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (10, 1, N'Jaipur Branch', N'BR009', N'jaipur@company.com', N'9876543218', N'56 MI Road', N'Bani Park', N'Jaipur', N'Rajasthan', N'India', N'302001', N'0141890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (11, 1, N'Lucknow Branch', N'BR010', N'lucknow@company.com', N'9876543219', N'11 Hazratganj', N'Aminabad', N'Lucknow', N'Uttar Pradesh', N'India', N'226001', N'0522890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (12, 1, N'Kanpur Branch', N'BR011', N'kanpur@company.com', N'9876543220', N'21 Mall Road', N'Civil Lines', N'Kanpur', N'Uttar Pradesh', N'India', N'208001', N'0512890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (13, 1, N'Patna Branch', N'BR012', N'patna@company.com', N'9876543221', N'67 Bailey Road', N'Fraser Road', N'Patna', N'Bihar', N'India', N'800001', N'0612890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (14, 1, N'Surat Branch', N'BR013', N'surat@company.com', N'9876543222', N'90 Ring Road', N'Adajan', N'Surat', N'Gujarat', N'India', N'395001', N'0261890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (15, 1, N'Nagpur Branch', N'BR014', N'nagpur@company.com', N'9876543223', N'45 Wardha Road', N'Sitabuldi', N'Nagpur', N'Maharashtra', N'India', N'440001', N'0712890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (16, 1, N'Indore Branch', N'BR015', N'indore@company.com', N'9876543224', N'32 MG Road', N'Vijay Nagar', N'Indore', N'Madhya Pradesh', N'India', N'452001', N'0731890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (17, 1, N'Bhopal Branch', N'BR016', N'bhopal@company.com', N'9876543225', N'78 New Market', N'TT Nagar', N'Bhopal', N'Madhya Pradesh', N'India', N'462001', N'0755890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (18, 1, N'Coimbatore Branch', N'BR017', N'coimbatore@company.com', N'9876543226', N'22 Avinashi Road', N'RS Puram', N'Coimbatore', N'Tamil Nadu', N'India', N'641001', N'0422890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (19, 1, N'Vizag Branch', N'BR018', N'vizag@company.com', N'9876543227', N'90 Beach Road', N'MVP Colony', N'Visakhapatnam', N'Andhra Pradesh', N'India', N'530001', N'0891890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (20, 1, N'Chandigarh Branch', N'BR019', N'chandigarh@company.com', N'9876543228', N'17 Sector 17', N'Sector 22', N'Chandigarh', N'Punjab', N'India', N'160001', N'0172890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (21, 1, N'Noida Branch', N'BR020', N'noida@company.com', N'9876543229', N'56 Sector 18', N'Atta Market', N'Noida', N'Uttar Pradesh', N'India', N'201301', N'0120890123', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:23:08.093' AS DateTime), CAST(N'2025-09-09T17:23:08.093' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (22, 1, N'New York Branch', N'BR021', N'newyork@company.com', N'12125550101', N'100 Wall St', N'Manhattan', N'New York', N'New York', N'USA', N'10005', N'12125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (23, 1, N'Los Angeles Branch', N'BR022', N'la@company.com', N'13105550102', N'200 Sunset Blvd', N'Hollywood', N'Los Angeles', N'California', N'USA', N'90028', N'13105550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (24, 1, N'Chicago Branch', N'BR023', N'chicago@company.com', N'13125550103', N'300 Michigan Ave', N'Downtown', N'Chicago', N'Illinois', N'USA', N'60601', N'13125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (25, 1, N'Toronto Branch', N'BR024', N'toronto@company.com', N'14165550104', N'400 King St', N'Financial District', N'Toronto', N'Ontario', N'Canada', N'M5H 1J9', N'14165550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (26, 1, N'Vancouver Branch', N'BR025', N'vancouver@company.com', N'16045550105', N'500 Granville St', N'Downtown', N'Vancouver', N'British Columbia', N'Canada', N'V6C 1T2', N'16045550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (27, 1, N'Montreal Branch', N'BR026', N'montreal@company.com', N'15145550106', N'600 Rue Sainte-Catherine', N'Ville-Marie', N'Montreal', N'Quebec', N'Canada', N'H3B 1E1', N'15145550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (28, 1, N'London Branch', N'BR027', N'london@company.com', N'442071111111', N'10 Downing St', N'Westminster', N'London', N'England', N'UK', N'SW1A 2AA', N'442071199999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (29, 1, N'Manchester Branch', N'BR028', N'manchester@company.com', N'441612555010', N'700 Oxford Rd', N'Chorlton', N'Manchester', N'England', N'UK', N'M13 9PL', N'441612559999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (30, 1, N'Birmingham Branch', N'BR029', N'birmingham@company.com', N'441212555011', N'800 Broad St', N'Edgbaston', N'Birmingham', N'England', N'UK', N'B1 2EA', N'441212559999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (31, 1, N'Glasgow Branch', N'BR030', N'glasgow@company.com', N'441412555012', N'900 Buchanan St', N'City Centre', N'Glasgow', N'Scotland', N'UK', N'G1 3JN', N'441412559999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (32, 1, N'Sydney Branch', N'BR031', N'sydney@company.com', N'61225550113', N'100 George St', N'CBD', N'Sydney', N'New South Wales', N'Australia', N'2000', N'61225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (33, 1, N'Melbourne Branch', N'BR032', N'melbourne@company.com', N'61325550114', N'200 Collins St', N'CBD', N'Melbourne', N'Victoria', N'Australia', N'3000', N'61325550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (34, 1, N'Brisbane Branch', N'BR033', N'brisbane@company.com', N'61725550115', N'300 Queen St', N'CBD', N'Brisbane', N'Queensland', N'Australia', N'4000', N'61725550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (35, 1, N'Perth Branch', N'BR034', N'perth@company.com', N'61825550116', N'400 St Georges Tce', N'CBD', N'Perth', N'Western Australia', N'Australia', N'6000', N'61825550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (36, 1, N'Dubai Branch', N'BR035', N'dubai@company.com', N'971425550117', N'100 Sheikh Zayed Rd', N'Downtown', N'Dubai', N'Dubai', N'UAE', N'00000', N'971425559999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (37, 1, N'Abu Dhabi Branch', N'BR036', N'abudhabi@company.com', N'97125550118', N'200 Corniche Rd', N'Central', N'Abu Dhabi', N'Abu Dhabi', N'UAE', N'00000', N'97125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (38, 1, N'Doha Branch', N'BR037', N'doha@company.com', N'97425550119', N'300 West Bay', N'Al Dafna', N'Doha', N'Doha', N'Qatar', N'00000', N'97425550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (39, 1, N'Singapore Branch', N'BR038', N'singapore@company.com', N'65625550120', N'400 Orchard Rd', N'CBD', N'Singapore', N'Central', N'Singapore', N'238879', N'65625550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (40, 1, N'Kuala Lumpur Branch', N'BR039', N'kualalumpur@company.com', N'60325550121', N'500 Jalan Ampang', N'KLCC', N'Kuala Lumpur', N'Selangor', N'Malaysia', N'50450', N'60325550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (41, 1, N'Bangkok Branch', N'BR040', N'bangkok@company.com', N'66225550122', N'600 Sukhumvit Rd', N'Asok', N'Bangkok', N'Bangkok', N'Thailand', N'10110', N'66225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:24:20.907' AS DateTime), CAST(N'2025-09-09T17:24:20.907' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (42, 1, N'Paris Branch', N'BR041', N'paris@company.com', N'33125550123', N'10 Champs Elysees', N'8th Arr', N'Paris', N'Île-de-France', N'France', N'75008', N'33125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (43, 1, N'Berlin Branch', N'BR042', N'berlin@company.com', N'4930125550124', N'20 Alexanderplatz', N'Mitte', N'Berlin', N'Berlin', N'Germany', N'10178', N'4930125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (44, 1, N'Munich Branch', N'BR043', N'munich@company.com', N'498925550125', N'30 Marienplatz', N'Altstadt', N'Munich', N'Bavaria', N'Germany', N'80331', N'498925550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (45, 1, N'Frankfurt Branch', N'BR044', N'frankfurt@company.com', N'496925550126', N'40 Zeil Street', N'Innenstadt', N'Frankfurt', N'Hesse', N'Germany', N'60313', N'496925550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (46, 1, N'Madrid Branch', N'BR045', N'madrid@company.com', N'349125550127', N'50 Gran Via', N'Centro', N'Madrid', N'Madrid', N'Spain', N'28013', N'349125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (47, 1, N'Barcelona Branch', N'BR046', N'barcelona@company.com', N'349325550128', N'60 La Rambla', N'Ciutat Vella', N'Barcelona', N'Catalonia', N'Spain', N'08002', N'349325550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (48, 1, N'Rome Branch', N'BR047', N'rome@company.com', N'39065550129', N'70 Via del Corso', N'Centro Storico', N'Rome', N'Lazio', N'Italy', N'00186', N'39065550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (49, 1, N'Milan Branch', N'BR048', N'milan@company.com', N'39025550130', N'80 Via Torino', N'Centro', N'Milan', N'Lombardy', N'Italy', N'20123', N'39025550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (50, 1, N'Zurich Branch', N'BR049', N'zurich@company.com', N'414425550131', N'90 Bahnhofstrasse', N'Altstadt', N'Zurich', N'Zurich', N'Switzerland', N'8001', N'414425550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (51, 1, N'Geneva Branch', N'BR050', N'geneva@company.com', N'41225550132', N'100 Rue du Rhône', N'Centre', N'Geneva', N'Geneva', N'Switzerland', N'1204', N'41225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (52, 1, N'Tokyo Branch', N'BR051', N'tokyo@company.com', N'81325550133', N'200 Shinjuku St', N'Shinjuku', N'Tokyo', N'Tokyo', N'Japan', N'160-0022', N'81325550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (53, 1, N'Osaka Branch', N'BR052', N'osaka@company.com', N'81625550134', N'210 Namba St', N'Chuo-ku', N'Osaka', N'Osaka', N'Japan', N'542-0076', N'81625550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (54, 1, N'Seoul Branch', N'BR053', N'seoul@company.com', N'82225550135', N'220 Gangnam-daero', N'Gangnam', N'Seoul', N'Seoul', N'South Korea', N'06164', N'82225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (55, 1, N'Busan Branch', N'BR054', N'busan@company.com', N'825125550136', N'230 Haeundae St', N'Haeundae', N'Busan', N'Busan', N'South Korea', N'48095', N'825125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (56, 1, N'Beijing Branch', N'BR055', N'beijing@company.com', N'861025550137', N'240 Wangfujing St', N'Dongcheng', N'Beijing', N'Beijing', N'China', N'100006', N'861025550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (57, 1, N'Shanghai Branch', N'BR056', N'shanghai@company.com', N'862125550138', N'250 Nanjing Rd', N'Huangpu', N'Shanghai', N'Shanghai', N'China', N'200001', N'862125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (58, 1, N'Hong Kong Branch', N'BR057', N'hongkong@company.com', N'85225550139', N'260 Queens Rd', N'Central', N'Hong Kong', N'Hong Kong', N'China', N'999077', N'85225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (59, 1, N'Bangkok Sukhumvit Branch', N'BR058', N'bangkok2@company.com', N'66225550140', N'270 Sukhumvit Rd', N'Nana', N'Bangkok', N'Bangkok', N'Thailand', N'10110', N'66225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (60, 1, N'Jakarta Branch', N'BR059', N'jakarta@company.com', N'622125550141', N'280 Sudirman St', N'Menteng', N'Jakarta', N'Jakarta', N'Indonesia', N'10270', N'622125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (61, 1, N'Manila Branch', N'BR060', N'manila@company.com', N'63225550142', N'290 Ayala Ave', N'Makati', N'Manila', N'Metro Manila', N'Philippines', N'1226', N'63225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (62, 1, N'Cape Town Branch', N'BR061', N'capetown@company.com', N'27215550143', N'300 Long St', N'City Bowl', N'Cape Town', N'Western Cape', N'South Africa', N'8001', N'27215550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (63, 1, N'Johannesburg Branch', N'BR062', N'johannesburg@company.com', N'271125550144', N'310 Sandton Dr', N'Sandton', N'Johannesburg', N'Gauteng', N'South Africa', N'2196', N'271125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (64, 1, N'Nairobi Branch', N'BR063', N'nairobi@company.com', N'2542025550145', N'320 Kenyatta Ave', N'CBD', N'Nairobi', N'Nairobi', N'Kenya', N'00100', N'2542025550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (65, 1, N'Lagos Branch', N'BR064', N'lagos@company.com', N'234125550146', N'330 Victoria Island', N'VI', N'Lagos', N'Lagos', N'Nigeria', N'101241', N'234125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (66, 1, N'Cairo Branch', N'BR065', N'cairo@company.com', N'20225550147', N'340 Tahrir Sq', N'Downtown', N'Cairo', N'Cairo', N'Egypt', N'11511', N'20225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (67, 1, N'Casablanca Branch', N'BR066', N'casablanca@company.com', N'2125225550148', N'350 Mohammed V Blvd', N'Maarif', N'Casablanca', N'Casablanca', N'Morocco', N'20000', N'2125225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (68, 1, N'Istanbul Branch', N'BR067', N'istanbul@company.com', N'902125550149', N'360 Istiklal Cd', N'Beyoglu', N'Istanbul', N'Istanbul', N'Turkey', N'34430', N'902125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (69, 1, N'Ankara Branch', N'BR068', N'ankara@company.com', N'903125550150', N'370 Kizilay Sq', N'Kizilay', N'Ankara', N'Ankara', N'Turkey', N'06420', N'903125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (70, 1, N'Moscow Branch', N'BR069', N'moscow@company.com', N'749525550151', N'380 Red Square', N'Tverskoy', N'Moscow', N'Moscow', N'Russia', N'109012', N'749525550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (71, 1, N'Mexico City Branch', N'BR071', N'mexicocity@company.com', N'525525550153', N'400 Reforma Ave', N'Juarez', N'Mexico City', N'CDMX', N'Mexico', N'06600', N'525525550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (72, 1, N'São Paulo Branch', N'BR072', N'saopaulo@company.com', N'551125550154', N'410 Paulista Ave', N'Bela Vista', N'São Paulo', N'SP', N'Brazil', N'01311-200', N'551125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (73, 1, N'Rio de Janeiro Branch', N'BR073', N'rio@company.com', N'552125550155', N'420 Copacabana Ave', N'Copacabana', N'Rio de Janeiro', N'RJ', N'Brazil', N'22070-002', N'552125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (74, 1, N'Buenos Aires Branch', N'BR074', N'buenosaires@company.com', N'541125550156', N'430 Florida St', N'Centro', N'Buenos Aires', N'CABA', N'Argentina', N'1005', N'541125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (75, 1, N'Santiago Branch', N'BR075', N'santiago@company.com', N'56225550157', N'440 Providencia Ave', N'Providencia', N'Santiago', N'RM', N'Chile', N'7500000', N'56225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (76, 1, N'Lima Branch', N'BR076', N'lima@company.com', N'511125550158', N'450 Miraflores', N'Miraflores', N'Lima', N'Lima', N'Peru', N'15074', N'511125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (77, 1, N'Bogota Branch', N'BR077', N'bogota@company.com', N'57125550159', N'460 Carrera 7', N'Chapinero', N'Bogota', N'Cundinamarca', N'Colombia', N'110111', N'57125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (78, 1, N'Caracas Branch', N'BR078', N'caracas@company.com', N'582125550160', N'470 Sabana Grande', N'Libertador', N'Caracas', N'Distrito Capital', N'Venezuela', N'1010', N'582125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (79, 1, N'Lisbon Branch', N'BR079', N'lisbon@company.com', N'3512125550161', N'480 Avenida da Liberdade', N'Baixa', N'Lisbon', N'Lisbon', N'Portugal', N'1250-096', N'3512125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (80, 1, N'Amsterdam Branch', N'BR080', N'amsterdam@company.com', N'312025550162', N'490 Damrak', N'Centrum', N'Amsterdam', N'North Holland', N'Netherlands', N'1012', N'312025550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:26:00.210' AS DateTime), CAST(N'2025-09-09T17:26:00.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (81, 1, N'Brussels Branch', N'BR081', N'brussels@company.com', N'32225550163', N'500 Grand Place', N'Central', N'Brussels', N'Brussels-Capital', N'Belgium', N'1000', N'32225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (82, 1, N'Vienna Branch', N'BR082', N'vienna@company.com', N'43125550164', N'510 Kärntner Strasse', N'Innere Stadt', N'Vienna', N'Vienna', N'Austria', N'1010', N'43125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (83, 1, N'Stockholm Branch', N'BR083', N'stockholm@company.com', N'46825550165', N'520 Drottninggatan', N'Norrmalm', N'Stockholm', N'Stockholm', N'Sweden', N'111 21', N'46825550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (84, 1, N'Copenhagen Branch', N'BR084', N'copenhagen@company.com', N'453225550166', N'530 Strøget', N'Indre By', N'Copenhagen', N'Capital Region', N'Denmark', N'1050', N'453225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (85, 1, N'Helsinki Branch', N'BR085', N'helsinki@company.com', N'358925550167', N'540 Aleksanterinkatu', N'Kluuvi', N'Helsinki', N'Uusimaa', N'Finland', N'00100', N'358925550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (86, 1, N'Oslo Branch', N'BR086', N'oslo@company.com', N'472225550168', N'550 Karl Johans gate', N'Sentrum', N'Oslo', N'Oslo', N'Norway', N'0154', N'472225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (87, 1, N'Warsaw Branch', N'BR087', N'warsaw@company.com', N'482225550169', N'560 Nowy Swiat', N'Sródmiescie', N'Warsaw', N'Mazovia', N'Poland', N'00-042', N'482225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (88, 1, N'Prague Branch', N'BR088', N'prague@company.com', N'420225550170', N'570 Wenceslas Sq', N'Nové Mesto', N'Prague', N'Prague', N'Czech Republic', N'110 00', N'420225550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (89, 1, N'Budapest Branch', N'BR089', N'budapest@company.com', N'36125550171', N'580 Váci Street', N'Belváros', N'Budapest', N'Pest', N'Hungary', N'1056', N'36125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (90, 1, N'Athens Branch', N'BR090', N'athens@company.com', N'302125550172', N'590 Ermou Street', N'Syntagma', N'Athens', N'Attica', N'Greece', N'10563', N'302125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
INSERT [dbo].[Branches] ([BranchId], [CompanyId], [BranchName], [BranchCode], [Email], [Phone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [FaxNumber], [Website], [HeadOfBranch], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (91, 1, N'Dublin Branch', N'BR091', N'dublin@company.com', N'353125550173', N'600 O’Connell Street', N'Northside', N'Dublin', N'Leinster', N'Ireland', N'D01', N'353125550999', N'www.company.com', NULL, NULL, CAST(N'2025-09-09T17:27:01.810' AS DateTime), CAST(N'2025-09-09T17:27:01.810' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Branches] OFF
GO
SET IDENTITY_INSERT [dbo].[ChildModules] ON 

INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (1, 2, N'Company', N'/CompanyInfo/Company', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (2, 2, N'Branch', N'/CompanyInfo/Branch', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (3, 2, N'Department', N'/CompanyInfo/Department', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (4, 2, N'Sub Department', N'/CompanyInfo/SubDepartment', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (5, 2, N'Holiday Category', N'/CompanyInfo/HolidayCategory', 5, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (6, 2, N'Holiday Type', N'/CompanyInfo/HolidayType', 6, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (7, 2, N'Projects', N'/CompanyInfo/Projects', 7, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (8, 3, N'Users', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (9, 3, N'Roles', N'/CompanyInfo/Roles', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (10, 3, N'Employees', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (11, 4, N'Designations', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (12, 4, N'Award Types', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (13, 4, N'Awards', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (14, 4, N'Promotions', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (15, 4, N'Performance', N'#', 5, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (16, 4, N'Resignations', N'#', 6, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (17, 4, N'Terminations', N'#', 7, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (18, 4, N'Warnings', N'#', 8, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (19, 4, N'Trips', N'#', 9, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (20, 4, N'Complaints', N'#', 10, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (21, 4, N'Transfers', N'#', 11, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (22, 4, N'Announcements', N'#', 12, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (23, 4, N'Asset Management', N'#', 13, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (24, 4, N'Training', N'#', 14, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (25, 5, N'Job Categories', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (26, 5, N'Job Types', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (27, 5, N'Job Locations', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (28, 5, N'Custom Questions', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (29, 5, N'Job Postings', N'#', 5, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (30, 5, N'Candidate Sources', N'#', 6, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (31, 5, N'Candidates', N'#', 7, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (32, 5, N'Interview Types', N'#', 8, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (33, 5, N'Interview Rounds', N'#', 9, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (34, 5, N'Interviews', N'#', 10, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (35, 5, N'Interview Feedback', N'#', 11, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (36, 5, N'Candidate Assessments', N'#', 12, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (37, 5, N'Offer Templates', N'#', 13, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (38, 5, N'Offers', N'#', 14, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (39, 5, N'Onboarding Checklists', N'#', 15, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (40, 5, N'Checklist Items', N'#', 16, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (41, 5, N'Candidate Onboarding', N'#', 17, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (42, 5, N'Career', N'#', 18, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (43, 6, N'Contract Types', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (44, 6, N'Employee Contracts', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (45, 6, N'Contract Templates', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (46, 7, N'Document Categories', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (47, 7, N'Document Types', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (48, 7, N'HR Documents', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (49, 7, N'Acknowledgments', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (50, 7, N'Document Templates', N'#', 5, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (51, 8, N'Meeting Types', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (52, 8, N'Meeting Rooms', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (53, 8, N'Meetings', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (54, 8, N'Meeting Attendees', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (55, 8, N'Meeting Minutes', N'#', 5, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (56, 8, N'Action Items', N'#', 6, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (57, 11, N'Leave Category', N'/CompanyInfo/LeaveCategory', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (58, 11, N'Leave Policies', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (59, 11, N'Leave Applications', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (60, 11, N'Leave Balances', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (61, 12, N'Shifts', N'/CompanyInfo/Shift', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (62, 12, N'Attendance Policies', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (63, 12, N'Attendance Records', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (64, 12, N'Attendance Regularizations', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (65, 14, N'Time Entries', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (66, 15, N'Salary Components', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (67, 15, N'Employee Salaries', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (68, 15, N'Payroll Runs', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (69, 15, N'Payslips', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (70, 17, N'Landing Page', N'#', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (71, 17, N'Custom Pages', N'#', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (72, 17, N'Contact Inquiries', N'#', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (73, 17, N'Newsletter', N'#', 4, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (74, 19, N'Parent Modules', N'/Modules/Parent', 1, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (75, 19, N'Child Modules', N'/Modules/Child', 2, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (76, 19, N'Sub-Child Modules', N'/Modules/SubChild', 3, 1)
INSERT [dbo].[ChildModules] ([ChildModuleId], [ParentModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (77, 19, N'Module Actions', N'/Modules/Actions', 4, 1)
SET IDENTITY_INSERT [dbo].[ChildModules] OFF
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyId], [CompanyName], [CompanyCode], [RegistrationNumber], [CompanyType], [IndustryType], [TaxId], [PrimaryEmail], [PrimaryPhone], [SecondaryPhone], [AddressLine1], [AddressLine2], [City], [State], [Country], [ZipCode], [WorkingDaysPerWeek], [PrimaryHRName], [PrimaryHREmail], [PrimaryHRPhone], [DefaultCurrency], [DefaultTimeZone], [FinancialYearStart], [FinancialYearEnd], [PayrollCycle], [DefaultLeavePolicy], [ShiftTimings], [LogoUrl], [TagLine], [BrandingColor], [Website], [LinkedInUrl], [TwitterUrl], [FacebookUrl], [InstagramUrl], [ParentCompanyId], [NumberOfEmployees], [Certifications], [InsuranceDetails], [Note], [CreatedDate]) VALUES (1, N'Zentora Corp', N'ZEN123', N'REG-987654', N'Private Limited', N'IT Services', N'TAX-123456', N'hr@zentora.com', N'1234567890', N'0987654321', N'123 Tech Park', N'Suite 500', N'New York', N'NY', N'USA', N'10001', 5, N'John Doe', N'albusdumbledor@zentora.com', N'1234567890', N'USD', N'UTC-05:00', N'01-Apr', N'31-Mar', N'Monthly', N'Standard Policy', N'9am-6pm', N'https://example.com/logo.png', N'Innovation for Tomorrow', N'#123456', N'https://acme.com', N'https://linkedin.com/company/zentora', N'https://twitter.com/zentora', N'https://facebook.com/zentora', N'https://instagram.com/zentora', 1, 100, N'ISO 9001', N'Full Coverage', N'Head Office', CAST(N'2025-09-03T20:29:12.680' AS DateTime))
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (12, 2, N'Finance', N'DPT001', N'Handles financial planning and accounting.', N'Head Finance', N'finance@company.com', N'9876500001', 25, N'Critical for budgets', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (13, 2, N'Human Resources', N'DPT002', N'Manages recruitment, training, and employee relations.', N'Head HR', N'hr@company.com', N'9876500002', 30, N'Employee engagement team', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (14, 3, N'Information Technology', N'DPT003', N'Maintains IT infrastructure and software.', N'Head IT', N'it@company.com', N'9876500003', 40, N'24x7 IT support', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (15, 4, N'Marketing', N'DPT004', N'Responsible for branding and promotions.', N'Head Marketing', N'marketing@company.com', N'9876500004', 20, N'Drives campaigns', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (16, 5, N'Sales', N'DPT005', N'Handles product sales and customer acquisition.', N'Head Sales', N'sales@company.com', N'9876500005', 50, N'Revenue team', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (17, 6, N'Logistics', N'DPT006', N'Manages transportation and supply chain.', N'Head Logistics', N'logistics@company.com', N'9876500006', 35, N'Coordinates deliveries', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (18, 7, N'Operations', N'DPT007', N'Oversees daily company activities.', N'Head Operations', N'operations@company.com', N'9876500007', 60, N'Business backbone', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (19, 8, N'Legal', N'DPT008', N'Handles contracts and legal issues.', N'Head Legal', N'legal@company.com', N'9876500008', 12, N'Ensures compliance', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (20, 9, N'Customer Support', N'DPT009', N'Manages customer queries and complaints.', N'Head Support', N'support@company.com', N'9876500009', 55, N'Call center staff', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (21, 10, N'Administration', N'DPT010', N'Office management and administration.', N'Head Admin', N'admin@company.com', N'9876500010', 22, N'Facility management', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (22, 11, N'Research & Development', N'DPT011', N'Develops new products and services.', N'Head R&D', N'rnd@company.com', N'9876500011', 28, N'Innovation team', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (23, 12, N'Procurement', N'DPT012', N'Purchases materials and vendor management.', N'Head Procurement', N'procurement@company.com', N'9876500012', 18, N'Critical for sourcing', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (24, 13, N'Quality Assurance', N'DPT013', N'Ensures quality standards are met.', N'Head QA', N'qa@company.com', N'9876500013', 25, N'ISO compliance', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (25, 14, N'Production', N'DPT014', N'Oversees product manufacturing.', N'Head Production', N'production@company.com', N'9876500014', 70, N'Factory staff', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (26, 15, N'Maintenance', N'DPT015', N'Maintains equipment and infrastructure.', N'Head Maintenance', N'maintenance@company.com', N'9876500015', 16, N'Works with ops', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (27, 16, N'Training & Development', N'DPT016', N'Employee skill enhancement.', N'Head Training', N'training@company.com', N'9876500016', 14, N'L&D experts', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (28, 17, N'Design', N'DPT017', N'Creates product and UI designs.', N'Head Design', N'design@company.com', N'9876500017', 19, N'Creative team', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (29, 18, N'Public Relations', N'DPT018', N'Manages public image and media.', N'Head PR', N'pr@company.com', N'9876500018', 10, N'Press relations', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (30, 19, N'Audit', N'DPT019', N'Conducts financial and process audits.', N'Head Audit', N'audit@company.com', N'9876500019', 9, N'Reports compliance', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (31, 20, N'Security', N'DPT020', N'Ensures workplace safety and security.', N'Head Security', N'security@company.com', N'9876500020', 45, N'Guards & monitoring', CAST(N'2025-09-11T16:36:25.040' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (32, 21, N'Corporate Strategy', N'DPT021', N'Formulates long-term company goals.', N'Head Strategy', N'strategy@company.com', N'9876500021', 12, N'Drives future direction', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (33, 22, N'Investor Relations', N'DPT022', N'Manages relationships with investors.', N'Head IR', N'investors@company.com', N'9876500022', 8, N'Supports shareholders', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (34, 23, N'Taxation', N'DPT023', N'Handles corporate tax compliance.', N'Head Taxation', N'tax@company.com', N'9876500023', 15, N'Works with finance', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (35, 24, N'Supply Chain Management', N'DPT024', N'Manages end-to-end supply chain.', N'Head SCM', N'scm@company.com', N'9876500024', 40, N'Critical operations unit', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (36, 25, N'E-Commerce', N'DPT025', N'Handles online business and portals.', N'Head E-Commerce', N'ecommerce@company.com', N'9876500025', 18, N'Drives online sales', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (37, 26, N'Business Intelligence', N'DPT026', N'Analyzes data for decision making.', N'Head BI', N'bi@company.com', N'9876500026', 14, N'Supports management', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (38, 27, N'Data Science', N'DPT027', N'Builds predictive and AI models.', N'Head Data Science', N'datascience@company.com', N'9876500027', 20, N'AI-driven insights', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (39, 28, N'Cybersecurity', N'DPT028', N'Protects systems from cyber threats.', N'Head Cybersecurity', N'cyber@company.com', N'9876500028', 25, N'Critical for IT safety', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (40, 29, N'Cloud Services', N'DPT029', N'Manages cloud platforms.', N'Head Cloud', N'cloud@company.com', N'9876500029', 22, N'Infrastructure as a service', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (41, 30, N'Innovation Lab', N'DPT030', N'Researches new technologies.', N'Head Innovation', N'innovation@company.com', N'9876500030', 12, N'Think-tank team', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (42, 31, N'Corporate Communications', N'DPT031', N'Handles internal and external communication.', N'Head CorpComm', N'corpcomm@company.com', N'9876500031', 16, N'Brand messaging', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (43, 32, N'Environmental Health & Safety', N'DPT032', N'Focuses on workplace safety and environment.', N'Head EHS', N'ehs@company.com', N'9876500032', 28, N'Critical compliance team', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (44, 33, N'Sustainability', N'DPT033', N'Promotes eco-friendly operations.', N'Head Sustainability', N'sustainability@company.com', N'9876500033', 11, N'CSR & environment', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (45, 34, N'Corporate Social Responsibility', N'DPT034', N'Handles CSR initiatives.', N'Head CSR', N'csr@company.com', N'9876500034', 10, N'Community outreach', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (46, 35, N'Export Control', N'DPT035', N'Ensures compliance with export laws.', N'Head Export', N'export@company.com', N'9876500035', 7, N'Legal compliance', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (47, 36, N'Import & Customs', N'DPT036', N'Handles import and customs clearance.', N'Head Import', N'import@company.com', N'9876500036', 13, N'International logistics', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (48, 37, N'Corporate Governance', N'DPT037', N'Ensures ethical business practices.', N'Head Governance', N'governance@company.com', N'9876500037', 9, N'Board compliance', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (49, 38, N'Enterprise Architecture', N'DPT038', N'Designs enterprise-level IT systems.', N'Head EA', N'ea@company.com', N'9876500038', 15, N'Strategic IT planning', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (50, 39, N'Robotics Automation', N'DPT039', N'Implements RPA solutions.', N'Head Robotics', N'robotics@company.com', N'9876500039', 18, N'Automation team', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (51, 40, N'Artificial Intelligence Research', N'DPT040', N'Develops AI and ML projects.', N'Head AI Research', N'ai@company.com', N'9876500040', 20, N'Advanced R&D', CAST(N'2025-09-11T16:38:12.363' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (53, 22, N'Digital Marketing', N'DPT041', N'Runs online campaigns and social media.', N'Head Digital Marketing', N'digital@company.com', N'9876500041', 26, N'Boosts online presence', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (54, 23, N'Research & Innovation', N'DPT042', N'Focuses on new products and patents.', N'Head Innovation', N'innovation@company.com', N'9876500042', 18, N'Drives creativity', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (55, 24, N'Partner Relations', N'DPT043', N'Manages relationships with partners.', N'Head Partners', N'partners@company.com', N'9876500043', 12, N'Supports collaborations', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (56, 25, N'Talent Acquisition', N'DPT044', N'Responsible for hiring and recruitment.', N'Head Talent', N'talent@company.com', N'9876500044', 20, N'Supports HR', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (57, 26, N'Outbound Sales', N'DPT045', N'Handles outbound sales operations.', N'Head Outbound Sales', N'outsales@company.com', N'9876500045', 35, N'Drives new sales', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (58, 27, N'Inbound Sales', N'DPT046', N'Manages inbound sales leads.', N'Head Inbound Sales', N'insales@company.com', N'9876500046', 30, N'Supports customers', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (59, 28, N'Enterprise Solutions', N'DPT047', N'Provides enterprise-grade solutions.', N'Head Enterprise', N'enterprise@company.com', N'9876500047', 22, N'Targets large clients', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (60, 29, N'SME Solutions', N'DPT048', N'Caters to small and medium businesses.', N'Head SME', N'sme@company.com', N'9876500048', 25, N'Builds SME relations', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (61, 30, N'Field Services', N'DPT049', N'Handles onsite service delivery.', N'Head Field Services', N'field@company.com', N'9876500049', 28, N'Supports operations', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (62, 31, N'Global Operations', N'DPT050', N'Manages international operations.', N'Head Global Ops', N'globalops@company.com', N'9876500050', 45, N'Runs cross-border projects', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (63, 32, N'Mobile Development', N'DPT051', N'Builds mobile apps for clients.', N'Head Mobile Dev', N'mobile@company.com', N'9876500051', 27, N'Supports iOS/Android', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (64, 33, N'Web Development', N'DPT052', N'Develops websites and portals.', N'Head Web Dev', N'web@company.com', N'9876500052', 29, N'Focuses on web stack', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (65, 34, N'AI Research', N'DPT053', N'Focuses on AI/ML innovation.', N'Head AI', N'ai@company.com', N'9876500053', 18, N'Drives AI projects', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (66, 35, N'Robotics', N'DPT054', N'Builds robotic solutions.', N'Head Robotics', N'robotics@company.com', N'9876500054', 15, N'Supports automation', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (67, 36, N'Blockchain', N'DPT055', N'Explores blockchain technologies.', N'Head Blockchain', N'blockchain@company.com', N'9876500055', 14, N'Drives crypto initiatives', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (68, 37, N'Content Strategy', N'DPT056', N'Develops content across platforms.', N'Head Content', N'content@company.com', N'9876500056', 20, N'Works with marketing', CAST(N'2025-09-11T16:42:03.300' AS DateTime), CAST(N'2025-09-11T19:01:44.897' AS DateTime), N'SystemAdmin', N'John  Doe')
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (69, 38, N'Video Production', N'DPT057', N'Produces corporate videos.', N'Head Video', N'video@company.com', N'9876500057', 10, N'Supports branding', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (70, 39, N'Podcasting', N'DPT058', N'Manages audio content.', N'Head Podcasting', N'podcast@company.com', N'9876500058', 8, N'Drives digital voice', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (71, 40, N'Game Development', N'DPT059', N'Creates gaming applications.', N'Head Gaming', N'gaming@company.com', N'9876500059', 21, N'Supports entertainment', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (72, 41, N'User Experience (UX)', N'DPT060', N'Focuses on user experience and design.', N'Head UX', N'ux@company.com', N'9876500060', 16, N'Supports product design', CAST(N'2025-09-11T16:42:03.300' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (73, 42, N'Customer Loyalty', N'DPT061', N'Builds and manages customer loyalty programs.', N'Head Loyalty', N'loyalty@company.com', N'9876500061', 18, N'Enhances retention', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (74, 43, N'Data Science', N'DPT062', N'Analyzes complex data for insights.', N'Head Data Science', N'datascience@company.com', N'9876500062', 24, N'Supports business intelligence', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (75, 44, N'Cybersecurity', N'DPT063', N'Protects IT infrastructure from threats.', N'Head Cybersecurity', N'cyber@company.com', N'9876500063', 22, N'Secures systems', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (76, 45, N'Network Operations', N'DPT064', N'Manages corporate networks.', N'Head Network Ops', N'network@company.com', N'9876500064', 28, N'Ensures connectivity', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (77, 46, N'Technical Support', N'DPT065', N'Provides IT helpdesk services.', N'Head Tech Support', N'techsupport@company.com', N'9876500065', 35, N'Handles troubleshooting', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (78, 47, N'ERP Solutions', N'DPT066', N'Implements and maintains ERP systems.', N'Head ERP', N'erp@company.com', N'9876500066', 20, N'Supports operations', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (79, 48, N'Cloud Engineering', N'DPT067', N'Manages cloud platforms and services.', N'Head Cloud', N'cloud@company.com', N'9876500067', 26, N'Supports scalability', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (80, 49, N'DevOps', N'DPT068', N'Automates CI/CD pipelines.', N'Head DevOps', N'devops@company.com', N'9876500068', 19, N'Drives agility', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (81, 50, N'Product Analytics', N'DPT069', N'Analyzes product usage and KPIs.', N'Head Analytics', N'analytics@company.com', N'9876500069', 14, N'Guides product growth', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (82, 51, N'Global Finance', N'DPT070', N'Manages international financial operations.', N'Head Global Finance', N'gfinance@company.com', N'9876500070', 32, N'Supports global strategy', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (83, 52, N'Corporate Compliance', N'DPT071', N'Ensures compliance with global laws.', N'Head Compliance', N'ccompliance@company.com', N'9876500071', 17, N'Works with legal', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (84, 53, N'Sustainability', N'DPT072', N'Drives environmental and CSR programs.', N'Head Sustainability', N'green@company.com', N'9876500072', 13, N'Improves green footprint', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (85, 54, N'Facilities Management', N'DPT073', N'Manages buildings and facilities.', N'Head Facilities', N'facilities@company.com', N'9876500073', 27, N'Supports infrastructure', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (86, 55, N'Corporate Events', N'DPT074', N'Plans and executes company events.', N'Head Events', N'events@company.com', N'9876500074', 11, N'Enhances branding', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (87, 56, N'Cultural Programs', N'DPT075', N'Promotes cultural and social activities.', N'Head Culture', N'culture@company.com', N'9876500075', 9, N'Improves work environment', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (88, 57, N'Employee Wellness', N'DPT076', N'Focuses on employee health programs.', N'Head Wellness', N'wellness@company.com', N'9876500076', 15, N'Supports HR', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (89, 58, N'AI Solutions', N'DPT077', N'Delivers AI-powered business tools.', N'Head AI Solutions', N'aisolutions@company.com', N'9876500077', 20, N'Supports clients', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (90, 59, N'Training & Development', N'DPT078', N'Provides professional training programs.', N'Head Training', N'training@company.com', N'9876500078', 18, N'Supports workforce skills', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (91, 60, N'Business Transformation', N'DPT079', N'Leads large-scale transformation projects.', N'Head Transformation', N'transform@company.com', N'9876500079', 25, N'Drives efficiency', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
INSERT [dbo].[Departments] ([DepartmentID], [BranchID], [DepartmentName], [DepartmentCode], [Description], [HeadOfDepartment], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (92, 61, N'Knowledge Management', N'DPT080', N'Manages company-wide knowledge base.', N'Head Knowledge', N'knowledge@company.com', N'9876500080', 16, N'Supports innovation', CAST(N'2025-09-11T16:43:01.293' AS DateTime), NULL, N'SystemAdmin', NULL)
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeDetails] ON 

INSERT [dbo].[EmployeeDetails] ([Id], [EmpCode], [FirstName], [MiddleName], [LastName], [Username], [PhoneCode], [Phone], [Email], [Password], [Gender], [MaritalStatus], [Designation], [Company], [Branch], [Department], [SubDepartment], [OfficeShift], [OfficeLocation], [DOJ], [DOL], [DOB], [ProfileImage], [Address], [State], [City], [Country], [ZipCode], [RoleType], [LeaveCategory], [HolidayCategory], [ProjectRole], [ReportingManager], [AssociateReportingManager], [IsActive], [IsDeleted], [CreatedDate], [CreateBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (1, N'EMP001', N'John', NULL, N'Doe', N'superadmin', N'+91', N'9876543210', N'superadmin@zentora.com', N'superadmin@123', N'Male', N'Single', N'Founder', N'Zentora Pvt Ltd', N'HQ', N'IT', NULL, N'General', N'Noida', CAST(N'2023-01-01' AS Date), NULL, CAST(N'1990-01-01' AS Date), NULL, N'Noida, UP', N'Uttar Pradesh', N'Noida', N'India', N'201301', 1, NULL, NULL, NULL, 0, NULL, 1, 0, CAST(N'2025-08-26T14:21:37.503' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.503' AS DateTime))
INSERT [dbo].[EmployeeDetails] ([Id], [EmpCode], [FirstName], [MiddleName], [LastName], [Username], [PhoneCode], [Phone], [Email], [Password], [Gender], [MaritalStatus], [Designation], [Company], [Branch], [Department], [SubDepartment], [OfficeShift], [OfficeLocation], [DOJ], [DOL], [DOB], [ProfileImage], [Address], [State], [City], [Country], [ZipCode], [RoleType], [LeaveCategory], [HolidayCategory], [ProjectRole], [ReportingManager], [AssociateReportingManager], [IsActive], [IsDeleted], [CreatedDate], [CreateBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (2, N'EMP002', N'Alice', NULL, N'Smith', N'admin', N'+91', N'9876500001', N'admin@zentora.com', N'Admin@123', N'Female', N'Married', N'CEO', N'Zentora Pvt Ltd', N'HQ', N'HR', NULL, N'General', N'Noida', CAST(N'2023-02-01' AS Date), NULL, CAST(N'1992-05-05' AS Date), NULL, N'Delhi NCR', N'Delhi', N'New Delhi', N'India', N'110001', 2, NULL, NULL, NULL, 1, NULL, 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[EmployeeDetails] ([Id], [EmpCode], [FirstName], [MiddleName], [LastName], [Username], [PhoneCode], [Phone], [Email], [Password], [Gender], [MaritalStatus], [Designation], [Company], [Branch], [Department], [SubDepartment], [OfficeShift], [OfficeLocation], [DOJ], [DOL], [DOB], [ProfileImage], [Address], [State], [City], [Country], [ZipCode], [RoleType], [LeaveCategory], [HolidayCategory], [ProjectRole], [ReportingManager], [AssociateReportingManager], [IsActive], [IsDeleted], [CreatedDate], [CreateBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (3, N'EMP003', N'Robert', NULL, N'Brown', N'manager', N'+91', N'9876500002', N'manager@zentora.com', N'Manager@123', N'Male', NULL, N'Head Manager', N'Zentora Pvt Ltd', N'HQ', N'Operations', NULL, N'General', N'Noida', CAST(N'2023-03-01' AS Date), NULL, CAST(N'1988-07-15' AS Date), NULL, N'Noida Sector 62', N'Uttar Pradesh', N'Noida', N'India', N'201301', 3, NULL, NULL, NULL, 1, NULL, 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[EmployeeDetails] ([Id], [EmpCode], [FirstName], [MiddleName], [LastName], [Username], [PhoneCode], [Phone], [Email], [Password], [Gender], [MaritalStatus], [Designation], [Company], [Branch], [Department], [SubDepartment], [OfficeShift], [OfficeLocation], [DOJ], [DOL], [DOB], [ProfileImage], [Address], [State], [City], [Country], [ZipCode], [RoleType], [LeaveCategory], [HolidayCategory], [ProjectRole], [ReportingManager], [AssociateReportingManager], [IsActive], [IsDeleted], [CreatedDate], [CreateBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (4, N'EMP004', N'Sophia', NULL, N'Wilson', N'hruser', N'+91', N'9876500003', N'hr@zentora.com', N'HR@123', N'Female', NULL, N'HR Manager', N'Zentora Pvt Ltd', N'HQ', N'HR', NULL, N'General', N'Noida', CAST(N'2023-04-01' AS Date), NULL, CAST(N'1995-03-10' AS Date), NULL, N'Noida Sector 18', N'Uttar Pradesh', N'Noida', N'India', N'201301', 4, NULL, NULL, NULL, 1, NULL, 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[EmployeeDetails] ([Id], [EmpCode], [FirstName], [MiddleName], [LastName], [Username], [PhoneCode], [Phone], [Email], [Password], [Gender], [MaritalStatus], [Designation], [Company], [Branch], [Department], [SubDepartment], [OfficeShift], [OfficeLocation], [DOJ], [DOL], [DOB], [ProfileImage], [Address], [State], [City], [Country], [ZipCode], [RoleType], [LeaveCategory], [HolidayCategory], [ProjectRole], [ReportingManager], [AssociateReportingManager], [IsActive], [IsDeleted], [CreatedDate], [CreateBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (5, N'EMP005', N'David', NULL, N'Lee', N'teamlead', N'+91', N'9876500004', N'teamlead@zentora.com', N'Lead@123', N'Male', NULL, N'Team Executive', N'Zentora Pvt Ltd', N'HQ', N'Development', NULL, N'General', N'Noida', CAST(N'2023-05-01' AS Date), NULL, CAST(N'1991-12-25' AS Date), NULL, N'Delhi', N'Delhi', N'New Delhi', N'India', N'110001', 5, NULL, NULL, NULL, 1, NULL, 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[EmployeeDetails] ([Id], [EmpCode], [FirstName], [MiddleName], [LastName], [Username], [PhoneCode], [Phone], [Email], [Password], [Gender], [MaritalStatus], [Designation], [Company], [Branch], [Department], [SubDepartment], [OfficeShift], [OfficeLocation], [DOJ], [DOL], [DOB], [ProfileImage], [Address], [State], [City], [Country], [ZipCode], [RoleType], [LeaveCategory], [HolidayCategory], [ProjectRole], [ReportingManager], [AssociateReportingManager], [IsActive], [IsDeleted], [CreatedDate], [CreateBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (6, N'EMP006', N'Emma', NULL, N'Taylor', N'employee', N'+91', N'9876500005', N'employee@zentora.com', N'Emp@123', N'Female', NULL, N'Software Developer', N'Zentora Pvt Ltd', N'HQ', N'Support', NULL, N'General', N'Noida', CAST(N'2023-06-01' AS Date), NULL, CAST(N'1996-09-09' AS Date), NULL, N'Ghaziabad', N'Uttar Pradesh', N'Ghaziabad', N'India', N'201002', 6, NULL, NULL, NULL, 1, NULL, 1, 0, CAST(N'2025-08-26T14:21:37.510' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.510' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmployeeDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginDetails] ON 

INSERT [dbo].[LoginDetails] ([Id], [EmpId], [FullName], [Username], [Phone], [Email], [Password], [RoleType], [Token], [ExpiryToken], [LastLogin], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (1, 1, N'John Doe', N'superadmin', N'9876543210', N'superadmin@zentora.com', N'superadmin@123', 1, NULL, NULL, CAST(N'2026-04-19T23:17:23.640' AS DateTime), 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[LoginDetails] ([Id], [EmpId], [FullName], [Username], [Phone], [Email], [Password], [RoleType], [Token], [ExpiryToken], [LastLogin], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (2, 2, N'Alice Smith', N'admin', N'9876500001', N'admin@zentora.com', N'Admin@123', 2, NULL, NULL, CAST(N'2026-04-19T23:17:38.677' AS DateTime), 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[LoginDetails] ([Id], [EmpId], [FullName], [Username], [Phone], [Email], [Password], [RoleType], [Token], [ExpiryToken], [LastLogin], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (3, 3, N'Robert Brown', N'manager', N'9876500002', N'manager@zentora.com', N'Manager@123', 3, NULL, NULL, CAST(N'2025-08-26T15:23:54.130' AS DateTime), 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[LoginDetails] ([Id], [EmpId], [FullName], [Username], [Phone], [Email], [Password], [RoleType], [Token], [ExpiryToken], [LastLogin], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (4, 4, N'Sophia Wilson', N'hruser', N'9876500003', N'hr@zentora.com', N'HR@123', 4, NULL, NULL, CAST(N'2025-08-26T16:02:42.180' AS DateTime), 1, 0, CAST(N'2025-08-26T14:21:37.507' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.507' AS DateTime))
INSERT [dbo].[LoginDetails] ([Id], [EmpId], [FullName], [Username], [Phone], [Email], [Password], [RoleType], [Token], [ExpiryToken], [LastLogin], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (5, 5, N'David Lee', N'teamlead', N'9876500004', N'teamlead@zentora.com', N'Lead@123', 5, NULL, NULL, CAST(N'2025-08-26T15:24:29.013' AS DateTime), 1, 0, CAST(N'2025-08-26T14:21:37.510' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.510' AS DateTime))
INSERT [dbo].[LoginDetails] ([Id], [EmpId], [FullName], [Username], [Phone], [Email], [Password], [RoleType], [Token], [ExpiryToken], [LastLogin], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (6, 6, N'Emma Taylor', N'employee', N'9876500005', N'employee@zentora.com', N'Emp@123', 6, NULL, NULL, CAST(N'2025-08-26T15:24:41.797' AS DateTime), 1, 0, CAST(N'2025-08-26T14:21:37.510' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T14:21:37.510' AS DateTime))
SET IDENTITY_INSERT [dbo].[LoginDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[ModuleActions] ON 

INSERT [dbo].[ModuleActions] ([ActionId], [ActionName], [ActionCode]) VALUES (1, N'View', N'view')
INSERT [dbo].[ModuleActions] ([ActionId], [ActionName], [ActionCode]) VALUES (2, N'Add', N'add')
INSERT [dbo].[ModuleActions] ([ActionId], [ActionName], [ActionCode]) VALUES (3, N'Edit', N'edit')
INSERT [dbo].[ModuleActions] ([ActionId], [ActionName], [ActionCode]) VALUES (4, N'Delete', N'delete')
INSERT [dbo].[ModuleActions] ([ActionId], [ActionName], [ActionCode]) VALUES (5, N'All', N'all')
SET IDENTITY_INSERT [dbo].[ModuleActions] OFF
GO
SET IDENTITY_INSERT [dbo].[ParentModules] ON 

INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (1, N'Dashboard', N'layout-grid', N'/Home/Index', 1, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (2, N'Company Info', N'notebook-text', N'#', 2, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (3, N'Staff', N'users', N'#', 3, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (4, N'HR Management', N'briefcase', N'#', 4, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (5, N'Recruitment', N'user-plus', N'#', 5, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (6, N'Contract Management', N'file-text', N'#', 6, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (7, N'Document Management', N'folder', N'#', 7, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (8, N'Meetings', N'monitor-play', N'#', 8, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (9, N'Calendar', N'calendar', N'#', 9, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (10, N'Media Library', N'image', N'#', 10, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (11, N'Leave Management', N'calendar-days', N'#', 11, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (12, N'Attendance', N'clock', N'#', 12, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (13, N'Biometric Attendance', N'fingerprint', N'#', 13, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (14, N'Time Tracking', N'timer', N'#', 14, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (15, N'Payroll Management', N'dollar-sign', N'#', 15, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (16, N'Currency', N'coins', N'#', 16, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (17, N'Landing Page', N'palette', N'#', 17, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (18, N'Settings', N'settings', N'#', 18, 1)
INSERT [dbo].[ParentModules] ([ParentModuleId], [ModuleName], [IconClass], [RouteUrl], [SortOrder], [IsActive]) VALUES (19, N'Module Management', N'layers', N'#', 19, 1)
SET IDENTITY_INSERT [dbo].[ParentModules] OFF
GO
SET IDENTITY_INSERT [dbo].[RolePermissions] ON 

INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (1, 1, 1, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (2, 1, 1, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (3, 1, 1, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (4, 1, 1, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (5, 1, NULL, 1, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (6, 1, NULL, 1, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (7, 1, NULL, 1, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (8, 1, NULL, 1, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (9, 1, NULL, 2, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (10, 1, NULL, 2, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (11, 1, NULL, 2, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (12, 1, NULL, 2, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (13, 1, NULL, 3, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (14, 1, NULL, 3, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (15, 1, NULL, 3, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (16, 1, NULL, 3, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (17, 1, NULL, 4, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (18, 1, NULL, 4, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (19, 1, NULL, 4, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (20, 1, NULL, 4, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (21, 1, NULL, 5, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (22, 1, NULL, 5, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (23, 1, NULL, 5, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (24, 1, NULL, 5, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (25, 1, NULL, 6, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (26, 1, NULL, 6, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (27, 1, NULL, 6, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (28, 1, NULL, 6, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (29, 1, NULL, 7, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (30, 1, NULL, 7, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (31, 1, NULL, 7, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (32, 1, NULL, 7, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (33, 1, NULL, 8, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (34, 1, NULL, 8, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (35, 1, NULL, 8, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (36, 1, NULL, 8, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (37, 1, NULL, 9, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (38, 1, NULL, 9, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (39, 1, NULL, 9, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (40, 1, NULL, 9, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (41, 1, NULL, 10, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (42, 1, NULL, 10, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (43, 1, NULL, 10, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (44, 1, NULL, 10, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (45, 1, NULL, 11, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (46, 1, NULL, 11, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (47, 1, NULL, 11, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (48, 1, NULL, 11, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (49, 1, NULL, 12, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (50, 1, NULL, 12, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (51, 1, NULL, 12, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (52, 1, NULL, 12, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (53, 1, NULL, 13, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (54, 1, NULL, 13, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (55, 1, NULL, 13, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (56, 1, NULL, 13, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (57, 1, NULL, 14, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (58, 1, NULL, 14, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (59, 1, NULL, 14, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (60, 1, NULL, 14, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (61, 1, NULL, NULL, 1, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (62, 1, NULL, NULL, 1, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (63, 1, NULL, NULL, 1, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (64, 1, NULL, NULL, 1, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (65, 1, NULL, NULL, 2, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (66, 1, NULL, NULL, 2, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (67, 1, NULL, NULL, 2, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (68, 1, NULL, NULL, 2, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (69, 1, NULL, NULL, 3, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (70, 1, NULL, NULL, 3, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (71, 1, NULL, NULL, 3, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (72, 1, NULL, NULL, 3, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (73, 1, NULL, NULL, 4, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (74, 1, NULL, NULL, 4, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (75, 1, NULL, NULL, 4, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (76, 1, NULL, NULL, 4, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (77, 1, NULL, NULL, 5, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (78, 1, NULL, NULL, 5, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (79, 1, NULL, NULL, 5, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (80, 1, NULL, NULL, 5, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (81, 1, NULL, NULL, 6, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (82, 1, NULL, NULL, 6, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (83, 1, NULL, NULL, 6, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (84, 1, NULL, NULL, 6, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (85, 1, NULL, 16, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (86, 1, NULL, 16, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (87, 1, NULL, 16, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (88, 1, NULL, 16, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (89, 1, NULL, 17, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (90, 1, NULL, 17, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (91, 1, NULL, 17, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (92, 1, NULL, 17, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (93, 1, NULL, 18, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (94, 1, NULL, 18, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (95, 1, NULL, 18, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (96, 1, NULL, 18, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (97, 1, NULL, 19, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (98, 1, NULL, 19, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (99, 1, NULL, 19, NULL, 3)
GO
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (100, 1, NULL, 19, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (101, 1, NULL, 20, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (102, 1, NULL, 20, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (103, 1, NULL, 20, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (104, 1, NULL, 20, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (105, 1, NULL, 21, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (106, 1, NULL, 21, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (107, 1, NULL, 21, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (108, 1, NULL, 21, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (109, 1, NULL, 22, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (110, 1, NULL, 22, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (111, 1, NULL, 22, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (112, 1, NULL, 22, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (113, 1, NULL, NULL, 7, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (114, 1, NULL, NULL, 7, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (115, 1, NULL, NULL, 7, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (116, 1, NULL, NULL, 7, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (117, 1, NULL, NULL, 8, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (118, 1, NULL, NULL, 8, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (119, 1, NULL, NULL, 8, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (120, 1, NULL, NULL, 8, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (121, 1, NULL, NULL, 9, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (122, 1, NULL, NULL, 9, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (123, 1, NULL, NULL, 9, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (124, 1, NULL, NULL, 9, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (125, 1, NULL, NULL, 10, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (126, 1, NULL, NULL, 10, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (127, 1, NULL, NULL, 10, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (128, 1, NULL, NULL, 10, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (129, 1, NULL, NULL, 11, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (130, 1, NULL, NULL, 11, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (131, 1, NULL, NULL, 11, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (132, 1, NULL, NULL, 11, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (133, 1, NULL, NULL, 12, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (134, 1, NULL, NULL, 12, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (135, 1, NULL, NULL, 12, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (136, 1, NULL, NULL, 12, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (137, 1, NULL, NULL, 13, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (138, 1, NULL, NULL, 13, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (139, 1, NULL, NULL, 13, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (140, 1, NULL, NULL, 13, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (141, 1, NULL, NULL, 14, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (142, 1, NULL, NULL, 14, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (143, 1, NULL, NULL, 14, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (144, 1, NULL, NULL, 14, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (145, 1, NULL, 25, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (146, 1, NULL, 25, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (147, 1, NULL, 25, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (148, 1, NULL, 25, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (149, 1, NULL, 26, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (150, 1, NULL, 26, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (151, 1, NULL, 26, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (152, 1, NULL, 26, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (153, 1, NULL, 27, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (154, 1, NULL, 27, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (155, 1, NULL, 27, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (156, 1, NULL, 27, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (157, 1, NULL, 28, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (158, 1, NULL, 28, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (159, 1, NULL, 28, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (160, 1, NULL, 28, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (161, 1, NULL, 29, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (162, 1, NULL, 29, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (163, 1, NULL, 29, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (164, 1, NULL, 29, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (165, 1, NULL, 30, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (166, 1, NULL, 30, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (167, 1, NULL, 30, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (168, 1, NULL, 30, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (169, 1, NULL, 31, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (170, 1, NULL, 31, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (171, 1, NULL, 31, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (172, 1, NULL, 31, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (173, 1, NULL, 32, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (174, 1, NULL, 32, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (175, 1, NULL, 32, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (176, 1, NULL, 32, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (177, 1, NULL, 33, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (178, 1, NULL, 33, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (179, 1, NULL, 33, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (180, 1, NULL, 33, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (181, 1, NULL, 34, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (182, 1, NULL, 34, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (183, 1, NULL, 34, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (184, 1, NULL, 34, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (185, 1, NULL, 35, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (186, 1, NULL, 35, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (187, 1, NULL, 35, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (188, 1, NULL, 35, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (189, 1, NULL, 36, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (190, 1, NULL, 36, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (191, 1, NULL, 36, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (192, 1, NULL, 36, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (193, 1, NULL, 37, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (194, 1, NULL, 37, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (195, 1, NULL, 37, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (196, 1, NULL, 37, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (197, 1, NULL, 38, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (198, 1, NULL, 38, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (199, 1, NULL, 38, NULL, 3)
GO
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (200, 1, NULL, 38, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (201, 1, NULL, 39, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (202, 1, NULL, 39, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (203, 1, NULL, 39, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (204, 1, NULL, 39, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (205, 1, NULL, 40, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (206, 1, NULL, 40, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (207, 1, NULL, 40, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (208, 1, NULL, 40, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (209, 1, NULL, 41, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (210, 1, NULL, 41, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (211, 1, NULL, 41, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (212, 1, NULL, 41, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (213, 1, NULL, 42, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (214, 1, NULL, 42, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (215, 1, NULL, 42, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (216, 1, NULL, 42, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (217, 1, NULL, 43, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (218, 1, NULL, 43, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (219, 1, NULL, 43, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (220, 1, NULL, 43, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (221, 1, NULL, 44, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (222, 1, NULL, 44, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (223, 1, NULL, 44, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (224, 1, NULL, 44, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (225, 1, NULL, 45, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (226, 1, NULL, 45, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (227, 1, NULL, 45, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (228, 1, NULL, 45, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (229, 1, NULL, 46, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (230, 1, NULL, 46, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (231, 1, NULL, 46, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (232, 1, NULL, 46, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (233, 1, NULL, 47, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (234, 1, NULL, 47, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (235, 1, NULL, 47, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (236, 1, NULL, 47, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (237, 1, NULL, 48, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (238, 1, NULL, 48, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (239, 1, NULL, 48, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (240, 1, NULL, 48, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (241, 1, NULL, 49, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (242, 1, NULL, 49, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (243, 1, NULL, 49, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (244, 1, NULL, 49, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (245, 1, NULL, 50, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (246, 1, NULL, 50, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (247, 1, NULL, 50, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (248, 1, NULL, 50, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (249, 1, NULL, 51, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (250, 1, NULL, 51, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (251, 1, NULL, 51, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (252, 1, NULL, 51, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (253, 1, NULL, 52, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (254, 1, NULL, 52, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (255, 1, NULL, 52, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (256, 1, NULL, 52, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (257, 1, NULL, 53, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (258, 1, NULL, 53, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (259, 1, NULL, 53, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (260, 1, NULL, 53, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (261, 1, NULL, 54, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (262, 1, NULL, 54, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (263, 1, NULL, 54, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (264, 1, NULL, 54, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (265, 1, NULL, 55, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (266, 1, NULL, 55, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (267, 1, NULL, 55, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (268, 1, NULL, 55, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (269, 1, NULL, 56, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (270, 1, NULL, 56, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (271, 1, NULL, 56, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (272, 1, NULL, 56, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (273, 1, 9, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (274, 1, 9, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (275, 1, 9, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (276, 1, 9, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (277, 1, 10, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (278, 1, 10, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (279, 1, 10, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (280, 1, 10, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (281, 1, NULL, 57, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (282, 1, NULL, 57, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (283, 1, NULL, 57, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (284, 1, NULL, 57, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (285, 1, NULL, 58, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (286, 1, NULL, 58, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (287, 1, NULL, 58, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (288, 1, NULL, 58, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (289, 1, NULL, 59, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (290, 1, NULL, 59, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (291, 1, NULL, 59, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (292, 1, NULL, 59, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (293, 1, NULL, 60, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (294, 1, NULL, 60, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (295, 1, NULL, 60, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (296, 1, NULL, 60, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (297, 1, NULL, 61, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (298, 1, NULL, 61, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (299, 1, NULL, 61, NULL, 3)
GO
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (300, 1, NULL, 61, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (301, 1, NULL, 62, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (302, 1, NULL, 62, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (303, 1, NULL, 62, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (304, 1, NULL, 62, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (305, 1, NULL, 63, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (306, 1, NULL, 63, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (307, 1, NULL, 63, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (308, 1, NULL, 63, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (309, 1, NULL, 64, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (310, 1, NULL, 64, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (311, 1, NULL, 64, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (312, 1, NULL, 64, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (313, 1, 13, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (314, 1, 13, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (315, 1, 13, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (316, 1, 13, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (317, 1, NULL, 65, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (318, 1, NULL, 65, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (319, 1, NULL, 65, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (320, 1, NULL, 65, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (321, 1, NULL, 66, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (322, 1, NULL, 66, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (323, 1, NULL, 66, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (324, 1, NULL, 66, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (325, 1, NULL, 67, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (326, 1, NULL, 67, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (327, 1, NULL, 67, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (328, 1, NULL, 67, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (329, 1, NULL, 68, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (330, 1, NULL, 68, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (331, 1, NULL, 68, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (332, 1, NULL, 68, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (333, 1, NULL, 69, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (334, 1, NULL, 69, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (335, 1, NULL, 69, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (336, 1, NULL, 69, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (337, 1, 16, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (338, 1, 16, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (339, 1, 16, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (340, 1, 16, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (341, 1, NULL, 70, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (342, 1, NULL, 70, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (343, 1, NULL, 70, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (344, 1, NULL, 70, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (345, 1, NULL, 71, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (346, 1, NULL, 71, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (347, 1, NULL, 71, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (348, 1, NULL, 71, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (349, 1, NULL, 72, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (350, 1, NULL, 72, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (351, 1, NULL, 72, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (352, 1, NULL, 72, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (353, 1, NULL, 73, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (354, 1, NULL, 73, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (355, 1, NULL, 73, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (356, 1, NULL, 73, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (357, 1, 18, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (358, 1, 18, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (359, 1, 18, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (360, 1, 18, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (361, 1, NULL, 74, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (362, 1, NULL, 74, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (363, 1, NULL, 74, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (364, 1, NULL, 74, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (365, 1, NULL, 75, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (366, 1, NULL, 75, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (367, 1, NULL, 75, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (368, 1, NULL, 75, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (369, 1, NULL, 76, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (370, 1, NULL, 76, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (371, 1, NULL, 76, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (372, 1, NULL, 76, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (373, 1, NULL, 77, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (374, 1, NULL, 77, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (375, 1, NULL, 77, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (376, 1, NULL, 77, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (433, 2, 1, NULL, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (434, 2, 1, NULL, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (435, 2, 1, NULL, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (436, 2, 1, NULL, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (437, 2, NULL, 1, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (438, 2, NULL, 1, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (439, 2, NULL, 1, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (440, 2, NULL, 1, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (441, 2, NULL, 2, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (442, 2, NULL, 2, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (443, 2, NULL, 2, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (444, 2, NULL, 2, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (445, 2, NULL, 3, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (446, 2, NULL, 3, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (447, 2, NULL, 3, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (448, 2, NULL, 3, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (449, 2, NULL, 4, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (450, 2, NULL, 4, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (451, 2, NULL, 4, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (452, 2, NULL, 4, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (453, 2, NULL, 5, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (454, 2, NULL, 5, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (455, 2, NULL, 5, NULL, 3)
GO
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (456, 2, NULL, 5, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (457, 2, NULL, 6, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (458, 2, NULL, 6, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (459, 2, NULL, 6, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (460, 2, NULL, 6, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (461, 2, NULL, 7, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (462, 2, NULL, 7, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (463, 2, NULL, 7, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (464, 2, NULL, 7, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (465, 2, NULL, 8, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (466, 2, NULL, 8, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (467, 2, NULL, 8, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (468, 2, NULL, 8, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (469, 2, NULL, 9, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (470, 2, NULL, 9, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (471, 2, NULL, 9, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (472, 2, NULL, 9, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (473, 2, NULL, 10, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (474, 2, NULL, 10, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (475, 2, NULL, 10, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (476, 2, NULL, 10, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (477, 2, NULL, 11, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (478, 2, NULL, 11, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (479, 2, NULL, 11, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (480, 2, NULL, 11, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (481, 2, NULL, 12, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (482, 2, NULL, 12, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (483, 2, NULL, 12, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (484, 2, NULL, 12, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (485, 2, NULL, 13, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (486, 2, NULL, 13, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (487, 2, NULL, 13, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (488, 2, NULL, 13, NULL, 4)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (489, 2, NULL, 14, NULL, 1)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (490, 2, NULL, 14, NULL, 2)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (491, 2, NULL, 14, NULL, 3)
INSERT [dbo].[RolePermissions] ([PermissionId], [RoleId], [ParentModuleId], [ChildModuleId], [SubChildModuleId], [ActionId]) VALUES (492, 2, NULL, 14, NULL, 4)
SET IDENTITY_INSERT [dbo].[RolePermissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleId], [RoleName], [RoleDescription], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (1, 1, N'Superadmin', N'test', 1, 0, CAST(N'2025-08-26T12:42:53.163' AS DateTime), 1, CAST(N'2026-04-19T22:51:39.507' AS DateTime), 1, CAST(N'2025-08-26T12:42:53.163' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleId], [RoleName], [RoleDescription], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (2, 2, N'Admin', N'test', 1, 0, CAST(N'2025-08-26T12:42:53.163' AS DateTime), 1, CAST(N'2026-04-19T23:16:40.200' AS DateTime), 1, CAST(N'2025-08-26T12:42:53.163' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleId], [RoleName], [RoleDescription], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (3, 3, N'Manager', N'test', 1, 0, CAST(N'2025-08-26T12:42:53.163' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T12:42:53.163' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleId], [RoleName], [RoleDescription], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (4, 4, N'HR', N'test', 1, 0, CAST(N'2025-08-26T12:42:53.163' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T12:42:53.163' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleId], [RoleName], [RoleDescription], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (5, 5, N'Team Leader', N'test ', 1, 0, CAST(N'2025-08-26T12:42:53.163' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T12:42:53.163' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleId], [RoleName], [RoleDescription], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [SystemAddedOn]) VALUES (6, 6, N'Employee', N'test', 1, 0, CAST(N'2025-08-26T12:42:53.163' AS DateTime), 1, NULL, NULL, CAST(N'2025-08-26T12:42:53.163' AS DateTime))
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[SubChildModules] ON 

INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (1, 15, N'Indicator Categories', N'#', 1, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (2, 15, N'Indicators', N'#', 2, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (3, 15, N'Goal Types', N'#', 3, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (4, 15, N'Employee Goals', N'#', 4, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (5, 15, N'Review Cycles', N'#', 5, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (6, 15, N'Employee Reviews', N'#', 6, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (7, 23, N'Asset Types', N'#', 1, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (8, 23, N'Assets', N'#', 2, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (9, 23, N'Dashboard', N'#', 3, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (10, 23, N'Depreciation', N'#', 4, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (11, 24, N'Training Types', N'#', 1, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (12, 24, N'Training Programs', N'#', 2, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (13, 24, N'Training Sessions', N'#', 3, 1)
INSERT [dbo].[SubChildModules] ([SubChildModuleId], [ChildModuleId], [ModuleName], [RouteUrl], [SortOrder], [IsActive]) VALUES (14, 24, N'Employee Trainings', N'#', 4, 1)
SET IDENTITY_INSERT [dbo].[SubChildModules] OFF
GO
SET IDENTITY_INSERT [dbo].[SubDepartments] ON 

INSERT [dbo].[SubDepartments] ([SubDepartmentID], [DepartmentID], [SubDepartmentName], [SubDepartmentCode], [Description], [HeadOfSubDept], [HeadEmail], [HeadPhone], [NumberOfEmployees], [Notes], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy]) VALUES (2, 65, N'Cloud Service', N'SD002', N'none', N'Rubeus Hagrid', N'aicloud@service.com', N'3432544433', 100, N'none', CAST(N'2025-09-16T14:04:41.027' AS DateTime), NULL, N'John  Doe', NULL)
SET IDENTITY_INSERT [dbo].[SubDepartments] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Branches__1C61B88895A57287]    Script Date: 03-07-2026 11:07:52 AM ******/
ALTER TABLE [dbo].[Branches] ADD UNIQUE NONCLUSTERED 
(
	[BranchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Departme__6EA8896DD3CB0327]    Script Date: 03-07-2026 11:07:52 AM ******/
ALTER TABLE [dbo].[Departments] ADD UNIQUE NONCLUSTERED 
(
	[DepartmentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ModuleAc__BF8232EFE28C3964]    Script Date: 03-07-2026 11:07:52 AM ******/
ALTER TABLE [dbo].[ModuleActions] ADD UNIQUE NONCLUSTERED 
(
	[ActionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__SubDepar__7F7CE2392969E56A]    Script Date: 03-07-2026 11:07:52 AM ******/
ALTER TABLE [dbo].[SubDepartments] ADD  CONSTRAINT [UQ__SubDepar__7F7CE2392969E56A] UNIQUE NONCLUSTERED 
(
	[SubDepartmentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Branches] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ChildModules] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[ChildModules] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ParentModules] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[ParentModules] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SubChildModules] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[SubChildModules] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SubDepartments] ADD  CONSTRAINT [DF__SubDepart__Creat__4BAC3F29]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Branches]  WITH CHECK ADD  CONSTRAINT [FK_Branches_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Branches] CHECK CONSTRAINT [FK_Branches_Companies]
GO
ALTER TABLE [dbo].[ChildModules]  WITH CHECK ADD FOREIGN KEY([ParentModuleId])
REFERENCES [dbo].[ParentModules] ([ParentModuleId])
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Branches] FOREIGN KEY([BranchID])
REFERENCES [dbo].[Branches] ([BranchId])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Branches]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD FOREIGN KEY([ActionId])
REFERENCES [dbo].[ModuleActions] ([ActionId])
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD FOREIGN KEY([ChildModuleId])
REFERENCES [dbo].[ChildModules] ([ChildModuleId])
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD FOREIGN KEY([ParentModuleId])
REFERENCES [dbo].[ParentModules] ([ParentModuleId])
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD FOREIGN KEY([SubChildModuleId])
REFERENCES [dbo].[SubChildModules] ([SubChildModuleId])
GO
ALTER TABLE [dbo].[SubChildModules]  WITH CHECK ADD FOREIGN KEY([ChildModuleId])
REFERENCES [dbo].[ChildModules] ([ChildModuleId])
GO
ALTER TABLE [dbo].[SubDepartments]  WITH CHECK ADD  CONSTRAINT [FK_SubDepartments_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[SubDepartments] CHECK CONSTRAINT [FK_SubDepartments_Departments]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [CHK_SingleModuleLevel] CHECK  (([ParentModuleId] IS NOT NULL AND [ChildModuleId] IS NULL AND [SubChildModuleId] IS NULL OR [ParentModuleId] IS NULL AND [ChildModuleId] IS NOT NULL AND [SubChildModuleId] IS NULL OR [ParentModuleId] IS NULL AND [ChildModuleId] IS NULL AND [SubChildModuleId] IS NOT NULL))
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [CHK_SingleModuleLevel]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBranch]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteBranch]
    @BranchId INT
AS
BEGIN
    DELETE FROM Branches WHERE BranchId = @BranchId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCompany]
    @CompanyId INT
AS
BEGIN
    DELETE FROM Company
    WHERE CompanyId = @CompanyId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDepartment]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteDepartment]
    @DepartmentId INT
AS
BEGIN
    DELETE FROM Departments WHERE DepartmentID = @DepartmentId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSubDepartment]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteSubDepartment]
    @SubDepartmentId INT
AS
BEGIN
    DELETE FROM SubDepartments WHERE SubDepartmentID = @SubDepartmentId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetBranchById]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBranchById]
    @BranchId INT
AS
BEGIN
    SELECT 
        b.BranchId,
        b.CompanyId,
        c.CompanyName,
        b.BranchName,
        b.BranchCode,
        b.Email,
        b.Phone,
        b.AddressLine1,
        b.AddressLine2,
        b.City,
        b.State,
        b.Country,
        b.ZipCode,
        b.FaxNumber,
        b.Website
    FROM Branches b
    INNER JOIN Company c ON b.CompanyId = c.CompanyId
    WHERE b.BranchId = @BranchId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetBranches]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBranches]
AS
BEGIN
    SELECT 
        b.BranchId,
        b.BranchName,
        b.BranchCode,
        b.Email,
        b.Phone,
        b.AddressLine1,
        b.AddressLine2,
        b.City,
        b.State,
        b.Country,
        b.ZipCode,
        b.FaxNumber,
        b.Website,
        c.CompanyName
    FROM Branches b
    INNER JOIN Company c ON b.CompanyId = c.CompanyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCompany]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCompany]
AS
BEGIN
    SELECT * FROM Company
END
GO
/****** Object:  StoredProcedure [dbo].[GetCompanyById]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCompanyById]
    @CompanyId INT
AS
BEGIN
    SELECT *
    FROM Company
    WHERE CompanyId = @CompanyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetDepartmentById]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetDepartmentById]
    @DepartmentId INT
AS
BEGIN
    SELECT 
        d.DepartmentID,
        d.BranchId,
        b.BranchName,
        d.DepartmentName,
        d.DepartmentCode,
        d.HeadEmail,
        d.HeadPhone,
        d.Description,
        d.HeadOfDepartment,
        d.Notes,
        d.NumberOfEmployees
    FROM Departments d
    INNER JOIN Branches b ON d.BranchID = b.BranchId
    WHERE d.DepartmentID = @DepartmentId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetDepartments]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetDepartments]
AS
BEGIN
	SELECT 
        d.DepartmentID,
        d.DepartmentName,
        d.DepartmentCode,
        d.HeadEmail,
        d.HeadPhone,
		d.Description,
		d.HeadOfDepartment,
		d.NumberOfEmployees,
		d.Notes,
        b.BranchName
    FROM Departments d
    INNER JOIN Branches b ON d.BranchID = b.BranchId
end
GO
/****** Object:  StoredProcedure [dbo].[GetSubDepartmentById]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetSubDepartmentById]
    @SubDepartmentId INT
AS
BEGIN
    SELECT 
        sd.SubDepartmentID,
        sd.DepartmentID,
        d.DepartmentName,
        sd.SubDepartmentName,
        sd.SubDepartmentCode,
        sd.HeadEmail,
        sd.HeadPhone,
        sd.Description,
        sd.HeadOfSubDept,
        sd.Notes,
        sd.NumberOfEmployees
    FROM SubDepartments sd
    INNER JOIN Departments d ON sd.DepartmentID = d.DepartmentID
    WHERE sd.SubDepartmentID = @SubDepartmentId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetSubDepartments]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetSubDepartments]
AS
BEGIN
	SELECT 
        sd.SubDepartmentID,
        sd.SubDepartmentName,
        sd.SubDepartmentCode,
        sd.HeadEmail,
        sd.HeadPhone,
		sd.Description,
		sd.HeadOfSubDept,
		sd.NumberOfEmployees,
		sd.Notes,
        d.DepartmentName
    FROM SubDepartments sd
    INNER JOIN Departments d ON sd.DepartmentID = d.DepartmentID
end
GO
/****** Object:  StoredProcedure [dbo].[InsertCompany]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCompany]
    @CompanyName NVARCHAR(200),
    @CompanyCode NVARCHAR(50),
    @RegistrationNumber NVARCHAR(100),
    @CompanyType NVARCHAR(100),
    @IndustryType NVARCHAR(100),
    @TaxId NVARCHAR(100),
    @PrimaryEmail NVARCHAR(200),
    @PrimaryPhone NVARCHAR(20),
    @SecondaryPhone NVARCHAR(20),
    @AddressLine1 NVARCHAR(250),
    @AddressLine2 NVARCHAR(250),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @Country NVARCHAR(100),
    @ZipCode NVARCHAR(20),
    @WorkingDaysPerWeek INT,
    @PrimaryHRName NVARCHAR(200),
    @PrimaryHREmail NVARCHAR(200),
    @PrimaryHRPhone NVARCHAR(20),
    @DefaultCurrency NVARCHAR(50),
    @DefaultTimeZone NVARCHAR(100),
    @FinancialYearStart NVARCHAR(50),
    @FinancialYearEnd NVARCHAR(50),
    @PayrollCycle NVARCHAR(100),
    @DefaultLeavePolicy NVARCHAR(200),
    @ShiftTimings NVARCHAR(200),
    @LogoUrl NVARCHAR(500),
    @TagLine NVARCHAR(200),
    @BrandingColor NVARCHAR(100),
    @Website NVARCHAR(200),
    @LinkedInUrl NVARCHAR(200),
    @TwitterUrl NVARCHAR(200),
    @FacebookUrl NVARCHAR(200),
    @InstagramUrl NVARCHAR(200),
    @ParentCompanyId INT,
    @NumberOfEmployees INT,
    @Certifications NVARCHAR(500),
    @InsuranceDetails NVARCHAR(500),
    @Note NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Company (
        CompanyName, CompanyCode, RegistrationNumber, CompanyType, IndustryType, TaxId,
        PrimaryEmail, PrimaryPhone, SecondaryPhone, AddressLine1, AddressLine2, City, State, Country, ZipCode,
        WorkingDaysPerWeek, PrimaryHRName, PrimaryHREmail, PrimaryHRPhone,
        DefaultCurrency, DefaultTimeZone, FinancialYearStart, FinancialYearEnd, PayrollCycle,
        DefaultLeavePolicy, ShiftTimings,
        LogoUrl, TagLine, BrandingColor, Website, LinkedInUrl, TwitterUrl, FacebookUrl, InstagramUrl,
        ParentCompanyId, NumberOfEmployees, Certifications, InsuranceDetails, Note
    )
    VALUES (
        @CompanyName, @CompanyCode, @RegistrationNumber, @CompanyType, @IndustryType, @TaxId,
        @PrimaryEmail, @PrimaryPhone, @SecondaryPhone, @AddressLine1, @AddressLine2, @City, @State, @Country, @ZipCode,
        @WorkingDaysPerWeek, @PrimaryHRName, @PrimaryHREmail, @PrimaryHRPhone,
        @DefaultCurrency, @DefaultTimeZone, @FinancialYearStart, @FinancialYearEnd, @PayrollCycle,
        @DefaultLeavePolicy, @ShiftTimings,
        @LogoUrl, @TagLine, @BrandingColor, @Website, @LinkedInUrl, @TwitterUrl, @FacebookUrl, @InstagramUrl,
        @ParentCompanyId, @NumberOfEmployees, @Certifications, @InsuranceDetails, @Note
    )
END
GO
/****** Object:  StoredProcedure [dbo].[SaveBranch]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveBranch]
    @CompanyId INT,
    @BranchName NVARCHAR(200),
    @BranchCode NVARCHAR(50),
    @Email NVARCHAR(200),
    @Phone NVARCHAR(50),
    @AddressLine1 NVARCHAR(200),
    @AddressLine2 NVARCHAR(200),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @Country NVARCHAR(100),
    @ZipCode NVARCHAR(20),
    @FaxNumber NVARCHAR(50),
    @Website NVARCHAR(200)
AS
BEGIN
    INSERT INTO Branches
    (
        CompanyId, BranchName, BranchCode, Email, Phone,
        AddressLine1, AddressLine2, City, State, Country,
        ZipCode, FaxNumber, Website
    )
    VALUES
    (
        @CompanyId, @BranchName, @BranchCode, @Email, @Phone,
        @AddressLine1, @AddressLine2, @City, @State, @Country,
        @ZipCode, @FaxNumber, @Website
    )
END
GO
/****** Object:  StoredProcedure [dbo].[SaveDepartment]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveDepartment]
    @DepartmentName NVARCHAR(100),
    @BranchId INT,
    @DepartmentCode NVARCHAR(50),
    @HeadEmail NVARCHAR(100),
    @HeadPhone NVARCHAR(20),
    @Description NVARCHAR(MAX),
    @HeadOfDepartment NVARCHAR(100),
    @NumberOfEmployees INT,
    @Notes NVARCHAR(MAX),
    @CreatedBy NVARCHAR(50)
AS
BEGIN
    INSERT INTO Departments (
        DepartmentName, BranchId, DepartmentCode, HeadEmail, HeadPhone, Description,
        HeadOfDepartment, NumberOfEmployees, Notes, CreatedAt, CreatedBy
    )
    VALUES (
        @DepartmentName, @BranchId, @DepartmentCode, @HeadEmail, @HeadPhone, @Description,
        @HeadOfDepartment, @NumberOfEmployees, @Notes, GETDATE(), @CreatedBy
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SaveSubDepartment]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveSubDepartment]
    @SubDepartmentName NVARCHAR(100),
    @DepartmentId INT,
    @SubDepartmentCode NVARCHAR(50),
    @HeadEmail NVARCHAR(100),
    @HeadPhone NVARCHAR(20),
    @Description NVARCHAR(MAX),
    @HeadOfSubDepartment NVARCHAR(100),
    @NumberOfEmployees INT,
    @Notes NVARCHAR(MAX),
    @CreatedBy NVARCHAR(50)
AS
BEGIN
    INSERT INTO SubDepartments (
        SubDepartmentName, DepartmentID, SubDepartmentCode, HeadEmail, HeadPhone, Description,
        HeadOfSubDept, NumberOfEmployees, Notes, CreatedAt, CreatedBy
    )
    VALUES (
        @SubDepartmentName, @DepartmentId, @SubDepartmentCode, @HeadEmail, @HeadPhone, @Description,
        @HeadOfSubDepartment, @NumberOfEmployees, @Notes, GETDATE(), @CreatedBy
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetSidebarModules]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetSidebarModules]
AS
BEGIN
    -- SET NOCOUNT ON prevents extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON;

    -- 1. Get Active Parent Modules
    SELECT ParentModuleId, ModuleName, IconClass, RouteUrl, SortOrder 
    FROM ParentModules 
    WHERE IsActive = 1 
    ORDER BY SortOrder;

    -- 2. Get Active Child Modules
    SELECT ChildModuleId, ParentModuleId, ModuleName, RouteUrl, SortOrder 
    FROM ChildModules 
    WHERE IsActive = 1 
    ORDER BY SortOrder;

    -- 3. Get Active Sub-Child Modules
    SELECT SubChildModuleId, ChildModuleId, ModuleName, RouteUrl, SortOrder 
    FROM SubChildModules 
    WHERE IsActive = 1 
    ORDER BY SortOrder;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetSidebarModulesByRole]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetSidebarModulesByRole]
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Gather all Sub-Children the user explicitly has access to
    SELECT DISTINCT SubChildModuleId INTO #AllowedSubChildren 
    FROM RolePermissions 
    WHERE RoleId = @RoleId AND SubChildModuleId IS NOT NULL;

    -- 2. Gather all Children (Direct access OR they have access to a Sub-Child underneath it)
    SELECT DISTINCT ChildModuleId INTO #AllowedChildren 
    FROM RolePermissions 
    WHERE RoleId = @RoleId AND ChildModuleId IS NOT NULL
    UNION
    SELECT ChildModuleId FROM SubChildModules WHERE SubChildModuleId IN (SELECT SubChildModuleId FROM #AllowedSubChildren);

    -- 3. Gather all Parents (Direct access OR they have access to a Child underneath it)
    SELECT DISTINCT ParentModuleId INTO #AllowedParents 
    FROM RolePermissions 
    WHERE RoleId = @RoleId AND ParentModuleId IS NOT NULL
    UNION
    SELECT ParentModuleId FROM ChildModules WHERE ChildModuleId IN (SELECT ChildModuleId FROM #AllowedChildren);

    -- RESULT 1: Return Allowed Parents
    SELECT ParentModuleId, ModuleName, IconClass, RouteUrl, SortOrder 
    FROM ParentModules 
    WHERE IsActive = 1 AND ParentModuleId IN (SELECT ParentModuleId FROM #AllowedParents)
    ORDER BY SortOrder;

    -- RESULT 2: Return Allowed Children
    SELECT ChildModuleId, ParentModuleId, ModuleName, RouteUrl, SortOrder 
    FROM ChildModules 
    WHERE IsActive = 1 AND ChildModuleId IN (SELECT ChildModuleId FROM #AllowedChildren)
    ORDER BY SortOrder;

    -- RESULT 3: Return Allowed Sub-Children
    SELECT SubChildModuleId, ChildModuleId, ModuleName, RouteUrl, SortOrder 
    FROM SubChildModules 
    WHERE IsActive = 1 AND SubChildModuleId IN (SELECT SubChildModuleId FROM #AllowedSubChildren)
    ORDER BY SortOrder;

    -- Clean up temp tables
    DROP TABLE #AllowedSubChildren;
    DROP TABLE #AllowedChildren;
    DROP TABLE #AllowedParents;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertRole]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 2. INSERT Stored Procedure
CREATE PROCEDURE [dbo].[sp_InsertRole]
    @RoleId NVARCHAR(100), -- The string code from UI (e.g. 'SYS-ADM')
    @RoleName NVARCHAR(100),
    @RoleDescription NVARCHAR(MAX),
    @CreatedBy INT = NULL,
    @Permissions RolePermissionTVP READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @NewId INT;

        -- Insert into Roles table
        INSERT INTO [Roles] (RoleId, RoleName, RoleDescription, IsActive, IsDeleted, CreatedDate, CreatedBy, SystemAddedOn)
        VALUES (@RoleId, @RoleName, @RoleDescription, 1, 0, GETDATE(), @CreatedBy, GETDATE());
        
        SET @NewId = SCOPE_IDENTITY();

        -- Bulk Insert Permissions from TVP
        INSERT INTO [RolePermissions] (RoleId, ParentModuleId, ChildModuleId, SubChildModuleId, ActionId)
        SELECT @NewId, ParentModuleId, ChildModuleId, SubChildModuleId, ActionId
        FROM @Permissions;

        COMMIT TRANSACTION;
        SELECT @NewId AS Id; 
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateRole]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 3. UPDATE Stored Procedure
CREATE PROCEDURE [dbo].[sp_UpdateRole]
    @Id INT, -- The PK
    @RoleId NVARCHAR(100), -- The string code
    @RoleName NVARCHAR(100),
    @RoleDescription NVARCHAR(MAX),
    @UpdatedBy INT = NULL,
    @Permissions RolePermissionTVP READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Update Role details
        UPDATE [Roles] 
        SET RoleId = @RoleId, 
            RoleName = @RoleName, 
            RoleDescription = @RoleDescription, 
            UpdatedDate = GETDATE(),
            UpdatedBy = @UpdatedBy
        WHERE Id = @Id;

        -- Wipe old permissions for this role
        DELETE FROM [RolePermissions] WHERE RoleId = @Id;

        -- Bulk Insert fresh permissions from TVP
        INSERT INTO [RolePermissions] (RoleId, ParentModuleId, ChildModuleId, SubChildModuleId, ActionId)
        SELECT @Id, ParentModuleId, ChildModuleId, SubChildModuleId, ActionId
        FROM @Permissions;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateRoleStatus]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 4. STATUS / SOFT DELETE Stored Procedure
CREATE PROCEDURE [dbo].[sp_UpdateRoleStatus]
    @Id INT,
    @IsActive BIT = NULL,
    @IsDeleted BIT = NULL,
    @UpdatedBy INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [Roles]
    SET 
        IsActive = ISNULL(@IsActive, IsActive),
        IsDeleted = ISNULL(@IsDeleted, IsDeleted),
        UpdatedDate = GETDATE(),
        UpdatedBy = ISNULL(@UpdatedBy, UpdatedBy)
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateBranch]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateBranch]
    @BranchId INT,
    @CompanyId INT,
    @BranchName NVARCHAR(200),
    @BranchCode NVARCHAR(100),
    @Email NVARCHAR(200),
    @Phone NVARCHAR(15),
    @AddressLine1 NVARCHAR(250),
    @AddressLine2 NVARCHAR(250),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @Country NVARCHAR(100),
    @ZipCode NVARCHAR(20),
    @FaxNumber NVARCHAR(50),
    @Website NVARCHAR(200)
AS
BEGIN
    UPDATE Branches
    SET 
        CompanyId = @CompanyId,
        BranchName = @BranchName,
        BranchCode = @BranchCode,
        Email = @Email,
        Phone = @Phone,
        AddressLine1 = @AddressLine1,
        AddressLine2 = @AddressLine2,
        City = @City,
        State = @State,
        Country = @Country,
        ZipCode = @ZipCode,
        FaxNumber = @FaxNumber,
        Website = @Website
    WHERE BranchId = @BranchId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCompany]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCompany]
    @CompanyId INT,
    @CompanyName NVARCHAR(200),
    @CompanyCode NVARCHAR(50),
    @RegistrationNumber NVARCHAR(100),
    @CompanyType NVARCHAR(100),
    @IndustryType NVARCHAR(100),
    @TaxId NVARCHAR(100),
    @PrimaryEmail NVARCHAR(200),
    @PrimaryPhone NVARCHAR(20),
    @SecondaryPhone NVARCHAR(20),
    @AddressLine1 NVARCHAR(200),
    @AddressLine2 NVARCHAR(200),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @Country NVARCHAR(100),
    @ZipCode NVARCHAR(20),
    @WorkingDaysPerWeek INT,
    @PrimaryHRName NVARCHAR(100),
    @PrimaryHREmail NVARCHAR(100),
    @PrimaryHRPhone NVARCHAR(20),
    @DefaultCurrency NVARCHAR(50),
    @DefaultTimeZone NVARCHAR(100),
    @FinancialYearStart NVARCHAR(20),
    @FinancialYearEnd NVARCHAR(20),
    @PayrollCycle NVARCHAR(50),
    @DefaultLeavePolicy NVARCHAR(200),
    @ShiftTimings NVARCHAR(100),
    @LogoUrl NVARCHAR(200),
    @TagLine NVARCHAR(200),
    @BrandingColor NVARCHAR(50),
    @Website NVARCHAR(200),
    @LinkedInUrl NVARCHAR(200),
    @TwitterUrl NVARCHAR(200),
    @FacebookUrl NVARCHAR(200),
    @InstagramUrl NVARCHAR(200),
    @ParentCompanyId INT,
    @NumberOfEmployees INT,
    @Certifications NVARCHAR(200),
    @InsuranceDetails NVARCHAR(200),
    @Note NVARCHAR(500)
AS
BEGIN
    UPDATE Company
    SET CompanyName = @CompanyName,
        CompanyCode = @CompanyCode,
        RegistrationNumber = @RegistrationNumber,
        CompanyType = @CompanyType,
        IndustryType = @IndustryType,
        TaxId = @TaxId,
        PrimaryEmail = @PrimaryEmail,
        PrimaryPhone = @PrimaryPhone,
        SecondaryPhone = @SecondaryPhone,
        AddressLine1 = @AddressLine1,
        AddressLine2 = @AddressLine2,
        City = @City,
        State = @State,
        Country = @Country,
        ZipCode = @ZipCode,
        WorkingDaysPerWeek = @WorkingDaysPerWeek,
        PrimaryHRName = @PrimaryHRName,
        PrimaryHREmail = @PrimaryHREmail,
        PrimaryHRPhone = @PrimaryHRPhone,
        DefaultCurrency = @DefaultCurrency,
        DefaultTimeZone = @DefaultTimeZone,
        FinancialYearStart = @FinancialYearStart,
        FinancialYearEnd = @FinancialYearEnd,
        PayrollCycle = @PayrollCycle,
        DefaultLeavePolicy = @DefaultLeavePolicy,
        ShiftTimings = @ShiftTimings,
        LogoUrl = @LogoUrl,
        TagLine = @TagLine,
        BrandingColor = @BrandingColor,
        Website = @Website,
        LinkedInUrl = @LinkedInUrl,
        TwitterUrl = @TwitterUrl,
        FacebookUrl = @FacebookUrl,
        InstagramUrl = @InstagramUrl,
        ParentCompanyId = @ParentCompanyId,
        NumberOfEmployees = @NumberOfEmployees,
        Certifications = @Certifications,
        InsuranceDetails = @InsuranceDetails,
        Note = @Note
    WHERE CompanyId = @CompanyId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDepartment]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDepartment]
    @DepartmentId INT,
    @DepartmentName NVARCHAR(100),
    @BranchId INT,
    @DepartmentCode NVARCHAR(50),
    @HeadEmail NVARCHAR(100),
    @HeadPhone NVARCHAR(20),
    @Description NVARCHAR(MAX),
    @HeadOfDepartment NVARCHAR(100),
    @NumberOfEmployees INT,
    @Notes NVARCHAR(MAX),
    @UpdatedBy NVARCHAR(50)
AS
BEGIN
    UPDATE Departments
    SET DepartmentName = @DepartmentName,
        BranchId = @BranchId,
        DepartmentCode = @DepartmentCode,
        HeadEmail = @HeadEmail,
        HeadPhone = @HeadPhone,
        Description = @Description,
        HeadOfDepartment = @HeadOfDepartment,
        NumberOfEmployees = @NumberOfEmployees,
        Notes = @Notes,
        UpdatedAt = GETDATE(),
        UpdatedBy = @UpdatedBy
    WHERE DepartmentId = @DepartmentId;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateSubDepartment]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSubDepartment]
    @SubDepartmentId INT,
    @SubDepartmentName NVARCHAR(100),
    @DepartmentId INT,
    @SubDepartmentCode NVARCHAR(50),
    @HeadEmail NVARCHAR(100),
    @HeadPhone NVARCHAR(20),
    @Description NVARCHAR(MAX),
    @HeadOfSubDepartment NVARCHAR(100),
    @NumberOfEmployees INT,
    @Notes NVARCHAR(MAX),
    @UpdatedBy NVARCHAR(50)
AS
BEGIN
    UPDATE SubDepartments
    SET SubDepartmentName = @SubDepartmentName,
        DepartmentId = @DepartmentId,
        SubDepartmentCode = @SubDepartmentCode,
        HeadEmail = @HeadEmail,
        HeadPhone = @HeadPhone,
        Description = @Description,
        HeadOfSubDept = @HeadOfSubDepartment,
        NumberOfEmployees = @NumberOfEmployees,
        Notes = @Notes,
        UpdatedAt = GETDATE(),
        UpdatedBy = @UpdatedBy
    WHERE SubDepartmentID = @SubDepartmentId;
END;
GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 03-07-2026 11:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateUser]
    @UserIdentifier NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if valid login exists
    IF EXISTS (
        SELECT 1 
        FROM LoginDetails L
        INNER JOIN EmployeeDetails E ON L.EmpId = E.Id
        WHERE (L.Email = @UserIdentifier OR L.Phone = @UserIdentifier OR L.Username = @UserIdentifier)
          AND L.Password = @Password
          AND L.IsActive = 1
    )
    BEGIN
        -- Update LastLogin timestamp
        UPDATE LoginDetails
        SET LastLogin = GETDATE()
        WHERE (Email = @UserIdentifier OR Phone = @UserIdentifier OR Username = @UserIdentifier)
          AND Password = @Password;

        -- Return combined data
        SELECT 
            L.Id,
            L.EmpId,
            L.Username,
            L.Email,
            L.Phone,
            L.Token,
            L.ExpiryToken,
            L.LastLogin,
            L.RoleType, -- This is the string code (e.g. SYS-ADM)
            R.RoleId AS RoleIntId, -- ADDED THIS: The integer Primary Key for the sidebar!
            R.RoleName,
            E.*
        FROM LoginDetails L
        INNER JOIN EmployeeDetails E ON L.EmpId = E.Id
        INNER JOIN Roles R ON L.RoleType = R.RoleId
        WHERE (L.Email = @UserIdentifier OR L.Phone = @UserIdentifier OR L.Username = @UserIdentifier)
          AND L.Password = @Password
          AND L.IsActive = 1;
    END
    ELSE
    BEGIN
        -- Return nothing if invalid
        SELECT NULL AS LoginId, 'Invalid credentials' AS Message;
    END
END
GO
