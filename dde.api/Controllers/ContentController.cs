using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dde.api.Helpers;
using dde.api.Models;
using dde.dataaccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dde.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private DDEContext _entities;
        private readonly AppSettings _appSettings;

        public ContentController(IOptions<AppSettings> appSettings, DDEContext entities)
        {
            this._entities = entities;
            this._appSettings = appSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContentModel model)
        {
            try
            {
                string fileName = await WriteFile(model.content);

                //create multimedia
                Multimedia multimedia = new Multimedia();
                multimedia.Url = fileName;
                multimedia.FechaCreacion = DateTime.Now;
                multimedia.FechaModificacion = DateTime.Now;

                _entities.Multimedia.Add(multimedia);
                _entities.SaveChanges();

                ProgramacionMultimedia programacionMultimedia = new ProgramacionMultimedia();
                programacionMultimedia.FechaHoraCreacion = DateTime.Now;
                programacionMultimedia.FechaHoraModificacion = DateTime.Now;
                programacionMultimedia.FechaInicioProgramacion = DateTime.Now.Date;
                programacionMultimedia.FechaTerminoProgramacion = DateTime.Now.Date;
                programacionMultimedia.MultimediaId = multimedia.MultimediaId;
                programacionMultimedia.UsuarioCreacionId = 1;

                _entities.ProgramacionMultimedia.Add(programacionMultimedia);
                _entities.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet]
        public IActionResult Get()
        {
           
        }

        [HttpGet("{id}")]
        public async Task<FileStreamResult> Get(string id)
        {
            var path = _appSettings.URLContent + id;
            var stream = System.IO.File.OpenRead(path);
            return new FileStreamResult(stream, "application/octet-stream");
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = string.Empty;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.
                if (!Directory.Exists(_appSettings.URLContent))
                {
                    Directory.CreateDirectory(_appSettings.URLContent);
                }

                var path = Path.Combine(_appSettings.URLContent,
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                //log error
            }
            return fileName;
        }
    }
}