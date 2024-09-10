using PT_EDII_POS.Domain.Items;

namespace PT_EDII_POS.API.Features.Items;

public static class Helper
{
    public static Item ToItem(this CreateItemCommand command)
    {
        return new Item()
        {
            NamaBarang = command.NamaBarang,
            Harga = command.Harga,
            StokAwal = command.StokAwal,
            Kategori = command.Kategori,
            UrlGambar = command.Gambar.FileName
        };
    }
    public static Item MapToItem(this UpdateItemCommand command)
    {
        return new Item()
        {
            NamaBarang = command.NamaBarang,
            Harga = command.Harga,
            StokAwal = command.StokAwal,
            Kategori = command.Kategori,
            UrlGambar = command.Gambar.FileName
        };
    }
}
