using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.Menu;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Services;

namespace MotoMotoFood.Menus
{
    public static class MenuCadastro
    {
        public static void Exibir()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Cadastro ---");
                Console.WriteLine("1 - Cadastro Cliente");
                Console.WriteLine("2 - Cadastro Restaurante");
                Console.WriteLine("3 - Cadastro Entregador");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarCliente();
                        break;
                    case "2":
                        CadastrarRestaurante();
                        break;
                    case "3":
                        CadastrarEntregador();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        private static void CadastrarRestaurante()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Restaurante ---");
            Console.Write("Nome do Restaurante: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (BancoDeDadosFake.Usuarios.Any(r => r.Email == email))
            {
                Console.WriteLine("Já existe um restaurante com esse e-mail.");
                return;
            }

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine();

            Console.Write("Horário de Funcionamento: ");
            string horario = Console.ReadLine();

            Console.Write("Tempo de Entrega (ex: 30min): ");
            string tempo = Console.ReadLine();

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

        private static void CadastrarEntregador()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Entregador ---");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (BancoDeDadosFake.Usuarios.Any(e => e.Email == email))
            {
                Console.WriteLine("Já existe um entregador com esse e-mail.");
                return;
            }

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            Console.Write("CNH: ");
            string cnh = Console.ReadLine();

            Console.Write("Meio de Transporte (carro, moto, bicicleta): ");
            string transporte = Console.ReadLine();

            // Gerando um ID único para o entregador
            int novoId = BancoDeDadosFake.Usuarios.Count + 1;

            // Criando o objeto corretamente com o construtor
            var entregador = new Entregador(novoId, nome, email, senha, endereco, cpf, cnh, Util.Enums.TipoTransporte.Moto);

            BancoDeDadosFake.Usuarios.Add(entregador);
            Console.WriteLine($"\nEntregador {nome} cadastrado com sucesso!");
            MenuEntregador.ExibirMenuEntregador(entregador);
        }





        private static void CadastrarCliente()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Cliente ---");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (AutenticacaoService.EmailUsuarioExiste(email))
            {
                Console.WriteLine("Já existe um cliente com esse e-mail.");
                return;
            }

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

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

