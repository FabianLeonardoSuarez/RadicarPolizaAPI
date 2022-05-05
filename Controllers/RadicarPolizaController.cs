using Microsoft.AspNetCore.Mvc;
using RadicarPolizaAPI.Dtos;
using RadicarPolizaAPI.Repository;
using RadicarPolizaAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace RadicarPolizaAPI.Controllers;
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RadicarPolizaController:ControllerBase
    {
        private IPoliza _PolizaRepository;
        private PolizaContext _context;
        private IJWTManagerRepository _jwtManager;
        public RadicarPolizaController(IPoliza polizamemoryrepo, 
                                       PolizaContext context, 
                                       IJWTManagerRepository jwtManager){
            _PolizaRepository = polizamemoryrepo; 
            _context = context;
            _jwtManager = jwtManager;
        }

        [AllowAnonymous]
	    [HttpPost]
	    [Route("authenticate")]
	    public IActionResult Authenticate(Users usersdata)
	    {
	    	var token = _jwtManager.Authenticate(usersdata);

	    	if (token == null)
	    	{
	    		return Unauthorized();
	    	}

	    	return Ok(token);
	    } 

        [HttpPost]
        public ActionResult<bool> CreatePoliza(PolizaDTO poliza){
            Models.Poliza newPoliza;

            try{
                newPoliza = ConvertDTOtoPoliza(poliza);
            } catch (System.Exception) {
                return Problem(detail: "Hay un error en la data suministrada, revise por favor si toda la informacion esta completa y en el formato correcto");
            }

            var result = _PolizaRepository.CreatePoliza(newPoliza);
            if(result)
                return Ok();
            else
                return Problem(detail: "Ocurrio un error - No es posible crear polizas que no esten vigentes.");
        }

       [HttpGet("{searchkey}")]
        public ActionResult<PolizaDTO> GetPolizaByPlacaOrIdPoliza(string searchkey){
            var poliza = _PolizaRepository.GetPolizaByPlacaOrIdPoliza(searchkey);
            if(poliza == null)
                return NotFound();
            else
                return ConvertPolizatoDTO(poliza);
        }

        private PolizaDTO ConvertPolizatoDTO(Models.Poliza poliza){
            return new PolizaDTO{
                IdPoliza = poliza.IdPoliza,
                PlanPoliza = poliza.PlanPoliza,
                FechaPoliza = poliza.FechaPoliza,
                IdCliente = poliza.Cliente.IdCliente,
                NombreCliente = poliza.Cliente.NombreCliente,
                IdentificacionCliente = poliza.Cliente.IdentificacionCliente,
                TipoIdentificacionCliente = (TipoIdentificacion)(int)poliza.Cliente.TipoIdentificacionCliente,
                FechaNacimientoCliente = poliza.Cliente.FechaNacimientoCliente,
                CiudadResidenciaCliente = poliza.Cliente.CiudadResidenciaCliente,
                DireccionResidenciaCliente = poliza.Cliente.DireccionResidenciaCliente,
                IdAutomotor = poliza.Automotor.IdAutomotor,
                PlacaAutomotor = poliza.Automotor.PlacaAutomotor,
                ModeloAutomotor = poliza.Automotor.ModeloAutomotor,
                InspeccionAutomotor = poliza.Automotor.InspeccionAutomotor,
                MaximoValorCobertura = poliza.MaximoValorCobertura
            };
        }
        private Models.Poliza ConvertDTOtoPoliza(PolizaDTO poliza){
            return new Models.Poliza{
                IdPoliza = Guid.NewGuid(),
                PlanPoliza = poliza.PlanPoliza,
                FechaPoliza = poliza.FechaPoliza,
                MaximoValorCobertura = poliza.MaximoValorCobertura,
                Cliente = new Models.Cliente { IdCliente = Guid.NewGuid(),
                                               NombreCliente = poliza.NombreCliente,
                                               IdentificacionCliente = poliza.IdentificacionCliente,
                                               TipoIdentificacionCliente = (Models.TipoIdentificacion)(int)poliza.TipoIdentificacionCliente,
                                               FechaNacimientoCliente = poliza.FechaNacimientoCliente,
                                               CiudadResidenciaCliente = poliza.CiudadResidenciaCliente,
                                               DireccionResidenciaCliente = poliza.DireccionResidenciaCliente},
                Automotor = new Models.Automotor { IdAutomotor = Guid.NewGuid(),
                                                   PlacaAutomotor = poliza.PlacaAutomotor,
                                                   ModeloAutomotor = poliza.ModeloAutomotor,
                                                   InspeccionAutomotor = poliza.InspeccionAutomotor}
            };
        }

    }
