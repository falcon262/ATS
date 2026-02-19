using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace ATS.AI.Parsing
{
    public class DocxTextExtractor : IDocumentTextExtractor, ITransientDependency
    {
        private readonly ILogger<DocxTextExtractor> _logger;

        public DocxTextExtractor(ILogger<DocxTextExtractor> logger)
        {
            _logger = logger;
        }

        public bool CanHandle(string fileName)
        {
            return fileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) ||
                   fileName.EndsWith(".doc", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ExtractTextAsync(Stream stream, string fileName)
        {
            try
            {
                var sb = new StringBuilder();
                
                using (var doc = WordprocessingDocument.Open(stream, false))
                {
                    var body = doc.MainDocumentPart?.Document?.Body;
                    if (body != null)
                    {
                        sb.Append(body.InnerText);
                    }
                }

                var text = sb.ToString();
                return await Task.FromResult(NormalizeText(text));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting text from DOCX {FileName}", fileName);
                throw;
            }
        }

        private string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Remove excessive whitespace
            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ");
            
            // Remove control characters
            text = System.Text.RegularExpressions.Regex.Replace(text, @"[\x00-\x1F\x7F]", string.Empty);
            
            // Limit length to ~30k characters
            if (text.Length > 30000)
            {
                text = text.Substring(0, 30000);
            }

            return text.Trim();
        }
    }
}

