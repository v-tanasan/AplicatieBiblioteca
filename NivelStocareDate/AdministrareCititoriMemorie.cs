using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareCititoriMemorie : IStocareCititori
    {
        private List<Cititor> cititori;

        public AdministrareCititoriMemorie()
        {
            cititori = new List<Cititor>();
        }

        public void AddCititor(Cititor cititor)
        {
            cititor.Id = GetNextIdCititor();
            cititori.Add(cititor);
        }

        public List<Cititor> GetCititori()
        {
            return cititori;
        }

        public Cititor? GetCititor(int idCititor)
        {
            foreach (Cititor cititor in cititori)
            {
                if (cititor.Id == idCititor)
                {
                    return cititor;
                }
            }

            return null;
        }

        public Cititor? GetCititor(string nume, string cnp)
        {
            return cititori?.FirstOrDefault(cititor =>
                cititor.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) &&
                cititor.Cnp.Equals(cnp, StringComparison.OrdinalIgnoreCase)
            );
        }

        public bool UpdateCititor(Cititor c)
        {
            throw new Exception("Optiunea UpdateCititor nu este implementata");
        }

        public int GetNextIdCititor()
        {
            if (cititori.Count == 0)
            {
                return 1;
            }

            return cititori.Last().Id + 1;
        }

        public void RemoveCititor(int idCititor)
        {
            Console.WriteLine("\n!...Stergere carte, neimplementata...!");
            //var liniiNoi = File.ReadAllLines(numeFisier)
            //    .Where(l => int.Parse(l.Split('|')[0]) != idCarte)
            //    .ToList();

            //File.WriteAllLines(numeFisier, liniiNoi);
        }

    }
}