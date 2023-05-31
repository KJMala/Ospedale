using Ospedale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ospedale.Controllers
{
    public class ValuesController : ApiController
    {
        ModelDbContext dbContext = new ModelDbContext();
        // GET api/values
        public IEnumerable<Visita> Get()
        {
            return dbContext.Visita.ToList();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        // PRIMA CHIAMATA API
        [HttpGet]
        [Route("api/Visite/ElencoVisitePazienteByCodiceFiscale/{CodiceFiscale}")]
        public IEnumerable<VisitaJson> ListaVisitePazienteByCodiceFiscale(string CodiceFiscale)
        {
            List<VisitaJson> visitaJson = new List<VisitaJson>();
            List<Visita> visite = dbContext.Visita.Where(x => x.Paziente.CodiceFiscale == CodiceFiscale).ToList();
            foreach (Visita v in visite)
            {
                VisitaJson vjson = new VisitaJson();
                vjson.IdVisita = v.IdVisita;
                vjson.IdMedico = v.IdMedico;
                vjson.IdPaziente = v.IdPaziente;
                vjson.IdReparto = v.IdReparto;
                vjson.NomeVisita = v.NomeVisita;
                vjson.FrequenzaCardiaca = v.FrequenzaCardiaca;
                vjson.StatoPaziente = v.StatoPaziente;
                vjson.PressioneMassima = v.PressioneMassima;
                vjson.PressioneMinima = v.PressioneMinima;
                vjson.IdFarmaco = v.IdFarmaco;
                vjson.Temperatura = v.Temperatura;
                vjson.NomeReparto = v.Reparto.NomeReparto;
                vjson.CognomeMedico = v.Medico.Cognome;
                vjson.Posologia = v.Posologia;
                

                visitaJson.Add(vjson);
            }

            return (visitaJson);
        }


        // SECONDA CHIAMATA API
        [HttpGet]
        [Route("api/Visite/ElencoVisiteByReparto/{NomeReparto}")]
        public IEnumerable<RepartoJson> ListaVisitePazienteByReparto(string NomeReparto)
        {
            
            List<RepartoJson> repartoJson = new List<RepartoJson>();

            List<Visita> visite = dbContext.Visita.Where(x => x.Reparto.NomeReparto == NomeReparto).OrderBy(x => x.Reparto.NomeReparto).ToList();

            RepartoJson vjson = new RepartoJson();
            vjson.NumeroRicoveri = 0;

            foreach (Visita v in visite)
            {

                foreach (Reparto r in dbContext.Reparto.Where(x => x.NomeReparto == v.Reparto.NomeReparto).OrderBy(x => x.NomeReparto).ToList())
                {
                    vjson.NomeReparto = r.NomeReparto;
                    vjson.NumeroRicoveri += 1;
                }
                
            }
            repartoJson.Add(vjson);
            return (repartoJson);
        }
    }
}
