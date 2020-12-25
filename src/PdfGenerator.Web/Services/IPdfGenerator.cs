using System.Threading.Tasks;

namespace PdfGenerator.Web.Services
{
    public interface IPdfGenerator
    {
        Task<byte[]> GenerateExamplePdf(string userName);
    }
}