-- =========================================================================
-- Zentora HRMS - Missing Tables Creation and Seed Data Script
-- Run this script on your Zentora database to create and seed missing tables
-- =========================================================================

USE Zentora;
GO

-- 1. Create HolidayCategory Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HolidayCategory')
BEGIN
    CREATE TABLE dbo.HolidayCategory (
        HolidayCategoryId INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) NOT NULL,
        IsActive BIT DEFAULT 1
    );
    
    INSERT INTO dbo.HolidayCategory (CategoryName, IsActive) VALUES 
    (N'National Holidays', 1),
    (N'Regional Holidays', 1),
    (N'Company Foundation Day', 1);
    
    PRINT 'Created and seeded dbo.HolidayCategory table.';
END
ELSE
BEGIN
    PRINT 'dbo.HolidayCategory table already exists.';
END
GO

-- 2. Create HolidayType Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HolidayType')
BEGIN
    CREATE TABLE dbo.HolidayType (
        HolidayTypeId INT IDENTITY(1,1) PRIMARY KEY,
        TypeName NVARCHAR(100) NOT NULL,
        IsActive BIT DEFAULT 1
    );
    
    INSERT INTO dbo.HolidayType (TypeName, IsActive) VALUES 
    (N'Public Holiday', 1),
    (N'Restricted Holiday', 1),
    (N'Observance', 1);
    
    PRINT 'Created and seeded dbo.HolidayType table.';
END
ELSE
BEGIN
    PRINT 'dbo.HolidayType table already exists.';
END
GO

-- 3. Create LeaveCategory Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LeaveCategory')
BEGIN
    CREATE TABLE dbo.LeaveCategory (
        LeaveCategoryId INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) NOT NULL,
        TotalDays INT NOT NULL,
        IsActive BIT DEFAULT 1
    );
    
    INSERT INTO dbo.LeaveCategory (CategoryName, TotalDays, IsActive) VALUES 
    (N'Sick Leave', 12, 1),
    (N'Casual Leave', 12, 1),
    (N'Earned Leave', 15, 1),
    (N'Maternity Leave', 90, 1);
    
    PRINT 'Created and seeded dbo.LeaveCategory table.';
END
ELSE
BEGIN
    PRINT 'dbo.LeaveCategory table already exists.';
END
GO

-- 4. Create Shifts Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Shifts')
BEGIN
    CREATE TABLE dbo.Shifts (
        ShiftId INT IDENTITY(1,1) PRIMARY KEY,
        ShiftName NVARCHAR(100) NOT NULL,
        StartTime VARCHAR(50) NOT NULL,
        EndTime VARCHAR(50) NOT NULL,
        IsActive BIT DEFAULT 1
    );
    
    INSERT INTO dbo.Shifts (ShiftName, StartTime, EndTime, IsActive) VALUES 
    (N'General Shift', '09:00 AM', '06:00 PM', 1),
    (N'Morning Shift', '06:00 AM', '02:00 PM', 1),
    (N'Night Shift', '10:00 PM', '06:00 AM', 1);
    
    PRINT 'Created and seeded dbo.Shifts table.';
END
ELSE
BEGIN
    PRINT 'dbo.Shifts table already exists.';
END
GO

-- 5. Create Projects Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Projects')
BEGIN
    CREATE TABLE dbo.Projects (
        ProjectId INT IDENTITY(1,1) PRIMARY KEY,
        ProjectName NVARCHAR(100) NOT NULL,
        ProjectCode NVARCHAR(50) NOT NULL,
        Description NVARCHAR(MAX),
        StartDate DATE,
        EndDate DATE,
        Status NVARCHAR(50),
        IsActive BIT DEFAULT 1
    );
    
    INSERT INTO dbo.Projects (ProjectName, ProjectCode, Description, StartDate, EndDate, Status, IsActive) VALUES 
    (N'Zentora HRMS Web App', N'PRJ001', N'Development of new HRMS platform.', '2026-01-01', '2026-12-31', N'In Progress', 1),
    (N'Client Portal Migrations', N'PRJ002', N'Migrating old PHP portals to modern Stack.', '2026-03-01', '2026-08-31', N'In Progress', 1);
    
    PRINT 'Created and seeded dbo.Projects table.';
END
ELSE
BEGIN
    PRINT 'dbo.Projects table already exists.';
END
GO
