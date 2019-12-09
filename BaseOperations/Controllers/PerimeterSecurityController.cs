using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

using Microsoft.AspNetCore.Mvc;
using BaseOperations.Data;
using BaseOperations.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Builder;

namespace BaseOperations.Controllers
{
    public class PerimeterSecurityController : Controller
    {
        private readonly BaseContext _context;
        static string mlEndpoint = Environment.GetEnvironmentVariable("ml_endpoint");

        private readonly IHttpClientFactory _clientFactory;

        public PerimeterSecurityController(BaseContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Retraining()
        {
            return View();
        }

        public IActionResult ApplyModel()
        {
            return View();
        }

        public async Task<IActionResult> ProcessImages()
        {
            string inferenceUrl = mlEndpoint + "url";

            var events = from e in _context.Events
                         select e;

            events = events.OrderByDescending(e => e.Timestamp);

            //var client = _clientFactory.CreateClient();

            //foreach(Event e in events)
            //{
            //    var response = await client.PostAsync(inferenceUrl, new StringContent(JsonConvert.SerializeObject(new InferenceRequest { url = e.ImageURL })));
            //    var parsedResponse = JsonConvert.DeserializeObject<InferenceResponse>(await response.Content.ReadAsStringAsync());
            //    if(parsedResponse.predictions.Find(item => item.tagName == "NotMyTruck").probability > parsedResponse.predictions.Find(item => item.tagName == "MyTruck").probability)
            //    {
            //        e.ContainsTruck = false;
            //    }
            //    else
            //    {
            //        e.ContainsTruck = true;
            //    }
            //}

            //await _context.SaveChangesAsync();

            return PartialView(events.ToList());
        }
    }
    class InferenceRequest
    {
        public string url { get; set; }
    }

    class InferenceResponse
    {
        public DateTime created { get; set; }
        public string id { get; set; }
        public string iteration { get; set; }
        public List<Prediction> predictions { get; set; }
        public string project { get; set; }
    }

    class Prediction
    {
        public object boundingBox { get; set; }
        public double probability { get; set; }
        public string tagId { get; set; }
        public string tagName { get; set; }
    }
}