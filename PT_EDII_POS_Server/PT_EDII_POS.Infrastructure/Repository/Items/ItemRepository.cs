using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PT_EDII_POS.Application.Items;
using PT_EDII_POS.Domain.Items;
using PT_EDII_POS.Infrastructure.DataContext;

namespace PT_EDII_POS.Infrastructure.Repository.Items;

public class ItemRepository(AppDbContext dbContext) : IItemRepository
{

    public async Task<List<Item>> GetItems()
    {
        var result = await dbContext.Items.ToListAsync();
        return result;
    }
    public async Task<ErrorOr<Item>> CreateItem(Item item)
    {
        var result = await dbContext.Items.AddAsync(item);

        await dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<ErrorOr<Item>> UpdateItem(int id, Item itemUpdated)
    {
        var item = await dbContext.Items.SingleOrDefaultAsync(x => x.Id == id);

        if (item is null)
            return Error.NotFound("Item.NotFound");

        Item updatedItem = MapToNewItem(item, itemUpdated);

        var result = dbContext.Items.Update(updatedItem);

        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    private Item MapToNewItem(Item item, Item itemUpdated)
    {
        item.NamaBarang = itemUpdated.NamaBarang;
        item.Kategori = itemUpdated.Kategori;
        item.StokAwal = itemUpdated.StokAwal;
        item.UrlGambar = itemUpdated.UrlGambar;
        return item;
    }


    public async Task<ErrorOr<Item>> DeleteItem(int id)
    {
        var item = await dbContext.Items.SingleOrDefaultAsync(x => x.Id == id);

        if (item is null)
            return Error.NotFound("Item.NotFound");

        var result = dbContext.Items.Remove(item);

        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
}
