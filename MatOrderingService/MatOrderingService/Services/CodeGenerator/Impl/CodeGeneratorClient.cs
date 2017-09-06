using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatOrderingService.Services.CodeGenerator.Impl
{
    public class CodeGeneratorClient: ICodeGenerator
    {
        public async Task<string> GenerateAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6470/");

                var result = await client.GetAsync($"api/code/{id}");
                
                string res = await result.Content.ReadAsStringAsync();
                return res;
            }
        }
    }
}
