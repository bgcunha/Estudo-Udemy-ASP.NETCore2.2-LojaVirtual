using LojaVirtual.Controllers.Libraries.Arquivo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ImagemController : Controller
    {
        [HttpPost]
        public IActionResult Armazenar(IFormFile file)
        {
            var caminhoArquivoTemp = GerenciadorArquivo.CadastrarImagemProduto(file);

            if (caminhoArquivoTemp.Length > 0)
            {
                return Ok(new { caminho = caminhoArquivoTemp });
            }
            else
            {
                return new StatusCodeResult(500);
            }            
        }

        public IActionResult Deletar(string caminho)
        {
            if (GerenciadorArquivo.ExcluirImagemProduto(caminho))
                return Ok();

            return BadRequest();
        }
    }
}
