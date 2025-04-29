namespace Dictionary
{
    partial class Dictionary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dictionary));
            this.SearchButton = new System.Windows.Forms.Button();
            this.WordBox = new System.Windows.Forms.TextBox();
            this.DictionaryDescription = new System.Windows.Forms.Label();
            this.EnglishButton = new System.Windows.Forms.RadioButton();
            this.RomanianButton = new System.Windows.Forms.RadioButton();
            this.Title = new System.Windows.Forms.Label();
            this.DefinitionBox = new System.Windows.Forms.TextBox();
            this.RomEngButton = new System.Windows.Forms.RadioButton();
            this.EngRomButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.AccessibleDescription = "Click to see word description";
            this.SearchButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SearchButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.SearchButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.SearchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.SearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButton.Location = new System.Drawing.Point(420, 107);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(93, 52);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.WhenClick_Search);
            // 
            // WordBox
            // 
            this.WordBox.Location = new System.Drawing.Point(227, 118);
            this.WordBox.Name = "WordBox";
            this.WordBox.Size = new System.Drawing.Size(187, 33);
            this.WordBox.TabIndex = 1;
            // 
            // DictionaryDescription
            // 
            this.DictionaryDescription.AutoSize = true;
            this.DictionaryDescription.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DictionaryDescription.Location = new System.Drawing.Point(12, 40);
            this.DictionaryDescription.Name = "DictionaryDescription";
            this.DictionaryDescription.Size = new System.Drawing.Size(223, 30);
            this.DictionaryDescription.TabIndex = 2;
            this.DictionaryDescription.Text = "Choose the dictionary";
            // 
            // EnglishButton
            // 
            this.EnglishButton.AutoSize = true;
            this.EnglishButton.Location = new System.Drawing.Point(17, 73);
            this.EnglishButton.Name = "EnglishButton";
            this.EnglishButton.Size = new System.Drawing.Size(91, 29);
            this.EnglishButton.TabIndex = 3;
            this.EnglishButton.TabStop = true;
            this.EnglishButton.Text = "English";
            this.EnglishButton.UseVisualStyleBackColor = true;
            // 
            // RomanianButton
            // 
            this.RomanianButton.AutoSize = true;
            this.RomanianButton.Location = new System.Drawing.Point(17, 108);
            this.RomanianButton.Name = "RomanianButton";
            this.RomanianButton.Size = new System.Drawing.Size(116, 29);
            this.RomanianButton.TabIndex = 4;
            this.RomanianButton.TabStop = true;
            this.RomanianButton.Text = "Romanian";
            this.RomanianButton.UseVisualStyleBackColor = true;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI Semibold", 25.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Title.Location = new System.Drawing.Point(524, 24);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(319, 46);
            this.Title.TabIndex = 5;
            this.Title.Text = "Bilingual dictionary";
            // 
            // DefinitionBox
            // 
            this.DefinitionBox.AllowDrop = true;
            this.DefinitionBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.DefinitionBox.Location = new System.Drawing.Point(17, 213);
            this.DefinitionBox.MaxLength = 0;
            this.DefinitionBox.Multiline = true;
            this.DefinitionBox.Name = "DefinitionBox";
            this.DefinitionBox.ReadOnly = true;
            this.DefinitionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DefinitionBox.Size = new System.Drawing.Size(826, 229);
            this.DefinitionBox.TabIndex = 6;
            this.DefinitionBox.Visible = false;
            // 
            // RomEngButton
            // 
            this.RomEngButton.AutoSize = true;
            this.RomEngButton.Location = new System.Drawing.Point(17, 176);
            this.RomEngButton.Name = "RomEngButton";
            this.RomEngButton.Size = new System.Drawing.Size(185, 29);
            this.RomEngButton.TabIndex = 7;
            this.RomEngButton.TabStop = true;
            this.RomEngButton.Text = "Romanian-English";
            this.RomEngButton.UseVisualStyleBackColor = true;
            // 
            // EngRomButton
            // 
            this.EngRomButton.AutoSize = true;
            this.EngRomButton.Location = new System.Drawing.Point(17, 141);
            this.EngRomButton.Name = "EngRomButton";
            this.EngRomButton.Size = new System.Drawing.Size(185, 29);
            this.EngRomButton.TabIndex = 8;
            this.EngRomButton.TabStop = true;
            this.EngRomButton.Text = "English-Romanian";
            this.EngRomButton.UseVisualStyleBackColor = true;
            // 
            // Dictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(885, 454);
            this.Controls.Add(this.EngRomButton);
            this.Controls.Add(this.RomEngButton);
            this.Controls.Add(this.DefinitionBox);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.RomanianButton);
            this.Controls.Add(this.EnglishButton);
            this.Controls.Add(this.DictionaryDescription);
            this.Controls.Add(this.WordBox);
            this.Controls.Add(this.SearchButton);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Dictionary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dictionary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox WordBox;
        private System.Windows.Forms.Label DictionaryDescription;
        private System.Windows.Forms.RadioButton EnglishButton;
        private System.Windows.Forms.RadioButton RomanianButton;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.RadioButton RomEngButton;
        private System.Windows.Forms.RadioButton EngRomButton;
        public System.Windows.Forms.TextBox DefinitionBox;
    }
}

