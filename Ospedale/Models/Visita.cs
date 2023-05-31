namespace Ospedale.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visita")]
    public partial class Visita
    {
        [Key]
        public int IdVisita { get; set; }

        [Display(Name = "Cognome Medico")]
        public int IdMedico { get; set; }

        [Display(Name = "Cognome Reparto")]
        public int IdReparto { get; set; }

        [Display(Name = "CF Paziente")]
        public int IdPaziente { get; set; }

        [Display(Name = "Data Visita")]
        public DateTime DataVisita { get; set; }

        [Required]
        [Display(Name = "Stato Paziente")]
        public string StatoPaziente { get; set; }

        [Display(Name = "Pressione Min")]
        public int PressioneMinima { get; set; }

        [Display(Name = "Pressione Max")]
        public int PressioneMassima { get; set; }

        [Display(Name = "T°")]
        public decimal Temperatura { get; set; }

        [Display(Name = "Freqeunza Cardiaca")]
        public int FrequenzaCardiaca { get; set; }

        [Display(Name = "Nome Farmaco")]
        public int? IdFarmaco { get; set; }

        [Display(Name = "Nome Visita")]
        public string NomeVisita { get; set; }

        [StringLength(50)]
        public string Posologia { get; set; }

        public virtual Farmaco Farmaco { get; set; }

        public virtual Medico Medico { get; set; }

        public virtual Paziente Paziente { get; set; }

        public virtual Reparto Reparto { get; set; }
    }

    public class VisitaJson
    {
        public int IdVisita { get; set; }

        public int IdMedico { get; set; }

        public int IdReparto { get; set; }

        public int IdPaziente { get; set; }

        public DateTime DataVisita { get; set; }

        [Required]
        public string StatoPaziente { get; set; }

        public int PressioneMinima { get; set; }

        public int PressioneMassima { get; set; }

        public decimal Temperatura { get; set; }

        public int FrequenzaCardiaca { get; set; }

        public int? IdFarmaco { get; set; }

        public string NomeVisita { get; set; }

        [StringLength(50)]
        public string Posologia { get; set; }

        public string NomeReparto { get; set; }

        public string CognomeMedico { get; set; }
    }

    public class RepartoJson
    {
        public string NomeReparto { get; set; }
        public int NumeroRicoveri { get; set; }
    }
}
