using Newtonsoft.Json.Linq;

namespace CDA.Models
{
    public class Auteur
    {

        public string? IdAuteur { get; set; }    
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public DateTime DateNaissance { get; set; }

        public virtual ICollection<Livre>? Livres { get; set; }

        
    }
}
