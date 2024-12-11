using BookStoreAPI.DTOs.OrderDTO;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        UnitOFWork _unit;
        public OrderController(UnitOFWork _unit)
        {
            this._unit = _unit;
        }
        [HttpPost]
        public IActionResult add(AddOrderDTO _order)
        {
            Order order = new Order()
            {
                cust_id = _order.cust_id,
                status = "create",
               
                orderDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),

            };
            _unit.OrdersRepositry.add(order);
            _unit.savechanges();
            decimal totalPrice=0;
            foreach (var item in _order.books)
            {
                Book book = _unit.BooksRepositry.selectbyid(item.book_id);
                totalPrice = totalPrice + ( book.price * item.quantity);
                OrderDetails _details = new OrderDetails()
                {
                    order_id = order.id,
                    book_id = item.book_id,
                    quantity = item.quantity,
                    unitprice = book.price,
                };
                if (book.srock > _details.quantity)
                {
                    _unit.OrderDetailsRepositry.add(_details);

                    book.srock -= item.quantity;
                    _unit.BooksRepositry.update(book);

                }
                else
                {
                    _unit.OrdersRepositry.delete(order.id);
                    return BadRequest("Invalid Quantity");
                }
            }
            order.totalprice = totalPrice;
            _unit.OrdersRepositry.update(order);
            _unit.savechanges();
            return Ok();

        }
        //didnt work
        [HttpDelete]
        public IActionResult delete(int id)
        {
            var order_id= _unit.OrdersRepositry.selectbyid(id);
            var order_idD =_unit.OrderDetailsRepositry.selectbyid(order_id.id);
            _unit.OrdersRepositry.delete(order_id.id);
            _unit.OrderDetailsRepositry.deleteOD(order_idD.order_id,order_idD.book_id);
            _unit.savechanges();
            return Ok();
        }

        }
    }
