using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using WebAppDemo3.Data;
using WebAppDemo3.Models;

namespace WebAppDemo3.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Shop
        public CartsController(ApplicationDbContext context)

        {
            this._context = context;
        }
        public ActionResult Index()
        {

            var _book = getAllBook();
            ViewBag.book = _book;
            return View();
        }

        //GET ALL BOOK
        public List<Book> getAllBook()
        {
            return _context.Book.ToList();
        }

        //GET DETAIL BOOK
        public Book getDetailBook(int id)
        {
            var book = _context.Book.Find(id);
            return book;
        }
      //  [Authorize(Roles = "User")]
        //ADD CART
        public IActionResult addCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart == null)
            {
                var book = getDetailBook(id);
                List<Cart> listCart = new List<Cart>()
                {
                   new Cart
                   {
                       Book = book,
                       Quantity = 1
                   }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Book.BookId == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Book = getDetailBook(id),
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                //  return Json(cart2);
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult updateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Book.BookId == id)
                        {
                            dataCart[i].Quantity = quantity;
                        }
                    }
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                var cart2 = HttpContext.Session.GetString("cart");
                return Ok(quantity);
            }
            return BadRequest();
            //var cart = HttpContext.Session.GetString("cart");
            //if (cart != null)
            //{
            //    List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
            //    if (quantity > 0)
            //    {
            //        for (int i = 0; i < dataCart.Count; i++)
            //        {
            //            if (dataCart[i].Book.BookId == id)
            //            {
            //                dataCart[i].Quantity = quantity;
            //            }
            //        }

            //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));

            //        return RedirectToAction("ListCart");
            //    }
            //}
            //return BadRequest();
        }
    
        [Authorize(Roles = "User")]
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Book.BookId == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }
        //[Authorize(Roles = "User")]
        public IActionResult checkOut()
        {
            //xử lí khi đặt hàng
            var cart = HttpContext.Session.GetString("cart");
            //double total = 0;
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                //foreach (var item in dataCart)
                //{
                //    //double paymemt = item.Quantity * item.Book.Price;
                //    //total += paymemt;

                //}
                for (int i = 0; i < dataCart.Count; i++)
                {
                    var order = new Order()
                    {
                        BookId = dataCart[i].Book.BookId,
                        BookTitle = dataCart[i].Book.Title,
                        Quantity = dataCart[i].Quantity,
                        OrderTime = DateTime.Now,
                        TotalPrice = dataCart[i].Book.Price * dataCart[i].Quantity,
                    };
                    _context.Order.AddAsync(order);
                    _context.SaveChanges();
                    TempData["Message"] = "Congrats, your order is completed!";
                }
                HttpContext.Session.Remove("cart");

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
