using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Dominio
{
    public class Revista
    {
        public int id;
        public string tipoColecao;
        public int numeroEdicao;
        public int anoRevista;
        public Caixa caixa;

        public Revista()
        {
            this.id = GeradorId.GerarIdRevista();
        }

        public Revista(int idSelecionado)
        {
            this.id = idSelecionado;
        }

        public override bool Equals(object obj)
        {
            Revista revista = (Revista)obj;

            if (id == revista.id)
                return true;
            else
                return false;
        }
    }
}
