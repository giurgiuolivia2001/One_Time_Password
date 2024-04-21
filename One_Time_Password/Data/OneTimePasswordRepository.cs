using System.Text.Json;

namespace One_Time_Password.Data;

public class OneTimePasswordRepository
{
    private const string fileName = "users.txt";

    public void Serialize<T>(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list);
        File.WriteAllText(fileName, jsonString);
    }

    public List<T> Deserialize<T>()
    {
        string jsonString = File.ReadAllText(fileName);
        try 
        {
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
        catch (Exception ex) 
        {
            return new List<T>();
        }
    }
}
