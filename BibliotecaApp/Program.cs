using System;
using GestiuneBiblioteca;
using LibrarieModele;

namespace BibliotecaApp
{
    class Program
    {
        public enum OptiuneMeniu
        {
            AdaugaCarte = 1,
            AfiseazaUltimaCarte = 2,
            AfiseazaCarti = 3,
            StergeCarte = 4,
            CautaCarte = 5,
            CautaAutor = 6,
            InregistrareCititor = 7,
            ImprumutaCarte = 8,
            ReturneazaCarte = 9,
            Iesire = 0
        }

        public static void Main()
        {
            Carte? carteNoua = null;
            administareBiblioteca adminBiblioteca = new administareBiblioteca();
            List<Carte> biblioteca = adminBiblioteca.getCarti();
            OptiuneMeniu optiune;

            do
            {
                Console.WriteLine("\n== Aplicatie Biblioteca ==\n");
                Console.WriteLine("Selectati o optiune:");
                Console.WriteLine("1. Adaugare carte");
                Console.WriteLine("2. Afiseaza ultima carte introdusa");
                Console.WriteLine("3. Afiseaza carti");
                Console.WriteLine("4. Sterge carte");
                Console.WriteLine("5. Cauta carte");
                Console.WriteLine("6. Cauta autor");
                Console.WriteLine("7. Inregistreaza cititor");
                Console.WriteLine("8. Imprumuta carte");
                Console.WriteLine("9. Returneaza carte");
                Console.WriteLine("0. Exit\n");

                int input = int.Parse(Console.ReadLine());
                optiune = (OptiuneMeniu)input;

                switch (optiune)
                {
                    case OptiuneMeniu.AdaugaCarte:
                        carteNoua = citireCarteTastatura();
                        if (carteNoua != null)
                        {
                            adminBiblioteca.addCarte(carteNoua);
                            Console.WriteLine("\nCarte adaugata cu succes!");
                        }
                        break;

                    case OptiuneMeniu.StergeCarte:
                        Carte stergCarte = citireTitluCarteTastatura(biblioteca);
                        adminBiblioteca.removeCarte(biblioteca, stergCarte);
                        break;

                    case OptiuneMeniu.AfiseazaUltimaCarte:
                        Console.WriteLine("\nUltima carte introdusa:");
                        afisareCarteNoua(carteNoua);
                        break;

                    case OptiuneMeniu.AfiseazaCarti:
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

                    case OptiuneMeniu.CautaCarte:
                        cautareTitluCarti(biblioteca);
                        break;

                    case OptiuneMeniu.CautaAutor:
                        linq_cautareAutorCarti(biblioteca);
                        break;

                    case OptiuneMeniu.Iesire:
                        Console.WriteLine("\nIesire din aplicatie...");
                        return;

                    default:
                        Console.WriteLine("!! Optiune invalida. Va rugam selectati o optiune valida !!");
                        break;
                }
            } while (optiune != OptiuneMeniu.Iesire);
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

        public static void cautareTitluCarti(List<Carte> biblio)
        { 
            Console.WriteLine("\nIntroduceti titlul cartii de cautat:");
            string titluCarte = Console.ReadLine();
            var cartiGasite = biblio.Where(a => 
                a.Titlu.Contains(titluCarte, StringComparison.OrdinalIgnoreCase));
            foreach (Carte c in biblio)
            {
                Console.WriteLine($"Id:{c.Id} ,Titlu: \"{c.Titlu}\", Autor: {c.Autor}");
            }
            
            if (!cartiGasite.Any())
            {
                Console.WriteLine("Cartea nu a fost gasita in biblioteca...");
            }
        }
        
        public static void linq_cautareAutorCarti(List<Carte> biblio)
        {
            Console.WriteLine("\nIntroduceti numele autorului:");
            string autorCarte = Console.ReadLine();         
            var autoriGasiti = biblio.Where(a =>
                a.Autor.Contains(autorCarte, StringComparison.OrdinalIgnoreCase));           
            foreach (Carte c in autoriGasiti)
            {
                Console.WriteLine($"Id:{c.Id} ,Titlu: \"{c.Titlu}\", Autor: {c.Autor}");
            }
            if (!autoriGasiti.Any())
            {
                Console.WriteLine("Nu a fost gasit nici un autor cu acest nume...");
            }
        }
    }
}
