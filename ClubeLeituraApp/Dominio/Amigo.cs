using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Dominio
{
    public class Amigo
    {
        public int id;
        public string nome;
        public string nomeResponsavel;
        public int telefone;
        public string local;

        public Amigo()
        {
            id = GeradorId.GerarIdAmigo();
        }

        public Amigo(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            Amigo amigo = (Amigo)obj;

            if (amigo.id == id)
                return true;
            else
                return false;
        }

    }
}
