using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace BusinessLogic.Services
{
    public class ReportCreatorServices
    {
        private string _json;
        private string _excel;
        private readonly IConfiguration _configuration;

        public ReportCreatorServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JsonConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_json))
                {
                    _json = _configuration.GetConnectionString("JSON");
                }

                return _json;
            }
        }

        public string ExcelConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_excel))
                {
                    _excel = _configuration.GetConnectionString("EXCEL");
                }

                return _excel;
            }
        }

        public void CreateJson<T>(T report)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize(report, options);

            File.WriteAllText(JsonConnectionString, json);
        }


        public void CreateExcel(ExcelPackage excel)
        {
            var excelFileInfo = new FileInfo(ExcelConnectionString);
            excel.SaveAs(excelFileInfo);
        }
    }
}