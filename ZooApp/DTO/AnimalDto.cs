using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class AnimalDto
    {
        /// <summary>
        /// Unique code of animal
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Breed id of animal
        /// </summary>
        [Required]
        public short IdBreed { get; set; }

        /// <summary>
        /// Name of animal
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Name of animal
        /// </summary>
        [Required]
        public short Age{ get; set; }

        /// <summary>
        /// Characteristics of animal
        /// </summary>
        public string Characteristics { get; set; }

        /// <summary>
        /// Weight of animal
        /// </summary>
        [Required]
        public decimal Weight { get; set; }

        
    }
}
