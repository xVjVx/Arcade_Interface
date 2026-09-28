namespace Arcade_Interface
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDireitaContainer = new System.Windows.Forms.Panel();
            this.panelDetalhesJogo = new System.Windows.Forms.Panel();
            this.lblDescricaoJogo = new System.Windows.Forms.Label();
            this.lblTagsJogo = new System.Windows.Forms.Label();
            this.picIconeJogo = new System.Windows.Forms.PictureBox();
            this.panelImagemFundo = new HighQualityPictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelDireitaContainer.SuspendLayout();
            this.panelDetalhesJogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconeJogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelImagemFundo)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelDireitaContainer, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(947, 512);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(150, 100, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(514, 506);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panelDireitaContainer
            // 
            this.panelDireitaContainer.AccessibleName = "panelDireitaContainer";
            this.panelDireitaContainer.Controls.Add(this.panelDetalhesJogo);
            this.panelDireitaContainer.Controls.Add(this.panelImagemFundo);
            this.panelDireitaContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDireitaContainer.Location = new System.Drawing.Point(520, 0);
            this.panelDireitaContainer.Margin = new System.Windows.Forms.Padding(0);
            this.panelDireitaContainer.Name = "panelDireitaContainer";
            this.panelDireitaContainer.Size = new System.Drawing.Size(427, 512);
            this.panelDireitaContainer.TabIndex = 1;
            // 
            // panelDetalhesJogo
            // 
            this.panelDetalhesJogo.Controls.Add(this.lblDescricaoJogo);
            this.panelDetalhesJogo.Controls.Add(this.lblTagsJogo);
            this.panelDetalhesJogo.Controls.Add(this.picIconeJogo);
            this.panelDetalhesJogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalhesJogo.Location = new System.Drawing.Point(0, 0);
            this.panelDetalhesJogo.Name = "panelDetalhesJogo";
            this.panelDetalhesJogo.Size = new System.Drawing.Size(427, 512);
            this.panelDetalhesJogo.TabIndex = 1;
            // 
            // lblDescricaoJogo
            // 
            this.lblDescricaoJogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescricaoJogo.AutoEllipsis = true;
            this.lblDescricaoJogo.Font = new System.Drawing.Font("Press Start 2P", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoJogo.Location = new System.Drawing.Point(3, 316);
            this.lblDescricaoJogo.Name = "lblDescricaoJogo";
            this.lblDescricaoJogo.Size = new System.Drawing.Size(421, 193);
            this.lblDescricaoJogo.TabIndex = 2;
            this.lblDescricaoJogo.Text = "label1";
            this.lblDescricaoJogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTagsJogo
            // 
            this.lblTagsJogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTagsJogo.Font = new System.Drawing.Font("Press Start 2P", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagsJogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(149)))), ((int)(((byte)(219)))));
            this.lblTagsJogo.Location = new System.Drawing.Point(3, 265);
            this.lblTagsJogo.Name = "lblTagsJogo";
            this.lblTagsJogo.Size = new System.Drawing.Size(421, 26);
            this.lblTagsJogo.TabIndex = 1;
            this.lblTagsJogo.Text = "label1";
            this.lblTagsJogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picIconeJogo
            // 
            this.picIconeJogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picIconeJogo.Location = new System.Drawing.Point(3, 3);
            this.picIconeJogo.Name = "picIconeJogo";
            this.picIconeJogo.Size = new System.Drawing.Size(424, 209);
            this.picIconeJogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconeJogo.TabIndex = 0;
            this.picIconeJogo.TabStop = false;
            // 
            // panelImagemFundo
            // 
            this.panelImagemFundo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImagemFundo.Image = global::Arcade_Interface.Properties.Resources.iscte_logo;
            this.panelImagemFundo.Location = new System.Drawing.Point(0, 0);
            this.panelImagemFundo.Name = "panelImagemFundo";
            this.panelImagemFundo.Size = new System.Drawing.Size(427, 512);
            this.panelImagemFundo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.panelImagemFundo.TabIndex = 0;
            this.panelImagemFundo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.ClientSize = new System.Drawing.Size(947, 512);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelDireitaContainer.ResumeLayout(false);
            this.panelDetalhesJogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIconeJogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelImagemFundo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelDireitaContainer;
        private HighQualityPictureBox panelImagemFundo;
        private System.Windows.Forms.Panel panelDetalhesJogo;
        private System.Windows.Forms.PictureBox picIconeJogo;
        private System.Windows.Forms.Label lblTagsJogo;
        private System.Windows.Forms.Label lblDescricaoJogo;
    }
}

