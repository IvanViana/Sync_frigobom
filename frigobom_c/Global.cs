using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace frigobom_c
{
    public static class Global
     {
        private static string caminho = AppDomain.CurrentDomain.BaseDirectory;
        private static string m_perfil = "";
            public static string Perfil
            {
                get { return m_perfil; }
                set { m_perfil = value; }
            }
            private static string _login = "";
            private static int _CtrLog =0;
        public static string Login
            {
                get { return _login; }
                set { _login = value; }
            }
            public static int CtrLog
            {
                get { return _CtrLog; }
                set { _CtrLog = value; }
            }

        public static void gravaLog(string mensagem)
        {
            try
            {
                string arqLog1 = CaminhoDadosXML(caminho) + @"Dados\LOG.sql";
                //string arqLog1 = @"C:\Users\Ivan\Documents\FRIGOBOM DADOS.FDB\LOG.sql";
                string oLog1 = DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm") + " | " + mensagem + "\r\n";
                StreamWriter writer1e = new StreamWriter(arqLog1, true);
                writer1e.WriteLine(oLog1);
                writer1e.Flush();
                writer1e.Close();
                oLog1 = null;
                arqLog1 = null;
            }
            catch (Exception)
            {

                return;
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

        public static void gravaLogAzure()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //string arqLog = @"C:\Users\Ivan\Documents\FRIGOBOM DADOS.FDB\LOG.sql";
            string arqLog = CaminhoDadosXML(caminho) + @"Dados\LOG.sql";

            var prodx = from p in XElement.Load((CaminhoDadosXML(caminho) + @"Dados\Conexoes.xml")).Elements("Conexao")
                        where p.Element("tipo").Value == "azure"
                        select new
                        {
                            servidor = p.Element("servidor").Value,
                            usuario = p.Element("usuario").Value,
                            senha = p.Element("senha").Value,
                            banco = p.Element("banco").Value,
                        };
            // Executa a consulta
            foreach (var produto in prodx)
            {
                builder.DataSource = produto.servidor;
                builder.UserID = produto.usuario;
                builder.Password = produto.senha;
                builder.InitialCatalog = produto.banco;
                builder.ConnectTimeout = 60;
            }

            //limpa memória
            prodx = null;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    StreamReader texto = new StreamReader(arqLog);
                    // textBox3.Text = texto.ReadToEnd();
                    string oLogSql = "";
                    string s = texto.ReadToEnd();
                    int i = 0;
                    string sql = @"INSERT INTO [dbo].[vendas_frigobom_log]
                                       ([F_log])
                                 VALUES ";

                    string[] words = Regex.Split(s, "\r\n");


                    foreach (string Linha in words)
                    {
                        if (i==0)
                        {
                            oLogSql += sql + "('"+  Linha.Replace("'","|") + "')";
                            i++;
                        }
                        else if (i==999)
                        {
                            oLogSql += ",('" + Linha.Replace("'", "|") + "')";
                            i = 0;
                        }
                        else
                        {
                            oLogSql += ",('" + Linha.Replace("'", "|") + "')";
                            i++;
                        }

                        
                        
                    }

                    ///fecho e deleto o arquivo.
                    texto.Close();
                    
                        if (File.Exists(arqLog))
                        {
                            File.Delete(arqLog);
                        }

                    //exporto dados.
                    SqlCommand command = new SqlCommand(oLogSql, connection);
                    command.CommandTimeout = 60;
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                    //log.WriteEntry(watch.Elapsed + " | Exportação para o cloud concluida " +  "\r\n"); 
                    sql = null;
                    oLogSql = null;
                    s = null;
                    words = null;
                    arqLog = null;



                }
                catch (Exception ex)
                {
                    //log.WriteEntry(ex); 

                  string oLog =  " Erro Exportar Log no Azure linha 160 " + ex.Message + "\r\n";
                  Global.gravaLog(oLog);
                }


            }



        }


     }
    
}
