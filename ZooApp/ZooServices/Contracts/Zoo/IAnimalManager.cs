using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZooServices.Contracts.Zoo
{
    /// <summary>
    /// Contract of Animal Manager
    /// </summary>
    public interface IAnimalManager
    {
        /// <summary>
        /// Get all Animals
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<AnimalDto>> GetAllAnimalAsync();

        /// <summary>
        /// Get one Animal
        /// </summary>
        /// <returns></returns>
        public Task<AnimalDto> GetAnimalAsync(string name);


        /// <summary>
        /// Save one object of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <returns></returns>
        public Task<bool> SaveAnimalAsync(AnimalDto animalDto, string user);

        /// <summary>
        /// Update data of Animal
        /// </summary>
        /// <param name="animalDto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> UpdateAnimalAsync(AnimalDto animalDto, string user);


        /// <summary>
        /// Delete data of Animal
        /// </summary>
        /// <param name="idAnimal"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> DeleteAnimalAsync(int idAnimal);
    }
}
