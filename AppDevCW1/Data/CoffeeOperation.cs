using System;
using System.Data;
using System.Text.Json;
using AppDevCW1.Data;
using AppDevCW1.Pages;

namespace AppDevCW1.Data
{
    public class CoffeesOperation
    {
        public static void SaveAllCoffees(List<Coffees> Coffees)
        {
            string appDataDirectoryPath = Func.GetAppDirectoryPath();
            string appCoffeesFilePath = Func.GetAppCoffeesFilePath();
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(Coffees);
            File.WriteAllText(appCoffeesFilePath, json);
        }

        public static List<Coffees> AddCoffees(string CoffeesName, int CoffeesPrice)
        {
            List<Coffees> Coffees = GetAllCoffees();
            Coffees.Add(
            new Coffees
            {
                ID = Coffees.Count() + 1,
                CoffeesName = CoffeesName,
                CoffeesPrice = CoffeesPrice,
            }
        );
            SaveAllCoffees(Coffees);
            return Coffees;
        }

        // Get all Coffees if the file exists else return empty list
        public static List<Coffees> GetAllCoffees()
        {
            string CoffeesFilePath = Func.GetAppCoffeesFilePath();
            if (!File.Exists(CoffeesFilePath))
            {
                return new List<Coffees>();
            }

            var json = File.ReadAllText(CoffeesFilePath);

            return JsonSerializer.Deserialize<List<Coffees>>(json);
        }

        public static List<Coffees> UpdateCoffees(int ID, string newCoffeesName, int newCoffePrice)
        {
            List<Coffees> Coffees = GetAllCoffees();
            Coffees CoffeesToUpdate = Coffees.Find(x => x.ID == ID);
            Coffees.Remove(CoffeesToUpdate);
            Coffees.Add(
            new Coffees
            {
                ID = ID,
                CoffeesName = newCoffeesName,
                CoffeesPrice = newCoffePrice,
            });
            SaveAllCoffees(Coffees);
            return Coffees;
        }

        public static List<Coffees> DeleteCoffees(int ID)
        {
            List<Coffees> Coffees = GetAllCoffees();
            Coffees CoffeesToDelete = Coffees.Find(x => x.ID == ID);

            if (CoffeesToDelete == null)
                throw new Exception("Coffees is not found");

            Coffees.Remove(CoffeesToDelete);
            SaveAllCoffees(Coffees);
            return Coffees;
        }
    }
}
