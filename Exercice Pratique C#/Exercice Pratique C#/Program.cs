using System;
using System.Collections.Generic;


public interface IEmpruntable
{
    string Description { get; set; }
    void Emprunter();
    void Retourner();
}


public class Livre
{
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public int AnneeDePublication { get; set; }
    public string Genre { get; set; }
    public string Emprunte { get; set; }

    public Livre(string titre, string auteur, int anneeDePublication, string genre, string emprunte)
    {
        Titre = titre;
        Auteur = auteur;
        AnneeDePublication = anneeDePublication;
        Genre = genre;
        Emprunte = emprunte;
    }
}


public class LivreEmpruntable : Livre, IEmpruntable
{
    public string Description { get; set; }

    public LivreEmpruntable(string titre, string auteur, int anneeDePublication, string genre, string description)
        : base(titre, auteur, anneeDePublication, genre, "Disponible")
    {
        Description = description;
    }

    public void Emprunter()
    {
        if (Emprunte == "Disponible")
        {
            Emprunte = "Emprunté";
            Description = "Emprunté";
        }
        else
        {
            Console.WriteLine($"Le livre '{Titre}' n'est pas disponible pour l'emprunt.");
        }
    }

    public void Retourner()
    {
        Emprunte = "Disponible";
        Description = "Disponible";
    }
}



public class AllGenre
{
    public List<string> Genres { get; set; }

    public AllGenre()
    {
        Genres = new List<string>();
    }

    public void AddGenre(string genre)
    {
        Genres.Add(genre);
    }
}

public class Membre
{
    public string Nom { get; set; }
    public string Adresse { get; set; }
    public int NumeroDeMembre { get; set; }

    public Membre(string nom, string adresse, int numerodemembre)
    {
        Nom = nom;
        Adresse = adresse;
        NumeroDeMembre = numerodemembre;
    }
}

public class Bibliotheque
{
    public List<Livre> Livres { get; set; }
    public List<Membre> Membres { get; set; }

    public Bibliotheque()
    {
        Livres = new List<Livre>();
        Membres = new List<Membre>();
    }

    public void AjouterLivre(Livre livre)
    {
        Livres.Add(livre);
    }

    public void SupprimerLivre(Livre livre)
    {
        Livres.Remove(livre);
    }

    public void AjouterMembre(Membre membre)
    {
        Membres.Add(membre);
    }

    public void SupprimerMembre(Membre membre)
    {
        Membres.Remove(membre);
    }

    public void ChangerStatutEmprunt(string titreLivre, string nouveauStatut)
    {
        Livre livre = Livres.Find(l => l.Titre == titreLivre);

        if (livre != null)
        {
            livre.Emprunte = nouveauStatut;
            Console.WriteLine($"Le statut d'emprunt du livre '{livre.Titre}' a été changé en '{nouveauStatut}'.");
        }
        else
        {
            Console.WriteLine("Livre non trouvé.");
        }
    }

    public void GestionDemprunt()
    {
        Console.Write("Voulez-vous modifier un livre en emprunté ou disponible ? (oui ou non) ");
        string modifEmprunte = Console.ReadLine();

        if (modifEmprunte.ToLower() == "oui")
        {
            Console.Write("Entrez le titre du livre à modifier : ");
            string titreLivre = Console.ReadLine();

            Livre livre = Livres.Find(l => l.Titre == titreLivre);

            if (livre != null)
            {
                Console.Write("Nouveau statut d'emprunt (Disponible ou Emprunte) : ");
                string nouveauStatut = Console.ReadLine();

                ChangerStatutEmprunt(titreLivre, nouveauStatut);
            }
            else
            {
                Console.WriteLine("Livre non trouvé.");
            }
        }
        else if (modifEmprunte.ToLower() == "non")
        {
            Console.WriteLine("OK");
        }
        else
        {
            Console.WriteLine("Choix non valide.");
        }
    }




}

class Program
{
    static void Main()
    {
        Bibliotheque bibliotheque = new Bibliotheque();

        Livre livre1 = new Livre("Titre1", "Auteur1", 2023, "Fiction", "Emprunte");
        Livre livre2 = new Livre("Titre2", "Auteur2", 2043, "Action", "Disponible");
        Membre membre1 = new Membre("Theo", "18 rue schnapper", 1);
        Membre membre2 = new Membre("Camille", "Aix en Provence", 2);

        bibliotheque.AjouterLivre(livre1);
        bibliotheque.AjouterLivre(livre2);
        bibliotheque.AjouterMembre(membre1);
        bibliotheque.AjouterMembre(membre2);

        foreach (var livre in bibliotheque.Livres)
        {
            Console.WriteLine($"Livre Titre = {livre.Titre}, Auteur = {livre.Auteur}, Année de publication = {livre.AnneeDePublication}, Genre = {livre.Genre}, Statut = {livre.Emprunte}");

        }

        foreach (var membre in bibliotheque.Membres)
        {
            Console.WriteLine($"Membre Nom = {membre.Nom}, Adresse = {membre.Adresse}, Numero de membre = {membre.NumeroDeMembre}");
        }

        Console.Write("Afficher les genres ? ");
        string listeGenre = Console.ReadLine();

        AllGenre allGenres = new AllGenre();
        allGenres.AddGenre("Fiction");
        allGenres.AddGenre("Action");
        allGenres.AddGenre("Romance");
        allGenres.AddGenre("Science");

        if (listeGenre.ToLower() == "oui")
        {
            DisplayGenres(allGenres);

            Console.Write("Voulez-vous ajouter un nouveau genre ? (oui ou non) ");
            string ajouterGenre = Console.ReadLine();
            if (ajouterGenre.ToLower() == "oui")
            {
                Console.Write("Entrez le nouveau genre : ");
                string nouveauGenre = Console.ReadLine();
                allGenres.AddGenre(nouveauGenre);
                Console.WriteLine($"Le genre '{nouveauGenre}' a été ajouté.");

                DisplayGenres(allGenres);
            }
        }
        else if (listeGenre.ToLower() == "non")
        {
            Console.Write("Voulez-vous supprimer un livre ? (oui ou non) ");
            string supprimerLivre = Console.ReadLine();
            if (supprimerLivre.ToLower() == "oui")
            {
                Console.Write("Quel livre souhaitez-vous supprimer ? ");
                string titreLivreASupprimer = Console.ReadLine();
                Livre livreASupprimer = bibliotheque.Livres.Find(l => l.Titre == titreLivreASupprimer);
                if (livreASupprimer != null)
                {
                    bibliotheque.SupprimerLivre(livreASupprimer);
                    Console.WriteLine("Livre supprimé.");
                    DisplayLivres(bibliotheque);
                }
                else
                {
                    Console.WriteLine("Livre non trouvé.");
                }
            }
            else if (supprimerLivre.ToLower() == "non")
            {
                Console.Write("Voulez-vous supprimer un membre ? (oui ou non) ");
                string supprimerMembre = Console.ReadLine();
                if (supprimerMembre.ToLower() == "oui")
                {
                    Console.Write("Quel membre souhaitez-vous supprimer ? ");
                    string nomMembreASupprimer = Console.ReadLine();
                    Membre membreASupprimer = bibliotheque.Membres.Find(m => m.Nom == nomMembreASupprimer);
                    if (membreASupprimer != null)
                    {
                        bibliotheque.SupprimerMembre(membreASupprimer);
                        Console.WriteLine("Membre supprimé.");
                        DisplayMembres(bibliotheque);
                    }
                    else
                    {
                        Console.WriteLine("Membre non trouvé.");
                    }
                }
                else if (supprimerMembre.ToLower() == "non")
                {
                    Console.Write("Voulez-vous ajouter un nouveau livre ? (oui ou non) ");
                    string ajouterNouveauLivre = Console.ReadLine();
                    if (ajouterNouveauLivre.ToLower() == "oui")
                    {
                        Console.Write("Entrez le titre du nouveau livre : ");
                        string nouveauLivreTitre = Console.ReadLine();
                        Console.Write("Entrez l'auteur du nouveau livre : ");
                        string nouveauLivreAuteur = Console.ReadLine();
                        Console.Write("Entrez l'année de publication du nouveau livre : ");
                        int nouveauLivreAnnee = int.Parse(Console.ReadLine());
                        Console.Write("Entrez le genre du nouveau livre : ");
                        string nouveauLivreGenre = Console.ReadLine();
                        Console.Write("Entrez diponible ou emprunté  : ");
                        string nouveauLivreEmprunte = Console.ReadLine();
                        Livre nouveauLivre = new Livre(nouveauLivreTitre, nouveauLivreAuteur, nouveauLivreAnnee, nouveauLivreGenre, nouveauLivreEmprunte);
                        bibliotheque.AjouterLivre(nouveauLivre);
                        Console.WriteLine($"Le livre '{nouveauLivreTitre}' a été ajouté");
                        DisplayLivres(bibliotheque);
                    }
                    else if (ajouterNouveauLivre.ToLower() == "non")
                    {
                        Console.Write("Voulez-vous ajouter un nouveau membre ? (oui ou non) ");
                        string ajouterNouveauMembre = Console.ReadLine();
                        if (ajouterNouveauMembre.ToLower() == "oui")
                        {
                            Console.Write("Entrez le nom du nouveau membre : ");
                            string nouveauMembreNom = Console.ReadLine();
                            Console.Write("Entrez l'adresse du nouveau membre : ");
                            string nouveauMembreAdresse = Console.ReadLine();
                            Console.Write("Entrez le numéro de membre du nouveau membre : ");
                            int nouveauMembreNumero = int.Parse(Console.ReadLine());

                            Membre nouveauMembre = new Membre(nouveauMembreNom, nouveauMembreAdresse, nouveauMembreNumero);
                            bibliotheque.AjouterMembre(nouveauMembre);
                            Console.WriteLine($"Le membre '{nouveauMembreNom}' a été ajouté");
                            DisplayMembres(bibliotheque);
                        }
                        else if (ajouterNouveauMembre.ToLower() == "non")
                        {
                            Console.WriteLine("OK");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("OK");
            }


            Console.WriteLine("Acceder a la modification de Emprunt ou disponible ?");
            string modifEmprunte = Console.ReadLine();
            if (modifEmprunte.ToLower() == "oui")
            {
                bibliotheque.GestionDemprunt();
            }
            else
            {
                Console.WriteLine("OK");
            }

            foreach (var livre in bibliotheque.Livres)
            {
                Console.WriteLine($"Livre Titre = {livre.Titre}, Auteur = {livre.Auteur}, Année de publication = {livre.AnneeDePublication}, Genre = {livre.Genre}, Statut = {livre.Emprunte}");

            }

        }


        static void DisplayGenres(AllGenre allGenres)
        {
            Console.WriteLine("Genres disponibles : " + string.Join(", ", allGenres.Genres));
        }

        static void DisplayLivres(Bibliotheque bibliotheque)
        {
            Console.WriteLine("\nLivres dans la bibliothèque :");
            foreach (var livre in bibliotheque.Livres)
            {
                Console.WriteLine($"Livre Titre = {livre.Titre}, Auteur = {livre.Auteur}, Année de publication = {livre.AnneeDePublication}, Genre = {livre.Genre}, Statut = {livre.Emprunte}");
            }
        }


        static void DisplayMembres(Bibliotheque bibliotheque)
        {
            Console.WriteLine("\nMembres dans la bibliothèque :");
            foreach (var membre in bibliotheque.Membres)
            {
                Console.WriteLine($"Membre Nom = {membre.Nom}, Adresse = {membre.Adresse}, Numero de membre = {membre.NumeroDeMembre}");
            }
        }
    }
}
