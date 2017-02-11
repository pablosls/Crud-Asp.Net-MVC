using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SMLINFO.SisCadastro.Web.Ui.Models
{
    public class AlunosMOD
    {
        
        public int Id { get; set; }

        [Display(Name = "Código do Aluno")]
        [Required(ErrorMessage = "Digite o Código do Aluno")]
        public String CodMatricula { get; set; }

        [Display(Name = "Nome do Aluno")]
        [Required(ErrorMessage = "Digite o nome do Aluno")]
        public String Nome { get; set; }

        [Display(Name = "Data de Nascimento" )]
        [Required(ErrorMessage = "Digite a data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Idade")]
        public int Idade { get; set; }
    }
}