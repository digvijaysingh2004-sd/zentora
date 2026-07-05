$projectDir = "C:\digvijayProjects\Projects\zentora\zentoraHRMS"

# 1. Update Existing Controllers
$recruitmentController = "$projectDir\Controllers\RecruitmentController.cs"
(Get-Content $recruitmentController) -replace "}", "    public ActionResult RecruitmentAgencies() { return View(); }`r`n        public ActionResult RecruitmentProcesses() { return View(); }`r`n        public ActionResult InterviewTypes() { return View(); }`r`n        public ActionResult Interviews() { return View(); }`r`n        public ActionResult BackgroundChecks() { return View(); }`r`n    }`r`n}" | Set-Content $recruitmentController

$leaveController = "$projectDir\Controllers\LeaveController.cs"
(Get-Content $leaveController) -replace "}", "    public ActionResult LeavePolicies() { return View(); }`r`n    }`r`n}" | Set-Content $leaveController

$attendanceController = "$projectDir\Controllers\AttendanceController.cs"
(Get-Content $attendanceController) -replace "}", "    public ActionResult AttendancePolicies() { return View(); }`r`n        public ActionResult AttendanceRegularizations() { return View(); }`r`n    }`r`n}" | Set-Content $attendanceController

# 2. Create New Controllers
$controllers = @{
    "DocumentController" = "DocumentCategories", "DocumentTypes", "HRDocuments", "Acknowledgments", "DocumentTemplates"
    "MeetingController" = "MeetingTypes", "MeetingRooms", "Meetings", "MeetingAttendees", "MeetingMinutes", "ActionItems"
    "TimeTrackingController" = "TimeEntries"
    "LandingPageController" = "LandingPage", "CustomPages", "ContactInquiries", "Newsletter"
}

foreach ($c in $controllers.Keys) {
    $content = "using System.Web.Mvc;`r`n`r`nnamespace zentoraHRMS.Controllers`r`n{`r`n    public class $c : Controller`r`n    {`r`n"
    foreach ($action in $controllers[$c]) {
        $content += "        public ActionResult $action() { return View(); }`r`n"
    }
    $content += "    }`r`n}"
    Set-Content "$projectDir\Controllers\$c.cs" $content
}

# 3. Create Views
$viewsToCreate = @(
    "Recruitment\RecruitmentAgencies", "Recruitment\RecruitmentProcesses", "Recruitment\InterviewTypes", "Recruitment\Interviews", "Recruitment\BackgroundChecks",
    "Contract\ContractTypes", "Contract\VendorContracts", "Contract\EmployeeContracts", "Contract\ContractTemplates",
    "Document\DocumentCategories", "Document\DocumentTypes", "Document\HRDocuments", "Document\Acknowledgments", "Document\DocumentTemplates",
    "Meeting\MeetingTypes", "Meeting\MeetingRooms", "Meeting\Meetings", "Meeting\MeetingAttendees", "Meeting\MeetingMinutes", "Meeting\ActionItems",
    "Leave\LeavePolicies",
    "Attendance\AttendancePolicies", "Attendance\AttendanceRegularizations",
    "TimeTracking\TimeEntries",
    "LandingPage\LandingPage", "LandingPage\CustomPages", "LandingPage\ContactInquiries", "LandingPage\Newsletter"
)

foreach ($v in $viewsToCreate) {
    $folder = Split-Path "$projectDir\Views\$v"
    if (!(Test-Path $folder)) { New-Item -ItemType Directory -Force -Path $folder | Out-Null }
    
    $viewName = Split-Path $v -Leaf
    $content = "@{`r`n    ViewBag.Title = `"$viewName`";`r`n}`r`n`r`n<div class=`"container-fluid`">`r`n    <div class=`"row justify-content-center py-5`">`r`n        <div class=`"col-xxl-5 col-xl-7 text-center`">`r`n            <h3 class=`"fw-bold`">@ViewBag.Title</h3>`r`n            <p class=`"text-muted`">This module is currently under development.</p>`r`n        </div>`r`n    </div>`r`n</div>"
    Set-Content "$projectDir\Views\$v.cshtml" $content
}

# 4. Add to csproj (Find <ItemGroup> with Views\ and append)
$csprojPath = "$projectDir\zentoraHRMS.csproj"
$csprojContent = Get-Content $csprojPath -Raw

$itemsToAdd = ""
foreach ($v in $viewsToCreate) {
    $itemsToAdd += "    <Content Include=`"Views\$v.cshtml`" />`r`n"
}
$itemsToAdd += "    <Compile Include=`"Controllers\DocumentController.cs`" />`r`n"
$itemsToAdd += "    <Compile Include=`"Controllers\MeetingController.cs`" />`r`n"
$itemsToAdd += "    <Compile Include=`"Controllers\TimeTrackingController.cs`" />`r`n"
$itemsToAdd += "    <Compile Include=`"Controllers\ContractController.cs`" />`r`n"
$itemsToAdd += "    <Compile Include=`"Controllers\LandingPageController.cs`" />`r`n"

$csprojContent = $csprojContent -replace "(<Content Include=`"Views\\Payroll\\Payslips\.cshtml`" />)", "`$1`r`n$itemsToAdd"
Set-Content $csprojPath $csprojContent -NoNewline

# 5. Execute SQL
$sql = @"
UPDATE ChildModules SET RouteUrl = '/Recruitment/RecruitmentAgencies' WHERE ModuleName = 'Recruitment Agencies';
UPDATE ChildModules SET RouteUrl = '/Recruitment/RecruitmentProcesses' WHERE ModuleName = 'Recruitment Processes';
UPDATE ChildModules SET RouteUrl = '/Recruitment/InterviewTypes' WHERE ModuleName = 'Interview Types';
UPDATE ChildModules SET RouteUrl = '/Recruitment/Interviews' WHERE ModuleName = 'Interviews';
UPDATE ChildModules SET RouteUrl = '/Recruitment/BackgroundChecks' WHERE ModuleName = 'Background Checks';
UPDATE ChildModules SET RouteUrl = '/Contract/ContractTypes' WHERE ModuleName = 'Contract Types';
UPDATE ChildModules SET RouteUrl = '/Contract/VendorContracts' WHERE ModuleName = 'Vendor Contracts';
UPDATE ChildModules SET RouteUrl = '/Contract/EmployeeContracts' WHERE ModuleName = 'Employee Contracts';
UPDATE ChildModules SET RouteUrl = '/Contract/ContractTemplates' WHERE ModuleName = 'Contract Templates';
UPDATE ChildModules SET RouteUrl = '/Document/DocumentCategories' WHERE ModuleName = 'Document Categories';
UPDATE ChildModules SET RouteUrl = '/Document/DocumentTypes' WHERE ModuleName = 'Document Types';
UPDATE ChildModules SET RouteUrl = '/Document/HRDocuments' WHERE ModuleName = 'HR Documents';
UPDATE ChildModules SET RouteUrl = '/Document/Acknowledgments' WHERE ModuleName = 'Acknowledgments';
UPDATE ChildModules SET RouteUrl = '/Document/DocumentTemplates' WHERE ModuleName = 'Document Templates';
UPDATE ChildModules SET RouteUrl = '/Meeting/MeetingTypes' WHERE ModuleName = 'Meeting Types';
UPDATE ChildModules SET RouteUrl = '/Meeting/MeetingRooms' WHERE ModuleName = 'Meeting Rooms';
UPDATE ChildModules SET RouteUrl = '/Meeting/Meetings' WHERE ModuleName = 'Meetings';
UPDATE ChildModules SET RouteUrl = '/Meeting/MeetingAttendees' WHERE ModuleName = 'Meeting Attendees';
UPDATE ChildModules SET RouteUrl = '/Meeting/MeetingMinutes' WHERE ModuleName = 'Meeting Minutes';
UPDATE ChildModules SET RouteUrl = '/Meeting/ActionItems' WHERE ModuleName = 'Action Items';
UPDATE ChildModules SET RouteUrl = '/Leave/LeavePolicies' WHERE ModuleName = 'Leave Policies';
UPDATE ChildModules SET RouteUrl = '/Attendance/AttendancePolicies' WHERE ModuleName = 'Attendance Policies';
UPDATE ChildModules SET RouteUrl = '/Attendance/AttendanceRegularizations' WHERE ModuleName = 'Attendance Regularizations';
UPDATE ChildModules SET RouteUrl = '/TimeTracking/TimeEntries' WHERE ModuleName = 'Time Entries';
UPDATE ChildModules SET RouteUrl = '/LandingPage/LandingPage' WHERE ModuleName = 'Landing Page';
UPDATE ChildModules SET RouteUrl = '/LandingPage/CustomPages' WHERE ModuleName = 'Custom Pages';
UPDATE ChildModules SET RouteUrl = '/LandingPage/ContactInquiries' WHERE ModuleName = 'Contact Inquiries';
UPDATE ChildModules SET RouteUrl = '/LandingPage/Newsletter' WHERE ModuleName = 'Newsletter';
"@
$sql | Out-File "C:\digvijayProjects\Projects\zentora\update_routes.sql"
