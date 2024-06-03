using AWSMvcSegundaPractica.Models;
using System.Net.Http.Headers;

namespace AWSMvcSegundaPractica.Services
{
    public class ServicePractica
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServicePractica(KeysModel keys)
        {
            this.UrlApi = keys.ApiEventoCategoria;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            string request = "api/practica/geteventos";
            List<Evento> data = await this.CallApiAsync<List<Evento>>(request);
            return data;
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            string request = "api/practica/getcategorias";
            List<Categoria> data = await this.CallApiAsync<List<Categoria>>(request);
            return data;
        }

        public async Task<List<Evento>> GetEventosCategoriaAsync(int idcategoria)
        {
            string request = "api/practica/geteventoscategoria/"+idcategoria;
            List<Evento> data = await this.CallApiAsync<List<Evento>>(request);
            return data;
        }
    }
}
