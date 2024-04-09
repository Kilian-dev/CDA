namespace CDA.Models
{
    public class Livre
    {

        public string? IdLivre { get; set; }

        public string? Nom { get; set; }

        public string? Genre { get; set; }

        public double Prix { get; set; }


        public string? DateParution { get; set; }

        // Relationnel

        public string? IdAuteur { get; set; }

        public virtual Auteur? Auteur { get; set; }

    }
}
