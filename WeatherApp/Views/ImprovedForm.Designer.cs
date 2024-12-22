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
			lblTem = new Label();
			label1 = new Label();
			lblIcon = new PictureBox();
			lblTemp = new Label();
			lblDescription = new Label();
			pNavbar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)lblIcon).BeginInit();
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
			pNavbar.Controls.Add(lblTem);
			pNavbar.Controls.Add(label1);
			pNavbar.Location = new Point(1, 0);
			pNavbar.Name = "pNavbar";
			pNavbar.Size = new Size(742, 63);
			pNavbar.TabIndex = 1;
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
			// ImprovedForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(743, 404);
			Controls.Add(lblDescription);
			Controls.Add(lblTemp);
			Controls.Add(lblIcon);
			Controls.Add(pNavbar);
			Controls.Add(lblCity);
			Name = "ImprovedForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "ImprovedForm";
			Load += ImprovedForm_Load;
			pNavbar.ResumeLayout(false);
			pNavbar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)lblIcon).EndInit();
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
	}
}