using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareImprumuturi
    {
        void AddImprumut(Imprumut imprumut);
        void RemoveImprumut(int idImprumut);
        List<Imprumut> GetImprumuturi();
        Imprumut GetImprumut(int id);
        Imprumut GetImprumut(int idcititor, DateTime dataimprumut);
        bool UpdateImprumut(Imprumut imprumutActualizat);
    }
}
