namespace ShadowrunInitiative
{
    partial class CharacterInitLine
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.characterNameLabel = new System.Windows.Forms.Label();
            this.initiativeNumeric = new System.Windows.Forms.NumericUpDown();
            this.seizeCheckBox = new System.Windows.Forms.CheckBox();
            this.Wuerfelbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.initiativeNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // characterNameLabel
            // 
            this.characterNameLabel.Location = new System.Drawing.Point(5, 6);
            this.characterNameLabel.Name = "characterNameLabel";
            this.characterNameLabel.Size = new System.Drawing.Size(118, 13);
            this.characterNameLabel.TabIndex = 0;
            this.characterNameLabel.Text = "Char Name";
            this.characterNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // initiativeNumeric
            // 
            this.initiativeNumeric.Location = new System.Drawing.Point(129, 4);
            this.initiativeNumeric.Name = "initiativeNumeric";
            this.initiativeNumeric.Size = new System.Drawing.Size(62, 20);
            this.initiativeNumeric.TabIndex = 1;
            // 
            // seizeCheckBox
            // 
            this.seizeCheckBox.AutoSize = true;
            this.seizeCheckBox.Location = new System.Drawing.Point(197, 6);
            this.seizeCheckBox.Name = "seizeCheckBox";
            this.seizeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.seizeCheckBox.TabIndex = 2;
            this.seizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // Wuerfelbutton
            // 
            this.Wuerfelbutton.Location = new System.Drawing.Point(218, 4);
            this.Wuerfelbutton.Name = "Wuerfelbutton";
            this.Wuerfelbutton.Size = new System.Drawing.Size(75, 23);
            this.Wuerfelbutton.TabIndex = 3;
            this.Wuerfelbutton.Text = "Würfeln";
            this.Wuerfelbutton.UseVisualStyleBackColor = true;
            this.Wuerfelbutton.Click += new System.EventHandler(this.Wuerfelbutton_Click);
            // 
            // CharacterInitLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Wuerfelbutton);
            this.Controls.Add(this.seizeCheckBox);
            this.Controls.Add(this.initiativeNumeric);
            this.Controls.Add(this.characterNameLabel);
            this.Name = "CharacterInitLine";
            this.Size = new System.Drawing.Size(300, 34);
            ((System.ComponentModel.ISupportInitialize)(this.initiativeNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label characterNameLabel;
        private System.Windows.Forms.NumericUpDown initiativeNumeric;
        private System.Windows.Forms.CheckBox seizeCheckBox;
        private System.Windows.Forms.Button Wuerfelbutton;
    }
}
