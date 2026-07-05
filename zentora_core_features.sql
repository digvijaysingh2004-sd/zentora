-- =========================================================================
-- Zentora HRMS - Core Features Tables Creation and Seed Data Script
-- Run this script on your Zentora database to enable:
-- Staff, HR Management, Recruitment, Leave, Attendance, and Payroll Tables
-- =========================================================================

USE Zentora;
GO

-- 1. Create Designations Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Designations')
BEGIN
    CREATE TABLE dbo.Designations (
        DesignationId INT IDENTITY(1,1) PRIMARY KEY,
        DesignationName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(MAX),
        IsActive BIT DEFAULT 1
    );
    INSERT INTO dbo.Designations (DesignationName, Description, IsActive) VALUES 
    (N'Founder', N'Company founder and executive.', 1),
    (N'CEO', N'Chief Executive Officer.', 1),
    (N'Head Manager', N'Manager of departmental operations.', 1),
    (N'HR Manager', N'Human Resources operations manager.', 1),
    (N'Software Developer', N'Builds and maintains software systems.', 1);
    
    PRINT 'Created and seeded dbo.Designations table.';
END
GO

-- 2. Create Awards Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Awards')
BEGIN
    CREATE TABLE dbo.Awards (
        AwardId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        AwardType NVARCHAR(100) NOT NULL,
        Gift NVARCHAR(100),
        CashPrice DECIMAL(18,2),
        AwardDate DATE,
        Description NVARCHAR(MAX)
    );
    INSERT INTO dbo.Awards (EmployeeId, AwardType, Gift, CashPrice, AwardDate, Description) VALUES 
    (6, N'Employee of the Month', N'Certificate & Gift Card', 500.00, '2026-06-01', N'Excellent performance in software delivery.');
    
    PRINT 'Created and seeded dbo.Awards table.';
END
GO

-- 3. Create Promotions Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Promotions')
BEGIN
    CREATE TABLE dbo.Promotions (
        PromotionId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        DesignationId INT NOT NULL,
        PromotionDate DATE,
        Description NVARCHAR(MAX)
    );
    INSERT INTO dbo.Promotions (EmployeeId, DesignationId, PromotionDate, Description) VALUES 
    (6, 3, '2026-07-01', N'Promoted to Head Manager due to continuous contributions.');
    
    PRINT 'Created and seeded dbo.Promotions table.';
END
GO

-- 4. Create Announcements Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Announcements')
BEGIN
    CREATE TABLE dbo.Announcements (
        AnnouncementId INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Description NVARCHAR(MAX),
        StartDate DATE,
        EndDate DATE,
        CreatedBy NVARCHAR(100)
    );
    INSERT INTO dbo.Announcements (Title, Description, StartDate, EndDate, CreatedBy) VALUES 
    (N'Quarterly General Meeting', N'All hands meeting scheduled for July 15th at 10 AM.', '2026-07-01', '2026-07-15', N'CEO');
    
    PRINT 'Created and seeded dbo.Announcements table.';
END
GO

-- 5. Create Assets Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Assets')
BEGIN
    CREATE TABLE dbo.Assets (
        AssetId INT IDENTITY(1,1) PRIMARY KEY,
        AssetName NVARCHAR(100) NOT NULL,
        AssetCode NVARCHAR(50) NOT NULL,
        EmployeeId INT,
        PurchaseDate DATE,
        WarrantyDate DATE,
        Status NVARCHAR(50)
    );
    INSERT INTO dbo.Assets (AssetName, AssetCode, EmployeeId, PurchaseDate, WarrantyDate, Status) VALUES 
    (N'Dell Latitude Laptop', N'AST-001', 6, '2025-01-10', '2028-01-10', N'Working'),
    (N'iPhone 15 Pro', N'AST-002', 2, '2025-05-12', '2027-05-12', N'Working');
    
    PRINT 'Created and seeded dbo.Assets table.';
END
GO

-- 6. Create JobPostings Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'JobPostings')
BEGIN
    CREATE TABLE dbo.JobPostings (
        JobId INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(150) NOT NULL,
        JobType NVARCHAR(50),
        Location NVARCHAR(100),
        NoOfVacancies INT,
        ExperienceRequired NVARCHAR(50),
        Description NVARCHAR(MAX),
        Status NVARCHAR(50)
    );
    INSERT INTO dbo.JobPostings (Title, JobType, Location, NoOfVacancies, ExperienceRequired, Description, Status) VALUES 
    (N'Senior C# Developer', N'Full-Time', N'Noida HQ', 3, N'5+ Years', N'Responsible for building ASP.NET MVC and Web API applications.', N'Active'),
    (N'HR Executive', N'Full-Time', N'Delhi NCR', 1, N'2+ Years', N'Responsible for hiring and onboarding processes.', N'Active');
    
    PRINT 'Created and seeded dbo.JobPostings table.';
END
GO

-- 7. Create Candidates Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Candidates')
BEGIN
    CREATE TABLE dbo.Candidates (
        CandidateId INT IDENTITY(1,1) PRIMARY KEY,
        JobId INT NOT NULL,
        FullName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(50),
        ResumeUrl NVARCHAR(200),
        ApplicationStatus NVARCHAR(50),
        AppliedDate DATE
    );
    INSERT INTO dbo.Candidates (JobId, FullName, Email, Phone, ResumeUrl, ApplicationStatus, AppliedDate) VALUES 
    (1, N'Sarah Connor', N'sarah@gmail.com', N'9898989898', N'/resumes/sarah_resume.pdf', N'Shortlisted', '2026-06-25'),
    (1, N'James Watson', N'james@gmail.com', N'9797979797', N'/resumes/james_resume.pdf', N'Applied', '2026-07-01');
    
    PRINT 'Created and seeded dbo.Candidates table.';
END
GO

-- 8. Create LeaveApplications Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LeaveApplications')
BEGIN
    CREATE TABLE dbo.LeaveApplications (
        LeaveApplicationId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        LeaveCategoryId INT NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NOT NULL,
        Reason NVARCHAR(MAX),
        Status NVARCHAR(50),
        AppliedDate DATE
    );
    INSERT INTO dbo.LeaveApplications (EmployeeId, LeaveCategoryId, StartDate, EndDate, Reason, Status, AppliedDate) VALUES 
    (6, 1, '2026-07-10', '2026-07-12', N'Family emergency leave request.', N'Pending', '2026-07-02');
    
    PRINT 'Created and seeded dbo.LeaveApplications table.';
END
GO

-- 9. Create AttendanceRecords Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AttendanceRecords')
BEGIN
    CREATE TABLE dbo.AttendanceRecords (
        AttendanceId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        AttendanceDate DATE NOT NULL,
        ClockIn TIME NOT NULL,
        ClockOut TIME,
        Status NVARCHAR(50)
    );
    INSERT INTO dbo.AttendanceRecords (EmployeeId, AttendanceDate, ClockIn, ClockOut, Status) VALUES 
    (6, '2026-07-01', '09:05:00', '18:02:00', N'Present'),
    (6, '2026-07-02', '09:12:00', '18:05:00', N'Present'),
    (6, '2026-07-03', '09:00:00', NULL, N'Present');
    
    PRINT 'Created and seeded dbo.AttendanceRecords table.';
END
GO

-- 10. Create SalaryComponents Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SalaryComponents')
BEGIN
    CREATE TABLE dbo.SalaryComponents (
        ComponentId INT IDENTITY(1,1) PRIMARY KEY,
        ComponentName NVARCHAR(100) NOT NULL,
        ComponentType NVARCHAR(50),
        Description NVARCHAR(MAX)
    );
    INSERT INTO dbo.SalaryComponents (ComponentName, ComponentType, Description) VALUES 
    (N'Basic Salary', N'Earning', N'Base pay rate.'),
    (N'HRA', N'Earning', N'House Rent Allowance.'),
    (N'Provident Fund', N'Deduction', N'PF deduction.');
    
    PRINT 'Created and seeded dbo.SalaryComponents table.';
END
GO

-- 11. Create EmployeeSalaries Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeSalaries')
BEGIN
    CREATE TABLE dbo.EmployeeSalaries (
        SalaryId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        BasicSalary DECIMAL(18,2) NOT NULL,
        Allowance DECIMAL(18,2) DEFAULT 0,
        Deduction DECIMAL(18,2) DEFAULT 0
    );
    INSERT INTO dbo.EmployeeSalaries (EmployeeId, BasicSalary, Allowance, Deduction) VALUES 
    (6, 60000.00, 5000.00, 3000.00),
    (5, 80000.00, 8000.00, 4000.00);
    
    PRINT 'Created and seeded dbo.EmployeeSalaries table.';
END
GO

-- 12. Create PayrollRuns Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PayrollRuns')
BEGIN
    CREATE TABLE dbo.PayrollRuns (
        PayrollRunId INT IDENTITY(1,1) PRIMARY KEY,
        MonthYear NVARCHAR(50) NOT NULL,
        ProcessedDate DATE NOT NULL,
        Status NVARCHAR(50)
    );
    INSERT INTO dbo.PayrollRuns (MonthYear, ProcessedDate, Status) VALUES 
    (N'06-2026', '2026-06-30', N'Paid');
    
    PRINT 'Created and seeded dbo.PayrollRuns table.';
END
GO

-- 13. Update Route URLs in ChildModules Table to direct to new features
UPDATE ChildModules SET RouteUrl = '/Staff/Users' WHERE ChildModuleId = 8;
UPDATE ChildModules SET RouteUrl = '/Staff/Employees' WHERE ChildModuleId = 10;
UPDATE ChildModules SET RouteUrl = '/HR/Designations' WHERE ChildModuleId = 11;
UPDATE ChildModules SET RouteUrl = '/HR/Awards' WHERE ChildModuleId = 13;
UPDATE ChildModules SET RouteUrl = '/HR/Promotions' WHERE ChildModuleId = 14;
UPDATE ChildModules SET RouteUrl = '/HR/Announcements' WHERE ChildModuleId = 22;
UPDATE ChildModules SET RouteUrl = '/HR/Assets' WHERE ChildModuleId = 23;
UPDATE ChildModules SET RouteUrl = '/Recruitment/JobPostings' WHERE ChildModuleId = 29;
UPDATE ChildModules SET RouteUrl = '/Recruitment/Candidates' WHERE ChildModuleId = 31;
UPDATE ChildModules SET RouteUrl = '/Leave/LeaveApplications' WHERE ChildModuleId = 59;
UPDATE ChildModules SET RouteUrl = '/Leave/LeaveBalances' WHERE ChildModuleId = 60;
UPDATE ChildModules SET RouteUrl = '/Attendance/AttendanceRecords' WHERE ChildModuleId = 63;
UPDATE ChildModules SET RouteUrl = '/Payroll/SalaryComponents' WHERE ChildModuleId = 66;
UPDATE ChildModules SET RouteUrl = '/Payroll/EmployeeSalaries' WHERE ChildModuleId = 67;
UPDATE ChildModules SET RouteUrl = '/Payroll/PayrollRuns' WHERE ChildModuleId = 68;
UPDATE ChildModules SET RouteUrl = '/Payroll/Payslips' WHERE ChildModuleId = 69;
GO
