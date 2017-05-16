using System.ComponentModel.DataAnnotations;

namespace Photography.Model
{
    public class Lens
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public int FocalLenght { get; set; }

        [RegularExpression(@"^\d+.\d{0,1}$")]
        public double MaxAperture  { get; set; }

        public string CompatibleWith { get; set; }

        public virtual Photographer Owner { get; set; }


    }
}
