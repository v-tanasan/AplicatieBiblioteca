using LibrarieModele;
using GestiuneBiblioteca;

namespace BibliotecaApp
{
    class Program
    {
        public static void Main()
        {
            Carte? carteNoua = null;
            administareBiblioteca adminBiblioteca = new administareBiblioteca();
            List<Carte> biblioteca = adminBiblioteca.getCarti();
            string optiune;

            do
            {
                Console.WriteLine("\n== Aplicatie Biblioteca ==\n");
                Console.WriteLine("Selectati o optiune:");
                Console.WriteLine("C. Adaugare carte");
                Console.WriteLine("U. Afiseaza ultima carte introdusa");
                Console.WriteLine("A. Afiseaza carti");
                Console.WriteLine("S. Sterge carte");
                Console.WriteLine("M. Inregistreaza cititor");
                Console.WriteLine("I. Imprumuta carte");
                Console.WriteLine("R. Returneaza carte");
                Console.WriteLine("X. Exit\n");

                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        carteNoua = citireCarteTastatura();
                        if (carteNoua != null)
                        {
                            adminBiblioteca.addCarte(carteNoua);
                            Console.WriteLine("\nCarte adaugata cu succes!");
                        }
                        break;

                    case "S":
                        Carte stergCarte = citireTitluCarteTastatura(biblioteca);
                        adminBiblioteca.removeCarte(biblioteca, stergCarte);
                        break;

                    case "U":
                        Console.WriteLine("\nUltima carte introdusa:");
                        afisareCarteNoua(carteNoua);
                        break;

                    case "A":
                        Console.WriteLine("\nCarti in biblioteca:");
                        if (biblioteca.Count != 0)
                        {
                            foreach (var carte in biblioteca)
                            {
                                Console.WriteLine($"Id: {carte.Id}, Titlu: \"{carte.Titlu}\", Autor: {carte.Autor}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Biblioteca nu are carti...\n");
                        }
                        break;

                    case "X":
                        Console.WriteLine("\nIesire din aplicatie...");
                        return;

                    default:
                        Console.WriteLine("!! Optiune invalida. Va rugam selectati o optiune valida !!");
                        break;
                        }
            } while (optiune.ToUpper() != "X");
            Console.ReadKey();
        }

        public static Carte citireCarteTastatura()
        {
            Console.WriteLine("\nIntroduceti titlul cartii:");
            string titlu = Console.ReadLine();
            Console.WriteLine("Introduceti autorul cartii:");
            string autor = Console.ReadLine();
            Carte carte = new Carte(0, titlu, autor);
            return carte;
        }

        public static Carte citireTitluCarteTastatura(List<Carte> biblio)
        {
            Carte stergCarte = null;
            Console.WriteLine("\nIntroduceti titlul cartii de sters:");
            string titluCarte = Console.ReadLine();
            foreach (var c in biblio)
            {
                if (c.Titlu == titluCarte)
                { 
                    stergCarte = c;
                    break;
                }
            }
            return stergCarte;
        }

        public static void afisareCarteNoua(Carte cNoua)
        {
            if (cNoua != null)
            {
                Console.WriteLine($"Id:{cNoua.Id} ,Titlu: \"{cNoua.Titlu}\", Autor: {cNoua.Autor}");
            }else
            {
                Console.WriteLine("Nu a fost introdusa nici o carte noua...");
            }
        }
    }
}
