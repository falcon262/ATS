using System.IO;
using System.Threading.Tasks;

namespace ATS.AI.Parsing
{
    public interface IOcrTextExtractor
    {
        Task<string> ExtractTextFromPdfAsync(Stream pdfStream);
    }
}

