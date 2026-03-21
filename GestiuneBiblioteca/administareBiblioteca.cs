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
    }
}
