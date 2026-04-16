using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareCartiFisierText : IStocareData
    {
        private const int ID_PRIMA_CARTE = 1;
        private const int INCREMENT = 1;
        private string numeFisier;

        public AdministrareCartiFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // se incearca deschiderea fisierului in modul OpenOrCreate
            // astfel incat sa fie creat daca nu exista
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddCarte(Carte carte)
        {
            carte.Id = GetNextIdCarte();

            // instructiunea 'using' va apela la final streamWriterFisierText.Close();
            // al doilea parametru setat la 'true' al constructorului StreamWriter indica
            // modul 'append' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(carte.ConversieLaSirPentruFisier());
            }
        }

        public List<Carte> GetCarti()
        {
            List<Carte> carti = new List<Carte>();

            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    carti.Add(new Carte(linieFisier));
                }
            }

            return carti;
        }

        public Carte GetCarte(string titlu, string autor)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Carte carte = new Carte(linieFisier);
                    if (carte.Titlu.Equals(titlu) && carte.Autor.Equals(autor))
                        return carte;
                }
            }

            return null;
        }

        public Carte GetCarte(int idCarte)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Carte carte = new Carte(linieFisier);
                    if (carte.Id == idCarte)
                        return carte;
                }
            }

            return null;
        }

        public bool UpdateCarte(Carte carteActualizata)
        {
            List<Carte> carti = GetCarti();
            bool actualizareCuSucces = false;

            //instructiunea 'using' va apela la final swFisierText.Close();
            //al doilea parametru setat la 'false' al constructorului StreamWriter indica modul 'overwrite' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, false))
            {
                foreach (Carte carte in carti)
                {
                    Carte cartePentruScrisInFisier = carte;
                    //informatiile despre cartea actualizata vor fi preluate din parametrul "carteActualizata"
                    if (carte.Id == carteActualizata.Id)
                    {
                        cartePentruScrisInFisier = carteActualizata;
                    }
                    streamWriterFisierText.WriteLine(cartePentruScrisInFisier.ConversieLaSirPentruFisier());
                }
                actualizareCuSucces = true;
            }

            return actualizareCuSucces;
        }

        private int GetNextIdCarte()
        {
            int IdCarte = ID_PRIMA_CARTE;

            List<Carte> carti = GetCarti();

            if (carti.Count == 0)
            {
                return 1;
            }

            return carti.Last().Id + INCREMENT;

        }

        public void RemoveCarte(int idCarte)
        {
            var liniiNoi = File.ReadAllLines(numeFisier)
                .Where(l => int.Parse(l.Split(';')[0]) != idCarte)
                .ToList();

            File.WriteAllLines(numeFisier, liniiNoi);
        }
    }
}
