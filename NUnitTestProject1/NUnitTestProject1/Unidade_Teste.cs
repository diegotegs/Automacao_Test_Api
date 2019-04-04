using System;
using System.Net;
using NUnit.Framework;
using RestSharp;
using Assert = NUnit.Framework.Assert;
using Newtonsoft.Json.Linq;

[TestFixture]
public class Test1
{

    [Test]
    public void CriarUsuario1()
    {
        string jsonString = @"{
                          ""name"": ""morpheus"",
                          ""job"": ""leader""
                                               }";

        RestApiHelper<CriarUsuario> restapi = new RestApiHelper<CriarUsuario>();
        var restUrl = restapi.SetUrl("api/users");
        var restRequest = restapi.CriarRequisicaoPost(jsonString);
        var response = restapi.GetResponse(restUrl, restRequest);
        CriarUsuario content = restapi.GetContent<CriarUsuario>(response);
        Assert.AreEqual(content.name, "morpheus");
        Assert.AreEqual(content.job, "leader");
    }


    [Test]
    public void UpdateUsuario()
    {
        // escrevendo uma string no formato de Json
        string paramentro = @"{
                          ""name"": ""Jarbas""
                                               }";
        //link para requisição do cliente
        var client = new RestClient("https://reqres.in/");
        //request com o endereço do metado e qual utilizar
        var request = new RestRequest("api/users/{id}", Method.PUT);
        //adicionando onde o id a ser feito a alteração
        request.AddUrlSegment("id", 2);
        //escrevendo o parametro acima no corpo da requisição
        request.AddJsonBody(paramentro);
        //executando o o endereço
        var conteudo = client.Execute(request);
        //passando o conteudo retornado para um objeto
        UpDateUser up = SimpleJson.DeserializeObject<UpDateUser>(conteudo.Content);
        //fazendo as comparações para do objeto retornado com o que era esperado
        Assert.AreEqual("Jarbas", up.name);
        //comparando se os codigos recebido era esperado.
        Assert.AreEqual(HttpStatusCode.OK, conteudo.StatusCode);

    }

    [Test]
    public void UpdateUsuarioPatch()
    {
        
        string paramentro = @"{
                           ""name"": ""Jarbas"",
                          ""job"": ""zion resident""
                                               }";
        
        var client = new RestClient("https://reqres.in/");        
        var request = new RestRequest("api/users/{id}", Method.PATCH);        
        request.AddUrlSegment("id", 2);        
        request.AddJsonBody(paramentro);        
        var conteudo = client.Execute(request);        
        UpDateUser up = SimpleJson.DeserializeObject<UpDateUser>(conteudo.Content);       
        Assert.AreEqual("Jarbas", up.name);
        Assert.AreEqual("zion resident", up.job);
        
        Assert.AreEqual(HttpStatusCode.OK, conteudo.StatusCode);

    }

    [Test]
    public void ConsultarPorId()
    {

        var cliente = new RestClient("https://reqres.in/");
        var request = new RestRequest("api/users/{id}", Method.GET);
        request.AddHeader("Accept", "application/json");
        request.AddUrlSegment("id", 2);
        var conteudo = cliente.Execute(request);
        ConsultaUsuario usuario = SimpleJson.DeserializeObject<ConsultaUsuario>(conteudo.Content);
        Assert.AreEqual("Janet", usuario.data.first_name);
        Assert.AreEqual("Weaver", usuario.data.last_name);
    }
    [Test]
    public void DeleteUsuario()
    {
        var cliente = new RestClient("https://reqres.in/");
        var request = new RestRequest("/api/users/{id}", Method.DELETE);
        request.AddUrlSegment("id", 1);
        var content = cliente.Execute(request);
        Assert.AreEqual(HttpStatusCode.NoContent, content.StatusCode);

    }
    [Test]
    public void ListarUsuario()
    {
        var client = new RestClient("https://reqres.in/");
        var request = new RestRequest("/api/unknown", Method.GET);
        var conteudo = client.Execute(request);
        ListaUsuario json = SimpleJson.DeserializeObject<ListaUsuario>(conteudo.Content);
        Assert.AreEqual("cerulean", json.data[0].name);
    }
    [Test]
    public void LoginFalho()
    {

        string json = @"{
    ""email"": ""peter @klaven""
}";

        var cliente = new RestClient("https://reqres.in/");
        var request = new RestRequest("api/login", Method.POST);
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        var conteudo = cliente.Execute(request);
        JObject obs = JObject.Parse(conteudo.Content);
        Assert.That(obs["error"].ToString(), Is.EqualTo("Missing password"), "Algo Relevante");

    }
    [Test]
    public void SingNotFound()
    {
        var client = new RestClient("https://reqres.in/");
        var request = new RestRequest("/api/unknown/{id}", Method.GET);
        request.AddUrlSegment("id", 23);
        var response = client.Execute(request);
        JObject json = JObject.Parse(response.Content);
        Console.WriteLine(json);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        Assert.AreEqual("{}", json.ToString());


    }



}