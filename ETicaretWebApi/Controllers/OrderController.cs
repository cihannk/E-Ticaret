using AutoMapper;
using ETicaretWebApi.Application.Operations.OrderOperations;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Extensions;
using ETicaretWebApi.Services.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Pay")]
        public IActionResult Pay(PaymentModel model)
        {
            ProcessOrder processOrderCommand = new ProcessOrder(_paymentService, _context, _mapper);
            if (ModelState.IsValid)
            {
                processOrderCommand.Model = model;
                processOrderCommand.Handle(User.Identity.GetId());
                return Ok("Ödeme başarılı oldu");
            }
            return BadRequest(ModelState);
        }
    }
}
