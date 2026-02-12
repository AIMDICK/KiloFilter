using System;
using System.Windows.Forms;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    public partial class CacheDecisionForm : Form
    {
        public DialogResult CacheDecision { get; private set; }

        public CacheDecisionForm(string message, string title, bool hasChanged)
        {
            InitializeComponent();
            
            this.Text = title;
            if (lblMessage != null)
                lblMessage.Text = message;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Configurar iconos según si ha cambiado o no
            if (pictureBox1 != null)
            {
                if (hasChanged)
                {
                    pictureBox1.Image = SystemIcons.Question.ToBitmap();
                }
                else
                {
                    pictureBox1.Image = SystemIcons.Information.ToBitmap();
                }
            }
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnUseCache = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            
            // pictureBox1
            this.pictureBox1.Location = new System.Drawing.Point(20, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            
            // lblMessage
            this.lblMessage.AutoSize = false;
            this.lblMessage.Location = new System.Drawing.Point(70, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(310, 80);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "";
            
            // btnUseCache
            this.btnUseCache.Location = new System.Drawing.Point(70, 110);
            this.btnUseCache.Name = "btnUseCache";
            this.btnUseCache.Size = new System.Drawing.Size(130, 35);
            this.btnUseCache.TabIndex = 2;
            this.btnUseCache.Text = Localization.Get("CACHE_BTN_USE_PREVIOUS");
            this.btnUseCache.UseVisualStyleBackColor = true;
            this.btnUseCache.Click += (s, e) => { this.CacheDecision = DialogResult.No; this.DialogResult = System.Windows.Forms.DialogResult.OK; this.Close(); };
            
            // btnRedo
            this.btnRedo.Location = new System.Drawing.Point(210, 110);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(130, 35);
            this.btnRedo.TabIndex = 3;
            this.btnRedo.Text = Localization.Get("CACHE_BTN_REDO");
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += (s, e) => { this.CacheDecision = DialogResult.Yes; this.DialogResult = System.Windows.Forms.DialogResult.OK; this.Close(); };
            
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(350, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = Localization.Get("CACHE_BTN_CANCEL");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += (s, e) => { this.CacheDecision = DialogResult.Cancel; this.DialogResult = System.Windows.Forms.DialogResult.OK; this.Close(); };
            
            // CacheDecisionForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 160);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUseCache);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CacheDecisionForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox? pictureBox1;
        private System.Windows.Forms.Label? lblMessage;
        private System.Windows.Forms.Button? btnUseCache;
        private System.Windows.Forms.Button? btnRedo;
        private System.Windows.Forms.Button? btnCancel;
    }
}
