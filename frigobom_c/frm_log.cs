using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace frigobom_c
{
    public partial class frm_log : Form
    {
        public string caminho;
        public frm_log()
        {
            InitializeComponent();
        }

        private void frm_log_Load(object sender, EventArgs e)
        {
             caminho = AppDomain.CurrentDomain.BaseDirectory;

            string arqLog = CaminhoDadosXML(caminho) + @"Dados\LOG.sql";
            //string arqLog = @"C:\Users\Ivan\Documents\FRIGOBOM DADOS.FDB\LOG.sql";
            LerArquivoTexto(arqLog);
        }

        public static string CaminhoDadosXML(string caminho)
        {

            if (caminho.IndexOf("\\bin\\Debug") != 0)
            {
                caminho = caminho.Replace("\\bin\\Debug", "");
            }
            return caminho;
        }
        private void LerArquivoTexto(string StrArquivo)
        {

            try
            {
                //Cria uma instância do StreamReader para ler o arquivo
                //A declaração using fecha o stream no fim do escopo
                using (StreamReader sr = new StreamReader(StrArquivo))
                {
                    String linha;
                    //Ler e exibe as linhas até alcançar o fim do arquivo.
                    while ((linha = sr.ReadLine()) != null)
                    {
                        //MessageBox.Show(linha);
                        textBox1.Text += linha + "\r\n";
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Não é possivel ler o arquivo");
                MessageBox.Show(e.Message);
                this.Close();

            }

        }
    }
}
