using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Dominio
{
    public class Emprestimo
    {
        public int id;
        public Amigo amigo;
        public Revista revista;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public string status;

        public Emprestimo()
        {
            id = GeradorId.GerarIdEmprestimo();
        }

        public Emprestimo(int idSelecionado)
        {
            this.id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            //if (DateTime.Now > dataEmprestimo)
                //resultadoValidacao += "O campo data de emprestimo não pode ser no passado \n";

            if (dataDevolucao < dataEmprestimo)
                resultadoValidacao += "O campo data de devoluçao precisa ser depois da data que a revista foi emprestada \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "EMPRESTIMO_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Emprestimo emprestimo = (Emprestimo)obj;

            if (emprestimo.id == id)
                return true;
            else
                return false;
        }
    }
}
