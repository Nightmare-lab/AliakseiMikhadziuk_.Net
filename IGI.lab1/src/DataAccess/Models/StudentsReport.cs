using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class StudentsReport
    {
        public IReadOnlyCollection<Students> Students { get; set; }

        [JsonPropertyName("Subjects")]
        public IReadOnlyCollection<SubjectReport> SubjectReports { get; set; }
    }
}