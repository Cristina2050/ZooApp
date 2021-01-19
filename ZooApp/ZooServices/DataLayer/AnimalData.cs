using AutoMapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooServices.Model;
using ZooServices.Repository.MySQL.Contract;

namespace ZooServices.DataLayer
{
    /// <summary>
    /// Class that manage persistence of Animal
    /// </summary>
    public class AnimalData
    {
        #region Properties
        internal IMapper _imapper;
        internal IZooRepository<Animal> _zooRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalData"/> class.
        /// </summary>
        /// <param name="imapper"></param>
        /// <param name="zooRepository"></param>
        public AnimalData(IMapper imapper, IZooRepository<Animal> zooRepository)
        {
            _imapper = imapper;
            _zooRepository = zooRepository;
        }
        #endregion

        #region Get Methods       

        /// <summary>
        /// Get all data of Animals 
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<AnimalDto>> GetAllAnimalsData()
        {
            try
            {
                ICollection<AnimalDto> _animalDtos = new List<AnimalDto>();
                ICollection<Animal> animals = _zooRepository.GetAll().Include(p=> p.Breeds).OrderBy(x => x.AnimName).ToList();

                if (animals != null)
                    _animalDtos = _imapper.Map<ICollection<Animal>, ICollection<AnimalDto>>(animals);

                return Task.FromResult(_animalDtos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get one register of Animals 
        /// </summary>
        /// <returns></returns>
        public Task<AnimalDto> FindAnimalByNameData(string name)
        {
            try
            {
                AnimalDto animalDto = new AnimalDto();
                Animal animal = _zooRepository.FindOne(p => p.AnimName.Equals(name));

                if (animal != null)
                    animalDto = _imapper.Map<Animal, AnimalDto>(animal);

                return Task.FromResult(animalDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Insert Methods


        /// <summary>
        /// Insert one register of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        public Task<bool> SaveAnimal(AnimalDto animalDto, string user)
        {
            try
            {
                Animal animal = _imapper.Map<AnimalDto, Animal>(animalDto);
                animal.AnimCreateDate = DateTime.Now;
                animal.AnimUserId = user;
                _zooRepository.Add(animal);
                _zooRepository.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update Methods
        /// <summary>
        /// Update one register of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        public Task<bool> UpdateAnimal(AnimalDto animalDto, string user)
        {
            try
            {
                bool updateResult = false;
                if (animalDto.Id != 0)
                {
                    Animal animal = _zooRepository.FindOne(x => x.AnimIdPk == animalDto.Id);

                    if (animal.AnimIdPk > 0)
                        animal = _imapper.Map<AnimalDto, Animal>(animalDto);
                        animal.AnimUpdateDate = DateTime.Now;
                        animal.AnimUserId = user;
                        _zooRepository.Update(animal);
                        _zooRepository.SaveChanges();
                        updateResult = true;
                }
                
                return Task.FromResult(updateResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete Methods
        /// <summary>
        /// Delete one register of Animal
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        public Task<bool> DeleteAnimal(int animalId)
        {
            try
            {
                bool updateResult = false;
                if (animalId != 0)
                {
                    Animal animal = _zooRepository.FindOne(x => x.AnimIdPk == animalId);

                    if (animal.AnimIdPk > 0)                       
                        _zooRepository.Delete(animal);
                        _zooRepository.SaveChanges();
                        updateResult = true;
                }

                return Task.FromResult(updateResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
