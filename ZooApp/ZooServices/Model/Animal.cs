using System;
using System.Collections.Generic;
using System.Text;

namespace ZooServices.Model
{
    public partial class Animal
    {

        public int AnimIdPk { get; set; }
        public short BreedIdPk { get; set; }
        public string AnimName { get; set; }
        public short AnimAge { get; set; }
        public decimal AnimWeight { get; set; }        
        public string AnimCharacteristics { get; set; }
        public string AnimUserId { get; set; }
        public DateTime AnimCreateDate { get; set; }
        public DateTime AnimUpdateDate { get; set; }

        public virtual Breed Breeds { get; set; }
    }
    
}
