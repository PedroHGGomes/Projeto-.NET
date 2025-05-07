using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebJogos.Models;

namespace WebJogos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<TaskItem> listaJogos = new List<TaskItem>();
        private static int proximoId = 1;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
                listaJogos.Add(task);
            }
            else
            {
                TaskItem existingTask = listaJogos.FirstOrDefault(t => t.Id == task.Id);

                if (existingTask != null)
                {
                    existingTask.Status = task.Status;
                    existingTask.Titulo = task.Titulo;
                    existingTask.Descricao = task.Descricao;
                    existingTask.Pontuacao = task.Pontuacao;
                }

            }

            return RedirectToAction("Listar");
        }

        public IActionResult Listar()
        {
            return View(listaJogos);
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                var tarefa = listaJogos.FirstOrDefault(t => t.Id == id);
                if (tarefa != null)
                {
                    listaJogos.Remove(tarefa);
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
            var tarefa = listaJogos.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                return View("Cadastrar", tarefa);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult PesquisarTarefa(string termo)
        {

            var tarefasEncontradas = listaJogos.Where(t => t.Titulo.Contains(termo) || t.Descricao.Contains(termo)).ToList();

            return View("Listar", tarefasEncontradas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
