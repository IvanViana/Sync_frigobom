using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;


namespace frigobom_c
{
    public partial class frm_con_fb : Form
    {
        public string caminho;
        public frm_con_fb()
        {
            InitializeComponent();
        }

        private void frm_con_fb_Load(object sender, EventArgs e)
        {
            caminho = AppDomain.CurrentDomain.BaseDirectory;

            // adicionar um try caso o arquivo não exista
            var prods = from p in XElement.Load((CaminhoDadosXML(caminho) + @"Dados\Conexoes.xml")).Elements("Conexao")
                        where p.Element("tipo").Value == "fb"
                        select new
                        {
                            servidor = p.Element("servidor").Value,
                            usuario = p.Element("usuario").Value,
                            senha = p.Element("senha").Value,
                            banco = p.Element("banco").Value,
                        };

            // Executa a consulta
            foreach (var produto in prods)
            {
                txt_firebird_ds.Text = produto.servidor;
                txt_firebird_user.Text = produto.usuario;
                txt_firebird_pass.Text = produto.senha;
                txt_firebird_db.Text = produto.banco;
            }

        }

        public static string CaminhoDadosXML(string caminho)
        {

            if (caminho.IndexOf("\\bin\\Debug") != 0)
            {
                caminho = caminho.Replace("\\bin\\Debug", "");
            }
            return caminho;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void bt_salvar_Click(object sender, EventArgs e)
        {
            if (txt_firebird_ds.Text.ToString().Trim()=="" || txt_firebird_db.Text.ToString()=="" || txt_firebird_user.Text.ToString()=="" || txt_firebird_pass.Text.ToString() == "")
            {
                MessageBox.Show("Preencha os Campos Corretamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int reg;
            using (DataSet dsResultado = new DataSet()) {
                dsResultado.ReadXml(CaminhoDadosXML(caminho)  +@"Dados\Conexoes.xml");
                if (dsResultado.Tables.Count == 0) { } else {
                    dsResultado.Tables[0].Constraints.Add("pk_codigo", dsResultado.Tables[0].Columns[0], true);
                    DataRow linharegistro = dsResultado.Tables[0].Rows.Find("fb");

                    
                    if (linharegistro != null)
                    {
                        reg = dsResultado.Tables[0].Rows.IndexOf(linharegistro);
                        dsResultado.Tables[0].Rows[reg][0] = "fb";
                        dsResultado.Tables[0].Rows[reg][1] = txt_firebird_ds.Text;
                        dsResultado.Tables[0].Rows[reg][2] = txt_firebird_user.Text;
                        dsResultado.Tables[0].Rows[reg][3] = txt_firebird_pass.Text;
                        dsResultado.Tables[0].Rows[reg][4] = txt_firebird_db.Text;
                        dsResultado.WriteXml(CaminhoDadosXML(caminho)+ @"Dados\Conexoes.xml", XmlWriteMode.IgnoreSchema);
                        MessageBox.Show("Login salvo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (testar_conexao(txt_firebird_ds.Text.ToString(), txt_firebird_db.Text.ToString(), txt_firebird_user.Text.ToString(), txt_firebird_pass.Text.ToString()) == true)
                        {
                            this.Close();
                        }
                        
                    }
                    
                }

            }
           
              
        }

        private void button2_Click(object sender, EventArgs e)
        {

            testar_conexao(txt_firebird_ds.Text.ToString(), txt_firebird_db.Text.ToString(), txt_firebird_user.Text.ToString(), txt_firebird_pass.Text.ToString());
           
        }

        public static Boolean testar_conexao(string ds, string db, string user, string pass)  {
            string firebird_ds = ds;
            string firebird_db = db;
            string firebird_user = user;
            string firebird_pass = pass;
            string strConn;
            FbConnection conn;

            strConn = "@DataSource=" + firebird_ds + "; Database=" + firebird_db + "; username= " + firebird_user + "; password = " + firebird_pass + "";
            conn = new FbConnection(strConn);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Conectado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conn.Close();
                    return true;
                }
            }
            catch (FbException Erro)
            {

                MessageBox.Show(Erro.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return false;

        }
    }
}
