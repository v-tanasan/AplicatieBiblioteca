using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        void AddCarte(Carte carte);
        void RemoveCarte(int idCarte);
        List<Carte> GetCarti();
        Carte GetCarte(int id);
        Carte GetCarte(string titlu, string autor);
        bool UpdateCarte(Carte carteActualizata);
    }
}
