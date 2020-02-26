using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace BusinessLogic.Services
{
    public class ReportServices
    {
        private ILogger<ReportServices> _logger;
        private ReportCreatorServices _reportCreatorServices;
        private IRepository<Students> _repository;

        public ReportServices(ILogger<ReportServices> logger,
            ReportCreatorServices reportCreatorServices,
            IRepository<Students> repository)
        {
            _logger = logger;
            _reportCreatorServices = reportCreatorServices;
            _repository = repository;
        }

        public void Choose(ReportType reportType)
        {
            var report = GetStudentReport(GetStudents());

            switch (reportType)
            {
                case ReportType.Json:
                    GenerateJson(report);
                    break;
                case ReportType.Excel:
                    GenerateExcel(report);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reportType), reportType, null);
            }
        }

        private List<SubjectReport> GetSubjectReports(IEnumerable<Students> students)
        {
            _logger.LogInformation("Get subject report");
            return students.SelectMany(student => student.MarkList)
                .GroupBy(mark => mark.SubjectName)
                .Select(mark => new SubjectReport
                {
                    Name = mark.Key,
                    AverageMark = mark.Average(subject => subject.SubjectMark),
                })
                .ToList();
        }

        private StudentsReport GetStudentReport(IReadOnlyCollection<Students> students)
        {
            return new StudentsReport
            {
                Students = students,
                SubjectReports = GetSubjectReports(students)
                
            };
        }

        private List<Students> GetStudents()
        {
            var students = _repository.GetAll().ToList();
            SetAverageMark(students);
            return students;
        }

        private static void SetAverageMark(IEnumerable<Students> students)
        {
            foreach (var student in students)
            {
                student.AverageMark = student.MarkList.Average(mark => mark.SubjectMark);
            }
        }

        private void GenerateJson(StudentsReport report)
        {
            _reportCreatorServices.CreateJson(report);

            _logger.LogInformation("Json generated");
        }

        private void GenerateExcel(StudentsReport report)
        {
            using var excel = new ExcelPackage();
            CreateReport(report, excel);

            _reportCreatorServices.CreateExcel(excel);

            _logger.LogInformation("Excel generated");
        }

        private void CreateReport(StudentsReport report, ExcelPackage excel)
        {
            var worksheet = excel.Workbook.Worksheets.Add("Education report");

            worksheet.Cells[1, 1].Value = "Students";

            var rowNumber = 2;
            worksheet.Cells[rowNumber, 1].Value = "Surname";
            worksheet.Cells[rowNumber, 2].Value = "Name";
            worksheet.Cells[rowNumber, 3].Value = "Average mark";
            rowNumber++;

            foreach (var student in report.Students)
            {
                worksheet.Cells[rowNumber, 1].Value = student.Surname;
                worksheet.Cells[rowNumber, 2].Value = student.Name;
                worksheet.Cells[rowNumber, 3].Value = student.AverageMark;
                rowNumber++;
            }

            rowNumber++;
            worksheet.Cells[rowNumber++, 1].Value = "Subjects";

            worksheet.Cells[rowNumber, 1].Value = "Name";
            worksheet.Cells[rowNumber++, 2].Value = "Average mark";

            foreach (var subjectReport in report.SubjectReports)
            {
                worksheet.Cells[rowNumber, 1].Value = subjectReport.Name;
                worksheet.Cells[rowNumber++, 2].Value = subjectReport.AverageMark;
            }

            worksheet.Cells.AutoFitColumns();
        }
    }
}
