﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeituraApp.Interfaces
{
    public interface ICadastravel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void ExcluirRegistro();

        void VisualizarRegistros();

        string ObterOpcao();
    }
}
