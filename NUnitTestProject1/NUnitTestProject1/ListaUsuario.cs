using System.Collections.Generic;



class ListaUsuario
{

    public int id { get; set; }
    public string name { get; set; }
    public int year { get; set; }
    public string color { get; set; }
    public string pantone_value { get; set; }

    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<ListaUsuario> data { get; set; }
}