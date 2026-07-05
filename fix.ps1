$bad1 = "    public ActionResult LeavePolicies() { return View(); }`r`n    }`r`n}"
$file1 = "C:\digvijayProjects\Projects\zentora\zentoraHRMS\Controllers\LeaveController.cs"
$content1 = [System.IO.File]::ReadAllText($file1)
$content1 = $content1.Replace($bad1, "}")
$idx1 = $content1.LastIndexOf("}")
if ($idx1 -gt 0) { $idx1 = $content1.LastIndexOf("}", $idx1 - 1) }
if ($idx1 -gt 0) { $content1 = $content1.Insert($idx1, "        public ActionResult LeavePolicies() { return View(); }`r`n") }
[System.IO.File]::WriteAllText($file1, $content1)

$bad2 = "    public ActionResult AttendancePolicies() { return View(); }`r`n        public ActionResult AttendanceRegularizations() { return View(); }`r`n    }`r`n}"
$file2 = "C:\digvijayProjects\Projects\zentora\zentoraHRMS\Controllers\AttendanceController.cs"
$content2 = [System.IO.File]::ReadAllText($file2)
$content2 = $content2.Replace($bad2, "}")
$idx2 = $content2.LastIndexOf("}")
if ($idx2 -gt 0) { $idx2 = $content2.LastIndexOf("}", $idx2 - 1) }
if ($idx2 -gt 0) { $content2 = $content2.Insert($idx2, "        public ActionResult AttendancePolicies() { return View(); }`r`n        public ActionResult AttendanceRegularizations() { return View(); }`r`n") }
[System.IO.File]::WriteAllText($file2, $content2)

$bad3 = "    public ActionResult RecruitmentAgencies() { return View(); }`r`n        public ActionResult RecruitmentProcesses() { return View(); }`r`n        public ActionResult InterviewTypes() { return View(); }`r`n        public ActionResult Interviews() { return View(); }`r`n        public ActionResult BackgroundChecks() { return View(); }`r`n    }`r`n}"
$bad4 = "    public ActionResult JobCategories() { return View(); }`r`n        public ActionResult JobTypes() { return View(); }`r`n        public ActionResult JobLocations() { return View(); }`r`n        public ActionResult CustomQuestions() { return View(); }`r`n        public ActionResult CandidateSources() { return View(); }`r`n        public ActionResult InterviewRounds() { return View(); }`r`n        public ActionResult InterviewFeedback() { return View(); }`r`n        public ActionResult CandidateAssessments() { return View(); }`r`n        public ActionResult OfferTemplates() { return View(); }`r`n        public ActionResult Offers() { return View(); }`r`n        public ActionResult OnboardingChecklists() { return View(); }`r`n        public ActionResult ChecklistItems() { return View(); }`r`n        public ActionResult CandidateOnboarding() { return View(); }`r`n        public ActionResult Career() { return View(); }`r`n    }`r`n}"
$file3 = "C:\digvijayProjects\Projects\zentora\zentoraHRMS\Controllers\RecruitmentController.cs"
$content3 = [System.IO.File]::ReadAllText($file3)
$content3 = $content3.Replace($bad4, "}")
$content3 = $content3.Replace($bad3, "}")
$idx3 = $content3.LastIndexOf("}")
if ($idx3 -gt 0) { $idx3 = $content3.LastIndexOf("}", $idx3 - 1) }
if ($idx3 -gt 0) { $content3 = $content3.Insert($idx3, "        public ActionResult RecruitmentAgencies() { return View(); }`r`n        public ActionResult RecruitmentProcesses() { return View(); }`r`n        public ActionResult InterviewTypes() { return View(); }`r`n        public ActionResult Interviews() { return View(); }`r`n        public ActionResult BackgroundChecks() { return View(); }`r`n        public ActionResult JobCategories() { return View(); }`r`n        public ActionResult JobTypes() { return View(); }`r`n        public ActionResult JobLocations() { return View(); }`r`n        public ActionResult CustomQuestions() { return View(); }`r`n        public ActionResult CandidateSources() { return View(); }`r`n        public ActionResult InterviewRounds() { return View(); }`r`n        public ActionResult InterviewFeedback() { return View(); }`r`n        public ActionResult CandidateAssessments() { return View(); }`r`n        public ActionResult OfferTemplates() { return View(); }`r`n        public ActionResult Offers() { return View(); }`r`n        public ActionResult OnboardingChecklists() { return View(); }`r`n        public ActionResult ChecklistItems() { return View(); }`r`n        public ActionResult CandidateOnboarding() { return View(); }`r`n        public ActionResult Career() { return View(); }`r`n") }
[System.IO.File]::WriteAllText($file3, $content3)

$bad5 = "    public ActionResult Resignations() { return View(); }`r`n        public ActionResult Terminations() { return View(); }`r`n        public ActionResult Warnings() { return View(); }`r`n        public ActionResult Trips() { return View(); }`r`n        public ActionResult Complaints() { return View(); }`r`n        public ActionResult Transfers() { return View(); }`r`n        public ActionResult Training() { return View(); }`r`n    }`r`n}"
$file5 = "C:\digvijayProjects\Projects\zentora\zentoraHRMS\Controllers\HRController.cs"
$content5 = [System.IO.File]::ReadAllText($file5)
$content5 = $content5.Replace($bad5, "}")
$idx5 = $content5.LastIndexOf("}")
if ($idx5 -gt 0) { $idx5 = $content5.LastIndexOf("}", $idx5 - 1) }
if ($idx5 -gt 0) { $content5 = $content5.Insert($idx5, "        public ActionResult Resignations() { return View(); }`r`n        public ActionResult Terminations() { return View(); }`r`n        public ActionResult Warnings() { return View(); }`r`n        public ActionResult Trips() { return View(); }`r`n        public ActionResult Complaints() { return View(); }`r`n        public ActionResult Transfers() { return View(); }`r`n        public ActionResult Training() { return View(); }`r`n") }
[System.IO.File]::WriteAllText($file5, $content5)
