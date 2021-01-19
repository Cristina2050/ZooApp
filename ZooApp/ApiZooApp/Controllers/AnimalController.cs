using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Utils.Exceptions;
using Utils.ResponseMessages;
using ZooServices.Contracts.Zoo;

namespace ApiZooApp.Controllers
{
    /// <summary>
    /// Animal Controller
    /// </summary>
    [SwaggerTag("Animal group Endpoints")]
    [Route("api/v1.0.0/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        #region Properties
        private IAnimalManager _animalManager;
        private readonly ILogger<AnimalController> _logger;
        //private readonly AppSettingsDto _appSettings;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalController"/> class.
        /// </summary>
        /// <param name="animalManager"></param>
        /// <param name="logger"></param>
        /// <param name="appSettings"></param>
        public AnimalController(//IOptions<AppSettingsDto> appSettings, 
            IAnimalManager animalManager, ILogger<AnimalController> logger)
        {
            //_appSettings = appSettings.Value;
            _animalManager = animalManager;
            _logger = logger;
        }
        #endregion

        #region Methods GET

        /// <summary>
        /// Get all data of Animal
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("AllAnimals")]
        [SwaggerResponse(200, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(401, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(404, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(405, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(500, Type = typeof(ResponseMessage<AnimalDto>))]
        public async Task<IActionResult> GetAllAnimalsAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Revise Parámetros requeridos", new Guid()));

                ICollection<AnimalDto>  response = await _animalManager.GetAllAnimalAsync();

                if (response == null)
                    return NotFound(new ResponseMessage<AnimalDto>("404", "No existen elementos", response));

                return Ok(new ResponseMessage<AnimalDto>("200", "ok", response));

            }
            catch (ExceptionApi ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al consultar los datos de animales.", id: guid));
            }
        }

        /// <summary>
        /// Get data of oneAnimal 
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("Animal")]
        [SwaggerResponse(200, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(401, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(404, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(405, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(500, Type = typeof(ResponseMessage<AnimalDto>))]
        public async Task<IActionResult> GetAnimalAsync([Required] string animalName)
        {            
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Revise Parámetros requeridos", new Guid()));
                AnimalDto response = await _animalManager.GetAnimalAsync(animalName);
                if (response == null)
                    return NotFound(new ResponseMessage<AnimalDto>("404", "No existen elementos", response));
                response = await _animalManager.GetAnimalAsync(animalName);
                return Ok(new ResponseMessage<AnimalDto>("200", "ok", response));
            }
            catch (ExceptionApi ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al consultar los datos de animales.", id: guid));
            }
        }
        #endregion

        #region Methods POST
        /// <summary>
        /// Insert one line of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost("InsertAnimal")]
        [SwaggerResponse(200, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(401, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(404, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(405, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(500, Type = typeof(ResponseMessage<AnimalDto>))]
        public async Task<IActionResult> InsertAnimalAsync([FromBody][Required] AnimalDto animalDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Revise Parámetros requeridos", new Guid()));
                
                string user = "USER";
                var created = await _animalManager.SaveAnimalAsync(animalDto, user);

                return StatusCode(201, new ResponseMessage<AnimalDto>("201", "Datos insertados con éxito", animalDto));

            }
            catch (ExceptionApi ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al insertar los datos de animales", id: guid));
            }
        }
        #endregion

        #region Methods PUT
        /// <summary>
        /// Update one regist of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut("UpdateAnimal")]
        [SwaggerResponse(200, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(401, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(404, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(405, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(500, Type = typeof(ResponseMessage<AnimalDto>))]
        public async Task<IActionResult> UpdateAnimalAsync([FromBody][Required] AnimalDto animalDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Revise Parámetros requeridos", new Guid()));

                string user = "USER";
                bool update = await _animalManager.UpdateAnimalAsync(animalDto, user);
                if (update)
                    return Ok(new ResponseMessage<AnimalDto>("200", "ok", animalDto));
                else
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al actualizar los datos de Animal", new Guid()));
            }
            catch (ExceptionApi ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al actualizar los datos de animales", id: guid));
            }
        }
        #endregion

        #region Methods Delete
        /// <summary>
        /// Delete one regist of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("DeleteAnimal")]
        [SwaggerResponse(200, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(400, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(401, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(404, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(405, Type = typeof(ResponseMessage<AnimalDto>))]
        [SwaggerResponse(500, Type = typeof(ResponseMessage<AnimalDto>))]
        public async Task<IActionResult> DeleteAnimalAsync([Required] int IdAnimal)
        {
            try
            {                
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Revise Parámetros requeridos", new Guid()));

                bool delete = await _animalManager.DeleteAnimalAsync(IdAnimal);
                if (delete)
                    return NoContent();
                else
                    return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al actualizar los datos de Animal", new Guid()));
            }
            catch (ExceptionApi ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                return BadRequest(new ResponseMessage<AnimalDto>("400", "Error al actualizar los datos de animales", id: guid));
            }
        }
        #endregion
    }
}
