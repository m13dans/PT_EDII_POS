namespace PT_EDII_POS.API.Features.Items;

public record CreateItemCommand(
    string NamaBarang,
    decimal Harga,
    int StokAwal,
    string Kategori,
    IFormFile Gambar);

public record UpdateItemCommand(
    string NamaBarang,
    decimal Harga,
    int StokAwal,
    string Kategori,
    IFormFile Gambar);
