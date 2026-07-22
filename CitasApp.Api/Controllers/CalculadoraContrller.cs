using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
        // GET: api/calculadora/sumar?num1=5&num2=10
        [HttpGet("sumar")]
        public IActionResult Sumar([FromQuery] double num1, [FromQuery] double num2)
        {
            var resultado = num1 + num2;
            return Ok(new { Operacion = "Suma", Numero1 = num1, Numero2 = num2, Resultado = resultado });
        }

        // GET: api/calculadora/restar?num1=20&num2=5
        [HttpGet("restar")]
        public IActionResult Restar([FromQuery] double num1, [FromQuery] double num2)
        {
            var resultado = num1 - num2;
            return Ok(new { Operacion = "Resta", Numero1 = num1, Numero2 = num2, Resultado = resultado });
        }

        // GET: api/calculadora/multiplicar?num1=3&num2=4 multiplicación
        [HttpGet("multiplicar")]
        public IActionResult Multiplicar([FromQuery] double num1, [FromQuery] double num2)
        {
            var resultado = num1 * num2;
            return Ok(new { Operacion = "Multiplicación", Numero1 = num1, Numero2 = num2, Resultado = resultado });
        }

        // GET: api/calculadora/dividir?num1=10&num2=2
        [HttpGet("dividir")]
        public IActionResult Dividir([FromQuery] double num1, [FromQuery] double num2)
        {
            // Validación importante para que la API no explote si dividen entre 0
            if (num2 == 0)
            {
                return BadRequest(new { Error = "Error matemático: No se puede dividir entre cero." });
            }

            var resultado = num1 / num2;
            return Ok(new { Operacion = "División", Numero1 = num1, Numero2 = num2, Resultado = resultado });
        }
    }
}