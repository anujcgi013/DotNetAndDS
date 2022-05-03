namespace CashTest
{
    partial class frmPopCashExchanger
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblIncome = new System.Windows.Forms.Label();
            this.SP = new System.IO.Ports.SerialPort(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWon2 = new System.Windows.Forms.Label();
            this.lblWon = new System.Windows.Forms.Label();
            this.lblInputAmount = new System.Windows.Forms.Label();
            this.lblPaymentAmount = new System.Windows.Forms.Label();
            this.lblCautionCash2 = new System.Windows.Forms.Label();
            this.lblCautionCash = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("굴림", 20F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(704, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 99);
            this.btnCancel.TabIndex = 180;
            this.btnCancel.Text = "Back";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblDestination
            // 
            this.lblDestination.BackColor = System.Drawing.Color.Transparent;
            this.lblDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDestination.Font = new System.Drawing.Font("맑은 고딕", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblDestination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(176)))), ((int)(((byte)(23)))));
            this.lblDestination.Location = new System.Drawing.Point(512, 145);
            this.lblDestination.Margin = new System.Windows.Forms.Padding(0);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(180, 48);
            this.lblDestination.TabIndex = 179;
            this.lblDestination.Text = "999,999";
            this.lblDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIncome
            // 
            this.lblIncome.BackColor = System.Drawing.Color.Transparent;
            this.lblIncome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIncome.Font = new System.Drawing.Font("맑은 고딕", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblIncome.ForeColor = System.Drawing.Color.White;
            this.lblIncome.Location = new System.Drawing.Point(511, 209);
            this.lblIncome.Margin = new System.Windows.Forms.Padding(0);
            this.lblIncome.Name = "lblIncome";
            this.lblIncome.Size = new System.Drawing.Size(181, 49);
            this.lblIncome.TabIndex = 178;
            this.lblIncome.Text = "999,999";
            this.lblIncome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(435, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 60);
            this.label2.TabIndex = 188;
            this.label2.Text = ":";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.ForeColor = System.Drawing.Color.Goldenrod;
            this.label1.Location = new System.Drawing.Point(435, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 60);
            this.label1.TabIndex = 187;
            this.label1.Text = ":";
            this.label1.Visible = false;
            // 
            // lblWon2
            // 
            this.lblWon2.BackColor = System.Drawing.Color.Transparent;
            this.lblWon2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWon2.Font = new System.Drawing.Font("나눔고딕", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblWon2.ForeColor = System.Drawing.Color.White;
            this.lblWon2.Location = new System.Drawing.Point(687, 211);
            this.lblWon2.Margin = new System.Windows.Forms.Padding(0);
            this.lblWon2.Name = "lblWon2";
            this.lblWon2.Size = new System.Drawing.Size(74, 61);
            this.lblWon2.TabIndex = 186;
            this.lblWon2.Text = "won";
            this.lblWon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWon2.Visible = false;
            // 
            // lblWon
            // 
            this.lblWon.BackColor = System.Drawing.Color.Transparent;
            this.lblWon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWon.Font = new System.Drawing.Font("나눔고딕", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblWon.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblWon.Location = new System.Drawing.Point(687, 145);
            this.lblWon.Margin = new System.Windows.Forms.Padding(0);
            this.lblWon.Name = "lblWon";
            this.lblWon.Size = new System.Drawing.Size(74, 61);
            this.lblWon.TabIndex = 185;
            this.lblWon.Text = "won";
            this.lblWon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWon.Visible = false;
            // 
            // lblInputAmount
            // 
            this.lblInputAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblInputAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInputAmount.Font = new System.Drawing.Font("나눔고딕", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblInputAmount.ForeColor = System.Drawing.Color.White;
            this.lblInputAmount.Location = new System.Drawing.Point(226, 209);
            this.lblInputAmount.Margin = new System.Windows.Forms.Padding(0);
            this.lblInputAmount.Name = "lblInputAmount";
            this.lblInputAmount.Size = new System.Drawing.Size(203, 61);
            this.lblInputAmount.TabIndex = 184;
            this.lblInputAmount.Text = "Input amount";
            this.lblInputAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInputAmount.Visible = false;
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPaymentAmount.Font = new System.Drawing.Font("나눔고딕", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblPaymentAmount.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblPaymentAmount.Location = new System.Drawing.Point(226, 131);
            this.lblPaymentAmount.Margin = new System.Windows.Forms.Padding(0);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(203, 78);
            this.lblPaymentAmount.TabIndex = 183;
            this.lblPaymentAmount.Text = "Payment amount";
            this.lblPaymentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPaymentAmount.Visible = false;
            // 
            // lblCautionCash2
            // 
            this.lblCautionCash2.BackColor = System.Drawing.Color.Transparent;
            this.lblCautionCash2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCautionCash2.Font = new System.Drawing.Font("나눔고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCautionCash2.ForeColor = System.Drawing.Color.White;
            this.lblCautionCash2.Location = new System.Drawing.Point(438, 386);
            this.lblCautionCash2.Margin = new System.Windows.Forms.Padding(0);
            this.lblCautionCash2.Name = "lblCautionCash2";
            this.lblCautionCash2.Size = new System.Drawing.Size(245, 117);
            this.lblCautionCash2.TabIndex = 182;
            this.lblCautionCash2.Text = "当該機器は紙幣を投入可能であり、投入口にコインまたは異物が投入される場合、その他の故障の原因になることがあります。";
            this.lblCautionCash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCautionCash2.Visible = false;
            // 
            // lblCautionCash
            // 
            this.lblCautionCash.BackColor = System.Drawing.Color.Transparent;
            this.lblCautionCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCautionCash.Font = new System.Drawing.Font("나눔고딕", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblCautionCash.ForeColor = System.Drawing.Color.White;
            this.lblCautionCash.Location = new System.Drawing.Point(100, 315);
            this.lblCautionCash.Margin = new System.Windows.Forms.Padding(0);
            this.lblCautionCash.Name = "lblCautionCash";
            this.lblCautionCash.Size = new System.Drawing.Size(418, 76);
            this.lblCautionCash.TabIndex = 181;
            this.lblCautionCash.Text = "No coins allowed.";
            this.lblCautionCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCautionCash.Visible = false;
            // 
            // frmPopCashExchanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1005, 532);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblIncome);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblWon2);
            this.Controls.Add(this.lblWon);
            this.Controls.Add(this.lblInputAmount);
            this.Controls.Add(this.lblPaymentAmount);
            this.Controls.Add(this.lblCautionCash2);
            this.Controls.Add(this.lblCautionCash);
            this.Name = "frmPopCashExchanger";
            this.Text = "frmPopCashExchanger";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPopCashExchanger_FormClosed);
            this.Load += new System.EventHandler(this.frmPopCashExchanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblIncome;
        private System.IO.Ports.SerialPort SP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWon2;
        private System.Windows.Forms.Label lblWon;
        private System.Windows.Forms.Label lblInputAmount;
        private System.Windows.Forms.Label lblPaymentAmount;
        private System.Windows.Forms.Label lblCautionCash2;
        private System.Windows.Forms.Label lblCautionCash;
    }
}