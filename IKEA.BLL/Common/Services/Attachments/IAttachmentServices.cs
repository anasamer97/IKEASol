using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.Attachments
{
	// This interface defines the contract for attachment services, specifically for uploading and deleting images.
	public interface IAttachmentServices
    {
        public string UploadImage(IFormFile File, string FolderName);

        public bool DeleteImage(string FilePath);
    }
}
