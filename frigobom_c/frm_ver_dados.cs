﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
using EnCryptDecrypt;

namespace frigobom_c
{
    public partial class frm_ver_dados : Form
    {
        string caminho;
        public frm_ver_dados()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exibir_azure();
        }
        public static string CaminhoDadosXML(string caminho)
        {

            if (caminho.IndexOf("\\bin\\Debug") != 0)
            {
                caminho = caminho.Replace("\\bin\\Debug", "");
            }
            return caminho;
        }
        private void exibir_azure()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
               
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

                    string cipherText = produto.senha.Trim();
                    string decryptedText = CryptorEngine.Decrypt(cipherText, true);

                    builder.DataSource = produto.servidor;
                    builder.UserID = produto.usuario;
                    builder.Password = decryptedText;
                    //builder.Password = produto.senha;
                    builder.InitialCatalog = produto.banco;
                }

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    StringBuilder sb = new StringBuilder();
                    
        

                    sb.Append(@"SELECT TOP(20)  [ID]
                                              ,[NUMERO]
                                              ,[ID_LOJA] as LOJA
                                              ,[DIGITACAO]
                                              ,[NOME_PESSOA]
                                              ,[NOME_NF]
                                              ,[ID_SEQ]
                                              ,[NOME_PRODUTO]
                                              ,[QUANTIDADE]
                                              ,[PRECOUNITARIO]
                                              ,[VALORTOTAL]
                                              ,[NOME_GRUPO]
                                              ,[NOME_SUBGRUPO]
                                              ,[HORA_INCLUSAO]");
                    sb.Append(" FROM dbo.vendas_frigobom_full ORDER BY id DESC;");
                    String sql = sb.ToString();

                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();

                    connection.Open();

                    da.Fill(ds, "teste");

                    bindingSource1.DataSource = ds.Tables[0].DefaultView;

                    bindingNavigator1.BindingSource = bindingSource1;

                    dataGridView1.DataSource = bindingSource1;

                    connection.Close();
                }
            }
            catch (SqlException eg)
            {
                Console.WriteLine(eg.ToString());
            }
            // Console.ReadLine();

        }

        private void frm_ver_dados_Load(object sender, EventArgs e)
        {
            caminho = AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
