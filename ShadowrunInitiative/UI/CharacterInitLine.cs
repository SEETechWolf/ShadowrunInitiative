using System.Windows.Forms;
using ShadowrunInitiative.Core;

namespace ShadowrunInitiative
{
    /// <summary>
    /// A control allowing input of initiative for a turn for a single character.
    /// </summary>
    public partial class CharacterInitLine : UserControl
    {
        Character m_Character;

        public CharacterInitLine(Character character)
        {
            InitializeComponent();

            m_Character = character;
            characterNameLabel.Text = character.name;
        }

        /// <summary>
        /// Apply the current control settings to the character object.
        /// </summary>
        public void ApplyInit()
        {
            m_Character.Initiative = (int)initiativeNumeric.Value;
            m_Character.seize = seizeCheckBox.Checked;
        }

        private void Wuerfelbutton_Click(object sender, System.EventArgs e)
        {
            initiativeNumeric.Value = m_Character.getIni();
        }
    }
}
