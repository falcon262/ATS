using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Content;
using PdfSharpCore.Pdf.Content.Objects;
using PdfSharpCore.Pdf.IO;
using Volo.Abp.DependencyInjection;

namespace ATS.AI.Parsing
{
    public class PdfTextExtractor : IDocumentTextExtractor, ITransientDependency
    {
        private readonly ILogger<PdfTextExtractor> _logger;
        private readonly IOcrTextExtractor _ocrExtractor;

        public PdfTextExtractor(ILogger<PdfTextExtractor> logger, IOcrTextExtractor ocrExtractor)
        {
            _logger = logger;
            _ocrExtractor = ocrExtractor;
        }

        public bool CanHandle(string fileName)
        {
            return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ExtractTextAsync(Stream stream, string fileName)
        {
            try
            {
                var text = await ExtractTextFromPdfAsync(stream);
                
                // Check if extraction yield is low (likely image-based PDF)
                if (text.Length < 100)
                {
                    _logger.LogInformation("Low text yield from PDF, falling back to OCR for {FileName}", fileName);
                    stream.Position = 0;
                    return await _ocrExtractor.ExtractTextFromPdfAsync(stream);
                }

                return NormalizeText(text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting text from PDF {FileName}", fileName);
                throw;
            }
        }

        private async Task<string> ExtractTextFromPdfAsync(Stream stream)
        {
            var sb = new StringBuilder();
            
            using (var document = PdfReader.Open(stream, PdfDocumentOpenMode.ReadOnly))
            {
                foreach (PdfPage page in document.Pages)
                {
                    var content = ContentReader.ReadContent(page);
                    var text = ExtractTextFromContent(content);
                    sb.AppendLine(text);
                }
            }

            return await Task.FromResult(sb.ToString());
        }

        private string ExtractTextFromContent(CObject content)
        {
            var sb = new StringBuilder();
            ExtractText(content, sb);
            return sb.ToString();
        }

        private void ExtractText(CObject obj, StringBuilder sb)
        {
            if (obj is COperator op)
            {
                if (op.OpCode.Name == "Tj" || op.OpCode.Name == "TJ")
                {
                    foreach (var operand in op.Operands)
                    {
                        if (operand is CString str)
                        {
                            sb.Append(str.Value);
                        }
                        else if (operand is CArray array)
                        {
                            foreach (var item in array)
                            {
                                if (item is CString arrayStr)
                                {
                                    sb.Append(arrayStr.Value);
                                }
                            }
                        }
                    }
                    sb.Append(" ");
                }
            }
            else if (obj is CSequence sequence)
            {
                foreach (var item in sequence)
                {
                    ExtractText(item, sb);
                }
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
            
            // Limit length to ~30k characters to control token usage
            if (text.Length > 30000)
            {
                text = text.Substring(0, 30000);
            }

            return text.Trim();
        }
    }
}

