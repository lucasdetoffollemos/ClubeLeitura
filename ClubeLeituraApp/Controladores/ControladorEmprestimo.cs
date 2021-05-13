using ClubeLeituraApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Controladores
{
    public class ControladorEmprestimo : ControladorBase
    {
        private ControladorAmigo controladorAmigo;
        private ControladorRevista controladorRevista;

        public ControladorEmprestimo( ControladorAmigo controladorAmigo, ControladorRevista controladorRevista)
        {
            this.controladorAmigo = controladorAmigo;
            this.controladorRevista = controladorRevista;
        }

        public string RegistrarEmprestimo(int id, int idAmigo, int idRevista, DateTime dataEmprestimo, DateTime dataDevolucao, string status)
        {
            Emprestimo emprestimo;

            int posicao;

            emprestimo = new Emprestimo();
            posicao = ObterPosicaoVaga();

            emprestimo.amigo = controladorAmigo.SelecionarAmigoPorId(idAmigo);
            emprestimo.revista = controladorRevista.SelecionarRevistaPorId(idRevista);
            emprestimo.dataEmprestimo = dataEmprestimo;
            emprestimo.dataDevolucao = dataDevolucao;
            emprestimo.status = status;

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                registros[posicao] = emprestimo;

            return resultadoValidacao;
        }

        public string RegistrarDevolucao(int id, string status)
        {
            Emprestimo emprestimo;

            int posicao;

            posicao = ObterPosicaoOcupada(new Emprestimo(id));
            emprestimo = (Emprestimo)registros[posicao];

            emprestimo.status = status;
            registros[posicao] = emprestimo;

            return "DEVOLUCAO_VALIDA";
        }

        public Emprestimo[] SelecionarTodosEmprestimos()
        {
            Emprestimo[] emprestimosAux = new Emprestimo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), emprestimosAux, emprestimosAux.Length);

            return emprestimosAux;
        }
    }
}
