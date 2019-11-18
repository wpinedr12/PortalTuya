using Newtonsoft.Json;
using PortalWeb.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PortalWeb.Servicios
{
    public class AsociarProductoServicio : IAsociarProductoServicio
    {
        private readonly HttpClient _client;

        const string BaseUrl = "https://localhost:44303/api/";

        public AsociarProductoServicio(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }
        public async Task<IEnumerable<Producto>> ObtenerProductos(int idCliente)
        {
            using (var resultado = await _client.GetAsync($"{BaseUrl}productos/cliente/{idCliente}"))
            {
                if (resultado.IsSuccessStatusCode)
                {
                    var content = await resultado.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Producto>>(content);
                }
                else
                {
                    throw new Exception(resultado.StatusCode.ToString());
                }
            }
        }
    }
}
