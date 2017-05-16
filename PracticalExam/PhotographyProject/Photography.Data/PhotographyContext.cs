using Photography.Model;

namespace Photography.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotographyContext : DbContext
    {
        
        public PhotographyContext()
            : base("name=PhotographyContext")
        {
        }

        public virtual IDbSet<Camera>  Cameras { get; set; }

        public  virtual IDbSet<Accessory> Accessories { get; set; }

        public virtual IDbSet<Lens> Lenses  { get; set; }

        public virtual IDbSet<Photographer> Photographers { get; set; }

        public virtual IDbSet<Workshop> Workshops { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>()
                .Map<DslrCamera>(m =>
                    m.Requires("DiscrColumnForDslrCamera")
                        .HasValue("DefaultDslrCamera")
                )
                .Map<MirrorlessCamera>(m =>
                    m.Requires("DiscrColumnForMirrorlessCamera")
                        .HasValue("DefaultMirrorlessCamera"));

            

            base.OnModelCreating(modelBuilder);
        }
    }

   
}