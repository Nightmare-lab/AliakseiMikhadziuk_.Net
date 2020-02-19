using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DataAccess.Interfaces;

namespace DataAccess.Repository
{
    internal class CsvRepository<T> : IRepository<T>
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CsvRepository<T>> _logger;

        public CsvRepository(IConfiguration configuration,
            ILogger<CsvRepository<T>> logger
            )
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IEnumerable<T> GetAll()
        {
            var entities = ReadFile();

            return entities;
        }

        public T Create(T item)
        {
            throw new NotImplementedException();
        }

        private string ConnectionString
        {
           get
           {
               if (string.IsNullOrEmpty(_connectionString))
               {
                   _connectionString = _configuration.GetConnectionString("");
               }

               return _connectionString;
           }
        }

        private IEnumerable<T> ReadFile()
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                TrimOptions = TrimOptions.Trim,
            };


            using var reader = new StreamReader(_connectionString);
            using var  csvReader = new CsvReader(reader,configuration);

            csvReader.Configuration.ReadingExceptionOccurred = ExceptionHandler;

            var list = csvReader.GetRecords<T>().ToList();

            return list;
        }

        private bool ExceptionHandler(CsvHelperException exception)
        {
            _logger.LogError(exception.Message);
            return false;
        }


    }
}