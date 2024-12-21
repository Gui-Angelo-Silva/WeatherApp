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
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeatherForm));
			lblCity = new Label();
			lblDescription = new Label();
			dgvForecast = new DataGridView();
			lblTemperature = new Label();
			panelForecast = new Panel();
			btnOtherView = new Button();
			((System.ComponentModel.ISupportInitialize)dgvForecast).BeginInit();
			panelForecast.SuspendLayout();
			SuspendLayout();
			// 
			// lblCity
			// 
			lblCity.AutoSize = true;
			lblCity.Font = new Font("Segoe UI", 12F);
			lblCity.ForeColor = Color.FromArgb(0, 123, 255);
			lblCity.Location = new Point(12, 9);
			lblCity.Name = "lblCity";
			lblCity.Size = new Size(83, 21);
			lblCity.TabIndex = 0;
			lblCity.Text = "Cidade: ---";
			// 
			// lblDescription
			// 
			lblDescription.AutoSize = true;
			lblDescription.Font = new Font("Segoe UI", 12F);
			lblDescription.ForeColor = Color.FromArgb(40, 167, 69);
			lblDescription.Location = new Point(12, 40);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(115, 21);
			lblDescription.TabIndex = 1;
			lblDescription.Text = "Clima Atual: ---";
			// 
			// dgvForecast
			// 
			dataGridViewCellStyle1.BackColor = Color.LightBlue;
			dgvForecast.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dgvForecast.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvForecast.BackgroundColor = Color.White;
			dgvForecast.BorderStyle = BorderStyle.None;
			dgvForecast.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvForecast.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			dgvForecast.DefaultCellStyle = dataGridViewCellStyle2;
			dgvForecast.GridColor = Color.LightGray;
			dgvForecast.Location = new Point(0, 0);
			dgvForecast.Name = "dgvForecast";
			dgvForecast.RowHeadersWidth = 51;
			dgvForecast.RowTemplate.Height = 29;
			dgvForecast.Size = new Size(784, 358);
			dgvForecast.TabIndex = 2;
			// 
			// lblTemperature
			// 
			lblTemperature.AutoSize = true;
			lblTemperature.Font = new Font("Segoe UI", 12F);
			lblTemperature.ForeColor = Color.FromArgb(255, 87, 34);
			lblTemperature.Location = new Point(196, 9);
			lblTemperature.Name = "lblTemperature";
			lblTemperature.Size = new Size(122, 21);
			lblTemperature.TabIndex = 3;
			lblTemperature.Text = "Temperatura: ---";
			// 
			// panelForecast
			// 
			panelForecast.AutoScroll = true;
			panelForecast.Controls.Add(dgvForecast);
			panelForecast.Location = new Point(12, 80);
			panelForecast.Name = "panelForecast";
			panelForecast.Size = new Size(784, 358);
			panelForecast.TabIndex = 4;
			// 
			// btnOtherView
			// 
			btnOtherView.Cursor = Cursors.Hand;
			btnOtherView.Location = new Point(701, 12);
			btnOtherView.Name = "btnOtherView";
			btnOtherView.Size = new Size(95, 27);
			btnOtherView.TabIndex = 5;
			btnOtherView.Text = "Aprimorado";
			btnOtherView.UseVisualStyleBackColor = true;
			// 
			// WeatherForm
			// 
			BackColor = Color.FromArgb(248, 249, 250);
			ClientSize = new Size(808, 461);
			Controls.Add(btnOtherView);
			Controls.Add(lblTemperature);
			Controls.Add(panelForecast);
			Controls.Add(lblDescription);
			Controls.Add(lblCity);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "WeatherForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Previsão do Tempo";
			Load += WeatherForm_Load;
			((System.ComponentModel.ISupportInitialize)dgvForecast).EndInit();
			panelForecast.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblTemperature;
		private Panel panelForecast;
		private Button btnOtherView;
	}
}
