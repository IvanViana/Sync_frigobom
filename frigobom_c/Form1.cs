using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Timers;
using System.Globalization;
using EnCryptDecrypt;
using Microsoft.Win32;

namespace frigobom_c
{
    public partial class Form1 : Form
    {
        public string caminho;
        delegate void SetTextCallback(string texto);



        // private string strConn ;
        //FbConnection conn;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
            //timer1.Enabled = true;
            caminho = AppDomain.CurrentDomain.BaseDirectory;
            string meuApp = caminho + "frigobom_c.exe";
            //Log log = new Log(); 
            // log.WriteEntry("Aplicação aberta!"); 

            // Create an EventLog instance and assign its source.
            darPlay();
            WriteRegistry(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "Sync_Frigobom", meuApp);


        }
        static void WriteRegistry(RegistryKey parentKey, String subKey, String valueName, Object value)
        {
            RegistryKey key;
            try
            {
                key = parentKey.OpenSubKey(subKey, true);
                if (key == null)
                    key = parentKey.CreateSubKey(subKey);

                //Set the value.
                key.SetValue(valueName, value);


            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Global.Login == "roda")
            {
                Thread trd = new Thread(new ThreadStart(this.atualizar_azure));
                trd.IsBackground = true;
                trd.Start();
                Global.Login = "nroda";
            }
            else
            {

            }












            // Log log = new Log(); 
            //log.WriteEntry("Início de Sync"); 







        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Log log = new Log(); 
            //log.WriteEntry("Aplicação foi fechada"); 

        }

        private void bt_azure_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Logica para tarefa em background ser feita
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Reporta o progresso do BackgroundWorker
        }
        public static string CaminhoDadosXML(string caminho)
        {

            if (caminho.IndexOf("\\bin\\Debug") != 0)
            {
                caminho = caminho.Replace("\\bin\\Debug", "");
            }
            return caminho;
        }


        private void bt_firebird_Click(object sender, EventArgs e)
        {
        }



        private void toAzureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ver_dados frm2 = new frm_ver_dados();
            frm2.ShowDialog(this);
        }

        private void azureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_con_azure frm = new frm_con_azure();
            frm.ShowDialog(this);
        }

        private void firebirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_con_fb frm1 = new frm_con_fb();
            frm1.ShowDialog(this);
        }

        public static Boolean testar_conexao(string ds, string db, string user, string pass)
        {
            string firebird_ds = ds;
            string firebird_db = db;
            string firebird_user = user;
            string firebird_pass = pass;
            string strConn;
            FbConnection conn;

            strConn = "@DataSource=" + firebird_ds + "; Database=" + firebird_db + "; username= " + firebird_user + "; password = " + firebird_pass + ";Pooling=false;";
            conn = new FbConnection(strConn);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    // MessageBox.Show("Conectado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conn.Close();
                    return true;
                }
            }
            catch (FbException Erro)
            {

                //MessageBox.Show(Erro.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return false;

        }



        private void button3_Click(object sender, EventArgs e)
        {
            darPlay();
            
        }


        private void DefinirTexto(string texto)
        {
            this.label1.Text = texto;
        }
        private void DesabilitarBotao(string texto)
        {
            this.label1.Text = texto;
        }

        public void atualizar_azure()
        {
            Global.Login = "nroda";


            string texto2 = "  Começando os Trabalhos!! ";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }

            //string caminho;
            string firebird_ds = "";
            string firebird_db = "";
            string firebird_user = "";
            string firebird_pass = "";
            string arqLog = CaminhoDadosXML(caminho) + @"Dados\LOG.sql";
            // string arqLog = @"C:\Users\Ivan\Documents\FRIGOBOM DADOS.FDB\LOG.sql";
            string nomeArquivo = CaminhoDadosXML(caminho) + @"Dados\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sql";
            //string nomeArquivo = @"C:\Users\Ivan\Documents\FRIGOBOM DADOS.FDB\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sql";
            String oLog = ">>> run " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + "\r\n" + "Summary \r\n";

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
                firebird_ds = produto.servidor;
                firebird_db = produto.banco;
                firebird_user = produto.usuario;
                firebird_pass = produto.senha;
            }


            texto2 = " Testado Conexão!! ";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }

            if (testar_conexao(firebird_ds, firebird_db, firebird_user, firebird_pass) == false)
            {
                texto2 = "Conexão ao Firebird Falhou ";
                if (this.label1.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(DefinirTexto);
                    this.Invoke(d, new object[] { texto2 });
                }
                else
                {
                    this.label1.Text = texto2;
                }

                Global.gravaLog("Erro de conexão com o FireBird");
                Global.Login = "roda";
                return;

            }


            texto2 = "  Conexão ao Fire OK!";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }


            Stopwatch watch = new Stopwatch();
            watch.Start();

            string strConn = "@DataSource=" + firebird_ds + "; Database=" + firebird_db + "; username= " + firebird_user + "; password = " + firebird_pass + ";Pooling=false";
            FbConnection conn = new FbConnection(strConn);


            ///pegar ultimo registro no cloud
            ///

            if (TestaAzure(caminho) == "-2146232060")
            {
                texto2 = "  Conexão com o cloud falhou!";
                if (this.label1.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(DefinirTexto);
                    this.Invoke(d, new object[] { texto2 });
                }
                else
                {
                    this.label1.Text = texto2;
                }


                Global.gravaLog("Erro de conexão com o cloud");
                Global.Login = "roda";
                return;
            }


            string LastReg = RetornaUltimo();
            if (LastReg == "-1")
            {
                Global.gravaLog("Erro ao conectar ao cloud e captura última atualização");
                Global.Login = "roda";
                return;
            }

            Int64 LimMax = Convert.ToInt64(LastReg) ;

            if (string.IsNullOrEmpty(LimMax.ToString()))
            {
                Global.gravaLog("Não foi possivel achar o último Registro no cloud - linha 353");
                Global.Login = "roda";
                return;
            }



            LimMax += 10000; 
            String fb_mysql;
            fb_mysql = @" select first 10000
                                mov_nf.id,
                                mov_nf.situacao,
                                mov_nf.id_pessoa,
                                mov_nf.emitente,
                                mov_nf.numero,
                                mov_nf.emissao,
                                mov_nf.tipo_pagamento,
                                mov_nf.id_loja,
                                mov_nf.modelo,
                                mov_nf.digitacao,
                                tb_nf_situacao.nome nome_nf_situacao,
                                tb_pessoas.nome nome_pessoa,
                                tb_nf_modelos.nome nome_nf,
                                mov_nf_itens.id_seq,
                                tb_produtos.nome nome_produto,
                                mov_nf_itens.id_produto,
                                mov_nf_itens.id_unidademedida,
                                mov_nf_itens.quantidade,
                                mov_nf_itens.precounitario,
                                mov_nf_itens.valortotal,
                                tb_produtos_unmedida.nome nome_unidade_medida,
                                tb_produtos.id_grupos,
                                tb_produtos_grupos.nome nome_grupo,
                                tb_produtos.id_subgrupos,
                                tb_produtos_subgrupos.nome nome_subgrupo
                            from tb_nf_modelos
                               right outer
                            join mov_nf on (tb_nf_modelos.id = mov_nf.modelo)

                            inner
                            join mov_nf_itens on (mov_nf.id = mov_nf_itens.id)

                            left outer join tb_produtos_unmedida on(mov_nf_itens.id_unidademedida = tb_produtos_unmedida.id)
                               left outer join tb_produtos on(mov_nf_itens.id_produto = tb_produtos.id)
                               left outer join tb_produtos_subgrupos on(tb_produtos.id_subgrupos = tb_produtos_subgrupos.id)
                               left outer join tb_produtos_grupos on(tb_produtos.id_grupos = tb_produtos_grupos.id)
                               left outer join tb_pessoas on(mov_nf.id_pessoa = tb_pessoas.id)
                               inner join tb_nf_situacao on(mov_nf.situacao = tb_nf_situacao.id)
                            where
                               (
                                  (mov_nf.emitente = 'P') and (mov_nf.id> " + LastReg + " and mov_nf.id< " + LimMax + @" ) 
                               and
                                  (tb_nf_situacao.nome = 'Documento regular')
                               ) order by mov_nf.id;";


            watch.Stop();
            //Log log = new Log(); 
            // log.WriteEntry(watch.Elapsed + " | Tempo de processamento na Consulta Firebird " +  "\r\n"); 
            oLog += watch.Elapsed + " | Tempo de processamento na montagem SQL " + "\r\n";

            ///medir o tempo de execução
            watch.Restart();

            texto2 = "  Executando consulta no Fire ";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }


            int num_newRegistro = 0;
            try
            {
                FbCommand cmd = new FbCommand(fb_mysql, conn);
                // FbDataAdapter DA = new FbDataAdapter(cmd);
                FbDataAdapter DA2 = new FbDataAdapter(cmd);
                DataSet DS = new DataSet();
                DataTable DT = new DataTable();
                conn.Open();

                //  DA.Fill(DS, "teste"); 
                DA2.Fill(DT);

                conn.Close();

                num_newRegistro = Convert.ToInt16(DT.Rows.Count.ToString());

                if (num_newRegistro < 1)
                {

                  
                    texto2 = "  Não há dados novos! ";
                    if (this.label1.InvokeRequired)
                    {
                        SetTextCallback d = new SetTextCallback(DefinirTexto);
                        this.Invoke(d, new object[] { texto2 });
                    }
                    else
                    {
                        this.label1.Text = texto2;
                    }

                    watch.Stop();
                    //Log log = new Log(); 
                    // log.WriteEntry(watch.Elapsed + " | Tempo de processamento na Consulta Firebird " +  "\r\n"); 
                    oLog += watch.Elapsed + " | Não Há dados novos - aguardar próximo. " + "\r\n";
                    StreamWriter writer3 = new StreamWriter(arqLog, true);
                    //Fecha o Log

                    writer3.WriteLineAsync(oLog);
                    oLog = null;
                    writer3.Flush();
                    writer3.Close();

                    Global.gravaLogAzure();
                    Global.Login = "roda";
                    return;

                }


                watch.Stop();
                //Log log = new Log(); 
                // log.WriteEntry(watch.Elapsed + " | Tempo de processamento na Consulta Firebird " +  "\r\n"); 
                oLog += watch.Elapsed + " | Tempo de processamento na Consulta Firebird " + "\r\n";


                // dataGridView2.DataSource = DS;
                // dataGridView2.DataMember = "teste";

                //textBox2.Text += "fim conexao";
                //  textBox2.Text += "GErar SQL";

                //comecar o loop
                string str = string.Empty;
                string Oinsert = string.Empty;
                string Ovalues = string.Empty;
                string Osql = string.Empty;
                string str2 = string.Empty;

                watch.Restart();


                texto2 = "  Montando Arquivo ";
                if (this.label1.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(DefinirTexto);
                    this.Invoke(d, new object[] { texto2 });
                }
                else
                {
                    this.label1.Text = texto2;
                }

                Oinsert = @"INSERT INTO [dbo].[vendas_frigobom_full]
           ([ID]
           ,[SITUACAO]
           ,[ID_PESSOA]
           ,[EMITENTE]
           ,[NUMERO]
           ,[EMISSAO]
           ,[TIPO_PAGAMENTO]
           ,[ID_LOJA]
           ,[MODELO]
           ,[DIGITACAO]
           ,[NOME_NF_SITUACAO]
           ,[NOME_PESSOA]
           ,[NOME_NF]
           ,[ID_SEQ]
           ,[NOME_PRODUTO]
           ,[ID_PRODUTO]
           ,[ID_UNIDADEMEDIDA]
           ,[QUANTIDADE]
           ,[PRECOUNITARIO]
           ,[VALORTOTAL]
           ,[NOME_UNIDADE_MEDIDA]
           ,[ID_GRUPOS]
           ,[NOME_GRUPO]
           ,[ID_SUBGRUPOS]
           ,[NOME_SUBGRUPO])
             VALUES ";

                int i = 0;
                int c = 0;
                string b = DT.Rows.Count.ToString();
                foreach (DataRow dRow in DT.Rows)
                {
                    i++;
                    c++;
                    if (i == 1)
                    {
                        Osql += "\r\n" + Oinsert + "(" + TrataNulo(dRow["id"].ToString()) + ",'" + dRow["situacao"].ToString() + "'," + TrataNulo(dRow["id_pessoa"].ToString()) + ",'" + dRow["emitente"].ToString() + "'," + TrataNulo(dRow["numero"].ToString()) + ",'" + ConvertData(dRow["emissao"].ToString()) + "','" + dRow["tipo_pagamento"].ToString() + "',"
                             + TrataNulo(dRow["id_loja"].ToString()) + ",'" + dRow["modelo"].ToString() + "','" + ConvertData(dRow["digitacao"].ToString()) + "','" + dRow["nome_nf_situacao"].ToString().Replace("'", "'+char(39)+'") + "','" + dRow["nome_pessoa"].ToString().Replace("'", "'+char(39)+'") + "','" + dRow["nome_nf"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_seq"].ToString()) + ",'"
                             + dRow["nome_produto"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_produto"].ToString()) + "," + TrataNulo(dRow["id_unidademedida"].ToString()) + "," + dRow["quantidade"].ToString().Replace(",", ".") + "," + dRow["precounitario"].ToString().Replace(",", ".") + "," + dRow["valortotal"].ToString().Replace(",", ".") + ",'" + dRow["nome_unidade_medida"].ToString().Replace("'", "'+char(39)+'") + "',"
                             + TrataNulo(dRow["id_grupos"].ToString()) + ",'" + dRow["nome_grupo"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_subgrupos"].ToString()) + ",'" + dRow["nome_subgrupo"].ToString().Replace("'", "'+char(39)+'") + "')\r\n";

                    }
                    else if (i == 999)
                    {
                        i = 0;
                        Osql += ",(" + TrataNulo(dRow["id"].ToString()) + ",'" + dRow["situacao"].ToString() + "'," + TrataNulo(dRow["id_pessoa"].ToString()) + ",'" + dRow["emitente"].ToString() + "'," + TrataNulo(dRow["numero"].ToString()) + ",'" + ConvertData(dRow["emissao"].ToString()) + "','" + dRow["tipo_pagamento"].ToString() + "',"
                             + TrataNulo(dRow["id_loja"].ToString()) + ",'" + dRow["modelo"].ToString() + "','" + ConvertData(dRow["digitacao"].ToString()) + "','" + dRow["nome_nf_situacao"].ToString().Replace("'", "'+char(39)+'") + "','" + dRow["nome_pessoa"].ToString().Replace("'", "'+char(39)+'") + "','" + dRow["nome_nf"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_seq"].ToString()) + ",'"
                             + dRow["nome_produto"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_produto"].ToString()) + "," + TrataNulo(dRow["id_unidademedida"].ToString()) + "," + dRow["quantidade"].ToString().Replace(",", ".") + "," + dRow["precounitario"].ToString().Replace(",", ".") + "," + dRow["valortotal"].ToString().Replace(",", ".") + ",'" + dRow["nome_unidade_medida"].ToString().Replace("'", "'+char(39)+'") + "',"
                             + TrataNulo(dRow["id_grupos"].ToString()) + ",'" + dRow["nome_grupo"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_subgrupos"].ToString()) + ",'" + dRow["nome_subgrupo"].ToString().Replace("'", "'+char(39)+'") + "')\r\n";

                    }
                    else
                    {
                        Osql += ",(" + TrataNulo(dRow["id"].ToString()) + ",'" + dRow["situacao"].ToString() + "'," + TrataNulo(dRow["id_pessoa"].ToString()) + ",'" + dRow["emitente"].ToString() + "'," + TrataNulo(dRow["numero"].ToString()) + ",'" + ConvertData(dRow["emissao"].ToString()) + "','" + dRow["tipo_pagamento"].ToString() + "',"
                             + TrataNulo(dRow["id_loja"].ToString()) + ",'" + dRow["modelo"].ToString() + "','" + ConvertData(dRow["digitacao"].ToString()) + "','" + dRow["nome_nf_situacao"].ToString().Replace("'", "'+char(39)+'") + "','" + dRow["nome_pessoa"].ToString().Replace("'", "'+char(39)+'") + "','" + dRow["nome_nf"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_seq"].ToString()) + ",'"
                             + dRow["nome_produto"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_produto"].ToString()) + "," + TrataNulo(dRow["id_unidademedida"].ToString()) + "," + dRow["quantidade"].ToString().Replace(",", ".") + "," + dRow["precounitario"].ToString().Replace(",", ".") + "," + dRow["valortotal"].ToString().Replace(",", ".") + ",'" + dRow["nome_unidade_medida"].ToString().Replace("'", "'+char(39)+'") + "',"
                             + TrataNulo(dRow["id_grupos"].ToString()) + ",'" + dRow["nome_grupo"].ToString().Replace("'", "'+char(39)+'") + "'," + TrataNulo(dRow["id_subgrupos"].ToString()) + ",'" + dRow["nome_subgrupo"].ToString().Replace("'", "'+char(39)+'") + "')\r\n";
                    }
                }
                //limpa a dataset
                DT.Clear();

                watch.Stop();
                //log.WriteEntry(watch.Elapsed + " | Montagemd o Sql para exportação  " +  "\r\n"); 
                oLog += watch.Elapsed + " | Montagem do Sql para exportação " + "\r\n";

                watch.Restart();

                texto2 = "  Escrevendo Arquivo! ";
                if (this.label1.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(DefinirTexto);
                    this.Invoke(d, new object[] { texto2 });
                }
                else
                {
                    this.label1.Text = texto2;
                }



                // Cria um novo arquivo e devolve um StreamWriter para ele
                StreamWriter writer = new StreamWriter(nomeArquivo);

                /*
                if (!File.Exists(arqLog))
                {
                    StreamWriter writer1 = new StreamWriter(arqLog);
                }
                else
                {

                }*/

                // Agora é só sair escrevendo
                
                writer.WriteLine(Osql);

                // Não esqueça de fechar o arquivo ao terminar
                writer.Close();
                Osql = null;
                conn.Close();

                

                watch.Stop();
                //log.WriteEntry(watch.Elapsed + " | Montagemd o Sql para exportação  " +  "\r\n"); 
                oLog += watch.Elapsed + " | Escrevendo consulta SQL " + "\r\n";

            }
            catch (FbException e)
            {

                Global.gravaLog(e.Message.ToString() +"linha 618");
                Global.Login = "roda";
                Global.gravaLogAzure();
                //MessageBox.Show(e.StackTrace);

                //MessageBox.Show(e.TargetSite.ReflectedType.Name + " + " + e.TargetSite.Name);
                return;
            }


            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            watch.Restart();
            texto2 = "  Conexão ao cloud ";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }

            // adicionar um try caso o arquivo não exista
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

                string cipherText = produto.senha.Trim();
                string decryptedText = CryptorEngine.Decrypt(cipherText, true);
                

                builder.DataSource = produto.servidor;
                builder.UserID = produto.usuario;
                builder.Password = decryptedText;
                //builder.Password = produto.senha;
                builder.InitialCatalog = produto.banco;
                builder.ConnectTimeout = 600;
            }

            //limpa memória
            prodx = null;

            texto2 = "  exportando ao cloud! ";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    StreamReader texto = new StreamReader(nomeArquivo);
                    // textBox3.Text = texto.ReadToEnd();
                    String sql = texto.ReadToEnd();

                    ///fecho e deleto o arquivo.
                    texto.Close();
                    /*
                    if (File.Exists(nomeArquivo))
                    {
                        File.Delete(nomeArquivo);
                    }*/

                    //exporto dados.
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 600;
                    command.ExecuteNonQuery();
                    connection.Close();


                    //log.WriteEntry(watch.Elapsed + " | Exportação para o cloud concluida " +  "\r\n"); 
                    sql = null;

                }
                catch (Exception ex)
                {
                    //log.WriteEntry(ex); 
                    oLog += " | ocorreu um erro " + ex.Message + "\r\n";
                    Global.gravaLogAzure();
                    Global.Login = "roda";
                    return;


                }


            }

            watch.Stop();

            oLog += watch.Elapsed + " | Exportação para o cloud  " + "\r\n";


            StreamWriter writer1 = new StreamWriter(arqLog, true);
            //Fecha o Log

            writer1.WriteLineAsync(oLog);
            oLog = null;
            writer1.Flush();
            writer1.Close();

            texto2 = "Aguardando o Próximo!";
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DefinirTexto);
                this.Invoke(d, new object[] { texto2 });
            }
            else
            {
                this.label1.Text = texto2;
            }

            Global.Login = "roda";


            if (Global.CtrLog == 10)
            {
                Global.gravaLogAzure();
                Global.CtrLog = 0;

            }
            else
            {
                Global.CtrLog++;
            }





        }

        private void exportaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {

                mynotifyicon.BalloonTipText = "Aplicação Em execução";
                mynotifyicon.BalloonTipTitle = "Sync Frigobom";
                mynotifyicon.BalloonTipIcon = ToolTipIcon.Info;
                // mynotifyicon.Icon = new Icon(SystemIcons.Shield, 40, 40);
                mynotifyicon.Visible = true;
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                mynotifyicon.Visible = false;
            }
        }

        private void mynotifyicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            mynotifyicon.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            this.timer1.Start();
        }
        public static String ConvertData(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                try
                {
                    DateTime xdatetime = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    return xdatetime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (FormatException ex)
                {

                    return "";
                }
            }

        }
        public static String TrataNulo(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return "null";
            }
            else
            {
                return s;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        public static string RetornaUltimo()
        {
            try
            {
                string caminho = AppDomain.CurrentDomain.BaseDirectory;
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
                    builder.InitialCatalog = produto.banco;
                    builder.ConnectTimeout = 60;
                }

                prods = null;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {


                    StringBuilder sb = new StringBuilder();
                    
                    sb.Append("SELECT TOP(1) ID");
                    sb.Append(" FROM dbo.vendas_frigobom_full ORDER BY ID DESC");
                    String sql = sb.ToString();

                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    DataTable DT = new DataTable();

                    connection.Open();

                    da.Fill(DT);

                    string UltReg = "0";

                    string meu = DT.Rows.Count.ToString();

                    if (String.IsNullOrEmpty(meu))
                    {
                        UltReg = "0";
                    }
                    else
                    {
                        UltReg = DT.Rows[0]["ID"].ToString();
                    }

                    DT.Clear();
                    connection.Close();
                    return UltReg;

                }
            }
            catch (SqlException eg)
            {
                if (eg.ErrorCode == 11001)
                {
                    return "-1";
                }
                else
                {
                    Console.WriteLine(eg.ToString());
                    return null;
                }

            }
        }


        public static string TestaAzure(string caminho)
        {

            try
            {

                var prodx = from p in XElement.Load((CaminhoDadosXML(caminho) + @"Dados\Conexoes.xml")).Elements("Conexao")
                            where p.Element("tipo").Value == "azure"
                            select new
                            {
                                servidor = p.Element("servidor").Value,
                                usuario = p.Element("usuario").Value,
                                senha = p.Element("senha").Value,
                                banco = p.Element("banco").Value,
                            };

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                foreach (var produto in prodx)
                {

                    string cipherText = produto.senha.Trim();
                    string decryptedText = CryptorEngine.Decrypt(cipherText, true);

                    builder.DataSource = produto.servidor;
                    builder.UserID = produto.usuario;
                    builder.Password = decryptedText;
                  //  builder.Password = produto.senha;
                    builder.InitialCatalog = produto.banco;
                    builder.ConnectTimeout = 5;
                }

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }


                }
            }
            catch (SqlException Erro)
            {

                if (Erro.ErrorCode == -2146232060)
                {

                    return Erro.ErrorCode.ToString();
                }
                else
                {
                   // MessageBox.Show(Erro.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Erro.ErrorCode.ToString();
                }
                
            }
        }

        private void logLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_log frm3 = new frm_log();
            frm3.ShowDialog(this);
        }

        private void logAzureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_log_azure frm4 = new frm_log_azure();
            frm4.ShowDialog(this);
        }

        public void darPlay()
        {
            Int16 meuvalor = (short)num_hora.Value;

            if (button3.Text == "Conectar")
            {
                button3.Text = "Pausar";
                label1.Text = "Aguardando Horário";
                timer1.Interval = meuvalor * 1000;  //60000 minuto
                timer1.Enabled = true;
                Global.Login = "roda";

            }
            else
            {

                label1.Text = "Estou desligado";
                button3.Text = "Conectar";
                timer1.Enabled = false;
                String oLog = ">>> Stop " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + "\r\n \r\n";
                // string arqLog = @"C:\Users\Ivan\Documents\FRIGOBOM DADOS.FDB\LOG.sql";
                string arqLog = CaminhoDadosXML(caminho) + @"Dados\LOG.sql";
                StreamWriter writer1 = new StreamWriter(arqLog, true);
                writer1.WriteLine(oLog);
                writer1.Flush();
                writer1.Close();

            }

        }


    }

}
