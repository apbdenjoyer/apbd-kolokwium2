using apbd_kolokwium2.Controllers;
using apbd_kolokwium2.Data;
using apbd_kolokwium2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace apbd_kolokwium2.Services;

public class DbService : IDbService
{
    private DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Character> GetCharacterInfo(int characterId)
    {
        return await _context.Characters
            .Include(e => e.CharacterTitles)
            .ThenInclude(e => e.Title)
            .Include(e => e.Backpacks)
            .FirstOrDefaultAsync(e => e.Id == characterId);
    }

    public async Task<bool> DoesItemExist(int id)
    {
        var res = await _context.Items.AnyAsync(e => e.Id == id);
        return res;
    }

    public async Task<bool> DoesCharacterHaveEnoughSpaceToCarry(int charId, int[] items)
    {
        var weights = await _context.Items.Where(e => items.Contains(e.Id)).Select(e => e.Weight).FirstOrDefaultAsync();
        var max = await _context.Characters.Where(e => e.Id == charId).Select(e => e.MaxWeight).FirstOrDefaultAsync();
        var current = await _context.Characters.Where(e => e.Id == charId).Select(e => e.CurrentWeight)
            .FirstOrDefaultAsync();

        return current + weights <= max;
    }

    public async Task<NewItemDataDto> AddItemToBackpack(int itemId, int charId)
    {
        var added = _context.Backpacks.AddAsync(new Backpack()
        {
            Amount = 1, CharacterId = charId, ItemId = itemId
        });

        var item = new NewItemDataDto
        {
            Amount = added.Result.Entity.Amount,
            CharacterId = charId,
            Weight = await _context.Items.Where(e => e.Id == itemId).Select(e => e.Weight).FirstOrDefaultAsync()
        };

        return item;
    }
}