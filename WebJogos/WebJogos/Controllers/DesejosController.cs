using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebJogos.Models;

namespace WebJogos.Controllers
{
    public class DesejosController : Controller
    {
        public static List<TaskItem> listaDesejos = new List<TaskItem>();
        public static int proximoId = 1;

        public IActionResult Index()
        {
            return View("Listar", listaDesejos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (task.Id == 0)
            {
                task.Id = proximoId++;
                listaDesejos.Add(task);
            }
            else
            {
                TaskItem existingTask = listaDesejos.FirstOrDefault(t => t.Id == task.Id);

                if (existingTask != null)
                {
                    existingTask.Titulo = task.Titulo;
                    existingTask.Descricao = task.Descricao;
                    existingTask.Valor = task.Valor;
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Listar()
        {
            return View(listaDesejos);
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                var tarefa = listaDesejos.FirstOrDefault(t => t.Id == id);
                if (tarefa != null)
                {
                    listaDesejos.Remove(tarefa);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ObterTarefa(int id)
        {
            var tarefa = listaDesejos.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                return View("Cadastrar", tarefa);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult PesquisarJogo(string termo)
        {

            var tarefasEncontradas = listaDesejos.Where(t => t.Titulo.Contains(termo) || t.Descricao.Contains(termo)).ToList();

            return View("Listar", tarefasEncontradas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

