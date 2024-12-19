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
			((System.ComponentModel.ISupportInitialize)dgvForecast).BeginInit();
			SuspendLayout();
			// 
			// lblCity
			// 
			lblCity.AutoSize = true;
			lblCity.Location = new Point(12, 9);
			lblCity.Name = "lblCity";
			lblCity.Size = new Size(65, 15);
			lblCity.TabIndex = 0;
			lblCity.Text = "Cidade: ---";
			// 
			// lblDescription
			// 
			lblDescription.AutoSize = true;
			lblDescription.Location = new Point(12, 40);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(90, 15);
			lblDescription.TabIndex = 1;
			lblDescription.Text = "Clima Atual: ---";
			// 
			// dgvForecast
			// 
			dgvForecast.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvForecast.Location = new Point(12, 80);
			dgvForecast.Name = "dgvForecast";
			dgvForecast.RowHeadersWidth = 51;
			dgvForecast.RowTemplate.Height = 29;
			dgvForecast.Size = new Size(760, 358);
			dgvForecast.TabIndex = 2;
			// 
			// lblTemperature
			// 
			lblTemperature.AutoSize = true;
			lblTemperature.Location = new Point(196, 9);
			lblTemperature.Name = "lblTemperature";
			lblTemperature.Size = new Size(94, 15);
			lblTemperature.TabIndex = 3;
			lblTemperature.Text = "Temperatura: ---";
			// 
			// WeatherForm
			// 
			ClientSize = new Size(784, 461);
			Controls.Add(lblTemperature);
			Controls.Add(dgvForecast);
			Controls.Add(lblDescription);
			Controls.Add(lblCity);
			Name = "WeatherForm";
			Text = "Previsão do Tempo";
			Load += WeatherForm_Load;
			((System.ComponentModel.ISupportInitialize)dgvForecast).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblTemperature;
	}
}
