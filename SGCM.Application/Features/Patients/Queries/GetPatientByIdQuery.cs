using SGCM.Application.Contracts;
using SGCM.Application.Features.Patients.Dtos;
using SGCM.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Queries
{
    public sealed record GetPatientByIdQuery(Guid Id) : IQuery<Result<PatientResponseDto>>;
}
