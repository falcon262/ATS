using System.IO;
using System.Threading.Tasks;

namespace ATS.AI.Parsing
{
    public interface IDocumentTextExtractor
    {
        Task<string> ExtractTextAsync(Stream stream, string fileName);
        bool CanHandle(string fileName);
    }
}

