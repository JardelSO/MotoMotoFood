using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MotoMotoFood.Models;
using MotoMotoFood.entitdades;
using MotoMotoFood.Services;
using MotoMotoFood.Util.Exceptions;

namespace MotoMotoFood.Util
{
    public class Helpers
    {
        public static void LerOpcaoSair()
        {
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        public static bool ValidarCEP(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            cep = Regex.Replace(cep, @"\D", "");

            if (cep.Length != 8)
                return false;

            return true;
        }

        public class StringToBoolConverter : JsonConverter<bool>
        {
            public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString() == "true";
                }
                return reader.GetBoolean();
            }

            public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
            {
                writer.WriteBooleanValue(value);
            }
        }

        public static void ExibirPedidosRestaurente(List<Pedido> pedidos)
        {
            decimal totalCompleto = 0;
            int numeroPedido = 1;

            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"===== Pedido #{numeroPedido++} =====");

                Console.WriteLine(string.Join("\n",
                    pedido.Itens.Select((item, index) =>
                        $"{index + 1}. {item.Produto.Nome} - {item.Produto.Preco.ToString("C")} (x{item.Quantidade})"
                    )
                ));

                Console.WriteLine($"Total do pedido: {pedido.ValorTotal.ToString("C")}");
                Console.WriteLine();

                totalCompleto += pedido.ValorTotal;
            }

            Console.WriteLine("==================================");
            Console.WriteLine($"Numero total de pedidos: {pedidos.Count}");
            Console.WriteLine($"Total acumulado de todos os pedidos: {totalCompleto.ToString("C")}");
        }

        public static void ExibirPedido(Pedido pedido)
        {
            for (int i = 0; i < pedido.Itens.Count(); i++)
            {
                Console.WriteLine($"{i + 1} - {pedido.Itens[i].Produto.Nome} - R${pedido.Itens[i].Produto.Preco} {pedido.Itens[i].Quantidade} (Unidade)");
            }
        }

        public static void ExibirProduto(List<Produto> produto)
        {
            if (!produto.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhum produto cadastrado.");
                Helpers.LerOpcaoSair();
                return;
            }
            for (int i = 0; i < produto.Count(); i++)
            {
                Console.WriteLine($"{i + 1} - {produto[i].Nome} - R${produto[i].Preco} - ({produto[i].QuantidadeDisponivel} Quantidade) - Descrição: {produto[i].Descricao}");
            }
        }

        private static readonly Regex regexEmail = new Regex(@"^[^@\s]+@[^@\s]+.[^@\s]+$", RegexOptions.Compiled);
        public static string LerString(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada))
                    return entrada;

                Console.WriteLine("Entrada inválida! O valor não pode ser vazio.");
            }
        }

        public static bool LerBool(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem + " (Sim/Não): ");
                string entrada = Console.ReadLine()?.Trim().ToLower(); // Remove espaços e converte para minúsculas

                if (entrada == "sim" || entrada == "s")
                    return true;
                if (entrada == "não" || entrada == "nao" || entrada == "n")
                    return false;

                Console.WriteLine("Entrada inválida! Digite 'Sim' ou 'Não'.");
            }
        }


        public static string LerEmailLogin(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && regexEmail.IsMatch(entrada))
                {
                    return entrada;
                }

                Console.WriteLine("Entrada inválida! O valor não corresponde a um email válido.");
            }
        }

        public static string LerEmailCadastro(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && regexEmail.IsMatch(entrada))
                {
                    if (!AutenticacaoService.EmailUsuarioExiste(entrada))
                    {
                        return entrada;
                    }
                    else
                    {
                        Console.WriteLine("E-mail já cadastrado.");
                    }
                }
                else { Console.WriteLine("Entrada inválida! O valor não corresponde a um email válido."); }

            }
        }
        public static string LerSenha(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && entrada.Length >= 6)
                    return entrada;

                Console.WriteLine("Entrada inválida! A senha deve ter pelo menos 6 caracteres.");
            }
        }

        public static decimal LerDecimal(string mensagem)
        {
            decimal valor;
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && decimal.TryParse(entrada, out valor) && valor > 0)
                    return valor;

                Console.WriteLine("Valor inválido! Digite um número decimal válido.");
            }
        }

        public static int LerInteiro(string mensagem)
        {
            int valor;
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && int.TryParse(entrada, out valor) && valor > 0)
                    return valor;

                Console.WriteLine("Valor inválido! Digite um número inteiro válido.");
            }
        }

        public static int LerInteiroComValorMaximo(string mensagem, int maxRange)
        {
            while (true)
            {
                Console.Write(mensagem);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int escolha) && escolha <= maxRange && escolha > -1)
                {
                    return escolha-1;
                }
                if (!string.IsNullOrWhiteSpace(input) && input.ToLower() == "v")
                {
                    return -1;
                }

                Console.WriteLine("Opção inválida! Digite um número válido.");
            }
        }

        public static int LerOpcaoListaProdutos(List<Produto> produtos, string mensagem)
        {
            Console.Clear();
            Console.WriteLine(mensagem);
            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                LerOpcaoSair();
                return -1;
            }

            ExibirProduto(produtos);

            return LerInteiroComValorMaximo("\nDigite o número do produto que deseja remover: ", produtos.Count);
        }

        public static string LerCnpj(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && ValidarCNPJ(entrada))
                    return entrada;

                Console.WriteLine("Entrada inválida! CNPJ inválido.");
            }
        }

        public static string LerCpf(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && ValidarCPF(entrada))
                    return entrada;

                Console.WriteLine("Entrada inválida! CPF inválido.");
            }
        }

        public static void LerValorParaSaque(string mensagem, Conta conta)
        {
            while (true)
            {
                if (conta.Sacar(LerDecimal(mensagem)))
                {
                    return;
                }
                Console.WriteLine("Saldo insuficiente!");
                System.Threading.Thread.Sleep(2000);
                return;
            }
        }

        public static void LerValorParaDeposito(string mensagem, Conta conta)
        {
            while (true)
            {

                var input = LerDecimal(mensagem);
                if (input == 0) return;
                if (conta.Depositar(input))
                {
                    Console.WriteLine("Deposito realizado com Sucesso!");
                    return;
                }
            }
        }

        public static string LerPlaca(string mensagem)
        {
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && ValidarPlaca(entrada))
                    return entrada;

                Console.WriteLine("Entrada inválida! Placa inválido.");
            }
        }

        private static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = Regex.Replace(cpf, @"\D", "");

            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
        private static bool ValidarCNPJ(string cnpj)
        {
            return cnpj.Length == 14 && long.TryParse(cnpj, out _);
        }
        private static bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return false;

            string padraoAntigo = @"^[A-Z]{3}-\d{4}$"; // Ex: ABC-1234
            string padraoMercosul = @"^[A-Z]{3}\d[A-Z]\d{2}$"; // Ex: ABC1D23

            return Regex.IsMatch(placa, padraoAntigo) || Regex.IsMatch(placa, padraoMercosul);
        }

    }

}
