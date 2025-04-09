using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.Attachments
{
	public class AttachmentServices : IAttachmentServices
	{
		public readonly List<string> AllowedExtensions = new List<string>()
		{
			".jpg",
			".jpeg",
			".png"
		};

		public const int MaxFileSize = 2_097_152;

		/*
			UploadImage should do the following:
			1. Store the file on the server ex. Presentation Layer; wwwroot/files/images
		    2. Return the path of the file to be stored in the database
		*/
		public string UploadImage(IFormFile File, string FolderName)
		{
			var FileExtension = Path.GetExtension(File.FileName);
			if(!AllowedExtensions.Contains(FileExtension))
			{
				throw new Exception("File type is not allowed");
			}

			if (File.Length > MaxFileSize)
			{
				throw new Exception("File size is too large");
			}

			var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", FolderName);

			if(!Directory.Exists(FolderPath))
			{
				Directory.CreateDirectory(FolderPath);
			}

			var FileName = $"{Guid.NewGuid()}_{File.FileName}";

			var FilePath = Path.Combine(FolderPath, FileName);

			using var FileStream = new FileStream(FilePath, FileMode.Create);
			File.CopyTo(FileStream);

			return FileName;
		}

		public bool DeleteImage(string FilePath)
		{
			if(File.Exists(FilePath))
			{
				File.Delete(FilePath);
				return true;
			}

			return false;
		}
	}
}
