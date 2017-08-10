using ShadowrunInitiative.Core;
using System;
using System.Windows.Forms;

namespace ShadowrunInitiative
{
    /// <summary>
    /// Form for creating a new Character object.
    /// </summary>
    public partial class NewCharacter : Form
    {
        public Character Character;

        public NewCharacter()
        {
            InitializeComponent();

            Character = new Character();
            okButton.Enabled = false;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            enableOK();
        }

        private void pcCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            enableOK();
        }

        private void edgeNumeric_ValueChanged(object sender, EventArgs e)
        {
            enableOK();
        }

        private void reactNumeric_ValueChanged(object sender, EventArgs e)
        {
            enableOK();
        }

        private void intuitNumeric_ValueChanged(object sender, EventArgs e)
        {
            enableOK();
        }

        private void intW_ValueChanged(object sender, EventArgs e)
        {
            enableOK();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Character.iniWuerfel = (int)iniW.Value;
            Character.intuition = (int)intuitNumeric.Value;
            Character.reaction = (int)reactNumeric.Value;
            Character.edge = (int)edgeNumeric.Value;
            Character.PC = pcCheckbox.Checked;
            Character.name = nameTextBox.Text;
        }

        private void enableOK()
        {
            okButton.Enabled = !string.IsNullOrWhiteSpace(nameTextBox.Text)
                && (int)reactNumeric.Value > 0
                && intuitNumeric.Value > 0
                && iniW.Value >0;

        }
    }
}
