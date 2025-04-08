﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;


namespace Clientes.Dominio.Entidade
{
    public class EntidadeBase
    {
        [NotMapped] 
        public FluentValidation.Results.ValidationResult ValidationResult { get; private set; } = new FluentValidation.Results.ValidationResult();

        public bool Validar<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return ValidationResult.IsValid;
        }

        public IEnumerable<string> ObterErros()
        {
            return ValidationResult?.Errors.Select(e => e.ErrorMessage) ?? [];
        }
    }
}
