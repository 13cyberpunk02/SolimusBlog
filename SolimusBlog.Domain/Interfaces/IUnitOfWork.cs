﻿namespace SolimusBlog.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}