namespace Ospedale.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Farmaco")]
    public partial class Farmaco
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farmaco()
        {
            Visita = new HashSet<Visita>();
        }

        [Key]
        public int IdFarmaco { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome Farmaco")]
        public string NomeFarmaco { get; set; }

        [Display(Name = "Quantità")]
        public int Quantià { get; set; }

        [Display(Name = "Nome Reparto")]
        public int IdReparto { get; set; }

        public virtual Reparto Reparto { get; set; }

        [Display(Name = "Data Invio Farmaco")]
        public DateTime DataInvioFarmaco { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
