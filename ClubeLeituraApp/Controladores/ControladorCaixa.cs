using ClubeLeituraApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Controladores
{
    public class ControladorCaixa: ControladorBase
    {

        public string RegistrarCaixa(int id, string cor, string etiqueta)
        {
            Caixa caixa;

            int posicao;

            if (id == 0)
            {
                caixa = new Caixa();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Caixa(id));
                caixa = (Caixa)registros[posicao];
            }

            caixa.cor = cor;
            caixa.etiqueta = etiqueta;

            //string resultadoValidacao = caixa.Validar();

            //if (resultadoValidacao == "CAIXA_VALIDO")
            registros[posicao] = caixa;

            return "CAIXA_VALIDA";
        }

        public Caixa SelecionarCaixaPorId(int id)
        {
            return (Caixa)SelecionarRegistroPorId(new Caixa(id));
        }

        public bool ExcluirCaixa(int idSelecionado)
        {
            return ExcluirRegistro(new Caixa(idSelecionado));
        }

        public Caixa[] SelecionarTodasCaixas()
        {
            Caixa[] caixasAux = new Caixa[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), caixasAux, caixasAux.Length);

            return caixasAux;
        }
    }
}
