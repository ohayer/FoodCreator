using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Creator.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BinaryController : ControllerBase
    {
        // POST
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (file.ContentType != "application/pdf")
            {
                return BadRequest("Only pdf files are supported.");
            }
            
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", "file.bin");

            using (var FileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(FileStream);
            }
            
            var fileContent = await System.IO.File.ReadAllBytesAsync(filePath);

            var binaryString = new StringBuilder();
            foreach (var byteValue in fileContent)
            {
                binaryString.Append(Convert.ToString(byteValue, 2).PadLeft(8, '0'));
            }

            return Ok(new
            {
                message = "File uploaded and converted to binary successfully.",
                binaryContent = binaryString.ToString()
            });
        }
        
        [HttpGet("get-binary-content")]
        public IActionResult GetBinaryContent()
        {
            // Ścieżka do pliku z binarną zawartością
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", "file.bin");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Binary file not found.");
            }

            var fileContent = System.IO.File.ReadAllBytes(filePath);

            // Zamiana zawartości binarnej na ciąg 0 i 1
            var binaryString = new StringBuilder();
            foreach (var byteValue in fileContent)
            {
                binaryString.Append(Convert.ToString(byteValue, 2).PadLeft(8, '0')); // 8-bitowy zapis
            }

            return Ok(new { binaryContent = binaryString.ToString() });
        }
    }
}