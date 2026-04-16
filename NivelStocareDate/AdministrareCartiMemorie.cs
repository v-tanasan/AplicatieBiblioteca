using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareCartiMemorie : IStocareData
    {
        private List<Carte> carti;

        public AdministrareCartiMemorie()
        {
            carti = new List<Carte>();
        }

        public void AddCarte(Carte carte)
        {
            carte.Id = GetNextIdCarte();
            carti.Add(carte);
        }

        public List<Carte> GetCarti()
        {
            return carti;
        }

        public Carte? GetCarte(int idCarte)
        {
            foreach (Carte carte in carti)
            {
                if (carte.Id == idCarte)
                {
                    return carte;
                }
            }

            return null;
        }

        public Carte? GetCarte(string titlu, string autor)
        {
            return carti?.FirstOrDefault(carte =>
                carte.Titlu.Equals(titlu, StringComparison.OrdinalIgnoreCase) &&
                carte.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase)
            );
        }

        public bool UpdateCarte(Carte c)
        {
            throw new Exception("Optiunea UpdateCarte nu este implementata");
        }

        public int GetNextIdCarte()
        {
            if (carti.Count == 0)
            {
                return 1;
            }

            return carti.Last().Id + 1;
        }

        public void RemoveCarte(int idCarte)
        {
            Console.WriteLine("\n!...Stergere carte, neimplementata...!");
            //var liniiNoi = File.ReadAllLines(numeFisier)
            //    .Where(l => int.Parse(l.Split('|')[0]) != idCarte)
            //    .ToList();

            //File.WriteAllLines(numeFisier, liniiNoi);
        }

    }
}