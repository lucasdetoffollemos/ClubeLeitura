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
    class TelaCaixa : TelaBase, ICadastravel
    {
        private ControladorCaixa controladorCaixa;

        public TelaCaixa(ControladorCaixa controlador)
          : base("Cadastro de Caixa")
        {
            controladorCaixa = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova caixa...");

            bool conseguiuGravar = GravarCaixa(0);

            if (conseguiuGravar)
                ApresentarMensagem("Caixa inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir a caixa", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando as caixas...");

            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Caixa[] caixas = controladorCaixa.SelecionarTodasCaixas();

            if (caixas.Length == 0)
            {
                ApresentarMensagem("Nenhuma caixa cadastrada!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < caixas.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   caixas[i].id, caixas[i].cor, caixas[i].etiqueta);
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarCaixa(id);

            if (conseguiuGravar) 
                ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar a caixa", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja excluir: ");
            int id= Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorCaixa.ExcluirCaixa(id);

            if (conseguiuExcluir) 
                ApresentarMensagem("Caixa excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir a caixa", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

       
        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova caixa");
            Console.WriteLine("Digite 2 para visualizar as caixas");
            Console.WriteLine("Digite 3 para editar uma caixa");
            Console.WriteLine("Digite 4 para excluir uma caixa");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region métodos privados
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Cor", "Etiqueta");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarCaixa(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            resultadoValidacao = controladorCaixa.RegistrarCaixa(
                id, cor, etiqueta);

            if (resultadoValidacao != "CAIXA_VALIDA")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        #endregion


    }
}
