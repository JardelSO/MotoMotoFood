﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.Menu;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Services;
using MotoMotoFood.Util;

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
                string opcao = Helpers.LerString("Escolha uma opção: ");

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
                //Console.ReadLine();
            }
        }

        private static void CadastrarRestaurante()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Restaurante ---");
            string nome = Helpers.LerString("Nome do Restaurante: ");

            string email = Helpers.LerEmail("Email: ");

            if (BancoDeDadosFake.Usuarios.Any(r => r.Email == email))
            {
                Console.WriteLine("Já existe um restaurante com esse e-mail.");
                return;
            }

            string senha = Helpers.LerSenha("Senha: ");

            string endereco = Helpers.LerString("Endereço: ");

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

        private static void CadastrarEntregador()
        {
            Console.Clear();
            Console.WriteLine("--- Cadastro Entregador ---");

            string nome = Helpers.LerString("Nome: ");
            string email = Helpers.LerEmail("Email: ");

            if (BancoDeDadosFake.Usuarios.Any(e => e.Email == email))
            {
                Console.WriteLine("Já existe um entregador com esse e-mail.");
                return;
            }

            string senha = Helpers.LerSenha("Senha: ");
            string endereco = Helpers.LerString("Endereço: ");
            string cpf = Helpers.LerCpf("CPF: ");
            string cnh = Helpers.LerString("CNH: ");
            string transporte = Helpers.LerString("Meio de Transporte (carro, moto, bicicleta): ");

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

            string nome = Helpers.LerString("Nome: ");

            string email = Helpers.LerEmail("Email: ");

            if (AutenticacaoService.EmailUsuarioExiste(email))
            {
                Console.WriteLine("Já existe um cliente com esse e-mail.");
                return;
            }

            string senha = Helpers.LerSenha("Senha: ");
            string endereco = Helpers.LerEmail("Endereço: ");
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

