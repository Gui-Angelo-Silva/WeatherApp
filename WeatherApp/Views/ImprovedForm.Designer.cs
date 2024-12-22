namespace WeatherApp.Views
{
	partial class ImprovedForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImprovedForm));
			lblCity = new Label();
			pNavbar = new Panel();
			lblExit = new Label();
			lblTem = new Label();
			label1 = new Label();
			lblIcon = new PictureBox();
			lblTemp = new Label();
			lblDescription = new Label();
			lblHumidity = new Label();
			pictureBox1 = new PictureBox();
			pictureBox2 = new PictureBox();
			lblMax = new Label();
			lblMin = new Label();
			pNavbar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)lblIcon).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			SuspendLayout();
			// 
			// lblCity
			// 
			lblCity.AutoSize = true;
			lblCity.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblCity.Location = new Point(31, 89);
			lblCity.Name = "lblCity";
			lblCity.Size = new Size(66, 32);
			lblCity.TabIndex = 0;
			lblCity.Text = "Jales";
			// 
			// pNavbar
			// 
			pNavbar.AutoSize = true;
			pNavbar.BackColor = Color.DodgerBlue;
			pNavbar.Controls.Add(lblExit);
			pNavbar.Controls.Add(lblTem);
			pNavbar.Controls.Add(label1);
			pNavbar.Location = new Point(1, 0);
			pNavbar.Name = "pNavbar";
			pNavbar.Size = new Size(742, 63);
			pNavbar.TabIndex = 1;
			// 
			// lblExit
			// 
			lblExit.AutoSize = true;
			lblExit.Cursor = Cursors.Hand;
			lblExit.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblExit.ForeColor = Color.Snow;
			lblExit.Location = new Point(695, 11);
			lblExit.Name = "lblExit";
			lblExit.Size = new Size(35, 37);
			lblExit.TabIndex = 4;
			lblExit.Text = "X";
			lblExit.Click += lblExit_Click;
			// 
			// lblTem
			// 
			lblTem.Font = new Font("Segoe UI", 14F);
			lblTem.ForeColor = SystemColors.ControlLightLight;
			lblTem.Image = (Image)resources.GetObject("lblTem.Image");
			lblTem.Location = new Point(11, 9);
			lblTem.Name = "lblTem";
			lblTem.Size = new Size(48, 43);
			lblTem.TabIndex = 3;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.ControlLightLight;
			label1.Location = new Point(65, 11);
			label1.Name = "label1";
			label1.Size = new Size(242, 37);
			label1.TabIndex = 2;
			label1.Text = "Previsão do Tempo";
			// 
			// lblIcon
			// 
			lblIcon.Location = new Point(22, 130);
			lblIcon.Name = "lblIcon";
			lblIcon.Size = new Size(32, 32);
			lblIcon.SizeMode = PictureBoxSizeMode.AutoSize;
			lblIcon.TabIndex = 3;
			lblIcon.TabStop = false;
			// 
			// lblTemp
			// 
			lblTemp.AutoSize = true;
			lblTemp.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblTemp.Location = new Point(118, 142);
			lblTemp.Name = "lblTemp";
			lblTemp.Size = new Size(90, 41);
			lblTemp.TabIndex = 4;
			lblTemp.Text = "22 °C";
			// 
			// lblDescription
			// 
			lblDescription.AutoSize = true;
			lblDescription.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblDescription.ForeColor = SystemColors.WindowFrame;
			lblDescription.Location = new Point(118, 181);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(202, 25);
			lblDescription.TabIndex = 5;
			lblDescription.Text = "Parcialmente Nublado";
			// 
			// lblHumidity
			// 
			lblHumidity.AutoSize = true;
			lblHumidity.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHumidity.ForeColor = Color.Gray;
			lblHumidity.Location = new Point(517, 178);
			lblHumidity.Name = "lblHumidity";
			lblHumidity.Size = new Size(94, 28);
			lblHumidity.TabIndex = 6;
			lblHumidity.Text = "Umidade";
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(517, 104);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(30, 30);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 7;
			pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
			pictureBox2.Location = new Point(517, 141);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(30, 30);
			pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox2.TabIndex = 8;
			pictureBox2.TabStop = false;
			// 
			// lblMax
			// 
			lblMax.AutoSize = true;
			lblMax.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblMax.ForeColor = Color.Gray;
			lblMax.Location = new Point(553, 104);
			lblMax.Name = "lblMax";
			lblMax.Size = new Size(60, 28);
			lblMax.TabIndex = 9;
			lblMax.Text = "30 °C";
			// 
			// lblMin
			// 
			lblMin.AutoSize = true;
			lblMin.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblMin.ForeColor = Color.Gray;
			lblMin.Location = new Point(553, 141);
			lblMin.Name = "lblMin";
			lblMin.Size = new Size(60, 28);
			lblMin.TabIndex = 10;
			lblMin.Text = "20 °C";
			// 
			// ImprovedForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(743, 404);
			Controls.Add(lblMin);
			Controls.Add(lblMax);
			Controls.Add(pictureBox2);
			Controls.Add(pictureBox1);
			Controls.Add(lblHumidity);
			Controls.Add(lblDescription);
			Controls.Add(lblTemp);
			Controls.Add(lblIcon);
			Controls.Add(pNavbar);
			Controls.Add(lblCity);
			FormBorderStyle = FormBorderStyle.None;
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			Name = "ImprovedForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "ImprovedForm";
			Load += ImprovedForm_Load;
			pNavbar.ResumeLayout(false);
			pNavbar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)lblIcon).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblCity;
		private Panel pNavbar;
		private Label label1;
		private Label lblTem;
		private PictureBox lblIcon;
		private Label lblTemp;
		private Label lblDescription;
		private Label lblHumidity;
		private PictureBox pictureBox1;
		private PictureBox pictureBox2;
		private Label lblMax;
		private Label lblMin;
		private Label lblExit;
	}
}