namespace LibrarieModele
{
    public class Carte
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        
        public Carte()
        { 
            Id = 0;
            Titlu = string.Empty;
            Autor = string.Empty;
        }

        public Carte(int id, string titlu, string autor)
        {
            Id = id;
            Titlu = titlu;
            Autor = autor;
        }
    }
}
