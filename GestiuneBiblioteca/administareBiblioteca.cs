using LibrarieModele;

namespace GestiuneBiblioteca
{
    public class administareBiblioteca
    {
        private List<Carte> carti;

        public administareBiblioteca()
        {
            carti = new List<Carte>();
        }

        public List<Carte> getCarti()
        { 
            return carti;
        }

        public void addCarte(Carte carte)
        {
            carte.Id = getNextId();
            carti.Add(carte);
        }

        public void removeCarte(List<Carte> lCarti, Carte carte)
        {
            if (carte == null)
            {
                Console.WriteLine("\nCartea nu a fost gasita in biblioteca!");
                return;
            }
            
            lCarti.Remove(carte);
            Console.WriteLine("\nCarte stearsa cu succes!");
        }

        public int getNextId()
        {
            if (carti.Count == 0)
            {
                return 1;
            }
            else
            {
                return carti.Last().Id + 1;
            }
        }
    }
}
