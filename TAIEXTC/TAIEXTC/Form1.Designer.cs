namespace TAIEXTC
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
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
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
            this.labTAIEXTC = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labTAIEXTC
            // 
            this.labTAIEXTC.AutoSize = true;
            this.labTAIEXTC.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labTAIEXTC.Location = new System.Drawing.Point(79, 22);
            this.labTAIEXTC.Name = "labTAIEXTC";
            this.labTAIEXTC.Size = new System.Drawing.Size(108, 19);
            this.labTAIEXTC.TabIndex = 0;
            this.labTAIEXTC.Text = "labTAIEXTC";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 62);
            this.Controls.Add(this.labTAIEXTC);
            this.Name = "Form1";
            this.Text = "TAIEXTC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTAIEXTC;
    }
}

