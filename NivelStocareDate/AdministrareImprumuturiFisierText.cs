using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareImprumuturiFisierText : IStocareImprumuturi
    {
        private const int ID_PRIMUL_IMPRUMUT = 1;
        private const int INCREMENT = 1;
        private string numeFisier;

        public AdministrareImprumuturiFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // se incearca deschiderea fisierului in modul OpenOrCreate
            // astfel incat sa fie creat daca nu exista
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddImprumut(Imprumut imprumut)
        {
            imprumut.Id = GetNextIdImprumut();

            // instructiunea 'using' va apela la final streamWriterFisierText.Close();
            // al doilea parametru setat la 'true' al constructorului StreamWriter indica
            // modul 'append' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(imprumut.ConversieLaSirPentruFisier());
            }
        }

        public List<Imprumut> GetImprumuturi()
        {
            List<Imprumut> imprumuturi = new List<Imprumut>();

            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    imprumuturi.Add(new Imprumut(linieFisier));
                }
            }

            return imprumuturi;
        }

        public Imprumut GetImprumut(int idcititor, DateTime dataimprumut)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Imprumut imprumut = new Imprumut(linieFisier);
                    if (imprumut.IdCititor == idcititor && imprumut.DataImprumut == dataimprumut)
                        return imprumut;
                }
            }

            return null;
        }

        public Imprumut GetImprumut(int idImprumut)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Imprumut imprumut = new Imprumut(linieFisier);
                    if (imprumut.Id == idImprumut)
                        return imprumut;
                }
            }

            return null;
        }

        public bool UpdateImprumut(Imprumut imprumutActualizat)
        {
            List<Imprumut> imprumuturi = GetImprumuturi();
            bool actualizareCuSucces = false;

            //instructiunea 'using' va apela la final swFisierText.Close();
            //al doilea parametru setat la 'false' al constructorului StreamWriter indica modul 'overwrite' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, false))
            {
                foreach (Imprumut imprumut in imprumuturi)
                {
                    Imprumut imprumutPentruScrisInFisier = imprumut;
                    //informatiile despre cartea actualizata vor fi preluate din parametrul "carteActualizata"
                    if (imprumut.Id == imprumutActualizat.Id)
                    {
                        imprumutPentruScrisInFisier = imprumutActualizat;
                    }
                    streamWriterFisierText.WriteLine(imprumutPentruScrisInFisier.ConversieLaSirPentruFisier());
                }
                actualizareCuSucces = true;
            }

            return actualizareCuSucces;
        }

        private int GetNextIdImprumut()
        {
            int IdImprumut = ID_PRIMUL_IMPRUMUT;

            List<Imprumut> imprumuturi = GetImprumuturi();

            if (imprumuturi.Count == 0)
            {
                return 1;
            }

            return imprumuturi.Last().Id + INCREMENT;

        }

        public void RemoveImprumut(int idImprumut)
        {
            var liniiNoi = File.ReadAllLines(numeFisier)
                .Where(l => int.Parse(l.Split(';')[0]) != idImprumut)
                .ToList();

            File.WriteAllLines(numeFisier, liniiNoi);
        }
    }
}
