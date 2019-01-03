using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using Library.Models.Catalog;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
		private ILibraryAsset _assets;

		public CatalogController(ILibraryAsset asset)
		{
			_assets = asset;

		}

		public IActionResult Index()
		{
			var assetModels = _assets.GetAll();

			var listingResult = assetModels.
				Select(result => new AssetIndexListingModel
				{
					Id = result.Id,
					ImageUrl = result.ImageUrl,
					AuthorOrDirector = _assets.GetDeweyIndex(result.Id),
					Title = result.Title,
					Type = _assets.GetType(result.Id)
				});


			var model = new AssetIndexModel()
			{
				Assets = listingResult
			};

			return View(model);
		}
    }
}
