﻿using Company;

namespace TransConnectConsole;

static class Program
{
    static void Main()
    {
        Company.Company transconnect = new Company.Company();
        var employee = transconnect.SearchByName("Alice","Joyeuse");

        return;

        /*
        string mainScreenStr = "Choisir une opération :\n" +
                               "1 - Afficher l'entreprise\n" +
                               "2 - Ajouter un employé\n" +
                               "3 - Retirer un employé\n" +
                               "4 - Afficher les clients" +
                               "5 - Ajouter un client\n" +
                               "6 - Retirer un client\n" +
                               "7 - Afficher les commandes\n" +
                               "8 - Ajouter une commande\n" +
                               "X ou CTRL + C - Quitter";

        mainScreenSelection:
        
        Console.WriteLine(mainScreenStr);

        string? numStr = Console.ReadLine();
        Console.Clear();
        
        switch (numStr)
        {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                break;
            case "6":
                break;
            case "7":
                break;
            case "8":
                break;
            case "X":
                return;
            default:
                goto mainScreenSelection;
        }
        */
    }

    class Strings
    {
        private string name;

        public string Name => name;

        public Strings(string name)
        {
            this.name = name;
        }
    }
}