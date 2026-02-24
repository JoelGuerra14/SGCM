using SGCM.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Shared.Domain.Entities
{
    public sealed class Insurer
    {
        public InsurerId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? ContactPhone { get; private set; }
        public string? ContactEmail { get; private set; }
        public string? Address { get; private set; }
        public string? Website { get; private set; }
        public bool IsActive { get; private set; }

        private Insurer() { }

        public static Insurer Create(string name, string? contactPhone, string? contactEmail, string? address, string? website)
        {
            return new Insurer
            {
                Id = InsurerId.New(),
                Name = name,
                ContactPhone = contactPhone,
                ContactEmail = contactEmail,
                Address = address,
                Website = website,
                IsActive = true
            };
        }
    }
}
