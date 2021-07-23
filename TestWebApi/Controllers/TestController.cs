using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TestWebApi.Models;
using TestWebApi.Models.Data;
using System.Data.Entity.Migrations;

namespace TestWebApi.Controllers
{
    public class TestController : ApiController
    {
        TestEntities ctx = new TestEntities();

        //[EnableCors(origins: "http://localhost:1117", headers: "*", methods: "*")]
        [HttpGet]
        public IEnumerable<tblCliente> ListadoCliente()
        {
            return ctx.tblCliente.Where(x => x.Estado == 1).ToArray();
        }

        [HttpPost]
        public string GrabarCliente(tblCliente cliente)
        {
            try
            {
                //tblCliente obj = Newtonsoft.Json.JsonConvert.DeserializeObject<tblCliente>(cliente);
                ctx.tblCliente.Add(cliente);
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        public IEnumerable<object> ListadoCita()
        {
            var query = from cita in ctx.tblClienteCita
                        join cliente in ctx.tblCliente on cita.IDCliente equals cliente.IDCliente
                        where cita.Estado == 1
                        select new { Cedula = cliente.Cedula, Nombre = cliente.Nombre + " " + cliente.Apellido, Fecha = cita.Fecha, Hora = cita.Hora, Estado = cita.Estado };
            return query.ToArray();
        }

        [HttpPost]
        public string GrabarCita(tblClienteCita cita)
        {
            try
            {
                if (cita.IDCliente == 0) throw new Exception("Seleccione un cliente");
                ctx.tblClienteCita.Add(cita);
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        public IEnumerable<object> ListadoAtencion()
        {
            var query = from atencion in ctx.tblAtencion
                        join cita in ctx.tblClienteCita on atencion.IDClienteCita equals cita.IDClienteCita
                        join cliente in ctx.tblCliente on cita.IDCliente equals cliente.IDCliente
                        where cita.Estado == 1 orderby cita.Fecha descending
                        select new { Cedula = cliente.Cedula, Nombre = cliente.Nombre + " " + cliente.Apellido, Fecha = cita.Fecha, Hora = cita.Hora, Descripcion = atencion.Descripcion };
            return query.ToArray();
        }

        [HttpGet]
        public IEnumerable<object> ListadoCitaById(int idcliente)
        {
            var query = from cita in ctx.tblClienteCita
                        where cita.Estado == 1 && cita.IDCliente == idcliente
                        select new { IDClienteCita = cita.IDClienteCita, Fecha = cita.Fecha, Hora = cita.Hora };
            return query.ToArray();
        }

        [HttpPost]
        public string GrabarAtencion(tblAtencion atencion)
        {
            try
            {
                if (atencion.IDClienteCita == 0) throw new Exception("Seleccione un cliente");
                ctx.tblAtencion.Add(atencion);
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpPost]
        public string EliminarCliente(int idcliente)
        {
            try
            {
                if (idcliente == 0) throw new Exception("Seleccione un cliente");
                tblCliente obj = ctx.tblCliente.Where(x => x.IDCliente == idcliente).FirstOrDefault();
                obj.Estado = 0;
                ctx.tblCliente.AddOrUpdate(obj);                

                var objcita = ctx.tblClienteCita.Where(x => x.IDCliente == idcliente).ToArray();
                foreach(tblClienteCita cita in objcita)
                {
                    cita.Estado = 0;
                    ctx.tblClienteCita.AddOrUpdate(objcita);

                    var objatencion = ctx.tblAtencion.Where(x => x.IDClienteCita == cita.IDClienteCita).ToArray();
                    foreach(tblAtencion atencion in objatencion)
                    {
                        atencion.Estado = 0;
                        ctx.tblAtencion.AddOrUpdate(atencion);
                    }
                }                
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
