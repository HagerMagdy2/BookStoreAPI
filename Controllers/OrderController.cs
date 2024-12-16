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
           // var order_idD =_unit.OrderDetailsRepositry.selectbyid(order_id.id);
            _unit.OrdersRepositry.delete(order_id.id);
           // _unit.OrderDetailsRepositry.deleteOD(order_idD.order_id,order_idD.book_id);
            _unit.savechanges();
            return Ok();
        }
        [HttpPut]
        //public IActionResult edit(int id,AddOrderDTO _orderdto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Order order =new Order()
        //        {
        //            id = id,
        //            cust_id = _orderdto.cust_id,
        //            status="updated", 
        //        };
        //        foreach (var item in _orderdto.books)
        //        {
        //            Book book = _unit.BooksRepositry.selectbyid(item.book_id);
        //            OrderDetails _details = new OrderDetails()
        //            {
        //                order_id = id,
        //                book_id = item.book_id,
        //                quantity = item.quantity,
        //                unitprice = book.price,
        //            };
        //            _unit.OrderDetailsRepositry.update(_details);
        //            _unit.savechanges() ;
        //        }
        //        _unit.OrdersRepositry.update(order);

        //        _unit.savechanges();
        //        return NoContent();

        //    }
        //    return BadRequest(ModelState);
        //}
        [HttpPut]
        public IActionResult EditOrder(int orderId, EditOrderDTO _order)
        {
            // First, retrieve the existing order
            Order existingOrder = _unit.OrdersRepositry.selectbyid(orderId);
            if (existingOrder == null)
            {
                return NotFound("Order not found");
            }

            // Check if order is in a state that can be edited
            if (existingOrder.status != "create")
            {
                return BadRequest("Order cannot be modified at this stage");
            }

            // Remove existing order details
            var existingOrderDetails = _unit.OrderDetailsRepositry
                .GetAll()
                .Where(od => od.order_id == orderId)
                .ToList();

            // Restore previous book stocks
            foreach (var existingDetail in existingOrderDetails)
            {
                var book = _unit.BooksRepositry.selectbyid(existingDetail.book_id);
                book.srock += existingDetail.quantity;
                _unit.BooksRepositry.update(book);
                _unit.OrderDetailsRepositry.delete(existingDetail.id);
            }

            // Recalculate total price
            decimal totalPrice = 0;
            foreach (var item in _order.books)
            {
                Book book = _unit.BooksRepositry.selectbyid(item.book_id);
                totalPrice += book.price * item.quantity;

                // Create new order details
                OrderDetails _details = new OrderDetails()
                {
                    order_id = existingOrder.id,
                    book_id = item.book_id,
                    quantity = item.quantity,
                    unitprice = book.price,
                };

                // Check stock availability
                if (book.srock >= _details.quantity)
                {
                    _unit.OrderDetailsRepositry.add(_details);
                    book.srock -= item.quantity;
                    _unit.BooksRepositry.update(book);
                }
                else
                {
                    // Rollback stock changes
                    //foreach (var existingDetail in )
                    //{
                    //    var rollbackBook = _unit.BooksRepositry.selectbyid(existingDetail.book_id);
                    //    rollbackBook.srock += existingDetail.quantity;
                    //    _unit.BooksRepositry.update(rollbackBook);
                    //    _unit.OrderDetailsRepositry.delete(existingDetail.id);
                    //}
                    return BadRequest("Invalid Quantity");
                }
            }

            // Update order details
            existingOrder.totalprice = totalPrice;
            existingOrder.orderDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            _unit.OrdersRepositry.update(existingOrder);
            _unit.savechanges();

            return Ok();
        }
    }
    }
