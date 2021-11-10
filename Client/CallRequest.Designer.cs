
namespace Client
{
    partial class CallRequest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnDeny = new System.Windows.Forms.Button();
            this.BtnAccept = new System.Windows.Forms.Button();
            this.lblRequest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnDeny
            // 
            this.BtnDeny.BackColor = System.Drawing.Color.Red;
            this.BtnDeny.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnDeny.Image = global::Client.Properties.Resources.icons8_silent_call_30px;
            this.BtnDeny.Location = new System.Drawing.Point(244, 184);
            this.BtnDeny.Name = "BtnDeny";
            this.BtnDeny.Size = new System.Drawing.Size(65, 52);
            this.BtnDeny.TabIndex = 1;
            this.BtnDeny.UseVisualStyleBackColor = false;
            this.BtnDeny.Click += new System.EventHandler(this.BtnDeny_Click);
            // 
            // BtnAccept
            // 
            this.BtnAccept.BackColor = System.Drawing.Color.Lime;
            this.BtnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAccept.Image = global::Client.Properties.Resources.icons8_phone_26px_1;
            this.BtnAccept.Location = new System.Drawing.Point(83, 184);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(65, 52);
            this.BtnAccept.TabIndex = 0;
            this.BtnAccept.UseVisualStyleBackColor = false;
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // lblRequest
            // 
            this.lblRequest.AutoSize = true;
            this.lblRequest.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequest.Location = new System.Drawing.Point(151, 56);
            this.lblRequest.Name = "lblRequest";
            this.lblRequest.Size = new System.Drawing.Size(70, 27);
            this.lblRequest.TabIndex = 2;
            this.lblRequest.Text = "label1";
            // 
            // CallRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(401, 297);
            this.Controls.Add(this.lblRequest);
            this.Controls.Add(this.BtnDeny);
            this.Controls.Add(this.BtnAccept);
            this.Name = "CallRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CallRequest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAccept;
        private System.Windows.Forms.Button BtnDeny;
        private System.Windows.Forms.Label lblRequest;
    }
}