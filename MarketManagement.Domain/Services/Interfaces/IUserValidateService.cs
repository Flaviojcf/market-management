﻿using MarketManagement.Domain.Validations;

namespace MarketManagement.Domain.Services.Interfaces
{
    public interface IUserValidateService
    {
        Task<ValidationResult> ValidateCreateUserAsync(string email);
        Task<ValidationResult> ValidateUpdateUserAsync(Guid id);
    }
}
