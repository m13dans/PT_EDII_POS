using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PT_EDII_POS.API.Features.Items;

namespace PT_EDII_POS.API.Features.Items;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.NamaBarang).MinimumLength(3).MaximumLength(256);
        RuleFor(x => x.StokAwal).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Kategori).MinimumLength(3).MaximumLength(256);

        // RuleFor(x => x.UrlGambar).MinimumLength(3).MaximumLength(256);
    }
}
public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.NamaBarang).MinimumLength(3).MaximumLength(256);
        RuleFor(x => x.StokAwal).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Kategori).MinimumLength(3).MaximumLength(256);
        // RuleFor(x => x.UrlGambar).MinimumLength(3).MaximumLength(256);
    }
}
