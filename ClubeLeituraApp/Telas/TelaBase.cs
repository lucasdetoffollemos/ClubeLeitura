using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Telas
{
    public abstract class TelaBase
    {
        private string titulo;

        public string Titulo { get { return titulo; } }

        public TelaBase(string titulo)
        {
            this.titulo = titulo;
        }

        protected void ApresentarMensagem(string mensagem, TipoMensagem tm)
        {
            switch (tm)
            {
                case TipoMensagem.Sucesso:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case TipoMensagem.Atencao:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case TipoMensagem.Erro:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        protected void ConfigurarTela(string subtitulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();

            Console.WriteLine(subtitulo);

            Console.WriteLine();
        }
    }
}
