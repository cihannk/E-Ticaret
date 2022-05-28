using AutoMapper;
using ETicaretWebApi.Application.Operations.OrderOperations;
using ETicaretWebApi.Application.Operations.OrderOperations.Queries.GetUserOrders;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Extensions;
using ETicaretWebApi.Services.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(IPaymentService paymentService, ETicaretDbContext context, IMapper mapper )
        {
            _paymentService = paymentService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("Secret/{price}")]
        public IActionResult GetClientSecret(decimal price)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)Math.Round(price * 100, 2),
                Currency = "try",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();
            var intent = service.Create(options);

            return Ok(new { id = intent.Id, client_secret = intent.ClientSecret });
        }

        [HttpPost("Pay")]
        public IActionResult Pay([FromBody] PaymentModel model)
        {
            var service = new PaymentIntentService();
            var paymentIntent = service.Get(model.IntentId);

            if (paymentIntent.Status == "succeeded")
            {
                paymentIntent = null;
                ProcessOrder processOrderCommand = new ProcessOrder(_context, _mapper);

                processOrderCommand.Model = model;
                processOrderCommand.Handle(User.Identity.GetId());
                return Ok("Ödeme başarılı oldu");
            }
            paymentIntent = null;
            return BadRequest("Ödeme başarısız oldu");
        }
        [HttpGet("User")]
        public IActionResult GetUserOrders()
        {
            int id = User.Identity.GetId();
            GetUserOrdersQuery query = new GetUserOrdersQuery(_context);
            query.UserId = id;
            return Ok(query.Handle());
        }
    }
}
