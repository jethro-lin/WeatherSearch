namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_country = new System.Windows.Forms.ComboBox();
            this.comboBox_town = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_time = new System.Windows.Forms.Label();
            this.label_temperature = new System.Windows.Forms.Label();
            this.label_body_temperature = new System.Windows.Forms.Label();
            this.label_humidity = new System.Windows.Forms.Label();
            this.label_precipitation = new System.Windows.Forms.Label();
            this.label_sunrise = new System.Windows.Forms.Label();
            this.label_sunset = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_country
            // 
            this.comboBox_country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_country.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_country.FormattingEnabled = true;
            this.comboBox_country.Location = new System.Drawing.Point(46, 12);
            this.comboBox_country.Name = "comboBox_country";
            this.comboBox_country.Size = new System.Drawing.Size(212, 32);
            this.comboBox_country.TabIndex = 0;
            this.comboBox_country.SelectedIndexChanged += new System.EventHandler(this.comboBox_country_SelectedIndexChanged);
            // 
            // comboBox_town
            // 
            this.comboBox_town.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_town.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_town.FormattingEnabled = true;
            this.comboBox_town.Location = new System.Drawing.Point(264, 12);
            this.comboBox_town.Name = "comboBox_town";
            this.comboBox_town.Size = new System.Drawing.Size(218, 32);
            this.comboBox_town.TabIndex = 1;
            this.comboBox_town.SelectedIndexChanged += new System.EventHandler(this.comboBox_town_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label_time, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_temperature, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_body_temperature, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_humidity, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_precipitation, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_sunrise, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_sunset, 3, 1);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(46, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 181);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_time.Location = new System.Drawing.Point(3, 0);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(0, 24);
            this.label_time.TabIndex = 0;
            // 
            // label_temperature
            // 
            this.label_temperature.AutoSize = true;
            this.label_temperature.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_temperature.Location = new System.Drawing.Point(221, 0);
            this.label_temperature.Name = "label_temperature";
            this.label_temperature.Size = new System.Drawing.Size(0, 24);
            this.label_temperature.TabIndex = 1;
            // 
            // label_body_temperature
            // 
            this.label_body_temperature.AutoSize = true;
            this.label_body_temperature.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_body_temperature.Location = new System.Drawing.Point(330, 0);
            this.label_body_temperature.Name = "label_body_temperature";
            this.label_body_temperature.Size = new System.Drawing.Size(0, 24);
            this.label_body_temperature.TabIndex = 2;
            // 
            // label_humidity
            // 
            this.label_humidity.AutoSize = true;
            this.label_humidity.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_humidity.Location = new System.Drawing.Point(3, 90);
            this.label_humidity.Name = "label_humidity";
            this.label_humidity.Size = new System.Drawing.Size(0, 24);
            this.label_humidity.TabIndex = 3;
            // 
            // label_precipitation
            // 
            this.label_precipitation.AutoSize = true;
            this.label_precipitation.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_precipitation.Location = new System.Drawing.Point(112, 90);
            this.label_precipitation.Name = "label_precipitation";
            this.label_precipitation.Size = new System.Drawing.Size(0, 24);
            this.label_precipitation.TabIndex = 4;
            // 
            // label_sunrise
            // 
            this.label_sunrise.AutoSize = true;
            this.label_sunrise.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_sunrise.Location = new System.Drawing.Point(221, 90);
            this.label_sunrise.Name = "label_sunrise";
            this.label_sunrise.Size = new System.Drawing.Size(0, 24);
            this.label_sunrise.TabIndex = 5;
            // 
            // label_sunset
            // 
            this.label_sunset.AutoSize = true;
            this.label_sunset.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_sunset.Location = new System.Drawing.Point(330, 90);
            this.label_sunset.Name = "label_sunset";
            this.label_sunset.Size = new System.Drawing.Size(0, 24);
            this.label_sunset.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 303);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboBox_town);
            this.Controls.Add(this.comboBox_country);
            this.Name = "Form1";
            this.Text = "即時天氣查詢";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_country;
        private System.Windows.Forms.ComboBox comboBox_town;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label_temperature;
        private System.Windows.Forms.Label label_body_temperature;
        private System.Windows.Forms.Label label_humidity;
        private System.Windows.Forms.Label label_precipitation;
        private System.Windows.Forms.Label label_sunrise;
        private System.Windows.Forms.Label label_sunset;
    }
}

