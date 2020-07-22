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

namespace Wellness.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WellnessFilesController: ControllerBase
    {
        [HttpPost]
        public async Task<IEnumerable<EventAttachment>> Post()
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
                            FilePath = filePath,
                            FileSize = formFile.Length,
                            Name = formFile.Name
                        });
                    }
                }
            }
            return fileList;
        }

    }
}
