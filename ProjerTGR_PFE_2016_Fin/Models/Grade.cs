//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjerTGR_PFE_2016_Fin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Grade
    {
        public Grade()
        {
            this.Union_ref_bo = new HashSet<Union_ref_bo>();
        }
    
        public int id_grad { get; set; }

        [Required]
        [Display(Name = "Numéro Grade")]
        public Nullable<int> Num_gard { get; set; }

        [Required]
        [DisplayName(" Grade")]
        public string Nom_grad { get; set; }

         [Required]
        public Nullable<int> id_corp { get; set; }
    
        public virtual Corps Corps { get; set; }
        public virtual ICollection<Union_ref_bo> Union_ref_bo { get; set; }
    }
}
