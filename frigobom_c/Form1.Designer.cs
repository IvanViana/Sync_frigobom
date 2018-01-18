namespace frigobom_c
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.conexãoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.firebirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toAzureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.num_hora = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mynotifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logAzureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_hora)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conexãoToolStripMenuItem1,
            this.exportarDadosToolStripMenuItem,
            this.exportaçãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(392, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // conexãoToolStripMenuItem1
            // 
            this.conexãoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firebirdToolStripMenuItem,
            this.azureToolStripMenuItem});
            this.conexãoToolStripMenuItem1.Name = "conexãoToolStripMenuItem1";
            this.conexãoToolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.conexãoToolStripMenuItem1.Text = "Conexão";
            // 
            // firebirdToolStripMenuItem
            // 
            this.firebirdToolStripMenuItem.Name = "firebirdToolStripMenuItem";
            this.firebirdToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.firebirdToolStripMenuItem.Text = "Firebird";
            this.firebirdToolStripMenuItem.Click += new System.EventHandler(this.firebirdToolStripMenuItem_Click);
            // 
            // azureToolStripMenuItem
            // 
            this.azureToolStripMenuItem.Name = "azureToolStripMenuItem";
            this.azureToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.azureToolStripMenuItem.Text = "Azure";
            this.azureToolStripMenuItem.Click += new System.EventHandler(this.azureToolStripMenuItem_Click);
            // 
            // exportarDadosToolStripMenuItem
            // 
            this.exportarDadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toAzureToolStripMenuItem});
            this.exportarDadosToolStripMenuItem.Name = "exportarDadosToolStripMenuItem";
            this.exportarDadosToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportarDadosToolStripMenuItem.Text = "Dados";
            // 
            // toAzureToolStripMenuItem
            // 
            this.toAzureToolStripMenuItem.Name = "toAzureToolStripMenuItem";
            this.toAzureToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.toAzureToolStripMenuItem.Text = "Consultar Azure";
            this.toAzureToolStripMenuItem.Click += new System.EventHandler(this.toAzureToolStripMenuItem_Click);
            // 
            // exportaçãoToolStripMenuItem
            // 
            this.exportaçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logLocalToolStripMenuItem,
            this.logAzureToolStripMenuItem});
            this.exportaçãoToolStripMenuItem.Name = "exportaçãoToolStripMenuItem";
            this.exportaçãoToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.exportaçãoToolStripMenuItem.Text = "Log";
            this.exportaçãoToolStripMenuItem.Click += new System.EventHandler(this.exportaçãoToolStripMenuItem_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 422);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(151, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Executar a cada                   hr";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(306, 418);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "Conectar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // num_hora
            // 
            this.num_hora.Location = new System.Drawing.Point(99, 420);
            this.num_hora.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_hora.Name = "num_hora";
            this.num_hora.Size = new System.Drawing.Size(44, 20);
            this.num_hora.TabIndex = 30;
            this.num_hora.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Crimson;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Snow;
            this.label1.Location = new System.Drawing.Point(41, 372);
            this.label1.MinimumSize = new System.Drawing.Size(320, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 17);
            this.label1.TabIndex = 32;
            this.label1.Text = "No Working!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mynotifyicon
            // 
            this.mynotifyicon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mynotifyicon.BalloonTipText = " - -";
            this.mynotifyicon.BalloonTipTitle = "Sync Frigobom";
            this.mynotifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("mynotifyicon.Icon")));
            this.mynotifyicon.Text = "Sync Frigobom";
            this.mynotifyicon.Visible = true;
            this.mynotifyicon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mynotifyicon_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::frigobom_c.Properties.Resources.frigo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(390, 383);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // logLocalToolStripMenuItem
            // 
            this.logLocalToolStripMenuItem.Name = "logLocalToolStripMenuItem";
            this.logLocalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logLocalToolStripMenuItem.Text = "Log Local";
            this.logLocalToolStripMenuItem.Click += new System.EventHandler(this.logLocalToolStripMenuItem_Click);
            // 
            // logAzureToolStripMenuItem
            // 
            this.logAzureToolStripMenuItem.Name = "logAzureToolStripMenuItem";
            this.logAzureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logAzureToolStripMenuItem.Text = "Log Azure";
            this.logAzureToolStripMenuItem.Click += new System.EventHandler(this.logAzureToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(392, 449);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.num_hora);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Sync Frigobom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_hora)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportarDadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toAzureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportaçãoToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem conexãoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem firebirdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown num_hora;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon mynotifyicon;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem logLocalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logAzureToolStripMenuItem;
    }
}

