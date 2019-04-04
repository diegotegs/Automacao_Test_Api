using RestSharp;
using System;
using System.IO;





public class RestApiHelper<T>
{
    public RestClient _cliente;
    public RestRequest _request;
    public string _urlBase = "https://reqres.in/";



    public RestClient SetUrl(string resourceUrl)
    {
        var url = Path.Combine(_urlBase, resourceUrl);
        var _cliente = new RestClient(url);
        return _cliente;
    }

    public RestRequest CriarRequisicaoPost(String jsonString)
    {
        _request = new RestRequest(Method.POST);
        _request.AddHeader("Accept", "application/json");
        _request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
        return _request;
    }

    public RestRequest CriarRequisicaoPut(String jsonString)
    {
        _request = new RestRequest(Method.PUT);
        _request.AddHeader("Accept", "application/json");
        _request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
        return _request;
    }

    public IRestRequest CriarRequisicaoGet()
    {
        _request = new RestRequest(Method.GET);
        _request.AddHeader("Accept", "application/json");
        return _request;
    }
    public IRestRequest CriarRequisicaoDelete()
    {
        _request = new RestRequest(Method.DELETE);
        _request.AddHeader("Accept", "application/json");
        return _request;
    }



    public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
    {
        return restClient.Execute(restRequest);

    }

    public DTO GetContent<DTO>(IRestResponse responde)
    {
        var content = responde.Content;
        DTO deseiralizeObject = SimpleJson.DeserializeObject<DTO>(content);
        return deseiralizeObject;

    }


}