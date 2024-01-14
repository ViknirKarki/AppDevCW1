using System;
using System.Text.Json;

namespace AppDevCW1.Data
{
    public class AddInsOperation
    {
        public static void SaveAllAddIns(List<AddIns> addIns)
        {
            string appDataDirectoryPath = Func.GetAppDirectoryPath();
            string appAddInsFilePath = Func.GetAppAddInsFilePath();
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(addIns);
            File.WriteAllText(appAddInsFilePath, json);
        }
        //to add name and price to the addin item
        public static List<AddIns> AddAddIn(string AddInName, int AddInPrice)
        {
            List<AddIns> addIns = GetAllAddIns();
            addIns.Add(
            new AddIns
            {
                ID = addIns.Count() + 1,
                AddInName = AddInName,
                AddInPrice = AddInPrice,
            }
        );
            SaveAllAddIns(addIns);
            return addIns;
        }

        // to Get all Coffees 
        public static List<AddIns> GetAllAddIns()
        {
            string addInsFilePath = Func.GetAppAddInsFilePath();
            if (!File.Exists(addInsFilePath))
            {
                return new List<AddIns>();
            }

            var json = File.ReadAllText(addInsFilePath);

            return JsonSerializer.Deserialize<List<AddIns>>(json);
        }
        //to update the addins made
        public static List<AddIns> UpdateAddIns(int ID, string newAddInName, int newAddInPrice)
        {
            List<AddIns> addIns = GetAllAddIns();
            AddIns addInToUpdate = addIns.Find(x => x.ID == ID);
            addIns.Remove(addInToUpdate);
            addIns.Add(
            new AddIns
            {
                ID = ID,
                AddInName = newAddInName,
                AddInPrice = newAddInPrice,
            });
            SaveAllAddIns(addIns);
            return addIns;
        }
        //to delete the addin made
        public static List<AddIns> DeleteAddIn(int ID)
        {
            List<AddIns> addIns = GetAllAddIns();
            AddIns addInToDelete = addIns.Find(x => x.ID == ID);

            if (addInToDelete == null)
                throw new Exception("AddIn is not found");

            addIns.Remove(addInToDelete);
            SaveAllAddIns(addIns);
            return addIns;
        }
    }
}
