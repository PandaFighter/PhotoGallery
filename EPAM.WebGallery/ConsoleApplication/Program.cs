using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Services;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var service = new ContentService();
			var albums = service.GetAllAlbum();
			foreach (var album in albums)
			{
				Console.WriteLine(album.Name);
			}
			Console.ReadKey();
		}
	}
}
