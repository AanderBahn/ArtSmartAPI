using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ArtSmartAPI.Data;
using ArtSmartAPI.Models;

namespace ArtSmartAPI.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : ApiController
    {
        private ArtsmartContainer db = new ArtsmartContainer();

        // GET: api/Products
        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<ProductDTO> GetProducts()
        {
            return db.Products.ToList()
                              .Select(x => ToDTO(x));
        }

        [Route("FindByName")]
        [HttpGet]
        public IEnumerable<ProductDTO> GetProductsByName(string name)

        {
            return db.Products.Where(x => x.Name == name)
                                           .ToList().Select(x => ToDTO(x));
        }
        
        //////////////////////////////////////////////////////////////////////////////////
      
        // GET: api/Products/5
        [Route("FindById")]
        [HttpGet]
        [ResponseType(typeof(ProductDTO))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return Ok(new ProductDTO());
            }

            return Ok(ToDTO(product));
        }

        [Route("category/{categoryId}",
           Name = "FindByCategoryId")]
        [ActionName("FindByCategoryId")]
        [HttpGet]
        [ResponseType(typeof(ProductDTO))]
        public IEnumerable<ProductDTO> GetProductsByCategoryId(int CategoryId)
        {
            return db.Products.Where(x => x.CategoryId == CategoryId)
                .ToList()
                .Select(x => ToDTO(x));
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
       
        // PUT: api/Products/5
        [Route("UpdateById")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductDTO modded)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }

            Product old = (from p in db.Products
                           where p.Id == id
                           select p).FirstOrDefault();

            
            old.Name = modded.Name;
            old.Description = modded.Description;
            old.Price = modded.Price;
            old.Brand = modded.Brand;
            old.WorkingArea = modded.WorkingArea;
            old.ResolutionId = modded.ResolutionId;
            old.Sensitivity = modded.Sensitivity;
            old.Material = modded.Material;
            old.Version = modded.Version;
            old.ImageUrl = modded.ImageUrl;
            old.CategoryId = modded.CategoryId;
            old.ProductSpecId = modded.ProductSpecId;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// //////////////////////////////////////////////////////////////////////////////

        // POST: api/Products
        [Route("CreateProduct")]
        [HttpPost]
        [ResponseType(typeof(ProductDTO))]
        public IHttpActionResult PostProduct(ProductDTO product)
        {
            Product p = ToEntity(product);

            db.Products.Add(p);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        // DELETE: api/Products/5
        [Route("DeleteById")]
        [HttpDelete]
        [ResponseType(typeof(ProductDTO))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private ProductDTO ToDTO(Product product)
        {
            return new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                WorkingArea = product.WorkingArea,
                ResolutionId = product.ResolutionId,
                Sensitivity = product.Sensitivity,
                Material = product.Material,
                Version = product.Version,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                ProductSpecId = product.ProductSpecId,
                ResoultionMeasurement = product.Resolution.Measurement,
                ResolutionSize = product.Resolution.Size
            };
        }

        private Product ToEntity(ProductDTO product)

        {
            return new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                WorkingArea = product.WorkingArea,
                ResolutionId = product.ResolutionId,
                Sensitivity = product.Sensitivity,
                Material = product.Material,
                Version = product.Version,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                ProductSpecId = product.ProductSpecId, 
            };
        }
    }/////////////////////////END
}