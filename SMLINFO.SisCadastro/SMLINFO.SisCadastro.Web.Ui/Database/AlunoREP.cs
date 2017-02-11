using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMLINFO.SisCadastro.Web.Ui.Models;
using SMLINFO.SisCadastro.Web.Ui.Database;

namespace SMLINFO.SisCadastro.Web.Ui.Database
{

    public class AlunoREP
    {
        // NO METODO DE CADASTRO DE PRODUTO VAMOS RECEBER AS INFORMAÇÕES DA TELA QUE A CONTROLLER NOS ENVIOU
        // E ADICIONA-LAS AO BANCO 
        public void CadastraAluno(AlunosMOD dadosAluno)
        {
            using (var conn = new DB_AVALIACAOEntities())
            {
                conn.CadAlunoPABLO.Add(new CadAlunoPABLO
                {
                    NOME = dadosAluno.Nome,
                    COD_MATRICULA = dadosAluno.CodMatricula,
                    DATA_NASCIMENTO = dadosAluno.DataNascimento
                });
                conn.SaveChanges();
            }
        }

        public List<AlunosMOD> ListarProdutos()
        {

            using (var conexao = new DB_AVALIACAOEntities())
            {
                return conexao.CadAlunoPABLO.ToList().ConvertAll(x => new AlunosMOD
                {
                    Id = x.ID_ALUNO,
                    Nome = x.NOME,
                    DataNascimento = x.DATA_NASCIMENTO,
                });
            }
        }

        public void ExcluirAluno(int id)
        {
            using (var conn = new DB_AVALIACAOEntities())
            {
                var aluno = conn.CadAlunoPABLO.Single(x => x.ID_ALUNO == id);
                conn.CadAlunoPABLO.Remove(aluno);
                conn.SaveChanges();
            }
        }

        public AlunosMOD RetornaAluno(string id)
        {
            using (var conexao = new DB_AVALIACAOEntities())
            {
                var hasItem = conexao.CadAlunoPABLO.Where(x => x.COD_MATRICULA == id).SingleOrDefault();
                if (hasItem != null)
                {
                    var aluno = conexao.CadAlunoPABLO.Single(x => x.COD_MATRICULA == id);
                    return new AlunosMOD
                    {
                        Id = aluno.ID_ALUNO,
                        CodMatricula = aluno.COD_MATRICULA,
                        Nome = aluno.NOME,
                        DataNascimento = aluno.DATA_NASCIMENTO
                    };
                }
                return null;
            }
        }

        public void SalvarEdicao(int id, AlunosMOD dadosAluno)
        {
            using (var conn = new DB_AVALIACAOEntities())
            {
                //fazemos um select no banco para retornar o produto 
                // com o id corresponde para a alteracao
                var aluno = conn.CadAlunoPABLO.Single(x => x.ID_ALUNO.Equals(id));

                // PARA ALTERAR OS VALORES NO BANCO 
                // DEPOIS DE RESGATAR O PRODUTO NOS ALTERADOS OS VALORES
                aluno.NOME = dadosAluno.Nome;
                aluno.COD_MATRICULA = dadosAluno.CodMatricula;
                aluno.ID_ALUNO = id;
                aluno.DATA_NASCIMENTO = dadosAluno.DataNascimento;

                conn.SaveChanges();

            }
        }
    }
}