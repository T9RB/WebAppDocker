namespace WebAPI.Model;

public class Person
{
    public long? id { get; set; }
    public string name { get; set; }
    public string displayName { get; set; } 
    public List<Skills> skills { get; set; }
}