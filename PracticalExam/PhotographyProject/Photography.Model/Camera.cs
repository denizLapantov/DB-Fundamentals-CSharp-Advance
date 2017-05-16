using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photography.Model
{
    public class Camera
    {
        public Camera()
        {
            this.PrimaryPhotographers = new HashSet<Photographer>();
            this.SecondaryPhotographers = new HashSet<Photographer>();
        }

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool IsFullFrame { get; set; }

        [Range(100,Int32.MaxValue)]
        public int MinIso { get; set; }

        public int MaxIso { get; set; }

        [InverseProperty("PrimaryCamera")]
        public virtual ICollection<Photographer> PrimaryPhotographers { get; set; }

        [InverseProperty("SecondaryCamera")]
        public virtual ICollection<Photographer> SecondaryPhotographers { get; set; }




    }
}
