using ClubeLeituraApp.Controladores;
using ClubeLeituraApp.Dominio;
using ClubeLeituraApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Telas
{
    class TelaAmigo : TelaBase, ICadastravel
    {
        private ControladorAmigo controladorAmigo;

        public TelaAmigo(ControladorAmigo controladorAmigo)
          : base("Cadastro de Amigo")
        {
            this.controladorAmigo = controladorAmigo;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo amigo...");

            bool conseguiuGravar = GravarAmigo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o amigo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando amigos...");

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-35} | {3,-25} | {4,-15}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Amigo[] amigos = controladorAmigo.SelecionarTodasAmigos();

            if (amigos.Length == 0)
            {
                ApresentarMensagem("Nenhum amigo cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < amigos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   amigos[i].id, amigos[i].nome, amigos[i].nomeResponsavel, amigos[i].telefone, amigos[i].local);
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do amigo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarAmigo(id);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o amigo", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do amigo deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorAmigo.ExcluirAmigo(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Amigo excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o amigo", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }


        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo amigo");
            Console.WriteLine("Digite 2 para visualizar os amigos");
            Console.WriteLine("Digite 3 para editar um amigo");
            Console.WriteLine("Digite 4 para excluir um amigo");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region métodos privados
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Nome", "Nome Responsável", "Telefone", "Local");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarAmigo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amigo: ");
            int telefone = Convert.ToInt32(Console.ReadLine());

            Console.Write("De onde é o amigo: ");
            string local =Console.ReadLine();

            resultadoValidacao = controladorAmigo.RegistrarAmigo(id, nome, nomeResponsavel, telefone, local);

            if (resultadoValidacao != "AMIGO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        #endregion
    }
}
