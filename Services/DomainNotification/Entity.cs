using System;
using System.ComponentModel.DataAnnotations;

namespace urlShortener.Services.DomainNotification
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public bool Valid { get; private set; }
        public bool Invalid => !Valid;
        public ValidationResult ValidationResult { get; private set; }

        // public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        // {
        //     ValidationResult = validator.Validate(model);
        //     return Valid = ValidationResult.IsValid;
        // }
    }
}