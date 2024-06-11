namespace apbd_kolokwium2.Models;

public class Backpack
{
    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public int Amount { get; set; }
}