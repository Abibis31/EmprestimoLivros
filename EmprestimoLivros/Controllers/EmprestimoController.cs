using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ClosedXML.Excel;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _db;
        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }


        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel? emprestimo = _db.Emprestimos.FirstOrDefault(e => e.Id == id);

            if (emprestimo == null) {
                return NotFound();
            }

            return View(emprestimo);
        }
        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel? emprestimo = _db.Emprestimos.FirstOrDefault(e => e.Id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }
        [HttpGet]
        public IActionResult Exportar() 
        {
            var dados = GetDados();

            using (XLWorkbook workBook = new XLWorkbook()) 
            {
                workBook.AddWorksheet(dados,"Dados empréstimos");
                using (MemoryStream stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimos.xlsx");
                }
            }
            
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName= "Dados empréstimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0) {
                foreach (var item in dados)
                {
                    dataTable.Rows.Add(item.Recebedor, item.Forcenedor, item.LivroEmprestado, item.dataUltimaAtualização);
                }
            }   
            return dataTable;

        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid)
            {
                emprestimos.dataUltimaAtualização = DateTime.Now;
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Empréstimo cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid)
            {
                var emprestimoDB = _db.Emprestimos.Find(emprestimos.Id);
                emprestimoDB.Forcenedor = emprestimos.Forcenedor;
                emprestimoDB.Recebedor = emprestimos.Recebedor;
                emprestimoDB.LivroEmprestado = emprestimos.LivroEmprestado;
                

                _db.Emprestimos.Update(emprestimoDB);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Empréstimo atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(emprestimos);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimos)
        {
            if(emprestimos == null || emprestimos.Id == 0)
            {
                return NotFound();
            }
            _db.Emprestimos.Remove(emprestimos);
            _db.SaveChanges();
            TempData["MensagemSucesso"] = "Empréstimo excluído com sucesso!";
            return RedirectToAction("Index");
        
            
        }
    }
}
