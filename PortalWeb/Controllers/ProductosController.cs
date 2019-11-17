using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalWeb.Models;
using PortalWeb.ViewModels;

namespace PortalWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient client;
        const string BaseUrl = "https://localhost:44303/api/";

        public ProductosController(IHttpClientFactory factory)
        {
            this.client = factory.CreateClient();
        }
        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AsociarProductoViewModel model)
        {
            var urlretorno = HttpContext.Request.Host.Value + "/Clientes/AsociarProducto?idCliente=" + model.Cliente.Id;
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                    new CrearProducto { TipoProducto = model.TipoProducto,
                                        ClienteId = model.Cliente.Id,
                                        UrlRetorno = urlretorno
                    }));
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var resultado = await client.PostAsync($"{BaseUrl}productos", byteContent))
                {
                    if (resultado.IsSuccessStatusCode)
                    {
                        //var content = await resultado.Content.ReadAsStringAsync();
                        //Producto producto = JsonConvert.DeserializeObject<Producto>(content);
                        return RedirectToAction("AsociarProducto", "Clientes", new { Id = model.Cliente.Id });
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return RedirectToAction("AsociarProducto", "Clientes", new { Id = model.Cliente.Id });
            }
        }

        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(int id, string urlReturno)
        {
            try
            {
                using (var resultado = await client.DeleteAsync($"{BaseUrl}productos/{id}"))
                {
                    if (resultado.IsSuccessStatusCode)
                    {
                        //var content = await resultado.Content.ReadAsStringAsync();
                        //Producto producto = JsonConvert.DeserializeObject<Producto>(content);
                        return Redirect(urlReturno);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
        }

    }
}