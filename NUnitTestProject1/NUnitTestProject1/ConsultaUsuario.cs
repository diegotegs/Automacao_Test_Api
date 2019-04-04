public class ConsultaUsuario
{

    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string avatar { get; set; }
    public ConsultaUsuario data { get; set; }
}


public class LoginFalho
{
    public string error { get; set; }
}