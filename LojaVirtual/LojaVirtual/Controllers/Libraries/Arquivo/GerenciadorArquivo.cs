using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
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

            return Path.Combine("/uploads/temp", nomeArquivo).Replace(@"\", "/");
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

        public static List<Imagem> MoverImagensProduto(List<string> caminhosTemp, int idProduto)
        {
            var CaminhoDefinitivoImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", idProduto.ToString());

            if (!Directory.Exists(CaminhoDefinitivoImagens))
                Directory.CreateDirectory(CaminhoDefinitivoImagens);

            var ImagensDefinitivo = new List<Imagem>();
            foreach (var CaminhoTemp in caminhosTemp)
            {
                var NomeArquivo = Path.GetFileName(CaminhoTemp);                
                var CaminoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);
                var CaminhoAbsolutoDefinitivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", idProduto.ToString(), NomeArquivo);

                if (File.Exists(CaminoAbsolutoTemp))
                {
                    File.Copy(CaminoAbsolutoTemp, CaminhoAbsolutoDefinitivo);

                    if (File.Exists(CaminhoAbsolutoDefinitivo))
                    {
                        File.Delete(CaminoAbsolutoTemp);

                        var caminhoRetorno = new Imagem{ Caminho = Path.Combine("/uploads", idProduto.ToString(), NomeArquivo).Replace(@"\", "/"), Id = idProduto};
                        ImagensDefinitivo.Add(caminhoRetorno);
                    }
                }
            }

            return ImagensDefinitivo;
        }

    }
}
