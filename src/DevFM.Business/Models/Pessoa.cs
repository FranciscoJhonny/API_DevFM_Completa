using System;
using System.Collections.Generic;
using System.Text;

namespace DevFM.Business.Models
{
    public class Pessoa : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Imagem { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }
    }
}
