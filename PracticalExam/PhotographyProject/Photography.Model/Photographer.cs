using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photography.Model
{
    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Lens>();
            this.Accessories = new HashSet<Accessory>();
            this.Workshops = new HashSet<Workshop>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"\+[0-9]{1,3}\/[0-9]{8,10}")]
        public string Phone { get; set; }

        public virtual Camera PrimaryCamera { get; set; }

        public virtual Camera SecondaryCamera { get; set; }

        public virtual ICollection<Lens> Lenses { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }

      
        public virtual Workshop Workshop { get; set; }

    }
}
