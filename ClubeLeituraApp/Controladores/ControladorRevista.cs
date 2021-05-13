using ClubeLeituraApp.Dominio;
using ClubeLeituraApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Controladores
{
    public class ControladorRevista : ControladorBase
    {
        private ControladorCaixa controladorCaixa;

        public ControladorRevista(ControladorCaixa controladorCaixa)
        {
            this.controladorCaixa = controladorCaixa;
        }


        public string RegistrarRevista(int id, int idCaixa, string tipoColecao, int numeroEdicao, int anoRevista)
        {
            Revista revista;

            int posicao;

            if (id == 0)
            {
                revista = new Revista();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Revista(id));
                revista = (Revista)registros[posicao];
            }

            revista.caixa = controladorCaixa.SelecionarCaixaPorId(idCaixa);
            revista.tipoColecao = tipoColecao;
            revista.numeroEdicao = numeroEdicao;
            revista.anoRevista = anoRevista;

            //string resultadoValidacao = revista.Validar();

            //if (resultadoValidacao == "CAIXA_VALIDO")
            registros[posicao] = revista;

            return "REVISTA_VALIDA";
        }

        public Revista SelecionarRevistaPorId(int id)
        {
            return (Revista)SelecionarRegistroPorId(new Revista(id));
        }

        public bool ExcluirRevista(int idSelecionado)
        {
            return ExcluirRegistro(new Revista(idSelecionado));
        }

        public Revista[] SelecionarTodasRevistas()
        {
            Revista[] revistasAux = new Revista[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), revistasAux, revistasAux.Length);

            return revistasAux;
        }
    }
}
