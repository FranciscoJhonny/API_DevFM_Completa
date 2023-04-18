﻿using DevFM.Business.Notificacoes;
using System.Collections.Generic;

namespace DevFM.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
