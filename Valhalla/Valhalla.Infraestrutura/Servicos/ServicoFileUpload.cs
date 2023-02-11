using System;
using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using Valhalla.Domain.Interfaces;

namespace Valhalla.Infraestrutura.Servicos
{
    public class ServicoFileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        public ServicoFileUpload(IWebHostEnvironment env)
        {
            _environment = env;
        }

        public Task UploadAsync(IFileListEntry arquivo)
        {
            return UploadAsync(arquivo, "Uploads");
        }

        public async Task UploadAsync(IFileListEntry arquivoEntrada, string caminho)
        {
            try
            {
                var path = Path.Combine(_environment.ContentRootPath, caminho);
                var pathcompleto = Path.Combine(path, arquivoEntrada.Name);

                Directory.CreateDirectory(path);

                var ms = new MemoryStream();
                await arquivoEntrada.Data.CopyToAsync(ms);

                using (FileStream arquivo = new FileStream(pathcompleto, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(arquivo);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}

