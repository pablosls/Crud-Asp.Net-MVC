using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMLINFO.SisCadastro.Web.Ui.Models;
using System.Web.Mvc;
using SMLINFO.SisCadastro.Web.Ui.Database;
using System.Xml;
using System.IO;

namespace SMLINFO.SisCadastro.Web.Ui.Controllers
{
    public class SistemaController : Controller
    {

        private readonly AlunoREP _repositorioAluno = new AlunoREP();
        public ActionResult Cadastro()
        {
            return View();
        }
   
        [HttpPost]
        public ActionResult RecebeCadastro(AlunosMOD dadosTela)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rep = _repositorioAluno.RetornaAluno(dadosTela.CodMatricula);
                    if(rep == null){
                        _repositorioAluno.CadastraAluno(dadosTela);
                        TempData.Add("sucesso", "Aluno Cadastrado com Sucesso");
                        return RedirectToAction("Cadastro");
                    }
                    else
                    {
                        TempData.Add("erro", "O Código do aluno já esta cadastrado");
                        return RedirectToAction("Cadastro");
                    }
                }
                catch (Exception)
                {
                    TempData.Add("erro", "Erro ao Cadastrar Aluno.");
                    return RedirectToAction("Cadastro");
                }
            }
            TempData.Add("erro", "Erro ao Cadastrar Aluno.");
            return RedirectToAction("Cadastro");
        }

    
        public ActionResult ExcluirAluno(int id)
        {
            try
            {
                _repositorioAluno.ExcluirAluno(id);
                TempData.Add("sucesso", "Aluno Excluido com Sucesso");
                return RedirectToAction("Cadastro");
            }
            catch (Exception)
            {
                TempData.Add("erro", "Erro ao Excluir Aluno");
                return RedirectToAction("Cadastro");
            }
        }

        [HttpGet]
        public ActionResult Editar(String id)
        {
            try
            {
                var aluno = _repositorioAluno.RetornaAluno(id);

                DateTime today = DateTime.Today;
                int age = today.Year - aluno.DataNascimento.Year;
                if (aluno.DataNascimento > today.AddYears(-age)) age--;
                aluno.Idade = age;
                return View(aluno);
            }
            catch (Exception)
            {
                TempData.Add("erro", "Aluno não encontrado.");
                return RedirectToAction("Cadastro");
            }
        }


        [HttpPost]
        public ActionResult Editar(int id, AlunosMOD dadosTela)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rep = _repositorioAluno.RetornaAluno(dadosTela.CodMatricula).Id;

                    _repositorioAluno.SalvarEdicao(rep, dadosTela);
                    TempData.Add("sucesso", dadosTela.Nome + " teve seus dados atualizados !");
                    return RedirectToAction("Cadastro");

                }
                catch (Exception)
                {

                    TempData.Add("erro", "erro ao atualizar dados do aluno " + dadosTela.Nome);
                    return RedirectToAction("Cadastro");
                }
            }
            return RedirectToAction("Cadastro");
        }


        [HttpGet]
        public ActionResult importaXml()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportXml(HttpPostedFileBase file)
        {

            try
            {
                if (file != null)
                {
                    string arq = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine("C:/AVALIACAO", arq);

                    file.SaveAs(path);

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(path);

                    XmlNodeList ndAlunos = xdoc.SelectNodes("/ALUNOS/ALUNO");
                    int countImported = 0;
                    foreach (XmlNode item in ndAlunos)
                    {
                        AlunosMOD aluno = new AlunosMOD()
                        {
                            CodMatricula = item.Attributes.GetNamedItem("COD_MATRICULA").Value,
                            DataNascimento = DateTime.Parse(item.Attributes.GetNamedItem("DATA_NASCIMENTO").Value),
                            Nome = item.Attributes.GetNamedItem("NOME").Value

                        };

                        if (_repositorioAluno.RetornaAluno(aluno.CodMatricula) == null)
                        {
                            _repositorioAluno.CadastraAluno(aluno);
                            countImported++;
                        }
                    };

                    TempData.Add("sucesso", " Xml Importado com sucesso! " + countImported + " novos alunos foram cadastrados");
                    return RedirectToAction("Cadastro");

                }

            }
            catch (Exception)
            {
                TempData.Add("erro", "Erro ao importar xml Aluno");
                return RedirectToAction("Cadastro");

            }
            TempData.Add("erro", "Erro ao importar xml Aluno");
            return RedirectToAction("Cadastro");
        }
    }
}