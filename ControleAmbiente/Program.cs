using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Program
    {
        private static Cadastro cadastro;        

        static void Main(string[] args)
        {
            bool valido = true;
            string opcao, nome, idInserido;
            int id, idAux;

            cadastro = new Cadastro();

            do
            {
                Console.WriteLine();
                Console.WriteLine("0. Finalizar processo");
                Console.WriteLine("1. Cadastrar ambiente");
                Console.WriteLine("2. Consultar ambiente");
                Console.WriteLine("3. Excluir ambiente");
                Console.WriteLine("4. Cadastrar usuario");
                Console.WriteLine("5. Consultar usuario");
                Console.WriteLine("6. Excluir usuario");
                Console.WriteLine("7. Conceder permissão de acesso ao usuario ");
                Console.WriteLine("8. Revogar permissão de acesso ao usuario ");
                Console.WriteLine("9. Registrar acesso");
                Console.WriteLine("10.  Consultar logs de acesso");
                Console.WriteLine();
                Console.Write("Selecione uma opção: ");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira um ID para o ambiente: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o nome do ambiente: ");
                                nome = Console.ReadLine();

                                cadastro.adicionarAmbiente(new Ambiente(id, nome, new Queue<Log>()));

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                if (ex.Message.Contains("já foi cadastrado".ToUpper()))
                                {
                                    Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                    Console.WriteLine();
                                }
                            }
                        } while (!valido);

                        break;
                    case "2":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.WriteLine(" == insira apenas os valores pelos quais deseja pesquisar == ");
                                Console.WriteLine(" == caso não queira utilizar algum dos filtros, deixe em branco e pressione enter == ");
                                Console.WriteLine(" == caso não preencha nenhum filtro, todos medicamentos serão retornados == ");
                                Console.WriteLine();

                                Console.Write("Insira um ID para o ambiente: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o nome do ambiente: ");
                                nome = Console.ReadLine();

                                cadastro.pesquisarAmbientes(new Ambiente(id, nome, new Queue<Log>()));

                                valido = true;

                                List<Ambiente> ambientesPesquisados = cadastro.pesquisarAmbientes(new Ambiente(id, nome, new Queue<Log>()));

                                if (ambientesPesquisados.Count <= 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foram encontrados resultados para a pesquisa");
                                }
                                else
                                {
                                    foreach (var r in ambientesPesquisados)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(r.toString());
                                    }

                                }

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;
                    case "3":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira o ID do ambiente a ser removido: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                if (cadastro.removerAmbiente(new Ambiente(id, String.Empty, new Queue<Log>())))
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Ambiente removido com sucesso");
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foram encontrados resultados para a pesquisa");
                                }

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;
                    case "4":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira um ID para o usuario: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o nome do usuario: ");
                                nome = Console.ReadLine();

                                cadastro.adicionarUsuario(new Usuario(id, nome,new List<Ambiente>()));

                                valido = true;

                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                if (ex.Message.Contains("01/01/2001".ToUpper()))
                                {
                                    Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                    Console.WriteLine();
                                }
                                else if (ex.Message.Contains(" Não há um medicamento registrado com o ID".ToUpper()))
                                {
                                    Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                    Console.WriteLine();

                                    valido = true;
                                }
                                else if (ex.Message.Contains(" já foi cadastrado para este medicamento".ToUpper()))
                                {
                                    Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                    Console.WriteLine();

                                    valido = true;
                                }
                                else
                                {
                                    Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                    Console.WriteLine();
                                }
                            }
                        } while (!valido);

                        break;
                    case "5":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.WriteLine(" == insira apenas os valores pelos quais deseja pesquisar == ");
                                Console.WriteLine(" == caso não queira utilizar algum dos filtros, deixe em branco e pressione enter == ");
                                Console.WriteLine(" == caso não preencha nenhum filtro, todos medicamentos serão retornados == ");
                                Console.WriteLine();

                                Console.Write("Insira um ID para o usuario: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o nome do usuario: ");
                                nome = Console.ReadLine();

                                cadastro.pesquisarUsuario(new Usuario(id, nome, new List<Ambiente>()));

                                valido = true;

                                List<Usuario> usuariosPesquisados = cadastro.pesquisarUsuarios(new Usuario(id, nome, new List<Ambiente>()));

                                if (usuariosPesquisados.Count <= 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foram encontrados resultados para a pesquisa");
                                }
                                else
                                {
                                    foreach (var r in usuariosPesquisados)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(r.toString());
                                    }

                                }


                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                if (ex.Message.Contains("01/01/2001".ToUpper()))
                                {
                                    Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                    Console.WriteLine();
                                }
                                else if (ex.Message.Contains(" Não há um medicamento registrado com o ID".ToUpper()))
                                {
                                    Console.WriteLine(String.Concat(" == ", ex.Message, " == ".ToUpper()));
                                    Console.WriteLine();

                                    valido = true;
                                }
                                else
                                {
                                    Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                    Console.WriteLine();
                                }
                            }
                        } while (!valido);

                        break;
                    case "6":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira o ID do usuario a ser removido: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                if (cadastro.removerUsuario(new Usuario(id, String.Empty, new List<Ambiente>())))
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Usuario removido com sucesso");
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foram encontrados resultados para a pesquisa ou o usuário ainda possui permissões, certifique-se de revogar todas permissões antes de excluir o usuário.");
                                }

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;
                    case "7":
                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira o ID do usuario: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o ID do ambiente: ");
                                idInserido = Console.ReadLine();
                                idAux = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Usuario usuarioPesquisa   = new Usuario(id, String.Empty, new List<Ambiente>());
                                Ambiente ambientePesquisa = new Ambiente(id, String.Empty, new Queue<Log>());

                                Usuario usuarioSelecionado = cadastro.pesquisarUsuario(usuarioPesquisa);
                                Ambiente ambienteSelecionado = cadastro.pesquisarAmbiente(ambientePesquisa);

                                if(usuarioSelecionado != null && ambienteSelecionado != null)
                                {
                                    if (usuarioSelecionado.concederPermissao(ambienteSelecionado))
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Ambiente liberado com sucesso");
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Ambiente já foi liberado para o {usuarioSelecionado.toString()}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foi encontrado o usuário e/ou ambiente");
                                }                                

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;
                    case "8":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira o ID do usuario: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o ID do ambiente: ");
                                idInserido = Console.ReadLine();
                                idAux = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Usuario usuarioPesquisa = new Usuario(id, String.Empty, new List<Ambiente>());
                                Ambiente ambientePesquisa = new Ambiente(id, String.Empty, new Queue<Log>());

                                Usuario usuarioSelecionado = cadastro.pesquisarUsuario(usuarioPesquisa);
                                Ambiente ambienteSelecionado = cadastro.pesquisarAmbiente(ambientePesquisa);

                                if (usuarioSelecionado != null && ambienteSelecionado != null)
                                {
                                    if (usuarioSelecionado.revogarPermissao(ambienteSelecionado))
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Ambiente revogado com sucesso");
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Ambiente já foi revogado para o {usuarioSelecionado.toString()}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foi encontrado o usuário e/ou ambiente");
                                }

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;
                    case "9":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira o ID do usuario: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Console.Write("Insira o ID do ambiente: ");
                                idInserido = Console.ReadLine();
                                idAux = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Usuario usuarioPesquisa = new Usuario(id, String.Empty, new List<Ambiente>());
                                Ambiente ambientePesquisa = new Ambiente(id, String.Empty, new Queue<Log>());

                                Usuario usuarioSelecionado = cadastro.pesquisarUsuario(usuarioPesquisa);
                                Ambiente ambienteSelecionado = cadastro.pesquisarAmbiente(ambientePesquisa);

                                if (usuarioSelecionado != null && ambienteSelecionado != null)
                                {
                                    Log log = new Log(cadastro.UltimoIdLog,DateTime.Now, usuarioSelecionado, usuarioSelecionado.possuiPermissao(ambienteSelecionado));

                                    ambienteSelecionado.registrarLog(log);
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Não foi encontrado o usuário e/ou ambiente");
                                }

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;
                    case "10":

                        do
                        {
                            try
                            {
                                if (valido)
                                    Console.Clear();

                                Console.Write("Insira o ID do ambiente: ");
                                idInserido = Console.ReadLine();
                                id = Convert.ToInt32((String.IsNullOrEmpty(idInserido) ? "0" : idInserido));

                                Ambiente ambienteSelecionado = cadastro.pesquisarAmbiente(new Ambiente(id, String.Empty, new Queue<Log>()));

                                if(ambienteSelecionado != null)
                                {
                                    foreach(var r in ambienteSelecionado.Logs)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(r.toString());
                                    }
                                }

                                valido = true;
                            }
                            catch (Exception ex)
                            {
                                valido = false;

                                Console.Clear();

                                Console.WriteLine(" == O valor ID aceita apenas números. == ".ToUpper());
                                Console.WriteLine();

                            }
                        } while (!valido);

                        break;

                }

                Console.WriteLine();
                Console.WriteLine("======== FIM OPERACAO ========");
                Console.WriteLine();

            } while (!opcao.Equals("0"));

            cadastro.upload();
        }
    }
}
