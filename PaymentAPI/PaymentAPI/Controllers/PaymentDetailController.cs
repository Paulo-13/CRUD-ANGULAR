using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
            {
            if (_context.PaymentDetails == null)
            {
                return NotFound();
            }
             return await _context.PaymentDetails.ToListAsync();
           }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetByIdPaymentDetail(int id)
        {
            if (_context.PaymentDetails == null)
            {
                return NotFound();
            }
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if(paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult>PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
           if ( id != paymentDetail.PaymentDetailId)
            {
                return BadRequest();
            }

           _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if(!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                
            }
            return Ok(await _context.PaymentDetails.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail( PaymentDetail paymentDetail)
        {
            if (_context.PaymentDetails == null)
            { 
                return Problem("Entity set 'PaymentDetailContext.PaymentDetails ' is null "); 
            
            }

            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(await _context.PaymentDetails.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            if (_context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(await _context.PaymentDetails.ToListAsync());

        }
        private bool PaymentDetailExists(int id)
        {
            return (_context.PaymentDetails?.Any(e => e.PaymentDetailId == id)).GetValueOrDefault();
        }

    }
    
    
}
