using ClubeLeituraApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Controladores
{
    public class ControladorAmigo : ControladorBase  
    {
        public string RegistrarAmigo(int id, string nome, string nomeResponsavel, int telefone, string local)
        {
            Amigo amigo;

            int posicao;

            if (id == 0)
            {
                amigo = new Amigo();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Amigo(id));
                amigo = (Amigo)registros[posicao];
            }

            amigo.nome = nome;
            amigo.nomeResponsavel = nomeResponsavel;
            amigo.telefone = telefone;
            amigo.local = local;

            //string resultadoValidacao = amigo.Validar();

            //if (resultadoValidacao == "CAIXA_VALIDO")
            registros[posicao] = amigo;

            return "AMIGO_VALIDO";
        }

        public Amigo SelecionarAmigoPorId(int id)
        {
            return (Amigo)SelecionarRegistroPorId(new Amigo(id));
        }

        public bool ExcluirAmigo(int idSelecionado)
        {
            return ExcluirRegistro(new Amigo(idSelecionado));
        }

        public Amigo[] SelecionarTodasAmigos()
        {
            Amigo[] amigosAux = new Amigo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), amigosAux, amigosAux.Length);

            return amigosAux;
        }
    }
}

