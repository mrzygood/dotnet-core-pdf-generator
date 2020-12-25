using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Web.Services;

namespace PdfGenerator.Web.Controllers
{
    public class CertificateController
    {
        private readonly IPdfGenerator _pdfGenerator;

        public CertificateController(IPdfGenerator pdfGenerator)
        {
            _pdfGenerator = pdfGenerator;
        }

        [HttpGet]
        public async Task<FileContentResult> GetCertificate()
        {
            var bytes = await _pdfGenerator.GenerateExamplePdf("John Smith");
            var fileStreamResult = new FileContentResult(bytes, "application/pdf");
            return fileStreamResult;
        }
    }
}