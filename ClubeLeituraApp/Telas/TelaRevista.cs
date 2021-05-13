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
    public class TelaRevista : TelaBase, ICadastravel
    {
        private ControladorRevista controladorRevista;
        private ControladorCaixa controladorCaixa;

        public TelaRevista(ControladorRevista controladorRevista, ControladorCaixa controladorCaixa):base("Tela Revista")
        {
            this.controladorRevista = controladorRevista;
            this.controladorCaixa = controladorCaixa;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova revista...");

            bool conseguiuGravar = GravarRevista(0);

            if (conseguiuGravar)
                ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir a revista", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando Revistas...");

            MontarCabecalhoTabela();

            Revista[] revistas = controladorRevista.SelecionarTodasRevistas();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhuma revista registrada!", TipoMensagem.Atencao);
                return;
            }

            foreach (Revista revista in revistas)
            {
                Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}",
                    revista.id, revista.caixa.cor, revista.tipoColecao, revista.numeroEdicao, revista.anoRevista);
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma revsita...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da revista que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuEditar = GravarRevista(idSelecionado);

            if (conseguiuEditar)
                ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar a revista", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma revista...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da revista que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorRevista.ExcluirRevista(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir a revista", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova revista");
            Console.WriteLine("Digite 2 para visualizar revsitas");
            Console.WriteLine("Digite 3 para editar uma revista");
            Console.WriteLine("Digite 4 para excluir uma revista");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }


        #region Métodos privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}", "Id", "Cor da caixa", "Tipo de coleção", "Número de edição", "Ano da Revista");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarRevista(int idRevista)
        {
            VisualizarCaixas();

            Console.Write("Digite o Id da caixa que voce deseja garvar sua revista: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o tipo de coleção: ");
            string tipoColecao = Console.ReadLine();

            Console.Write("Digite a número da ediçao da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digiteo o ano da revista: ");
            int anoRevista = Convert.ToInt32(Console.ReadLine());

            string resultadoValidacao = controladorRevista.RegistrarRevista(idRevista, idCaixa, tipoColecao, numeroEdicao, anoRevista);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "REVISTA_VALIDA")
            {
                Console.WriteLine(idRevista);
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private void VisualizarCaixas()
        {
            Console.WriteLine();
            Caixa[] caixas = controladorCaixa.SelecionarTodasCaixas();

            Console.WriteLine("{0,-10} | {1,-55} | {2,-35}", "Id" , "Cor", "Etiqueta");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            foreach (var c in caixas)
            {
                Console.WriteLine("{0,-10} | {1,-55} | {2,-35}", c.id, c.cor, c.etiqueta);
            }
            Console.WriteLine();
        }

        #endregion

    }
}
