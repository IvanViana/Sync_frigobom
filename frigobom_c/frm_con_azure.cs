using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace frigobom_c
{
    public partial class frm_con_azure : Form
    {
        public string caminho;
        public frm_con_azure()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_azure_ds.Text.ToString().Trim() == "" || txt_azure_db.Text.ToString() == "" || txt_azure_user.Text.ToString() == "" || txt_azure_pass.Text.ToString() == "")
            {
                MessageBox.Show("Preencha os Campos Corretamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int reg;
            using (DataSet dsResultado = new DataSet())
            {
                dsResultado.ReadXml(CaminhoDadosXML(caminho) + @"Dados\Conexoes.xml");
                if (dsResultado.Tables.Count == 0) { }
                else
                {
                    dsResultado.Tables[0].Constraints.Add("pk_codigo", dsResultado.Tables[0].Columns[0], true);
                    DataRow linharegistro = dsResultado.Tables[0].Rows.Find("azure");


                    if (linharegistro != null)
                    {
                        reg = dsResultado.Tables[0].Rows.IndexOf(linharegistro);
                        dsResultado.Tables[0].Rows[reg][0] = "azure";
                        dsResultado.Tables[0].Rows[reg][1] = txt_azure_ds.Text;
                        dsResultado.Tables[0].Rows[reg][2] = txt_azure_user.Text;
                        dsResultado.Tables[0].Rows[reg][3] = txt_azure_pass.Text;
                        dsResultado.Tables[0].Rows[reg][4] = txt_azure_db.Text;
                        dsResultado.WriteXml(CaminhoDadosXML(caminho) + @"Dados\Conexoes.xml", XmlWriteMode.IgnoreSchema);
                        MessageBox.Show("Login salvo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // testar_conexao(txt_firebird_ds.Text.ToString(), txt_firebird_db.Text.ToString(), txt_firebird_user.Text.ToString(), txt_firebird_pass.Text.ToString());
                        if (testa_azure(txt_azure_ds.Text.ToString(), txt_azure_user.Text, txt_azure_pass.Text, txt_azure_db.Text) == true)
                        {
                            this.Close();
                        }
                        
                        
                    }

                }

            }
        }

        private void frm_con_azure_Click(object sender, EventArgs e)
        {

        }

        private void frm_con_azure_Load(object sender, EventArgs e)
        {
            caminho = AppDomain.CurrentDomain.BaseDirectory;

            // adicionar um try caso o arquivo não exista
            var prods = from p in XElement.Load((CaminhoDadosXML(caminho) + @"Dados\Conexoes.xml")).Elements("Conexao")
                        where p.Element("tipo").Value == "azure"
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
                txt_azure_ds.Text = produto.servidor;
                txt_azure_user.Text = produto.usuario;
                txt_azure_pass.Text = produto.senha;
                txt_azure_db.Text = produto.banco;
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

        public static Boolean testa_azure(string txt_azure_ds, string txt_azure_user, string txt_azure_pass, string txt_azure_db )
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = txt_azure_ds;
                builder.UserID = txt_azure_user;
                builder.Password = txt_azure_pass;
                builder.InitialCatalog = txt_azure_db;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Conectado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        connection.Close();
                         return true;
                    }


                }
            }
            catch (SqlException Erro)
            {
                MessageBox.Show(Erro.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
               
                builder.DataSource = txt_azure_ds.Text.ToString();
                builder.UserID = txt_azure_user.Text.ToString();
                builder.Password = txt_azure_pass.Text.ToString();
                builder.InitialCatalog = txt_azure_db.Text.ToString();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    if (connection.State == ConnectionState.Open ) {
                        MessageBox.Show("Conectado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        connection.Close();
                        // return true;
                    }
                    
                    
                }
            }
            catch (SqlException Erro)
            {
                MessageBox.Show(Erro.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // return false;
            }

         
            
        }
    }
}
