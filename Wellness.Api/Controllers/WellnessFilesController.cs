using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;
using System.Text;
using System.Web;

namespace Wellness.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WellnessFilesController: ControllerBase
    {
        [HttpPost]
        public async Task<IEnumerable<EventAttachment>> Post([FromServices] IRequestDependencies requestDependencies)
        {
            var fileList = new List<EventAttachment>();
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var formFile in HttpContext.Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        fileList.Add(new EventAttachment()
                        {
                            ContentType = formFile.ContentType,
                            FilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/wellnessfiles/{HttpUtility.UrlEncode(formFile.ContentType)}/{Path.GetFileName(filePath)}",
                            FileSize = formFile.Length,
                            Name = formFile.Name
                        });
                    }
                }
            }
            return fileList;
        }

        [HttpGet("{contentType}/{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string contentType, [FromRoute] string fileName, [FromServices] IRequestDependencies requestDependencies)
        {
            if (fileName == null)
                return Content("filename not present");

            var path = Path.Combine(Path.GetTempPath(), fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, HttpUtility.UrlDecode(contentType), fileName);
        }
    }
}
