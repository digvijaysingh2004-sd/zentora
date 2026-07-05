$projectDir = "C:\digvijayProjects\Projects\zentora\zentoraHRMS"

# 1. Update Existing Controllers
$hrController = "$projectDir\Controllers\HRController.cs"
(Get-Content $hrController) -replace "}", "    public ActionResult Resignations() { return View(); }`r`n        public ActionResult Terminations() { return View(); }`r`n        public ActionResult Warnings() { return View(); }`r`n        public ActionResult Trips() { return View(); }`r`n        public ActionResult Complaints() { return View(); }`r`n        public ActionResult Transfers() { return View(); }`r`n        public ActionResult Training() { return View(); }`r`n    }`r`n}" | Set-Content $hrController

$recruitmentController = "$projectDir\Controllers\RecruitmentController.cs"
(Get-Content $recruitmentController) -replace "}", "    public ActionResult JobCategories() { return View(); }`r`n        public ActionResult JobTypes() { return View(); }`r`n        public ActionResult JobLocations() { return View(); }`r`n        public ActionResult CustomQuestions() { return View(); }`r`n        public ActionResult CandidateSources() { return View(); }`r`n        public ActionResult InterviewRounds() { return View(); }`r`n        public ActionResult InterviewFeedback() { return View(); }`r`n        public ActionResult CandidateAssessments() { return View(); }`r`n        public ActionResult OfferTemplates() { return View(); }`r`n        public ActionResult Offers() { return View(); }`r`n        public ActionResult OnboardingChecklists() { return View(); }`r`n        public ActionResult ChecklistItems() { return View(); }`r`n        public ActionResult CandidateOnboarding() { return View(); }`r`n        public ActionResult Career() { return View(); }`r`n    }`r`n}" | Set-Content $recruitmentController

# 2. Create New Controllers
$controllers = @{
    "TrainingController" = "TrainingTypes", "TrainingPrograms", "TrainingSessions", "EmployeeTrainings"
    "PerformanceController" = "IndicatorCategories", "Indicators", "GoalTypes", "EmployeeGoals", "ReviewCycles", "EmployeeReviews"
    "AssetManagementController" = "AssetTypes", "Assets", "Dashboard", "Depreciation"
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
    "HR\Resignations", "HR\Terminations", "HR\Warnings", "HR\Trips", "HR\Complaints", "HR\Transfers", "HR\Training",
    "Recruitment\JobCategories", "Recruitment\JobTypes", "Recruitment\JobLocations", "Recruitment\CustomQuestions", "Recruitment\CandidateSources", "Recruitment\InterviewRounds", "Recruitment\InterviewFeedback", "Recruitment\CandidateAssessments", "Recruitment\OfferTemplates", "Recruitment\Offers", "Recruitment\OnboardingChecklists", "Recruitment\ChecklistItems", "Recruitment\CandidateOnboarding", "Recruitment\Career",
    "Training\TrainingTypes", "Training\TrainingPrograms", "Training\TrainingSessions", "Training\EmployeeTrainings",
    "Performance\IndicatorCategories", "Performance\Indicators", "Performance\GoalTypes", "Performance\EmployeeGoals", "Performance\ReviewCycles", "Performance\EmployeeReviews",
    "AssetManagement\AssetTypes", "AssetManagement\Assets", "AssetManagement\Dashboard", "AssetManagement\Depreciation"
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
$itemsToAdd += "    <Compile Include=`"Controllers\TrainingController.cs`" />`r`n"
$itemsToAdd += "    <Compile Include=`"Controllers\PerformanceController.cs`" />`r`n"
$itemsToAdd += "    <Compile Include=`"Controllers\AssetManagementController.cs`" />`r`n"

$csprojContent = $csprojContent -replace "(<Content Include=`"Views\\Payroll\\Payslips\.cshtml`" />)", "`$1`r`n$itemsToAdd"
Set-Content $csprojPath $csprojContent -NoNewline

# 5. Execute SQL
$sql = @"
UPDATE ChildModules SET RouteUrl = '/HR/Resignations' WHERE ModuleName = 'Resignations';
UPDATE ChildModules SET RouteUrl = '/HR/Terminations' WHERE ModuleName = 'Terminations';
UPDATE ChildModules SET RouteUrl = '/HR/Warnings' WHERE ModuleName = 'Warnings';
UPDATE ChildModules SET RouteUrl = '/HR/Trips' WHERE ModuleName = 'Trips';
UPDATE ChildModules SET RouteUrl = '/HR/Complaints' WHERE ModuleName = 'Complaints';
UPDATE ChildModules SET RouteUrl = '/HR/Transfers' WHERE ModuleName = 'Transfers';
UPDATE ChildModules SET RouteUrl = '/HR/Training' WHERE ModuleName = 'Training';

UPDATE ChildModules SET RouteUrl = '/Recruitment/JobCategories' WHERE ModuleName = 'Job Categories';
UPDATE ChildModules SET RouteUrl = '/Recruitment/JobTypes' WHERE ModuleName = 'Job Types';
UPDATE ChildModules SET RouteUrl = '/Recruitment/JobLocations' WHERE ModuleName = 'Job Locations';
UPDATE ChildModules SET RouteUrl = '/Recruitment/CustomQuestions' WHERE ModuleName = 'Custom Questions';
UPDATE ChildModules SET RouteUrl = '/Recruitment/CandidateSources' WHERE ModuleName = 'Candidate Sources';
UPDATE ChildModules SET RouteUrl = '/Recruitment/InterviewRounds' WHERE ModuleName = 'Interview Rounds';
UPDATE ChildModules SET RouteUrl = '/Recruitment/InterviewFeedback' WHERE ModuleName = 'Interview Feedback';
UPDATE ChildModules SET RouteUrl = '/Recruitment/CandidateAssessments' WHERE ModuleName = 'Candidate Assessments';
UPDATE ChildModules SET RouteUrl = '/Recruitment/OfferTemplates' WHERE ModuleName = 'Offer Templates';
UPDATE ChildModules SET RouteUrl = '/Recruitment/Offers' WHERE ModuleName = 'Offers';
UPDATE ChildModules SET RouteUrl = '/Recruitment/OnboardingChecklists' WHERE ModuleName = 'Onboarding Checklists';
UPDATE ChildModules SET RouteUrl = '/Recruitment/ChecklistItems' WHERE ModuleName = 'Checklist Items';
UPDATE ChildModules SET RouteUrl = '/Recruitment/CandidateOnboarding' WHERE ModuleName = 'Candidate Onboarding';
UPDATE ChildModules SET RouteUrl = '/Recruitment/Career' WHERE ModuleName = 'Career';

UPDATE SubChildModules SET RouteUrl = '/Performance/IndicatorCategories' WHERE ModuleName = 'Indicator Categories';
UPDATE SubChildModules SET RouteUrl = '/Performance/Indicators' WHERE ModuleName = 'Indicators';
UPDATE SubChildModules SET RouteUrl = '/Performance/GoalTypes' WHERE ModuleName = 'Goal Types';
UPDATE SubChildModules SET RouteUrl = '/Performance/EmployeeGoals' WHERE ModuleName = 'Employee Goals';
UPDATE SubChildModules SET RouteUrl = '/Performance/ReviewCycles' WHERE ModuleName = 'Review Cycles';
UPDATE SubChildModules SET RouteUrl = '/Performance/EmployeeReviews' WHERE ModuleName = 'Employee Reviews';

UPDATE SubChildModules SET RouteUrl = '/AssetManagement/AssetTypes' WHERE ModuleName = 'Asset Types';
UPDATE SubChildModules SET RouteUrl = '/AssetManagement/Assets' WHERE ModuleName = 'Assets';
UPDATE SubChildModules SET RouteUrl = '/AssetManagement/Dashboard' WHERE ModuleName = 'Dashboard';
UPDATE SubChildModules SET RouteUrl = '/AssetManagement/Depreciation' WHERE ModuleName = 'Depreciation';

UPDATE SubChildModules SET RouteUrl = '/Training/TrainingTypes' WHERE ModuleName = 'Training Types';
UPDATE SubChildModules SET RouteUrl = '/Training/TrainingPrograms' WHERE ModuleName = 'Training Programs';
UPDATE SubChildModules SET RouteUrl = '/Training/TrainingSessions' WHERE ModuleName = 'Training Sessions';
UPDATE SubChildModules SET RouteUrl = '/Training/EmployeeTrainings' WHERE ModuleName = 'Employee Trainings';
"@
$sql | Out-File "C:\digvijayProjects\Projects\zentora\update_routes_final.sql"
