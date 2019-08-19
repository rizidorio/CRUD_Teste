using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Teste.Models;
using System.Drawing;
using System.Net;
using System.IO;

namespace CRUD_Teste.Controllers
{
    public class ProdutosController : Controller
    {
        ProdutoDbContext db;

        public ProdutosController()
        {
            //instaância do contexto
            db = new ProdutoDbContext();
        }

        // GET: Produtos
        public ActionResult Index()
        {
            //retorna todos os produtos e repassa para a view Index
            var produtos = db.produtos.ToList();
            return View(produtos);
        }

        //Método para incluir um novo produto
        public ActionResult Create()
        {
            ViewBag.Categorias = db.categorias;
            var model = new ProdutoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel model)
        {
            //formatos de imagens permitidas
            var imageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            //se o usuário não carregar uma imagem apresenta uma mensagens que a imagem é obrigatória
            if(model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Este campo é obrigatório.");
            }
            //se a imagem não for dos formatos aceitos apresenta uma mensagem com os formatos aceitos 
            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma imagem GIF, JPG ou PNG.");
            }

            //se os dados passados pelo usuário forem válidos, instancia um novo produto e salva os dados
            if (ModelState.IsValid)
            {
                var produto = new Produto();
                produto.Nome = model.Nome;
                produto.Preco = model.Preco;
                produto.Descricao = model.Descricao;
                produto.CategoriaId = model.CategoriaId;

                //Salvar a imagem para a pasta e pega o caminho
                var imagemNome = string.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now); //formata o nome da imagem pela data e hora do sistema
                var extensao = Path.GetExtension(model.ImageUpload.FileName).ToLower(); //pega a extensao da imagem selecionada

                using(var img = Image.FromStream(model.ImageUpload.InputStream))
                {
                    produto.Imagem = string.Format("/ProdutosImagens/{0}{1}", imagemNome, extensao);
                    
                    //Salva a imagem
                    SalvarNaPasta(img, produto.Imagem);
                }

                //se não ocorrer erro salva os dados no banco e retonar para a página Index
                db.produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //se ocorrer um erro retorna para página de cadastro
            ViewBag.Categorias = db.categorias;
            return View(model);
        }

        //GET:Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.produtos.Find(id);
            
            if(produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categorias = db.categorias;
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Descricao,Preco,CategoriaId")] Produto model)
        {
            if (ModelState.IsValid)
            {
                var produto = db.produtos.Find(model.ProdutoId);
                if(produto == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Preco = model.Preco;
                produto.CategoriaId = model.CategoriaId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = db.categorias;
            return View(model);
        }

        //GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.produtos.Find(id);
            if(produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categoria = db.categorias.Find(produto.CategoriaId).CategoriaNome;
            return View(produto);
        }

        //POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.produtos.Find(id);
            db.produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET:Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.produtos.Find(id);
            if(produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categoria = db.categorias.Find(produto.CategoriaId).CategoriaNome;
            return View(produto);
        }

        private void SalvarNaPasta(Image img, string caminho)
        {
            using(Image novaImagem = new Bitmap(img))
            {
                novaImagem.Save(Server.MapPath(caminho), img.RawFormat);
            }
        }
    }
}