using AutoMapper;
using DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Exceptions;
using ZooServices.Contracts.Zoo;
using ZooServices.DataLayer;
using ZooServices.Model;
using ZooServices.Repository.MySQL.Contract;

namespace ZooServices.BusinessLayer
{
    /// <summary>
    /// Implement contract of AnimalManager
    /// </summary>
    public class AnimalManager : IAnimalManager
    {
        #region Properties
        private AnimalData _animalData;
        private readonly ILogger<AnimalManager> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalManager"/> class.
        /// </summary>
        /// <param name="imapper"></param>
        /// <param name="zooRepository"></param>
        /// <param name="logger"></param>
        public AnimalManager(IMapper imapper, IZooRepository<Animal> zooRepository, ILogger<AnimalManager> logger)
        {
            _animalData = new AnimalData(imapper, zooRepository);
            _logger = logger;
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Get all Animal data
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<AnimalDto>> GetAllAnimalAsync()
        {
            try
            {
                ICollection<AnimalDto> animalDtos = await _animalData.GetAllAnimalsData();
                return animalDtos;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionApi(guid, "Existió un error al momento obtener la colección de datos de animales.");
            }
        }

        /// <summary>
        /// Get one Animal data
        /// </summary>
        /// <returns></returns>
        public async Task<AnimalDto> GetAnimalAsync(string name)
        {
            try
            {
                AnimalDto animalDtos = await _animalData.FindAnimalByNameData(name);
                return animalDtos;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionApi(guid, "Existió un error al momento obtener datos de animales.");
            }
        }
        #endregion

        #region Insert Methods
        /// <summary>
        /// Save one object of animals
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        public async Task<bool> SaveAnimalAsync(AnimalDto animalDto, string user)
        {
            try
            {
                bool result = await _animalData.SaveAnimal(animalDto, user);
                return result;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionApi(guid, "Existió un error al momento insertar los datos de las animales.");
            }
        }       
        #endregion

        #region Update Region
        /// <summary>
        /// Update data of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAnimalAsync(AnimalDto animalDto, string user)
        {
            try
            {
                DateTime updateDate = DateTime.Now;
                bool result = await _animalData.UpdateAnimal(animalDto, user);
                return result;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionApi(guid, "Existió un error al momento modificar los datos de animales.");
            }
        }
        #endregion

        #region Delete Region
        /// <summary>
        /// Delete data of Animal
        /// </summary>
        /// <param name="idAnimal"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAnimalAsync(int idAnimal)
        {
            try
            {
                DateTime updateDate = DateTime.Now;
                bool result = await _animalData.DeleteAnimal(idAnimal);
                return result;
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.Log(LogLevel.Error, ex, guid.ToString());
                throw new ExceptionApi(guid, "Existió un error al momento elimianr los datos de animales.");
            }
        }
        #endregion
    }
}
