using ClubeLeituraApp.Controladores;
using ClubeLeituraApp.Interfaces;
using System;

namespace ClubeLeituraApp.Telas
{
    public class TelaPrincipal : TelaBase
    {
        private readonly ControladorCaixa controladorCaixa;
        private readonly ControladorRevista controladorRevista;
        private readonly ControladorAmigo controladorAmigo;
        private readonly ControladorEmprestimo controladorEmprestimo;

        public TelaPrincipal(ControladorCaixa controladorCaixa, ControladorRevista controladorRevista, ControladorAmigo controladorAmigo, ControladorEmprestimo controladorEmprestimo)
          : base("Tela Principal")
        {
            this.controladorCaixa = controladorCaixa;
            this.controladorRevista = controladorRevista;
            this.controladorAmigo = controladorAmigo;
            this.controladorEmprestimo = controladorEmprestimo;
        }

        public TelaBase ObterOpcao()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;

            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastrar as caixas");
                Console.WriteLine("Digite 2 para o Cadastrar as revsitas");
                Console.WriteLine("Digite 3 para o Cadastrar os amiguinhos");
                Console.WriteLine("Digite 4 para o Realizar empréstimos");
                Console.WriteLine("Digite S para Sair");

                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = new TelaCaixa(controladorCaixa);

                else if (opcao == "2")
                    telaSelecionada = new TelaRevista(controladorRevista, controladorCaixa);

                else if (opcao == "3")
                    telaSelecionada = new TelaAmigo(controladorAmigo);

                else if (opcao == "4")
                    telaSelecionada = new TelaEmprestimo(controladorEmprestimo, controladorAmigo, controladorRevista);

                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }
}
