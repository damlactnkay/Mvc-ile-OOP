using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün ismi boş geçilemez");
            RuleFor(x => x.ProductName).MaximumLength(3).WithMessage("Ürün adı 3 harften fazla olmalı");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("stok boş olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş olamaz");

        }
    }

}

