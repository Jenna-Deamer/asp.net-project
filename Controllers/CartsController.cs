using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newAspProject.Models;
using Newtonsoft.Json;
using WorldDominion.Models;

namespace newAspProject.Controllers
{

    public class CartsController : Controller //inherit from controller. Controller is base class
    {
        //name of the col we want to put into our session. where we will store sessionKey
        private readonly string _cartSessionKey;

        //db connection
        private readonly ApplicationDbContext _context;

        //constructor will take a db context when it gets initialize. 
        public CartsController(ApplicationDbContext context)
        {
            _cartSessionKey = "Cart";
            _context = context;
        }

        //viewAction - returns a view when it is done
        public async Task<IActionResult> Index()
        {
            //get cart (or a new one if it doesn't exist)
            var cart = GetCart();

            if (cart == null)
            {
                return NotFound();
            }
            //if it exists, fill it with products
            if (cart.CartItems.Count > 0) //check if there are ny items in cart
            {
                /*
                SELECT * FROM Products
                JOIN Departments ON Products.DepartmentId = Departments.Id
                WHERE Products.Id =  (whatever p.id is ) LIMIT 1*/
                foreach (var cartItem in cart.CartItems)
                {
                    var product = await _context.Products
                    .Include(p => p.Department)
                    //return fist found instance or default value (null)
                    .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

                    //if a user added a product to a cart, and the product got deleted from the admin side
                    //this will stop this null product from attaching to the cart. Preventing errors & issues 
                    //with transactions

                    if (product != null)
                    {
                        cartItem.Product = product;
                    }
                }

            }
            return View(cart);
        }
        /*HTTP Decorator. it tells thw action 
                what kind of http method must be used to access it.
                we are saying it must be via post method. Only way to send post from frontend is through a form
                this must be accessed through a form */
        [HttpPost] //send a post request

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var cart = GetCart();
            //check if null
            if (cart == null)
            {
                //if is null return not found
                return NotFound(); //not likely to happen, but if it does we want to ensure we work around it
            }
            //If the user adds the same item twice, we want to +quaintly. instead of adding new rows. 

            var cartItem = cart.CartItems.Find(cartItem => cartItem.ProductId == productId);  //a simple WHERE clause
            /*If it finds an item in cart with an id that matches the productId been passed it will set the cart item to be that*/
            if (cartItem != null && cartItem.Product != null)
            {
                cartItem.Quantity += quantity;
            }

            //if the product is new and isn't in the cart already. Check DB ensure product exists. 
            else
            {
                var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null)
                {
                    return NotFound(); //user is trying to add an item that does not exist
                }
                //if it does create new cart item and add it to cart
                cartItem = new CartItem { ProductId = productId, Quantity = quantity, Product = product };
                cart.CartItems.Add(cartItem);
            }
            //run save cart method
            SaveCart(cart);
            //adds it to cart. sends user to index (shows them the item(s) in their cart)
            return RedirectToAction("Index");
        }

        private Cart? GetCart()
        {
            //return it as a string cant store objects in it
            var cartJson = HttpContext.Session.GetString(_cartSessionKey);
            //access the session on the object. Call get string to access cart prop and value

            //get & return cart. if no cart is saved create an empty one.
            return cartJson == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        //save the cart to the session key
        private void SaveCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart); //convert to string
            HttpContext.Session.SetString(_cartSessionKey, cartJson);
        }
    }
}
