using apbd_kolokwium2.Controllers;
using apbd_kolokwium2.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace apbd_kolokwium2.Services;

public interface IDbService
{
    public Task<Character> GetCharacterInfo(int id);

    public Task<bool> DoesItemExist(int id);

    public Task<bool> DoesCharacterHaveEnoughSpaceToCarry(int charId, int[] items);

    public Task<NewItemDataDto> AddItemToBackpack(int itemId, int charId);
}