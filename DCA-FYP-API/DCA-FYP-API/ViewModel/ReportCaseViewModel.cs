using DCA_FYP_API.Models;
using System;

namespace DCA_FYP_API.ViewModel
{
    public class ReportCaseViewModel
    {
        public int CaseId { get; set; }
        public User ReportedBy { get; set; }
        public User ReportedTo { get; set; }
        public User Student { get; set; }
        public Casecategory Casecategory { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CaseType { get; set; }
        public bool IsMeetingSet { get; set; } = false;
        public DateTime? ReportedDate { get; set; }
        public CaseMeeting Meeting { get; set; }
        public AssignPunishent Punishment { get; set; }
        public RequestAppeal  Appeal { get; set; }
    }
}