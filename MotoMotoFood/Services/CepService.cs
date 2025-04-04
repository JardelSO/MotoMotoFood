using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MotoMotoFood.entitdades;
using MotoMotoFood.Util;
using MotoMotoFood.Util.Exceptions;

namespace MotoMotoFood.Services
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://viacep.com.br/ws/")
            };
        }

        private async Task<Endereco> ConsultarCep(string cep)
        {
            cep = new string(cep.Where(char.IsDigit).ToArray());
            HttpResponseMessage response = await _httpClient.GetAsync($"{cep}/json/");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
            string content = await response.Content.ReadAsStringAsync();
            Endereco? endereco = JsonSerializer.Deserialize<Endereco>(content);
            if (endereco == null || endereco.Erro)
                throw new CepInvalidoException("CEP não encontrado");
            return endereco;

        }

        public async Task<Endereco> ObterEndereco()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Informe o CEP: ");
                string cep = Console.ReadLine();

                if (!Helpers.ValidarCEP(cep))
                {
                    Console.WriteLine("CEP inválido!");
                    continue;
                }

                try
                {
                    return await ConsultarCep(cep);
                }
                catch (CepInvalidoException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao consultar CEP: {ex.Message}");
                }
            }
        }
    }
}

