using Microsoft.AspNetCore.Http;
using System.IO;

namespace LojaVirtual.Controllers.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            var nomeArquivo = Path.GetFileName(file.FileName);
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", nomeArquivo);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/uploads/temp", nomeArquivo);
        }

        public static bool ExcluirImagemProduto(string caminho)
        {
            var caminhoDeletar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            if (File.Exists(caminhoDeletar))
            {
                File.Delete(caminhoDeletar);
                return true;
            }
            
            return false;
        }

    }
}
