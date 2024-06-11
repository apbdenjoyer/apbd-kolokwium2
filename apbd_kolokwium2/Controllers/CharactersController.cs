using apbd_kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_kolokwium2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        this._dbService = dbService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterWithInfo(int characterId)
    {
        var result = await _dbService.GetCharacterInfo(characterId);

        var character = new CharacterWithInfoDto()
        {
            FirstName = result.FirstName,
            LastName = result.LastName,
            CurrentWeight = result.CurrentWeight,
            MaxWeight = result.MaxWeight
        };
        
        return Ok(character);
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddNewItemsToCharacter([FromQuery] int characterId, int[] items)
    {

        var addedItems = new List<NewItemDataDto>();
        foreach (var itemId in items)
        {
            if (!await _dbService.DoesItemExist(itemId))
            {
                return NotFound($"Item of ID {itemId} not found.");
            }

            if (!await _dbService.DoesCharacterHaveEnoughSpaceToCarry(characterId, items))
            {
                return BadRequest($"Items can't be added to inventory - too much weight.");
            }

            var addedItem = _dbService.AddItemToBackpack(itemId, characterId);
            addedItems.Add(await addedItem);
        }

        return Ok(addedItems);
    }
}


public class NewItemDataDto
{
    public int Amount { get; set; }
    public int Weight { get; set; }
    public int CharacterId { get; set; }
}

public class CharacterWithInfoDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public IEnumerable<ItemDto> BackpackItems { get; set; } = new List<ItemDto>();

}

public class ItemDto
{
    public string ItemName { get; set; } = string.Empty;
    public int ItemWEight { get; set; }
    public int AMount { get; set; }
}
