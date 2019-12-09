using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseOperations.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Reflection.Metadata;
using BaseOperations.Models;
using System.Net.Http;
using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BaseOperations.Controllers
{
    public class SupplyManagementController : Controller
    {
        private readonly BaseContext _context;
        private readonly IWebHostEnvironment _env;
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient blobClient;
        private readonly CloudBlobContainer container;

        private readonly string subscriptionKey;
        private readonly string formRecognizerEndpoint;
        private readonly Guid modelGuid;
        private readonly IFormRecognizerClient formClient;

        public SupplyManagementController(BaseContext context, IWebHostEnvironment env, IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _context = context;
            _env = env;
            _configuration = configuration;
            storageAccount = CloudStorageAccount.Parse(_configuration.GetConnectionString("AzureStorageAccount"));
            blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference("pdfs");
            container.CreateIfNotExistsAsync();
            _clientFactory = clientFactory;

            subscriptionKey = _configuration.GetConnectionString("CogSvcsSubscriptionKey");
            formRecognizerEndpoint = _configuration.GetConnectionString("FormRecognizerEndpoint");
            modelGuid = new Guid(_configuration.GetConnectionString("ModelGuid"));
            formClient = new FormRecognizerClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = formRecognizerEndpoint
            };
            
        }

        public IActionResult Index()
        {
            return View(_context.Shipments.ToList());
        }
        public IActionResult AddShipment()
        {
            return View();
        }

        public IActionResult ViewShipments()
        {
            return PartialView("ViewShipments", _context.Shipments.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile upfile)
        {
            //var files = Request.Form.Files;
            var results = new FormResult();
            
                //string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();

                //filename = this.EnsureCorrectFilename(filename);

                var blockBlob = container.GetBlockBlobReference(Guid.NewGuid().ToString()+".pdf");
            blockBlob.Properties.ContentType = "application/pdf";

            //byte[] bArray = ms.ToArray();
                using(var stream = upfile.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(stream);
                    results.PdfPath = blockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
                await blockBlob.DownloadToFileAsync(blockBlob.Name, FileMode.Create);
                try
                {
                    results.FormResults = await formClient.AnalyzeWithCustomModelAsync(modelGuid, new FileStream(blockBlob.Name, FileMode.Open), contentType: "application/pdf");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                }

                //using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                //    await source.CopyToAsync(output);

            return PartialView(results);
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            return _env.WebRootPath + "\\uploads\\" + filename;
        }
    }
}