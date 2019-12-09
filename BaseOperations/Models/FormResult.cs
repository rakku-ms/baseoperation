using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;


namespace BaseOperations.Models
{
    public class FormResult
    {
        public string PdfPath { get; set; }
        public AnalyzeResult FormResults { get; set; }
    }
}
