using System.Collections.Generic;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace DataAccess.Models
{
    public class Students
    {
        [Name("Имя")]
        public string Name {get; set;}
        
        [Name("Фамилия")]
        public string Surname { get; set; }

        [JsonIgnore]
        public List<Mark> MarkList { get; set; }

        [Optional]
        public double AverageMark { get; set; }
    }
}
