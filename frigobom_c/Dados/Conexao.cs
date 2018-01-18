using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace frigobom_c.Dados
{
    public class Conexao
    {
        public string tipo { get; set; }
        public string servidor { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public string banco { get; set; }





    }
}

