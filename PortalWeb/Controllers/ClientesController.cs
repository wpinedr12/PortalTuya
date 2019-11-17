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
using PortalWeb.Servicios;
using PortalWeb.ViewModels;

namespace PortalWeb.Controllers
{
    public class ClientesController : Controller
    {

        private readonly HttpClient client;
        private readonly IAsociarProductoServicio asociarProductoServicio;
        const string BaseUrl = "https://localhost:44371/api/";
        public ClientesController(IHttpClientFactory factory, IAsociarProductoServicio asociarProductoServicio)
        {
            this.client = factory.CreateClient();
            this.asociarProductoServicio = asociarProductoServicio;
        }
        // GET: Clientes
        public async Task<ActionResult> Index()
        {
            using (var resultado = await client.GetAsync($"{BaseUrl}clientes"))
            {
                if (resultado.IsSuccessStatusCode)
                {
                    var content = await resultado.Content.ReadAsStringAsync();
                    IEnumerable<Cliente> clientes = JsonConvert.DeserializeObject<IEnumerable<Cliente>>(content);
                    return View(clientes);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        public async Task<ActionResult> AsociarProducto(int idCliente)
        {
            var productos = await asociarProductoServicio.ObtenerProductos(idCliente);

            using (var resultado = await client.GetAsync($"{BaseUrl}clientes/{idCliente}"))
            {
                if (resultado.IsSuccessStatusCode)
                {
                    var content = await resultado.Content.ReadAsStringAsync();
                    Cliente cliente = JsonConvert.DeserializeObject<Cliente>(content);

                    var viewModel = new AsociarProductoViewModel
                    {
                        Cliente = cliente,
                        Productos = productos
                    };

                    return View("Productos", viewModel);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente model)
        {
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var resultado = await client.PostAsync($"{BaseUrl}clientes", byteContent))
                {
                    if (resultado.IsSuccessStatusCode)
                    {
                        //var content = await resultado.Content.ReadAsStringAsync();
                        //Producto producto = JsonConvert.DeserializeObject<Producto>(content);
                        return RedirectToAction(nameof(Index));
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

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var resultado = await client.GetAsync($"{BaseUrl}clientes/{id}"))
            {
                if (resultado.IsSuccessStatusCode)
                {
                    var content = await resultado.Content.ReadAsStringAsync();
                    Cliente cliente = JsonConvert.DeserializeObject<Cliente>(content);
                    return View(cliente);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Cliente model)
        {
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var resultado = await client.PutAsync($"{BaseUrl}clientes/{id}", byteContent))
                {
                    if (resultado.IsSuccessStatusCode)
                    {
                        //var content = await resultado.Content.ReadAsStringAsync();
                        //Producto producto = JsonConvert.DeserializeObject<Producto>(content);
                        return RedirectToAction(nameof(Index));
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

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var resultado = await client.DeleteAsync($"{BaseUrl}clientes/{id}"))
                {
                    if (resultado.IsSuccessStatusCode)
                    {
                        //var content = await resultado.Content.ReadAsStringAsync();
                        //Producto producto = JsonConvert.DeserializeObject<Producto>(content);
                        return RedirectToAction(nameof(Index));
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