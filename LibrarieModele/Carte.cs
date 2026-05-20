namespace LibrarieModele
{
    public class Carte
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const int ID = 0;
        private const int TITLU = 1;
        private const int AUTOR = 2;
        private const int STATUS = 3;

        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public string Status { get; set; }

        public Carte()
        {
            Id = 0;
            Titlu = string.Empty;
            Autor = string.Empty;
            Status = string.Empty;
        }

        public Carte(int id, string titlu, string autor, string status)
        {
            Id = id;
            Titlu = titlu;
            Autor = autor;
            Status = status;
        }
 
        //constructor cu un singur parametru de tip string care reprezinta o linie dintr-un fisier text
        public Carte(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier
            //prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            this.Id = Convert.ToInt32(dateFisier[ID]);
            this.Titlu = dateFisier[TITLU];
            this.Autor = dateFisier[AUTOR];
            this.Status = dateFisier[STATUS];
        }
        
        public string ConversieLaSirPentruFisier()
        {
            string obiectCartePentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}",
                SEPARATOR_PRINCIPAL_FISIER,
                Id.ToString(),
                (Titlu ?? " NECUNOSCUT "),
                (Autor ?? " NECUNOSCUT "),
                (Status ?? " NECUNOSCUT ")
                );

            return obiectCartePentruFisier;
        }
    }
}
