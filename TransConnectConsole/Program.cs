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
        Console.WriteLine();
        switch (numStr)
        {
            //Display the company tree
            case "1":
                Console.WriteLine(transconnect.HeadOfCompany);
                Company.Company.PrintEmployeeTree(transconnect.HeadOfCompany._subordinates, 0);
                PressToContinue();
                goto mainScreenSelection;

            //Add an employee
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
                var newEmployee = new Employee(ssnum, firstname, lastname, birthDate, phone, address, email,
                    entryDate,
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
                transconnect.SaveToJson();
                Console.WriteLine("L'employé a été retiré");
                Console.WriteLine("Appuyez sur une touche");
                Console.ReadLine();
                Console.Clear();
                goto mainScreenSelection;

            //Modify an employee
            case "4":
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
                        Console.WriteLine(
                            "1 - Prénom" + employeeToModify._firstName + "\n"
                            + "2 - Nom de famille :" + employeeToModify._lastName + "\n"
                            + "3 - Téléphone : " + employeeToModify._phone + "\n"
                            + "4 - Addresse :" + employeeToModify._address + "\n"
                            + "5 - Adresse email :" + employeeToModify._email + "\n"
                            + "6 - Poste :" + employeeToModify._position + "\n"
                            + "7 - Salaire :" + employeeToModify._salary);
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
            case "5":
                break;

            //Add a client
            case "6":
                break;

            //Remove a client
            case "7":
                break;

            //Display orders
            case "8":
                break;

            //Add order
            case "9":
                break;

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
}