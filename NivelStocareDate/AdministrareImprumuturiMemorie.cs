using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareImprumuturiMemorie : IStocareImprumuturi
    {
        private List<Imprumut> imprumuturi;

        public AdministrareImprumuturiMemorie()
        {
            imprumuturi = new List<Imprumut>();
        }

        public void AddImprumut(Imprumut imprumut)
        {
            imprumut.Id = GetNextIdImprumut();
            imprumuturi.Add(imprumut);
        }

        public List<Imprumut> GetImprumuturi()
        {
            return imprumuturi;
        }

        public Imprumut? GetImprumut(int idImprumut)
        {
            foreach (Imprumut imprumut in imprumuturi)
            {
                if (imprumut.Id == idImprumut)
                {
                    return imprumut;
                }
            }

            return null;
        }

        public Imprumut? GetImprumut(int idimprumut, DateTime dataimprumut)
        {
            return imprumuturi?.FirstOrDefault(imprumut =>
                imprumut.Id == idimprumut &&
                imprumut.DataImprumut == dataimprumut
            );
        }

        public bool UpdateImprumut(Imprumut i)
        {
            throw new Exception("Optiunea UpdateImprumut nu este implementata");
        }

        public int GetNextIdImprumut()
        {
            if (imprumuturi.Count == 0)
            {
                return 1;
            }

            return imprumuturi.Last().Id + 1;
        }

        public void RemoveImprumut(int idImprumut)
        {
            Console.WriteLine("\n!...Stergere carte, neimplementata...!");
            //var liniiNoi = File.ReadAllLines(numeFisier)
            //    .Where(l => int.Parse(l.Split('|')[0]) != idCarte)
            //    .ToList();

            //File.WriteAllLines(numeFisier, liniiNoi);
        }

    }
}