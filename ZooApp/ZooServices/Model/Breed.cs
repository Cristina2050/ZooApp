using System;
using System.Collections.Generic;
using System.Text;

namespace ZooServices.Model
{
    public partial class Breed
    {
        public Breed()
        {
            Animal = new HashSet<Animal>();
        }

        public short BreedIdPk { get; set; }
        public string BreedName { get; set; }

        public virtual ICollection<Animal> Animal { get; set; }
    }
}
