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
    [RoutePrefix("CartItems")]
    public class CartItemsController : ApiController
    {
        private ArtsmartContainer db = new ArtsmartContainer();

        // GET: api/CartItems
        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<CartItemDTO> GetCartItems()
        {
            return db.CartItems.ToList()
                              .Select(x => ToDTO(x));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////


        // GET: api/CartItems/5
        [Route("FindById")]
        [HttpGet]
        [ResponseType(typeof(CartItemDTO))]
        public IHttpActionResult GetCartItem(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return Ok(new CartItemDTO());
            }

            return Ok(ToDTO(cartItem));
        }

        //[Route("category/{categoryId}",
        //   Name = "FindByCategoryId")]
        //[ActionName("FindByCategoryId")]
        //[HttpGet]
        //[ResponseType(typeof(CartItemDTO))]
        //public IEnumerable<CartItemDTO> GetCartItemsByCategoryId(int CategoryId)
        //{
        //    return db.CartItems.Where(x => x.CategoryId == CategoryId)
        //        .ToList()
        //        .Select(x => ToDTO(x));
        //}


        //////////////////////////////////////////////////////////////////////////////////////////////////

        // PUT: api/CartItems/5
        [Route("UpdateById")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCartItem(int id, CartItemDTO modded)
        {
            if (!CartItemExists(id))
            {
                return NotFound();
            }

            CartItem old = (from p in db.CartItems
                            where p.Id == id
                            select p).FirstOrDefault();

            old.CartId = modded.CartId;
            old.ProductId = modded.ProductId;
            old.Quantity = modded.Quantity;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
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

        /// //////////////////////////////////////////////////////////////////////////////////

        // POST: api/CartItems
        [Route("CreateCartItem")]
        [HttpPost]
        [ResponseType(typeof(CartItemDTO))]
        public IHttpActionResult PostCartItem(CartItemDTO cartItem)
        {
            var cartcheck =
           (from Cart in db.Carts
            where Cart.Id > 0
            select Cart).FirstOrDefault();

            if (cartcheck  != null )//If cart Id exist
            {
                cartItem.CartId = cartcheck.Id;
            }
            else //if no cart.Id exist
            {
                var newCart = new Cart();
                db.Carts.Add(newCart);
                cartItem.CartId = newCart.Id;
            }

            CartItem p = ToEntity(cartItem);
            db.CartItems.Add(p);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cartItem.Id }, cartItem);
        }


        /// ///////////////////////////////////////////////////////////////////////////////////////


        // DELETE: api/CartItems/5
        [Route("DeleteById")]
        [HttpDelete]
        [ResponseType(typeof(CartItemDTO))]
        public IHttpActionResult DeleteCartItem(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            db.CartItems.Remove(cartItem);
            db.SaveChanges();

            return Ok(cartItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartItemExists(int id)
        {
            return db.CartItems.Count(e => e.Id == id) > 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////


        private CartItemDTO ToDTO(CartItem cartItem)
        {
            return new CartItemDTO()
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }

        private CartItem ToEntity(CartItemDTO cartItem)

        {
            return new CartItem()
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }
    }/////////////////////////END
}



//IEnumerable<int> cartQuery =
// from Carts in Cart
// where Id > 0
// sel cartQuery.FirstOrDefault();
