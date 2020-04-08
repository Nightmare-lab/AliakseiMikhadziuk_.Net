using System;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Map
{
    public sealed class Mapping : ClassMap<Students>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Mapping> _logger;
        private int? _infoCount;

        public Mapping(IConfiguration configuration, ILogger<Mapping> logger)
        {
            _configuration = configuration;
            _logger = logger;
            Map(m => m.Surname).Index(0);
            Map(m => m.Name).Index(1);
            Map(m => m.MiddleName).Index(2);
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

                if (row.Context.Record.Length != allFieldCount)
                {
                    throw new CsvHelperException(row.Context);
                }

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