using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ImportCSV.Models
{
    public class UploadCsvViewModel
    {

        [Required(ErrorMessage = "Uploading File is required.")]
        public IFormFile File { get; set; }
    }
}
