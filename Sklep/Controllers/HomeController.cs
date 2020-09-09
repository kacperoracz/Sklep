using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklep.Models;
using Sklep.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Sklep.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly IRateRepository _rateRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;

        public HomeController(UserManager<IdentityUser> userManager, IProductRepository productRepository, IRateRepository rateRepository, ICartRepository cartRepository, IOrderRepository orderRepository, IOrderStatusRepository orderStatusRepository)
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _rateRepository = rateRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
        }

        public IActionResult Index()
        {
            List<Product> productsList = _productRepository.GetAllProducts().ToList();
            ViewBag.Products = productsList;
            return View();
        }

        public IActionResult ProductDetails(int productId)
        {
            ViewBag.Product = _productRepository.GetProductById(productId);
            ViewBag.Rating = _rateRepository.GetRatingByProductId(productId);
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _productRepository.AddProduct(product);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditProduct(int productId)
        {
            return View(_productRepository.GetProductById(productId));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult EditProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _productRepository.EditProduct(product);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "user")]
        public IActionResult AddRate(Rate rate)
        {
            rate.UserId = _userManager.GetUserId(User);
            if (_rateRepository.CheckRate(rate.UserId, rate.ProductId))
            {
                Rate editedRate = _rateRepository.GetRateByUserId(rate.UserId, rate.ProductId);
                editedRate.Value = rate.Value;
                _rateRepository.EditRate(editedRate);
            }
            else
                _rateRepository.AddRate(rate);
            return RedirectToAction("ProductDetails", new { productId = rate.ProductId});
        }

        [Authorize(Roles = "user")]
        public IActionResult Cart()
        {
            List<CartVM> products = new List<CartVM>();
            float? sum = 0;
            foreach(Cart cart in _cartRepository.GetCartsByUserId(_userManager.GetUserId(User)))
            {
                Product product = _productRepository.GetProductById(cart.ProductId);
                CartVM cartVM = new CartVM();
                cartVM.Name = product.Name;
                cartVM.Price = product.Price;
                cartVM.CartId = cart.Id;
                cartVM.ProductId = product.Id;
                products.Add(cartVM);
                sum += product.Price;
            }
            ViewBag.Products = products;
            ViewBag.Sum = sum;
            return View();
        }

        [Authorize(Roles = "user")]
        public IActionResult AddToCart(int productIdF)
        {
            if (_productRepository.GetProductById(productIdF).Number > 0)
            {
                Cart cart = new Cart();
                cart.ProductId = productIdF;
                cart.UserId = _userManager.GetUserId(User);
                _cartRepository.AddProductToCart(cart);
                _productRepository.RemoveProductNumber(productIdF, 1);
                return RedirectToAction("Cart");
            }
            else
            {
                ModelState.AddModelError("", "Brak rzedmiotów w sklepie. Nie można dodać do koszyka");
                return RedirectToAction("ProductDetails", new { productId = productIdF });
            }
        }

        [Authorize(Roles = "user")]
        public IActionResult RemoveFromCart(int cartId)
        {
            Cart cart = _cartRepository.GetCartById(cartId);
            _cartRepository.RemoveProductFromCart(cart);
            _productRepository.AddProductNumber(cart.ProductId, 1);
            return RedirectToAction("Cart");
        }

        public IActionResult Order()
        {
            List<Order> orders = new List<Order>();
            if (User.IsInRole("admin"))
            {
                orders = _orderRepository.GetAllOrders().ToList();
            }
            else
            {
                orders = _orderRepository.GetOrdersByUserId(_userManager.GetUserId(User)).ToList();
            }
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach(Order order in orders)
            {
                OrderVM orderVM = new OrderVM();
                orderVM.Name = _productRepository.GetProductById(order.ProductId).Name;
                orderVM.Price = order.Price;
                orderVM.Status = _orderStatusRepository.GetOrderStatusById(order.StatusId);
                orderVM.OrderId = order.Id;
                orderVM.UserName = _userManager.Users.FirstOrDefault(u => u.Id == order.UserId).UserName;
                orderVMs.Add(orderVM);
            }
            ViewBag.Statuses = _orderStatusRepository.GetAllStatuses();
            ViewBag.Orders = orderVMs;
            return View();
        }

        [Authorize(Roles = "user")]
        public IActionResult AddOrder()
        {
            List<Order> orders = new List<Order>();
            List<Cart> carts = _cartRepository.GetCartsByUserId(_userManager.GetUserId(User)).ToList();
            foreach (Cart cart in carts)
            {
                Product product = _productRepository.GetProductById(cart.ProductId);
                Order order = new Order();
                order.ProductId = cart.ProductId;
                order.Price = product.Price;
                order.UserId = _userManager.GetUserId(User);
                order.StatusId = 1;
                orders.Add(order);
                //_orderRepository.AddOrder(order, cart.Id);
            }
            _orderRepository.AddOrder(orders, carts);
            return RedirectToAction("Order");
        }

        [Authorize(Roles = "admin")]
        public IActionResult ChangeOrderStatus(int orderId, int statusId)
        {
            if(statusId == 5)
            {
                _productRepository.AddProductNumber(_orderRepository.GetOrderById(orderId).ProductId, 1);
            }
            _orderRepository.ChangeOrderStatus(orderId, statusId);
            return RedirectToAction("Order");
        }
    }
}
