﻿using System.Web.Mvc;
using Karol.Dominio;
using Karol.Aplicacao;

namespace Karol.UI.Web.Controllers
{
    public class LivroController : Controller
    {
       
        public ActionResult Index()
        {
            var aplicacao = new LivroAplicacao();
            var listaDeLivros = aplicacao.ListarTodos();
            return View(listaDeLivros);
        }

        public ActionResult Detalhes(int id)
        {
            var aplicacao = new LivroAplicacao();
            var livro = aplicacao.ListarPorId(id);
            if (livro == null)
                return HttpNotFound();
            return View(livro);
        }

        public ActionResult Cadastrar()
        {
            var aplicacao = new AutorAplicacao();
            
            ViewBag.ListaDeAutores = new SelectList(
                aplicacao.ListarTodos(),
                "AutorId",
                "Nome"
                );
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new LivroAplicacao();
                aplicacao.Salvar(livro);
                return RedirectToAction("Index");
            }

            return View(livro);
        }

        public ActionResult Editar(int id)
        {
            var aplicacao = new LivroAplicacao();
            var livro = aplicacao.ListarPorId(id);
            if (livro == null)
                return HttpNotFound();

            var aplicacaoAutor = new AutorAplicacao();
            ViewBag.ListaDeAutores = new SelectList(
                aplicacaoAutor.ListarTodos(),
                "AutorId",
                "Nome",
                livro.AutorId
                );

            return View(livro);
        }

        [HttpPost]
        public ActionResult Editar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new LivroAplicacao();
                aplicacao.Salvar(livro);
                return RedirectToAction("Index");
            }

            return View(livro);
        }


        public ActionResult Excluir(int id)
        {
            var aplicacao = new LivroAplicacao();
            var livro = aplicacao.ListarPorId(id);
            if (livro == null)
                return HttpNotFound();

            return View(livro);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var aplicacao = new LivroAplicacao();
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
