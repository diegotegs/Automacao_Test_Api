using System;
using System.Collections.Generic;
using System.Text;


   public class DelayedResponse
    {
       
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string avatar { get; set; }      
            public int page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
            public int total_pages { get; set; }
            public List<DelayedResponse> data { get; set; }
        
    }

