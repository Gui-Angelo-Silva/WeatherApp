namespace WeatherApp
{
	partial class WeatherForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.Label lblCity;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.DataGridView dgvForecast;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblCity = new Label();
			lblDescription = new Label();
			dgvForecast = new DataGridView();
			lblTemperature = new Label();
			panelForecast = new Panel();
			((System.ComponentModel.ISupportInitialize)dgvForecast).BeginInit();
			SuspendLayout();

			// 
			// lblCity
			// 
			lblCity.AutoSize = true;
			lblCity.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lblCity.Location = new Point(12, 9);
			lblCity.Name = "lblCity";
			lblCity.Size = new Size(100, 21);
			lblCity.TabIndex = 0;
			lblCity.Text = "Cidade: ---";
			lblCity.ForeColor = Color.FromArgb(0, 123, 255);

			// 
			// lblDescription
			// 
			lblDescription.AutoSize = true;
			lblDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lblDescription.Location = new Point(12, 40);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(130, 21);
			lblDescription.TabIndex = 1;
			lblDescription.Text = "Clima Atual: ---";
			lblDescription.ForeColor = Color.FromArgb(40, 167, 69);

			// 
			// dgvForecast
			// 
			dgvForecast.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvForecast.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvForecast.Location = new Point(0, 0);
			dgvForecast.Name = "dgvForecast";
			dgvForecast.RowHeadersWidth = 51;
			dgvForecast.RowTemplate.Height = 29;
			dgvForecast.Size = new Size(784, 358);
			dgvForecast.TabIndex = 2;
			dgvForecast.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
			dgvForecast.BackgroundColor = Color.White;
			dgvForecast.BorderStyle = BorderStyle.None;
			dgvForecast.GridColor = Color.LightGray;
			dgvForecast.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvForecast.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

			// 
			// panelForecast
			// 
			panelForecast.AutoScroll = true;
			panelForecast.Location = new Point(12, 80);
			panelForecast.Name = "panelForecast";
			panelForecast.Size = new Size(784, 358);
			panelForecast.TabIndex = 4;
			panelForecast.Controls.Add(dgvForecast);

			// 
			// lblTemperature
			// 
			lblTemperature.AutoSize = true;
			lblTemperature.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lblTemperature.Location = new Point(196, 9);
			lblTemperature.Name = "lblTemperature";
			lblTemperature.Size = new Size(125, 21);
			lblTemperature.TabIndex = 3;
			lblTemperature.Text = "Temperatura: ---";
			lblTemperature.ForeColor = Color.FromArgb(255, 87, 34);

			// 
			// WeatherForm
			// 
			ClientSize = new Size(808, 461);
			Controls.Add(lblTemperature);
			Controls.Add(panelForecast);
			Controls.Add(lblDescription);
			Controls.Add(lblCity);
			Name = "WeatherForm";
			Text = "Previsão do Tempo";
			BackColor = Color.FromArgb(248, 249, 250);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			StartPosition = FormStartPosition.CenterScreen;
			Load += WeatherForm_Load;

			((System.ComponentModel.ISupportInitialize)dgvForecast).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblTemperature;
		private Panel panelForecast;
	}
}
