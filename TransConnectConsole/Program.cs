using System.Globalization;
using System.Runtime.CompilerServices;
using Cities;
using Company;

namespace TransConnectConsole;

static class Program
{
    static void Main()
    {
        Company.Company transconnect = new Company.Company();

        string mainScreenStr = "Choisir une opération :\n" +
                               "1 - Afficher l'entreprise\n" +
                               "2 - Afficher un employé\n"+
                               "3 - Ajouter un employé\n" +
                               "4 - Retirer un employé\n" +
                               "5 - Modifier un employé\n" +
                               "6 - Afficher les clients\n" +
                               "7 - Ajouter un client\n" +
                               "8 - Retirer un client\n" +
                               "9 - Afficher les commandes\n" +
                               "10 - Ajouter une commande\n" +
                               "11 - Statistiques" +
                               "X ou CTRL + C - Quitter";

        mainScreenSelection:

        Console.WriteLine(mainScreenStr);

        string? numStr = Console.ReadLine();
        Console.Clear();
        Console.WriteLine();
        switch (numStr)
        {
            //Display the company tree
            case "1":
                transconnect.PrintEmployeeTree();
                PressToContinue();
                goto mainScreenSelection;

            //Display an employee
            case "2":
                Console.WriteLine("Entrer le nom de l'employé à afficher");
                var lastNameToDisplay = Console.ReadLine();
                Console.WriteLine("Entrer le prénom de l'employé à afficher");
                var firstNameToDisplay = Console.ReadLine();
                if (lastNameToDisplay == null || firstNameToDisplay == null)
                {
                    Console.WriteLine("Un des noms est null");
                    PressToContinue();
                    goto mainScreenSelection;
                }
                var employeeToDisplay = transconnect.SearchByName(firstNameToDisplay, lastNameToDisplay);
                if (employeeToDisplay == null)
                {
                    Console.WriteLine("Cet employé n'est pas dans l'entreprise");
                    PressToContinue();
                    goto mainScreenSelection;
                }
                Console.WriteLine(employeeToDisplay.AllFieldsString());
                PressToContinue();
                goto mainScreenSelection;

            //Add an employee
            case "3":
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
                var sex = (Sex) Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Entrer son salaire");
                var salary = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Entrer sa date de naissance");
                var birthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty, new CultureInfo("fr-FR"));
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
                var newEmployee = new Employee(ssnum, firstname, lastname, birthDate, phone, address, email,
                    entryDate,
                    position, salary, sex, new List<Employee>(0));
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
                        var supLastName = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Entrer le prénom de son supérieur");
                        var supFirstName= Console.ReadLine();

                        if (supLastName == null || supFirstName == null)
                        {
                            throw new Exception("One of the superior's names is null");
                        }

                        var superiorByName = transconnect.SearchByName(supFirstName, supLastName);
                        if (superiorByName != null) superiorByName._subordinates.Add(newEmployee);
                        else throw new Exception("Employee not found");
                        break;
                }

                transconnect.SaveToJson();
                Console.WriteLine("L'employé a été ajouté");
                Console.WriteLine("Appuyez sur une touche");
                Console.ReadLine();
                Console.Clear();
                goto mainScreenSelection;

            //Remove an employee
            case "4":
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
                transconnect.SaveToJson();
                Console.WriteLine("L'employé a été retiré");
                Console.WriteLine("Appuyez sur une touche");
                Console.ReadLine();
                Console.Clear();
                goto mainScreenSelection;

            //Modify an employee
            case "5":
                Console.WriteLine("Ecrivez le nom de famille de l'employé");
                var lastNameToModify = Console.ReadLine();
                Console.WriteLine("Ecrivez le prénom de l'employé");
                var firstNameToModify = Console.ReadLine();
                try
                {
                    var employeeToModify = transconnect.SearchByName(firstNameToModify!, lastNameToModify!);
                    Console.WriteLine("Entrez le champ que vous voulez modifier");
                    if (employeeToModify != null)
                    {
                        employeeToModify.DisplayModifiables();
                        var fieldToChange = Console.ReadLine();
                        switch (fieldToChange)
                        {
                            //Change first name
                            case "1":
                                Console.WriteLine("Ecrire le nouveau prénom");
                                var newModifiedFirstName = Console.ReadLine();
                                if (newModifiedFirstName != null)
                                    employeeToModify._firstName = newModifiedFirstName;
                                else Console.WriteLine("Prénom est null");
                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;

                            case "2":
                                Console.WriteLine("Ecrire le nouveau nom de famille");
                                var newModifiedLastName = Console.ReadLine();
                                if (newModifiedLastName != null) employeeToModify._lastName = newModifiedLastName;
                                else Console.WriteLine("Nom de famille est null");
                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;

                            case "3":
                                Console.WriteLine("Ecrire le nouveau téléphone");
                                var newModifiedPhone = Console.ReadLine();
                                if (newModifiedPhone != null) employeeToModify._phone = newModifiedPhone;
                                else Console.WriteLine("Téléphone est null");
                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;

                            case "4":
                                Console.WriteLine("Ecrire la nouvelle addresse");
                                var newModifiedAddress = Console.ReadLine();
                                if (newModifiedAddress != null) employeeToModify._address = newModifiedAddress;
                                else Console.WriteLine("La nouvelle addresse est null");
                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;

                            case "5":
                                Console.WriteLine("Ecrire la nouvelle addresse email");
                                var newModifiedMailAddress = Console.ReadLine();
                                if (newModifiedMailAddress != null)
                                    employeeToModify._email = newModifiedMailAddress;
                                else Console.WriteLine("La nouvelle addresse email est null");
                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;

                            case "6":
                                Console.WriteLine("Ecrire le nouveau poste");
                                var newModifiedPosition = Console.ReadLine();
                                if (newModifiedPosition != null) employeeToModify._position = newModifiedPosition;
                                else Console.WriteLine("Le nouveau poste est null");
                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;

                            case "7":
                                Console.WriteLine("Ecrire le nouveau salaire");
                                var newModifiedSalaryString = Console.ReadLine();
                                if (newModifiedSalaryString != null)
                                {
                                    var newModifiedSalary = Convert.ToInt32(newModifiedSalaryString);
                                    employeeToModify._salary = newModifiedSalary;
                                }
                                else Console.WriteLine("Le nouveau salaire est null");

                                PressToContinue();
                                transconnect.SaveToJson();
                                goto mainScreenSelection;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cet employé n'est pas dans l'entreprise");
                        Console.WriteLine("Appuyez sur une touche");
                        Console.ReadLine();
                        goto mainScreenSelection;
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Le prénom ou le nom de famille est null");
                }

                break;

            //Display Clients
            case "6":
                foreach (var client in transconnect.Clients)
                {
                    Console.WriteLine(client);
                    foreach (var order in client._orders)
                    {
                        Console.WriteLine("\t"+order);                        
                    }
                }
                break;

            //Add a client
            case "7":
                Console.WriteLine("Entrer son nom de famille");
                var clientLastname = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son prénom");
                var clientFirstname = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son numéro de sécurité sociale");
                var clientSsnum = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son sexe");
                Console.WriteLine("0 - Homme");
                Console.WriteLine("1 - Femme");
                var clientSex = (Sex) Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Entrer sa date de naissance");
                var clientBirthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty,new CultureInfo("fr-FR"));
                Console.Clear();
                Console.WriteLine("Entrer son numéro de téléphone");
                var clientPhone = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son addresse email");
                var clientEmail = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Entrer son addresse");
                var clientAddress = Console.ReadLine();
                Console.Clear();
                if (clientLastname == null ||
                    clientFirstname == null ||
                    clientSsnum == null ||
                    clientPhone == null ||
                    clientEmail == null ||
                    clientAddress == null) throw new Exception("Une des valeurs entrées est null");
                var newClient = new Client(clientSsnum, clientFirstname, clientLastname, clientBirthDate, clientPhone,
                    clientAddress, clientEmail, clientSex, new List<Order>(0));
                transconnect.AddClient(newClient);
                transconnect.SaveToJson();
                Console.WriteLine("Client ajouté");
                PressToContinue();
                goto mainScreenSelection;

            //Remove a client
            case "8":
                Console.WriteLine("Entrer le nom du client à retirer");
                var clientToRemoveLastName = Console.ReadLine();
                Console.WriteLine("Entrer le prénom du client à retirer");
                var clientToRemoveFirstName = Console.ReadLine();
                if (clientToRemoveLastName == null || clientToRemoveFirstName == null)
                {
                    Console.WriteLine("Un des noms est null");
                    PressToContinue();
                    goto mainScreenSelection;
                }

                transconnect.RemoveClient(clientToRemoveFirstName, clientToRemoveLastName);
                Console.WriteLine("Client retiré");
                PressToContinue();
                goto mainScreenSelection;

            //Display orders
            case "9":
                foreach (var order in transconnect.Orders)
                {
                    Console.WriteLine(order+"\n");
                }
                break;

            //Add order
            case "10":
                //getting the client who's ordering a delivery
                Console.WriteLine("Entrer le nom de famille du client qui commande");
                var clientOrderLastName = Console.ReadLine();
                Console.WriteLine("Entrer le prénom de famille du client qui commande");
                var clientOrderFirstName = Console.ReadLine();

                if (clientOrderLastName == null || clientOrderFirstName == null)
                {
                    Console.WriteLine("Un des noms entrés est null");
                    PressToContinue();
                    goto mainScreenSelection;
                }
                var clientWhoIsOrdering = transconnect.SearchClient(clientOrderFirstName, clientOrderLastName);
                if(clientWhoIsOrdering == null)
                {
                    Console.WriteLine("Ce client n'est pas inscrit dans l'entreprise");
                    PressToContinue();
                    goto mainScreenSelection;
                }
                
                //creating the order
                Console.WriteLine("Depuis quelle ville ?");
                var cityDepartureStr = Console.ReadLine();
                City? cityDeparture = transconnect.Map._cities.Find(
                    delegate(City city) { return city._name == cityDepartureStr; });
                if(cityDeparture == null)
                {
                    Console.WriteLine("Cette ville n'est pas dans la base de données");
                    PressToContinue();
                    goto mainScreenSelection;
                }

                Console.WriteLine("Vers quelle ville ?");
                var cityArrivalStr = Console.ReadLine();
                City? cityArrival = transconnect.Map._cities.Find(
                    delegate(City city) { return city._name == cityArrivalStr; });
                if(cityArrival == null)
                {
                    Console.WriteLine("Cette ville n'est pas dans la base de données");
                    PressToContinue();
                    goto mainScreenSelection;
                }

                var citiesPath = transconnect.Map.FindShortestPath(cityDeparture, cityArrival);
                
                var newOrder = new Order(DateTime.Now, DateTime.Now.Ticks.ToString(), clientWhoIsOrdering,
                    cityDeparture, cityArrival, citiesPath,0);
                
                clientWhoIsOrdering._orders.Add(newOrder);
                transconnect.Orders.Add(newOrder);

                break;
            
            case "11":
                Console.WriteLine("1 - Afficher les clients par ordre alphabétique");
                Console.WriteLine("2 - Afficher les clients par achats cumulés");
                var clientsMenuStr = Console.ReadLine();
                switch (clientsMenuStr)
                {
                    case "1":
                        transconnect.SortByClientName();
                        foreach (var client in transconnect.Clients)
                        {
                            Console.WriteLine(client);
                        }

                        break;
                    case "2":
                        transconnect.SortByClientOrders();
                        foreach (var client in transconnect.Clients)
                        {
                            Console.WriteLine(client);
                        }
                        break;
                }
                PressToContinue();
                goto mainScreenSelection;
            
            //Leave
            case "X":
                transconnect.SaveToJson();
                Console.WriteLine("Terminé");
                return;

            //Go to main screen
            default:
                goto mainScreenSelection;
        }
    }

    private static void PressToContinue()
    {
        Console.WriteLine("Appuyez sur une touche");
        Console.ReadLine();
        Console.Clear();
    }
}