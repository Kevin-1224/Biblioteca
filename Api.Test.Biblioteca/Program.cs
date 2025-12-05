using Biblioteca.Modelos;

namespace Api.Test.Biblioteca
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var rutaBibliotecaFisicas = "api/BibliotecaFisicas";
            httpClient.BaseAddress = new Uri("https://localhost:7243/");
            var response = httpClient.GetAsync("api/BibliotecaFisicas").Result;
            var json = response.Content.ReadAsStringAsync().Result;

            var bibliotecafisicas = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<BibliotecaFisica>>>(json);
            var nuevaBibliotecaFisica = new BibliotecaFisica
            {
                Id = 0,
                Nombre = "Biblioteca Central",
                Direccion = "Calle Principal 123"
            };
            var bibliotecafisicaJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevaBibliotecaFisica);
            var content = new StringContent(bibliotecafisicaJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(rutaBibliotecaFisicas, content).Result;
            var bibliotecaFisicaCreada=Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<BibliotecaFisica>>(json);
            //Actualizar datos

            bibliotecaFisicaCreada.Data.Nombre= "Biblioteca Actualizada";
            bibliotecafisicaJson = Newtonsoft.Json.JsonConvert.SerializeObject(bibliotecaFisicaCreada.Data);
            content = new StringContent(bibliotecafisicaJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{rutaBibliotecaFisicas}/{bibliotecaFisicaCreada.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var bibliotecaFisicaActualizada=Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<BibliotecaFisica>>(json);
            //Eliminar biblioteca
            response = httpClient.DeleteAsync($"{rutaBibliotecaFisicas}/{bibliotecaFisicaActualizada.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var bibliotecaFisicaEliminada=Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<BibliotecaFisica>>(json);

            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
