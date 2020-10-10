using System;
using System.ComponentModel.DataAnnotations;

namespace gtp.api.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        
        [Required]
        public string Titulo{ get; set; }
        
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
        public bool Done {get; set; }

        public string Usuario { get; set; }


    }
}