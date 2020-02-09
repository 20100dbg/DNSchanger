namespace DNSchanger
{
    partial class DNSchanger
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_curPri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_curSec = new System.Windows.Forms.TextBox();
            this.cb_predefinedDNS = new System.Windows.Forms.ComboBox();
            this.b_save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_interfaces = new System.Windows.Forms.ComboBox();
            this.link = new System.Windows.Forms.LinkLabel();
            this.l_description = new System.Windows.Forms.TextBox();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.tb_newSec = new System.Windows.Forms.TextBox();
            this.tb_newPri = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.b_saveAuto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_curPri
            // 
            this.tb_curPri.Location = new System.Drawing.Point(91, 66);
            this.tb_curPri.Name = "tb_curPri";
            this.tb_curPri.Size = new System.Drawing.Size(108, 20);
            this.tb_curPri.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Primary DNS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Secondary DNS";
            // 
            // tb_curSec
            // 
            this.tb_curSec.Location = new System.Drawing.Point(91, 104);
            this.tb_curSec.Name = "tb_curSec";
            this.tb_curSec.Size = new System.Drawing.Size(108, 20);
            this.tb_curSec.TabIndex = 2;
            // 
            // cb_predefinedDNS
            // 
            this.cb_predefinedDNS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_predefinedDNS.FormattingEnabled = true;
            this.cb_predefinedDNS.Location = new System.Drawing.Point(462, 12);
            this.cb_predefinedDNS.Name = "cb_predefinedDNS";
            this.cb_predefinedDNS.Size = new System.Drawing.Size(111, 21);
            this.cb_predefinedDNS.TabIndex = 4;
            this.cb_predefinedDNS.SelectedIndexChanged += new System.EventHandler(this.cb_predefinedDNS_SelectedIndexChanged);
            // 
            // b_save
            // 
            this.b_save.Location = new System.Drawing.Point(268, 149);
            this.b_save.Name = "b_save";
            this.b_save.Size = new System.Drawing.Size(75, 23);
            this.b_save.TabIndex = 5;
            this.b_save.Text = "Apply";
            this.b_save.UseVisualStyleBackColor = true;
            this.b_save.Click += new System.EventHandler(this.b_save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Interface";
            // 
            // cb_interfaces
            // 
            this.cb_interfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_interfaces.FormattingEnabled = true;
            this.cb_interfaces.Location = new System.Drawing.Point(91, 12);
            this.cb_interfaces.Name = "cb_interfaces";
            this.cb_interfaces.Size = new System.Drawing.Size(192, 21);
            this.cb_interfaces.TabIndex = 7;
            this.cb_interfaces.SelectedIndexChanged += new System.EventHandler(this.cb_interfaces_SelectedIndexChanged);
            // 
            // link
            // 
            this.link.AutoSize = true;
            this.link.Location = new System.Drawing.Point(474, 159);
            this.link.Name = "link";
            this.link.Size = new System.Drawing.Size(99, 13);
            this.link.TabIndex = 9;
            this.link.TabStop = true;
            this.link.Text = "Moar DNS services";
            // 
            // l_description
            // 
            this.l_description.Location = new System.Drawing.Point(383, 66);
            this.l_description.Multiline = true;
            this.l_description.Name = "l_description";
            this.l_description.ReadOnly = true;
            this.l_description.Size = new System.Drawing.Size(190, 57);
            this.l_description.TabIndex = 10;
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(91, 201);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(482, 50);
            this.tb_log.TabIndex = 11;
            // 
            // tb_newSec
            // 
            this.tb_newSec.Location = new System.Drawing.Point(235, 104);
            this.tb_newSec.Name = "tb_newSec";
            this.tb_newSec.Size = new System.Drawing.Size(108, 20);
            this.tb_newSec.TabIndex = 13;
            // 
            // tb_newPri
            // 
            this.tb_newPri.Location = new System.Drawing.Point(235, 66);
            this.tb_newPri.Name = "tb_newPri";
            this.tb_newPri.Size = new System.Drawing.Size(108, 20);
            this.tb_newPri.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Current";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "New";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(384, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "DNS services";
            // 
            // b_saveAuto
            // 
            this.b_saveAuto.Location = new System.Drawing.Point(91, 149);
            this.b_saveAuto.Name = "b_saveAuto";
            this.b_saveAuto.Size = new System.Drawing.Size(117, 23);
            this.b_saveAuto.TabIndex = 17;
            this.b_saveAuto.Text = "Set default (DHCP)";
            this.b_saveAuto.UseVisualStyleBackColor = true;
            this.b_saveAuto.Click += new System.EventHandler(this.b_saveAuto_Click);
            // 
            // DNSchanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 269);
            this.Controls.Add(this.b_saveAuto);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_newSec);
            this.Controls.Add(this.tb_newPri);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.l_description);
            this.Controls.Add(this.link);
            this.Controls.Add(this.cb_interfaces);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.b_save);
            this.Controls.Add(this.cb_predefinedDNS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_curSec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_curPri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DNSchanger";
            this.Text = "DNSchanger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_curPri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_curSec;
        private System.Windows.Forms.ComboBox cb_predefinedDNS;
        private System.Windows.Forms.Button b_save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_interfaces;
        private System.Windows.Forms.LinkLabel link;
        private System.Windows.Forms.TextBox l_description;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.TextBox tb_newSec;
        private System.Windows.Forms.TextBox tb_newPri;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button b_saveAuto;
    }
}

