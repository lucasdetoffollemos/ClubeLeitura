using ClubeLeituraApp.Controladores;
using ClubeLeituraApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Telas
{
    public class TelaEmprestimo : TelaBase
    {
        private ControladorEmprestimo controladorEmprestimo;
        private ControladorAmigo controladorAmigo;
        private ControladorRevista controladorRevista;


        public TelaEmprestimo(ControladorEmprestimo controladorEmprestimo, ControladorAmigo controladorAmigo, ControladorRevista controladorRevista) : base("Tela Revista")
        {
            this.controladorEmprestimo = controladorEmprestimo;
            this.controladorAmigo = controladorAmigo;
            this.controladorRevista = controladorRevista;
        }

        public void RegistrarEmprestimo()
        {
            ConfigurarTela("Realizando o Empréstimo...");

            bool conseguiuGravar = GravarEmprestimo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Empréstimo realizado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar realizar empréstimo", TipoMensagem.Erro);
                RegistrarEmprestimo();
            }
        }

        public void RegistrarDevolucao()
        {
            ConfigurarTela("Realizando Devolução...");

            VisualizarTodosEmprestimosAbertos();

            Console.WriteLine("Digite o id do empréstimo que deseja fazer devolucao: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = Devolver(id);

            if (conseguiuGravar)
                ApresentarMensagem("Devolução realizada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar realizar devolução", TipoMensagem.Erro);
                RegistrarEmprestimo();
            }
        }

        public void VisualizarEmprestimosAbertosDia()
        {
            ConfigurarTela("Visualizando Emprestimos do dia...");

            MontarCabecalhoTabela();

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarTodosEmprestimos();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum empréstimo aberto!", TipoMensagem.Atencao);
                return;
            }

            foreach (Emprestimo e in emprestimos)
            {
                if(e.dataEmprestimo.Date == DateTime.Now.Date && e.status == "EMPRESTADO")
                {
                    Console.WriteLine("{0,-5} | {1,-10} | {2,-25} | {3,-20} | {4,-20} | {5,-20}", e.id, e.amigo.nome, e.revista.tipoColecao, e.dataEmprestimo.ToString("dd/MM/yyyy"), e.dataDevolucao.ToString("dd/MM/yyyy"), e.status);
                }
            }
           
        }

        public void VisualizarEmprestimosFechadosDeterminadoMes()
        {
            Console.WriteLine("Digite o mês(em número), que deseja verificar as devoluções: ");
            int mes = Convert.ToInt32(Console.ReadLine());

            ConfigurarTela("Visualizando Emprestimos fechados...");

            MontarCabecalhoTabela();

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarTodosEmprestimos();

            //if (emprestimos.Length == 0)
            //{
            //    ApresentarMensagem("Nenhum empréstimo devolvido neste mes!", TipoMensagem.Atencao);
            //    return;
            //}

            foreach (Emprestimo e in emprestimos)
            {
                if (e.dataEmprestimo.Month == mes && e.status == "DEVOLVIDO")
                {
                    Console.WriteLine("{0,-5} | {1,-10} | {2,-25} | {3,-20} | {4,-20} | {5,-20}", e.id, e.amigo.nome, e.revista.tipoColecao, e.dataEmprestimo.ToString("dd/MM/yyyy"), e.dataDevolucao.ToString("dd/MM/yyyy"), e.status);
                }
            }

        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para realizar empréstimo");
            Console.WriteLine("Digite 2 para registrar devolucao");
            Console.WriteLine("Digite 3 para visualizar os empréstimos realizados no dia");
            Console.WriteLine("Digite 4 ");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-5} | {1,-10} | {2,-25} | {3,-20} | {4,-20} | {5,-20}", "Id", "Nome do amigunho", "Revista","Data Empréstimo", "Data Devolução", "Status");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarEmprestimo(int idEmprestimo)
        {

            VisualizarAmigos();

            Console.Write("Digite o Id do amiguinho que voce deseja emprestar a revista: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            VisualizarRevistas();

            Console.Write("Digite o Id da revista que voce deseja emprestar ao seu amigo: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a data de empréstimo da revista: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data de devolução da revista: ");
            DateTime dataDevolucao = Convert.ToDateTime(Console.ReadLine());

            string status = "EMPRESTADO";

            string resultadoValidacao = controladorEmprestimo.RegistrarEmprestimo(idEmprestimo, idAmigo, idRevista, dataEmprestimo, dataDevolucao, status);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "EMPRESTIMO_VALIDO")
            {
                Console.WriteLine(idEmprestimo);
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private void VisualizarTodosEmprestimosAbertos()
        {
            ConfigurarTela("Visualizando todos os empréstimos abertos...");

            MontarCabecalhoTabela();

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarTodosEmprestimos();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum empréstimo aberto!", TipoMensagem.Atencao);
                return;
            }

            foreach (Emprestimo e in emprestimos)
            {
                if (e.status == "EMPRESTADO")
                {
                    Console.WriteLine("{0,-5} | {1,-10} | {2,-25} | {3,-20} | {4,-20} | {5,-20}", e.id, e.amigo.nome, e.revista.tipoColecao, e.dataEmprestimo.ToString("dd/MM/yyyy"), e.dataDevolucao.ToString("dd/MM/yyyy"), e.status);
                }
            }
        }


        private bool Devolver(int idEmprestimo)
        {
            string status = "DEVOLVIDO";

            string resultadoValidacao = controladorEmprestimo.RegistrarDevolucao(idEmprestimo, status);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "DEVOLUCAO_VALIDA")
            {
                Console.WriteLine(idEmprestimo);
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private void VisualizarAmigos()
        {
            Console.WriteLine();
            Amigo[] amigos = controladorAmigo.SelecionarTodasAmigos();

            Console.WriteLine("{0,-10} | {1,-25} | {2,-35} | {3,-25} | {4,-15}", "Id", "Nome", "Nome Responsável", "Telefone", "Local");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            foreach (var a in amigos)
            {
                Console.WriteLine("{0,-10} | {1,-25} | {2,-35} | {3,-25} | {4,-15}", a.id, a.nome, a.nomeResponsavel, a.telefone, a.local);
            }
            Console.WriteLine();
        }

        private void VisualizarRevistas()
        {
            Console.WriteLine();
            Revista[] revistas = controladorRevista.SelecionarTodasRevistas();

            Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}", "Id", "Cor da caixa", "Tipo de coleção", "Número de edição", "Ano da Revista");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            foreach (var r in revistas)
            {
                Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}", r.id, r.caixa.cor, r.tipoColecao, r.numeroEdicao, r.anoRevista);
            }
            Console.WriteLine();
        }

        #endregion
    }
}
