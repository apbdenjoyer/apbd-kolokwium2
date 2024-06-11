namespace apbd_kolokwium2.Models;

public class Title
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();

}