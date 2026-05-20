using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareCititoriFisierText : IStocareCititori
    {
        private const int ID_PRIMUL_CITITOR = 1;
        private const int INCREMENT = 1;
        private string numeFisier;

        public AdministrareCititoriFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // se incearca deschiderea fisierului in modul OpenOrCreate
            // astfel incat sa fie creat daca nu exista
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddCititor(Cititor cititor)
        {
            cititor.Id = GetNextIdCititor();

            // instructiunea 'using' va apela la final streamWriterFisierText.Close();
            // al doilea parametru setat la 'true' al constructorului StreamWriter indica
            // modul 'append' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(cititor.ConversieLaSirPentruFisier());
            }
        }

        public List<Cititor> GetCititori()
        {
            List<Cititor> cititori = new List<Cititor>();

            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    cititori.Add(new Cititor(linieFisier));
                }
            }

            return cititori;
        }

        public Cititor GetCititor(string nume, string cnp)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Cititor cititor = new Cititor(linieFisier);
                    if (cititor.Nume.Equals(nume) && cititor.Cnp.Equals(cnp))
                        return cititor;
                }
            }

            return null;
        }

        public Cititor GetCititor(int idCititor)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Cititor cititor = new Cititor(linieFisier);
                    if (cititor.Id == idCititor)
                        return cititor;
                }
            }

            return null;
        }

        public bool UpdateCititor(Cititor cititorActualizat)
        {
            List<Cititor> cititori = GetCititori();
            bool actualizareCuSucces = false;

            //instructiunea 'using' va apela la final swFisierText.Close();
            //al doilea parametru setat la 'false' al constructorului StreamWriter indica modul 'overwrite' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, false))
            {
                foreach (Cititor cititor in cititori)
                {
                    Cititor cititorPentruScrisInFisier = cititor;
                    //informatiile despre cartea actualizata vor fi preluate din parametrul "carteActualizata"
                    if (cititor.Id == cititorActualizat.Id)
                    {
                        cititorPentruScrisInFisier = cititorActualizat;
                    }
                    streamWriterFisierText.WriteLine(cititorPentruScrisInFisier.ConversieLaSirPentruFisier());
                }
                actualizareCuSucces = true;
            }

            return actualizareCuSucces;
        }

        private int GetNextIdCititor()
        {
            int IdCititor = ID_PRIMUL_CITITOR;

            List<Cititor> cititori = GetCititori();

            if (cititori.Count == 0)
            {
                return 1;
            }

            return cititori.Last().Id + INCREMENT;

        }

        public void RemoveCititor(int idCititor)
        {
            var liniiNoi = File.ReadAllLines(numeFisier)
                .Where(l => int.Parse(l.Split(';')[0]) != idCititor)
                .ToList();

            File.WriteAllLines(numeFisier, liniiNoi);
        }
    }
}
