
namespace Client
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGroupID = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lsvMessage = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listMyMessage = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbCallTime = new System.Windows.Forms.Label();
            this.timerCall = new System.Windows.Forms.Timer(this.components);
            this.btnEndCall = new System.Windows.Forms.Button();
            this.btnCall = new System.Windows.Forms.Button();
            this.btnvoice = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(192, 29);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(236, 26);
            this.txtName.TabIndex = 0;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(79, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nick name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Group ID:";
            // 
            // txtGroupID
            // 
            this.txtGroupID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGroupID.Location = new System.Drawing.Point(192, 75);
            this.txtGroupID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGroupID.Name = "txtGroupID";
            this.txtGroupID.Size = new System.Drawing.Size(80, 26);
            this.txtGroupID.TabIndex = 3;
            this.txtGroupID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Location = new System.Drawing.Point(348, 75);
            this.txtID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(80, 26);
            this.txtID.TabIndex = 4;
            this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "ID:";
            // 
            // lsvMessage
            // 
            this.lsvMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvMessage.HideSelection = false;
            this.lsvMessage.Location = new System.Drawing.Point(4, 5);
            this.lsvMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lsvMessage.MultiSelect = false;
            this.lsvMessage.Name = "lsvMessage";
            this.lsvMessage.Size = new System.Drawing.Size(322, 377);
            this.lsvMessage.SmallImageList = this.imageList1;
            this.lsvMessage.TabIndex = 6;
            this.lsvMessage.UseCompatibleStateImageBehavior = false;
            this.lsvMessage.View = System.Windows.Forms.View.List;
            this.lsvMessage.Click += new System.EventHandler(this.lsvMessage_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8_play_64px_1.png");
            this.imageList1.Images.SetKeyName(1, "icons8_person_64px.png");
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Location = new System.Drawing.Point(12, 550);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(420, 26);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Lime;
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(505, 46);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 40);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "JOIN";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listMyMessage
            // 
            this.listMyMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listMyMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMyMessage.HideSelection = false;
            this.listMyMessage.Location = new System.Drawing.Point(334, 5);
            this.listMyMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listMyMessage.MultiSelect = false;
            this.listMyMessage.Name = "listMyMessage";
            this.listMyMessage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listMyMessage.RightToLeftLayout = true;
            this.listMyMessage.Size = new System.Drawing.Size(322, 377);
            this.listMyMessage.SmallImageList = this.imageList1;
            this.listMyMessage.TabIndex = 11;
            this.listMyMessage.UseCompatibleStateImageBehavior = false;
            this.listMyMessage.View = System.Windows.Forms.View.List;
            this.listMyMessage.Click += new System.EventHandler(this.lsvMessage_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lsvMessage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listMyMessage, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 140);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(660, 387);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // lbCallTime
            // 
            this.lbCallTime.AutoSize = true;
            this.lbCallTime.Location = new System.Drawing.Point(545, 118);
            this.lbCallTime.Name = "lbCallTime";
            this.lbCallTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbCallTime.Size = new System.Drawing.Size(91, 19);
            this.lbCallTime.TabIndex = 14;
            this.lbCallTime.Text = "Pending 0:0:0";
            this.lbCallTime.Visible = false;
            // 
            // timerCall
            // 
            this.timerCall.Interval = 1000;
            this.timerCall.Tick += new System.EventHandler(this.timerCall_Tick);
            // 
            // btnEndCall
            // 
            this.btnEndCall.BackColor = System.Drawing.Color.Red;
            this.btnEndCall.BackgroundImage = global::Client.Properties.Resources.icons8_delete_32px1;
            this.btnEndCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEndCall.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEndCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEndCall.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndCall.Location = new System.Drawing.Point(643, 108);
            this.btnEndCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Size = new System.Drawing.Size(29, 29);
            this.btnEndCall.TabIndex = 15;
            this.btnEndCall.UseVisualStyleBackColor = false;
            this.btnEndCall.Visible = false;
            this.btnEndCall.Click += new System.EventHandler(this.btnEndCall_Click);
            // 
            // btnCall
            // 
            this.btnCall.BackColor = System.Drawing.Color.Transparent;
            this.btnCall.BackgroundImage = global::Client.Properties.Resources.icons8_outgoing_call_48px;
            this.btnCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCall.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;
            this.btnCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCall.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall.Location = new System.Drawing.Point(632, 546);
            this.btnCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(40, 40);
            this.btnCall.TabIndex = 13;
            this.btnCall.UseVisualStyleBackColor = false;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // btnvoice
            // 
            this.btnvoice.BackColor = System.Drawing.Color.Transparent;
            this.btnvoice.BackgroundImage = global::Client.Properties.Resources.Mic;
            this.btnvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnvoice.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;
            this.btnvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnvoice.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnvoice.Location = new System.Drawing.Point(565, 546);
            this.btnvoice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnvoice.Name = "btnvoice";
            this.btnvoice.Size = new System.Drawing.Size(40, 40);
            this.btnvoice.TabIndex = 9;
            this.btnvoice.UseVisualStyleBackColor = false;
            this.btnvoice.Click += new System.EventHandler(this.btnvoice_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackgroundImage = global::Client.Properties.Resources.Send;
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSend.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;
            this.btnSend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(486, 546);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(50, 40);
            this.btnSend.TabIndex = 8;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(684, 611);
            this.Controls.Add(this.btnEndCall);
            this.Controls.Add(this.lbCallTime);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnvoice);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtGroupID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGroupID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lsvMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnvoice;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listMyMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Label lbCallTime;
        private System.Windows.Forms.Timer timerCall;
        private System.Windows.Forms.Button btnEndCall;
    }
}

