using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.entitdades;
using MotoMotoFood.Menu;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Services;
using MotoMotoFood.Util;

namespace MotoMotoFood.Menus
{
    public static class MenuCadastro
    {
        public static async Task ExibirAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Cadastro ---");
                Console.WriteLine("1 - Cadastro Cliente");
                Console.WriteLine("2 - Cadastro Restaurante");
                Console.WriteLine("3 - Cadastro Entregador");
                Console.WriteLine("0 - Voltar");
                string opcao = Helpers.LerString("Escolha uma opção: ");

                switch (opcao)
                {
                    case "1":
                        await CadastrarClienteAsync();
                        break;
                    case "2":
                        await CadastrarRestauranteAsync();
                        break;
                    case "3":
                        await CadastrarEntregadorAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione ENTER para continuar...");            }
        }

        private static async Task CadastrarRestauranteAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Restaurante ---");
            string nome = Helpers.LerString("Nome do Restaurante: ");

            string email = Helpers.LerEmailCadastro("Email: ");

            if (BancoDeDadosFake.Usuarios.Any(r => r.Email == email))
            {
                Console.WriteLine("Já existe um restaurante com esse e-mail.");
                return;
            }

            string senha = Helpers.LerSenha("Senha: ");

            CepService consultaCep = new CepService();
            Endereco endereco = await consultaCep.ObterEndereco();

            string cnpj = Helpers.LerCnpj("CNPJ: ");

            string horario = Helpers.LerString("Horário de Funcionamento: ");

            string tempo = Helpers.LerString("Tempo de Entrega (ex: 30min):");

            var restaurante = new Restaurante
            {
                NomeRestaurante = nome,
                Email = email,
                Senha = senha,
                Endereco = endereco,
                CNPJ = cnpj,
                HorarioFuncionamento = horario,
                TempoEntrega = tempo
            };

            BancoDeDadosFake.Usuarios.Add(restaurante);
            Console.WriteLine($"\nRestaurante {nome} cadastrado com sucesso!");
            MenuRestaurante.ExibirMenuRestaurante(restaurante);
        }

        private static async Task CadastrarEntregadorAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Entregador ---");

            string nome = Helpers.LerString("Nome: ");
            string email = Helpers.LerEmailCadastro("Email: ");

            if (BancoDeDadosFake.Usuarios.Any(e => e.Email == email))
            {
                Console.WriteLine("Já existe um entregador com esse e-mail.");
                return;
            }

            string senha = Helpers.LerSenha("Senha: ");
            CepService consultaCep = new CepService();
            Endereco endereco = await consultaCep.ObterEndereco();
            string cpf = Helpers.LerCpf("CPF: ");
            string cnh = Helpers.LerString("CNH: ");
            string transporte = Helpers.LerString("Meio de Transporte (carro, moto, bicicleta): ");

            int novoId = BancoDeDadosFake.Usuarios.Count + 1;

            Entregador entregador = new Entregador(novoId, nome, email, senha, endereco, cpf, cnh, Util.Enums.TipoTransporte.Moto);

            BancoDeDadosFake.Usuarios.Add(entregador);
            Console.WriteLine($"\nEntregador {nome} cadastrado com sucesso!");
            MenuEntregador.ExibirMenuEntregador(entregador);
        }

        private static async Task CadastrarClienteAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Cliente ---");

            string nome = Helpers.LerString("Nome: ");

            string email = Helpers.LerEmailCadastro("Email: ");

            if (AutenticacaoService.EmailUsuarioExiste(email))
            {
                Console.WriteLine("Já existe um cliente com esse e-mail.");
                Thread.Sleep(3000);
                return;
            }

            string senha = Helpers.LerSenha("Senha: ");
            CepService consultaCep = new CepService();
            Endereco endereco = await consultaCep.ObterEndereco();
            string cpf = Helpers.LerCpf("CPF: ");

            Cliente novoCliente = new Cliente
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                Endereco = endereco,
                CPF = cpf
            };

            BancoDeDadosFake.Usuarios.Add(novoCliente);
            Console.WriteLine($"\nCadastro realizado com sucesso! Bem-vindo(a), {nome}.");
            MenuCliente.Exibir(novoCliente);
        }
    }
}

