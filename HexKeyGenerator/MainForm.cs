using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HexKeyGenerator
{
    public partial class MainForm : Form
    {
        private Dictionary<string, string> companyKeyMappings;

        public MainForm()
        {
            InitializeComponent();
            companyKeyMappings = new Dictionary<string, string>();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.Text = "Hex Key Generator - 256-bit";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(700, 500);

            // Company Name Label and TextBox
            Label companyLabel = new Label();
            companyLabel.Text = "Company Name:";
            companyLabel.Location = new System.Drawing.Point(20, 20);
            companyLabel.Width = 120;
            this.Controls.Add(companyLabel);

            TextBox companyTextBox = new TextBox();
            companyTextBox.Name = "companyTextBox";
            companyTextBox.Location = new System.Drawing.Point(150, 20);
            companyTextBox.Width = 300;
            this.Controls.Add(companyTextBox);

            // Generate Button
            Button generateButton = new Button();
            generateButton.Text = "Generate Key";
            generateButton.Location = new System.Drawing.Point(470, 20);
            generateButton.Width = 100;
            generateButton.Click += (s, e1) => GenerateKeyClick(companyTextBox);
            this.Controls.Add(generateButton);

            // Generated Key Label and TextBox
            Label keyLabel = new Label();
            keyLabel.Text = "256-bit Hex Key:";
            keyLabel.Location = new System.Drawing.Point(20, 70);
            keyLabel.Width = 120;
            this.Controls.Add(keyLabel);

            TextBox keyTextBox = new TextBox();
            keyTextBox.Name = "keyTextBox";
            keyTextBox.Location = new System.Drawing.Point(150, 70);
            keyTextBox.Width = 500;
            keyTextBox.ReadOnly = true;
            keyTextBox.Font = new System.Drawing.Font("Courier New", 10);
            this.Controls.Add(keyTextBox);

            // Copy Button
            Button copyButton = new Button();
            copyButton.Text = "Copy";
            copyButton.Location = new System.Drawing.Point(660, 70);
            copyButton.Width = 20;
            copyButton.Click += (s, e1) => CopyToClipboard(keyTextBox);
            this.Controls.Add(copyButton);

            // Results ListBox
            Label resultLabel = new Label();
            resultLabel.Text = "Company - Key Mappings:";
            resultLabel.Location = new System.Drawing.Point(20, 120);
            resultLabel.Width = 200;
            this.Controls.Add(resultLabel);

            ListBox resultsListBox = new ListBox();
            resultsListBox.Name = "resultsListBox";
            resultsListBox.Location = new System.Drawing.Point(20, 150);
            resultsListBox.Width = 650;
            resultsListBox.Height = 300;
            this.Controls.Add(resultsListBox);

            // Clear Button
            Button clearButton = new Button();
            clearButton.Text = "Clear All";
            clearButton.Location = new System.Drawing.Point(20, 460);
            clearButton.Width = 100;
            clearButton.Click += (s, e1) => ClearAll(companyTextBox, keyTextBox, resultsListBox);
            this.Controls.Add(clearButton);
        }

        private void GenerateKeyClick(TextBox companyTextBox)
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
                TextBox keyTextBox = (TextBox)this.Controls["keyTextBox"];
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
            ListBox resultsListBox = (ListBox)this.Controls["resultsListBox"];
            resultsListBox.Items.Clear();

            foreach (var mapping in companyKeyMappings)
            {
                string displayText = $"{mapping.Key}: {mapping.Value}";
                resultsListBox.Items.Add(displayText);
            }
        }

        private void CopyToClipboard(TextBox keyTextBox)
        {
            if (!string.IsNullOrEmpty(keyTextBox.Text))
            {
                Clipboard.SetText(keyTextBox.Text);
                MessageBox.Show("Key copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearAll(TextBox companyTextBox, TextBox keyTextBox, ListBox resultsListBox)
        {
            companyTextBox.Clear();
            keyTextBox.Clear();
            companyKeyMappings.Clear();
            resultsListBox.Items.Clear();
            companyTextBox.Focus();
        }
    }
}
