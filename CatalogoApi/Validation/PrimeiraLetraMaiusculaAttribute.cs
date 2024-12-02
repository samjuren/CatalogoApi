using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CatalogoApi.Validation;

public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }
        
        var primeiraLetraMaiuscula = value.ToString()[0].ToString();

        if (primeiraLetraMaiuscula != primeiraLetraMaiuscula.ToUpper())
        {
            return new ValidationResult("A primiera letra do nome do produto deve ser maiúscula");
        }
        
        return ValidationResult.Success;
    }
}