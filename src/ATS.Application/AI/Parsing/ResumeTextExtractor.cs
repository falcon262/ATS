using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace ATS.AI.Parsing
{
    public class ResumeTextExtractor : ITransientDependency
    {
        private readonly IEnumerable<IDocumentTextExtractor> _extractors;
        private readonly ILogger<ResumeTextExtractor> _logger;

        public ResumeTextExtractor(
            IEnumerable<IDocumentTextExtractor> extractors,
            ILogger<ResumeTextExtractor> logger)
        {
            _extractors = extractors;
            _logger = logger;
        }

        public async Task<string> ExtractTextAsync(Stream stream, string fileName)
        {
            var extractor = _extractors.FirstOrDefault(e => e.CanHandle(fileName));
            
            if (extractor == null)
            {
                _logger.LogWarning("No extractor found for file {FileName}", fileName);
                return string.Empty;
            }

            _logger.LogInformation("Using {ExtractorType} for {FileName}", 
                extractor.GetType().Name, fileName);

            return await extractor.ExtractTextAsync(stream, fileName);
        }
    }
}

