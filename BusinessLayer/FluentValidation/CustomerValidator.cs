using BusinessLayer.FluentValidation;
using FluentValidation;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer> 
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("İsim boş olamaz");
            RuleFor(x => x.CustomerName).MinimumLength(3).WithMessage("İsim 3 karakterden az olamaz");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş olamaz");
        }
    }

}

