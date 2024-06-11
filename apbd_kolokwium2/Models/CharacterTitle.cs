namespace apbd_kolokwium2.Models;

public class CharacterTitle
{
    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;
    public int TitleId { get; set; }
    public Title Title { get; set; } = null!;
    public DateTime AcquiredAt { get; set; }

}