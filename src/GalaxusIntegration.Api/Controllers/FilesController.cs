using GalaxusIntegration.Application.Services;
using GalaxusIntegration.Shared.Enum;
using Microsoft.AspNetCore.Mvc;

namespace GalaxusIntegration.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly ProductFileService _productFileService;
        public FilesController(ProductFileService productFileService)
        {
            _productFileService = productFileService;
        }
        [HttpGet("processing-status")]
        public IActionResult GetProcessingStatus()
        {
            
            return Ok();
        }
        [HttpPost("{type}")]
        public async Task<ActionResult> UploadFile(ProductDataType type)
        {
            // Handle file upload
            try
            {
               var result=await _productFileService.GenerateProductDataFileAsync(type);
            return  Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpPost("reprocess/{id}")]
        public IActionResult ReprocessFile(string id)
        {
            // Reprocess file by id
            return Accepted();
        }

        [HttpGet("errors")]
        public IActionResult GetFileErrors()
        {
            // Return file errors
            return Ok();
        }
    }
}