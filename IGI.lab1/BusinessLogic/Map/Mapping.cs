using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Map
{
    public sealed class Mapping : ClassMap<Students>
    {
        private readonly IConfiguration _configuration;
        private int? _infoCount;

        public Mapping(IConfiguration configuration)
        {
            _configuration = configuration;

            Map(m => m.Surname).Name("Фамилия");
            Map(m => m.Name).Name("Имя");
            Map(m => m.MiddleName).Name("Отчество");
            Map(m => m.MarkList).ConvertUsing(GetSubjects);
        }

        public int InformationCount
        {
            get
            {
                if (!_infoCount.HasValue)
                {
                    _infoCount = int.Parse(_configuration.GetConnectionString("InformationParametersCount"));
                }

                return _infoCount.Value;
            }
        }

        private List<Mark> GetSubjects(IReaderRow row)
        {
            // get all columns specified in file
            var allFieldCount = row.Context.HeaderRecord.Length;

            var marks = new List<Mark>();
            for (var i = InformationCount; i < allFieldCount; i++)
            {
                marks.Add(new Mark
                {
                    SubjectName = row.Context.HeaderRecord[i],
                    SubjectMark = int.Parse(row.GetField(i)),
                });
            }

            return marks;
        }
    }
}