namespace WebAPI.Model;

public class PersonForResponse
{
    public string name { get; set; }
    public string displayName { get; set; }
    public List<Skills> skills { get; set; }
}