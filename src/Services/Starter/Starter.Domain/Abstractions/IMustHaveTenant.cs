﻿namespace Starter.Domain.Abstractions
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
