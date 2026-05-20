namespace LibrarieModele
{
    public class Imprumut
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const int ID = 0;
        private const int IDCARTE = 1;
        private const int IDCITITOR = 2;
        private const int DATAIMPRUMUT = 3;
        private const int DATARETURNARE = 4;

        public int Id { get; set; }
        public int IdCarte { get; set; }
        public int IdCititor { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime? DataReturnare { get; set; }

        public Imprumut()
        {
            Id = 0;
            IdCarte = 0;
            IdCititor = 0;
            DataImprumut = DateTime.Now;
            DataReturnare = null;
        }

        public Imprumut(int id, int idCarte, int idCititor, DateTime dataImprumut, DateTime? dataReturnare)
        {
            Id = id;
            IdCarte = idCarte;
            IdCititor = idCititor;
            DataImprumut = dataImprumut;
            DataReturnare = dataReturnare;
        }

        //constructor cu un singur parametru de tip string care reprezinta o linie dintr-un fisier text
        public Imprumut(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier
            //prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            this.Id = Convert.ToInt32(dateFisier[ID]);
            this.IdCarte = Convert.ToInt32(dateFisier[IDCARTE]);
            this.IdCititor = Convert.ToInt32(dateFisier[IDCITITOR]);
            this.DataImprumut = DateTime.Parse(dateFisier[DATAIMPRUMUT]);

            if (!string.IsNullOrWhiteSpace(dateFisier[DATARETURNARE]))
            {
                this.DataReturnare = DateTime.Parse(dateFisier[DATARETURNARE]);
            }else
            {
                this.DataReturnare = null;
            }
        }

        public string ConversieLaSirPentruFisier()
        {
            string obiectImprumutPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                SEPARATOR_PRINCIPAL_FISIER,
                Id.ToString(),
                IdCarte.ToString(),
                IdCititor.ToString(),
                (DataImprumut.ToString("o")),
                (DataReturnare?.ToString("o") ?? "")
                );

            return obiectImprumutPentruFisier;
        }
    }
}
