using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareCititori
    {
        void AddCititor(Cititor cititor);
        void RemoveCititor(int idCititor);
        List<Cititor> GetCititori();
        Cititor GetCititor(int id);
        Cititor GetCititor(string nume, string cnp);
        bool UpdateCititor(Cititor cititorActualizat);
    }
}
