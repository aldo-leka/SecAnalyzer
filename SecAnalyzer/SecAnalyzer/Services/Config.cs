using SecAnalyzer.Interfaces;
using SecAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecAnalyzer.Services
{
    public class Config : IConfig
    {
        private SecAnalyzerContext _context;
        private IDictionary<string, string> _config;

        public string FmpCloudBaseAddress => GetConfigValue("FmpCloudBaseAddress");
        public string FmpCloudApiKey => GetConfigValue("FmpCloudApiKey");

        public Config(SecAnalyzerContext context)
        {
            _context = context;
        }

        private string GetConfigValue(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (key.Length == 0) throw new ArgumentOutOfRangeException(nameof(key));

            if (_config == null)
            {
                _config = _context.Config.ToDictionary(c => c.Key, c => c.Value, StringComparer.OrdinalIgnoreCase);
            }

            if (_config.ContainsKey(key))
            {
                return _config[key];
            }

            return null;
        }
    }
}
