using ClubeLeituraApp.Controladores;
using ClubeLeituraApp.Interfaces;
using ClubeLeituraApp.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp
{
    class Program
    {
        static void Main(string[] args)
        {

            ControladorCaixa controladorCaixa = new ControladorCaixa();
            ControladorRevista controladorRevista = new ControladorRevista(controladorCaixa);
            ControladorAmigo controladorAmigo = new ControladorAmigo();
            ControladorEmprestimo controladorEmprestimo = new ControladorEmprestimo(controladorAmigo, controladorRevista);

            TelaPrincipal telaPrincipal = new TelaPrincipal(controladorCaixa, controladorRevista, controladorAmigo, controladorEmprestimo);

            while (true)
            {
                TelaBase telaSelecionada = telaPrincipal.ObterOpcao();

                if (telaSelecionada == null)
                    break;
                Console.Clear();

                if (telaSelecionada is TelaBase)
                    Console.WriteLine(telaSelecionada.Titulo); Console.WriteLine();

                if(telaSelecionada is ICadastravel)
                {
                    ICadastravel tela = (ICadastravel)telaSelecionada;
                    string opcao = tela.ObterOpcao();

                    if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2")
                    {
                        tela.VisualizarRegistros();
                        Console.ReadLine();
                    }

                    else if (opcao == "3")
                        tela.EditarRegistro();

                    else if (opcao == "4")
                        tela.ExcluirRegistro();

                    Console.Clear();
                }

                else if(telaSelecionada is TelaEmprestimo)
                {
                    TelaEmprestimo tela = (TelaEmprestimo)telaSelecionada;
                    string opcao = tela.ObterOpcao();

                    if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (opcao == "1")
                        tela.RegistrarEmprestimo();

                    else if (opcao == "2")
                        tela.RegistrarDevolucao();

                    else if (opcao == "3")
                    {
                        tela.VisualizarEmprestimosAbertosDia();
                        Console.ReadLine();
                    }

                    else if (opcao == "4")
                    {
                        tela.VisualizarEmprestimosFechadosDeterminadoMes();
                        Console.ReadLine();
                    }

                }
                
            }
        }
    }
}
