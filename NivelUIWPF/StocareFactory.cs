using NivelStocareDate;
using System.Configuration;
using System.IO;

namespace NivelUIWPF
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER_CARTI = "NumeFisierCarti";
        private const string NUME_FISIER_CITITORI = "NumeFisierCititori";
        private const string NUME_FISIER_IMPRUMUTURI = "NumeFisierImprumuturi";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";

            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER_CARTI] ?? "";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? "";
            // setare locatie fisier in directorul corespunzator solutiei
            // astfel incat datele din fisier sa poata fi utilizate si de alte proiecte
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;


            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareCartiMemorie();
                    case "txt":
                        return new AdministrareCartiFisierText(caleCompletaFisier + "." + formatSalvare);
                }
            }

            return null;
        }

        //--- pentru Cititori ----
        public static IStocareCititori GetAdministratorStocareCititori()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";

            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER_CITITORI] ?? "";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? "";
            // setare locatie fisier in directorul corespunzator solutiei
            // astfel incat datele din fisier sa poata fi utilizate si de alte proiecte
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;


            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareCititoriMemorie();
                    case "txt":
                        return new AdministrareCititoriFisierText(caleCompletaFisier + "." + formatSalvare);
                }
            }

            return null;
        }

        //--- pentru Imprumuturi ----
        public static IStocareImprumuturi GetAdministratorStocareImprumuturi()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";

            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER_IMPRUMUTURI] ?? "";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? "";
            // setare locatie fisier in directorul corespunzator solutiei
            // astfel incat datele din fisier sa poata fi utilizate si de alte proiecte
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;


            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareImprumuturiMemorie();
                    case "txt":
                        return new AdministrareImprumuturiFisierText(caleCompletaFisier + "." + formatSalvare);
                }
            }

            return null;
        }

    }
}
