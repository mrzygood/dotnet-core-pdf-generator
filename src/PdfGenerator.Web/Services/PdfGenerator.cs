using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Stubble.Core.Builders;
using Wkhtmltopdf.NetCore;
using Wkhtmltopdf.NetCore.Options;

namespace PdfGenerator.Web.Services
{
    public class CertificateModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string BackgroundPath { get; set; }
    }
    
    public class PdfGeneratorService : IPdfGenerator
    {
        private readonly IGeneratePdf _generatePdf;

        public PdfGeneratorService(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }
        
        public async Task<byte[]> GenerateExamplePdf(string userName)
        {
            var model = new CertificateModel
            {
                Name = userName,
                Date = DateTime.Now.ToShortDateString(),
                BackgroundPath = $"{AppContext.BaseDirectory}MustacheTemplates/Certificate/certificate_bg.jpg"
            };

            var certificateContent = await ProcessTemplate
                (model, @"./MustacheTemplates/Certificate/ExampleCertificateTemplate.html");

            var options = new ConvertOptions
            {
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(0 ,0 ,0 ,0),
            };

            _generatePdf.SetConvertOptions(options);
            
            var bytes = _generatePdf.GetPDF(certificateContent);
            
            // If you want to save file on disc
            // await File.WriteAllBytesAsync("file.pdf", bytes);

            return bytes;
        }
        
        private async Task<string> ProcessTemplate(object mustacheModel, string templateUri)
        {
            var stubble = new StubbleBuilder().Build();

            var output = string.Empty;
            using (StreamReader streamReader = new StreamReader(templateUri, Encoding.UTF8))
            {
                var content = await streamReader.ReadToEndAsync();
                output = await stubble.RenderAsync(content, mustacheModel);
            }

            return output;
        }
    }
}