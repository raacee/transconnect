using System.Threading.Channels;
using Company;

namespace TransConnectConsole;

static class Program
{
    static void Main()
    {
        Company.Company transconnect = new Company.Company();
        
        string mainScreenStr = "Choisir une opération :\n" +
                               "1 - Afficher l'entreprise\n" +
                               "2 - Ajouter un employé\n" +
                               "3 - Retirer un employé\n" +
                               "4 - Modifier un employé" +
                               "5 - Afficher les clients" +
                               "6 - Ajouter un client\n" +
                               "7 - Retirer un client\n" +
                               "8 - Afficher les commandes\n" +
                               "9 - Ajouter une commande\n" +
                               "X ou CTRL + C - Quitter";

        mainScreenSelection:
        
        Console.WriteLine(mainScreenStr);

        string? numStr = Console.ReadLine();
        Console.Clear();
        
        switch (numStr)
        {
            case "1":
                //Display Entreprise
                break;
            case "2":
                Console.WriteLine("Entrer son nom de famille");
                var lastname = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son prénom");
                var firstname = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son numéro de sécurité sociale");
                var ssnum = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer sa position");
                var position = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son sexe");
                Console.WriteLine("0 - Homme");
                Console.WriteLine("1 - Femme");
                var sexStr = (Sex) Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Entrer son nom de salaire");
                var salary = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Entrer sa date de naissance");
                var birthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                Console.Clear();
                Console.WriteLine("Entrer son numéro de téléphone");
                var phone = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son addresse email");
                var email = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son addresse");
                var address = Console.ReadLine();
                Console.Clear();
                var entryDate = DateTime.Now;
                if (lastname == null ||
                    firstname == null ||
                    ssnum == null ||
                    position == null ||
                    phone == null ||
                    email == null ||
                    address == null) throw new Exception("Une des valeurs entrées est null");
                var newEmployee = new Employee(ssnum, firstname, lastname, birthDate, phone, address, email, entryDate,
                    position, salary, sexStr, new List<Employee>(0));
                Console.WriteLine("Qui est son supérieur ?");
                Console.WriteLine("1 - Chercher par numéro de sécurité sociale");
                Console.WriteLine("2 - Chercher par nom");
                var searchMode = Console.ReadLine();
                switch (searchMode)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Entrer son numéro de sécurité sociale");
                        var supSsnum = Console.ReadLine();
                        if (supSsnum != null)
                        {
                            var superiorBySSnum = transconnect.SearchBySSnum(supSsnum);
                            if (superiorBySSnum != null) superiorBySSnum._subordinates.Add(newEmployee);
                            else throw new Exception("Employee not found");
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Entrer le nom de son supérieur");
                        var supFirstName = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Entrer le prénom de son supérieur");
                        var supLastName = Console.ReadLine();
                        
                        if (supLastName == null || supFirstName == null)
                        {
                            throw new Exception("One of the superior's names is null");
                        }
                        
                        var superiorByName = transconnect.SearchByName(supFirstName,supLastName);
                        if (superiorByName != null) superiorByName._subordinates.Add(newEmployee);
                        else throw new Exception("Employee not found");
                        break;
                }

                Console.WriteLine("L'employé a été ajouté");
                Console.WriteLine("Appuyez sur une touche");
                Console.ReadLine();
                Console.Clear();
                goto mainScreenSelection; 
                
            case "3":
                //remove an employee
                Console.WriteLine("Ecrire le prénom de l'employé à retirer");
                var firstnameToRemove = Console.ReadLine();
                Console.WriteLine("Ecrire le nom de famille de l'employé à retirer");
                var lastnameToRemove = Console.ReadLine();
                if (lastnameToRemove == null || firstnameToRemove == null)
                {
                    Console.WriteLine("Veuillez entrer un nom valide");
                    goto case "3";
                }
                var employeeToRemove = transconnect.SearchByName(firstnameToRemove, lastnameToRemove);
                if (employeeToRemove == null)
                {
                    Console.WriteLine("Cet employé n'est pas dans l'entreprise");
                    Console.WriteLine("Appuyer sur une touche");
                    Console.ReadLine();
                    Console.Clear();
                    goto mainScreenSelection;
                }
                transconnect.RemoveEmployee(employeeToRemove);
                Console.WriteLine("L'employé a été retiré");
                Console.WriteLine("Appuyez sur une touche");
                Console.ReadLine();
                Console.Clear();
                goto mainScreenSelection;
                
            case "4":
                //Modify an employee
                break;
            case "5":
                //Display Clients
                break;
            case "6":
                //Add a client
                break;
            case "7":
                //Remove a client
                break;
            case "8":
                //Display orders
                break;
            case "9":
                //Add order
                break;
            case "X":
                //Leave
                Console.WriteLine("Terminé");
                return;
            default:
                goto mainScreenSelection;
        }
        
    }
}