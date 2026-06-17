using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HexKeyGenerator
{
    public partial class MainForm : Form
    {
        private Dictionary<string, string> companyKeyMappings;
        private TextBox companyTextBox;
        private TextBox keyTextBox;
        private ListBox resultsListBox;

        public MainForm()
        {
            InitializeComponent();
            companyKeyMappings = new Dictionary<string, string>();
        }

        private void InitializeComponent()
        {
            this.companyTextBox = new System.Windows.Forms.TextBox();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.resultsListBox = new System.Windows.Forms.ListBox();

            // Form properties
            this.Text = "Hex Key Generator - 256-bit";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(700, 500);
            this.BackColor = System.Drawing.SystemColors.Control;

            // Company Name Label
            Label companyLabel = new Label();
            companyLabel.Text = "Company Name:";
            companyLabel.Location = new System.Drawing.Point(20, 20);
            companyLabel.Width = 120;
            companyLabel.AutoSize = false;
            this.Controls.Add(companyLabel);

            // Company Name TextBox
            this.companyTextBox.Location = new System.Drawing.Point(150, 20);
            this.companyTextBox.Width = 300;
            this.Controls.Add(this.companyTextBox);

            // Generate Button
            Button generateButton = new Button();
            generateButton.Text = "Generate Key";
            generateButton.Location = new System.Drawing.Point(470, 20);
            generateButton.Width = 100;
            generateButton.Click += GenerateButton_Click;
            this.Controls.Add(generateButton);

            // Generated Key Label
            Label keyLabel = new Label();
            keyLabel.Text = "256-bit Hex Key:";
            keyLabel.Location = new System.Drawing.Point(20, 70);
            keyLabel.Width = 120;
            keyLabel.AutoSize = false;
            this.Controls.Add(keyLabel);

            // Generated Key TextBox
            this.keyTextBox.Location = new System.Drawing.Point(150, 70);
            this.keyTextBox.Width = 500;
            this.keyTextBox.ReadOnly = true;
            this.keyTextBox.Font = new System.Drawing.Font("Courier New", 10);
            this.Controls.Add(this.keyTextBox);

            // Copy Button
            Button copyButton = new Button();
            copyButton.Text = "Copy";
            copyButton.Location = new System.Drawing.Point(660, 70);
            copyButton.Width = 20;
            copyButton.Click += CopyButton_Click;
            this.Controls.Add(copyButton);

            // Results Label
            Label resultLabel = new Label();
            resultLabel.Text = "Company - Key Mappings:";
            resultLabel.Location = new System.Drawing.Point(20, 120);
            resultLabel.Width = 200;
            resultLabel.AutoSize = false;
            this.Controls.Add(resultLabel);

            // Results ListBox
            this.resultsListBox.Location = new System.Drawing.Point(20, 150);
            this.resultsListBox.Width = 650;
            this.resultsListBox.Height = 300;
            this.Controls.Add(this.resultsListBox);

            // Clear Button
            Button clearButton = new Button();
            clearButton.Text = "Clear All";
            clearButton.Location = new System.Drawing.Point(20, 460);
            clearButton.Width = 100;
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string companyName = companyTextBox.Text.Trim();

            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Please enter a company name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = KeyGenerator.GenerateKeyForCompany(companyName);
                string hexKey = result.Item2;

                // Display the key
                keyTextBox.Text = hexKey;

                // Store in mappings
                if (companyKeyMappings.ContainsKey(companyName))
                {
                    companyKeyMappings[companyName] = hexKey;
                }
                else
                {
                    companyKeyMappings.Add(companyName, hexKey);
                }

                // Update results list
                UpdateResultsList();

                // Clear input for next entry
                companyTextBox.Clear();
                companyTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateResultsList()
        {
            resultsListBox.Items.Clear();

            foreach (var mapping in companyKeyMappings)
            {
                string displayText = $"{mapping.Key}: {mapping.Value}";
                resultsListBox.Items.Add(displayText);
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(keyTextBox.Text))
            {
                Clipboard.SetText(keyTextBox.Text);
                MessageBox.Show("Key copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            companyTextBox.Clear();
            keyTextBox.Clear();
            companyKeyMappings.Clear();
            resultsListBox.Items.Clear();
            companyTextBox.Focus();
        }
    }
}
