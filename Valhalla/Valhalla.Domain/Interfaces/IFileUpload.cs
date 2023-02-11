using System;
using BlazorInputFile;

namespace Valhalla.Domain.Interfaces
{
	public interface IFileUpload
	{
        Task UploadAsync(IFileListEntry arquivo, string caminho);
        Task UploadAsync(IFileListEntry arquivo);
    }
}

