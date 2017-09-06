using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Services.CodeGenerator
{
    public interface ICodeGenerator
    {
        Task<string> GenerateAsync(int id);
    }
}
