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
    public class CsvRepository<T> : IRepository<T>
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CsvRepository<T>> _logger;
        private readonly ClassMap<T> _map;

        public CsvRepository(IConfiguration configuration,
            ILogger<CsvRepository<T>> logger,
            ClassMap<T> map = null
            )
        {
            _configuration = configuration;
            _logger = logger;
            _map = map;
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
                   _connectionString = _configuration.GetConnectionString("CSV");
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


            using var reader = new StreamReader(ConnectionString);
            using var csvReader = new CsvReader(reader,configuration);

            csvReader.Configuration.ReadingExceptionOccurred = ExceptionHandler;

            if (_map != null)
            {
                csvReader.Configuration.RegisterClassMap(_map);
            }

            var list = new List<T>();

            try
            {
                list = csvReader.GetRecords<T>().ToList();
            }
            catch
            {
                return list;
            }

            return list;
        }

        private bool ExceptionHandler(CsvHelperException exception)
        {
            _logger.LogError(exception.Message);
            throw exception;
        }


    }
}