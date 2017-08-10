using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ShadowrunInitiative.Core;
using ShadowrunInitiative.Util;

namespace ShadowrunInitiative
{
    public partial class MainForm : Form
    {
        private CharacterSpeedComparer CharacterSorter = new CharacterSpeedComparer();

        private BindingList<Character> m_Characters = new BindingList<Character>();
        private Character m_CurrentCharacter = null;
        public TimeSpan CombatLength = TimeSpan.FromSeconds(0);
        private int m_CombatTurns = 0;
        private const int MAX_LOG = 64;
        private bool m_NewTurn = false;
        private bool m_NeedsInitiative = false;
        private BindingList<string> m_LogMessages = new BindingList<string>();

        public int CombatTurns
        {
            get { return m_CombatTurns; }
            set
            {
                m_CombatTurns = value;
                CombatLength = TimeSpan.FromSeconds(m_CombatTurns * 3f);

                List<string> comps = new List<string>();
                if (CombatLength.Hours > 0)
                    comps.Add(CombatLength.Hours + " hours");
                if (CombatLength.Minutes > 0)
                    comps.Add(CombatLength.Minutes + " min");
                if (CombatLength.Seconds > 0 || comps.Count == 0)
                    comps.Add(CombatLength.Seconds + " sec");
                combatTimeLabel.Text = string.Join(", ", comps);
            }
        }

        private Character SelectedCharacter
        {
            get { return charactersListBox.SelectedItem as Character; }
        }

        private void LogMessage(string str)
        {
            if (m_LogMessages.Count >= MAX_LOG)
                m_LogMessages.RemoveAt(0);
            m_LogMessages.Add(str);
        }

        public MainForm()
        {
            InitializeComponent();

            CombatTurns = 0;
            characterBox.Visible = false;
            logListBox.DataSource = m_LogMessages;
            SetCurrentCharacter(null);

            combatSituationPanel1.OnCombatSituationChanged += OnSituationChanged;
            OnSituationChanged(this, null);

            m_Characters.ListChanged += CharacterListChanged;
            CharacterListChanged(null, null);
            charactersListBox.DataSource = m_Characters;
        }

        private void OnSituationChanged(object sender, EventArgs e)
        {
            matrixLevelPanel.Visible = combatSituationPanel1.Situation == CombatSituation.MATRIX;
        }

        private void CharacterListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateCharacterDependent();
        }

        private void UpdateCharacterDependent()
        {
            nextTurnButton.Enabled = m_Characters.Any();
            newCombatButton.Enabled = m_Characters.Any();

            interrupt5Button.Enabled = SelectedCharacter != null && SelectedCharacter.Initiative >= 0;
            interrupt10Button.Enabled = SelectedCharacter != null && SelectedCharacter.Initiative >= 0;

            delayButton.Enabled = m_CurrentCharacter == SelectedCharacter;
        }

        private void charactersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load infos into character box
            if (SelectedCharacter != null)
            {
                characterBox.Visible = true;
                characterBox.Text = SelectedCharacter.name;
                pcCheckbox.Checked = SelectedCharacter.PC;
                incapacitatedCheckBox.Checked = SelectedCharacter.Incapacitated;
                edgeNumeric.Value = SelectedCharacter.edge;
                reactNumeric.Value = SelectedCharacter.reaction;
                intuitNumeric.Value = SelectedCharacter.intuition;
                initNumeric.Value = SelectedCharacter.Initiative;
            }
            else
            {
                characterBox.Visible = false;
            }
            UpdateCharacterDependent();
        }

        private void addCharacterButton_Click(object sender, EventArgs e)
        {
            NewCharacter newCharacterDialog = new NewCharacter();
            if (newCharacterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_Characters.Add(newCharacterDialog.Character);
                charactersListBox_SelectedIndexChanged(charactersListBox, null);
            }
        }

        private void pcCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                SelectedCharacter.PC = pcCheckbox.Checked;
        }

        private void incapacitatedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                SelectedCharacter.Incapacitated = incapacitatedCheckBox.Checked;
        }

        private void edgeNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                SelectedCharacter.edge = (int)edgeNumeric.Value;
        }

        private void reactNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                SelectedCharacter.reaction = (int)reactNumeric.Value;
        }

        private void intuitNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                SelectedCharacter.intuition = (int)intuitNumeric.Value;
        }

        private void initNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
                SelectedCharacter.Initiative = (int)initNumeric.Value;
        }

        private void removeCharacterButton_Click(object sender, EventArgs e)
        {
            if (m_Characters.Contains(SelectedCharacter))
            {
                if (MessageBox.Show(
                    "Really delete character '" + SelectedCharacter.name + "'?",
                    "Delete Character",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    m_Characters.Remove(SelectedCharacter);
                }
            }
        }

        private void interrupt5Button_Click(object sender, EventArgs e)
        {
            SpendInterrupt(-5);
        }

        private void interrupt10Button_Click(object sender, EventArgs e)
        {
            SpendInterrupt(-10);
        }

        private void SpendInterrupt(int value)
        {
            if (SelectedCharacter != null)
            {
                if (SelectedCharacter.Initiative + value >= 0)
                {
                    SelectedCharacter.Initiative += value;                    
                }else
                {
                    SelectedCharacter.Initiative = 0;
                }
                LogMessage(SelectedCharacter.name + " gibt "+value+" initiative aus.");
            }
            AdvanceTurn();
        }

        private void interruptXButton_Click(object sender, EventArgs e)
        {
            ChooseInterrupt choose = new ChooseInterrupt(SelectedCharacter);
            if (choose.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SpendInterrupt(choose.Value);
            }
        }

        private void newCombatButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Delete all NPCs and reset player characters?",
                "Reset Combat",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                for (int c = m_Characters.Count - 1; c >= 0; c--)
                {
                    if (!m_Characters[c].PC)
                        m_Characters.RemoveAt(c);
                    m_LogMessages.Clear();
                    CombatTurns = 0;
                }
            }
        }

        private void nextTurnButton_Click(object sender, EventArgs e)
        {
            CombatTurns = 0;
            AdvanceTurn();
        }

        private void SetCurrentCharacter(Character character)
        {
            m_CurrentCharacter = character;
            UpdateCharacterDependent();
            if (character != null)
            {
                nextTurnButton.Text = "Next Turn";
                //character.WentThisTurn = true;
                //currentTurnStaticLabel.Visible = true;
                currentCharLabel.Text = ">>" + character.name + "<<";

                string turnMessage = "TURN: " + character.name;
                if (character.seize)
                    turnMessage += " (seized)";
                LogMessage(turnMessage);
                charactersListBox.SelectedItem = character;
            }
            else
            {
                nextTurnButton.Text = "Begin Round";
                //currentTurnStaticLabel.Visible = false;
                currentCharLabel.Text = "N/A";
            }
        }

        private void AdvanceTurn()
        {
            //This is where the fun begins.
            if (m_NeedsInitiative)
            {
                //Query for initiatives
                QueryInitiatives query = new QueryInitiatives(m_Characters);
                if (query.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    m_NeedsInitiative = false;
                else
                    return;
            }

            IEnumerable<Character> delayed = m_Characters.Where(c => c.delay);
            IEnumerable<Character> stillCanAct = m_Characters.Where(c => c.Initiative > 0 && !c.Incapacitated && !c.delay);
            IEnumerable<Character> stillToGo = stillCanAct.Where(c => !c.wentThisTurn);
            if (stillToGo.Any())
            {
                //Someone's turn right now
                if (m_NewTurn)
                {
                    LogMessage("");
                    LogMessage("---- NEUE RUNDE ----");
                    m_NewTurn = false;
                }
                SetCurrentCharacter(stillToGo.OrderByDescending(c => c, CharacterSorter).First());
                foreach(Character c in delayed)
                {
                    c.Initiative = m_CurrentCharacter.Initiative;
                }
            }
            else
            {
                //New round
                foreach (Character c in m_Characters)
                    c.TurnReset();
                SetCurrentCharacter(null);
                CombatTurns++;
                m_LogMessages.Clear();
                LogMessage("");
                LogMessage("-+-+ NEUE RUNDE +-+-");

                //Query for initiatives
                QueryInitiatives query = new QueryInitiatives(m_Characters);
                if (query.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    AdvanceTurn();
                else
                    m_NeedsInitiative = true;
            }
            m_NewTurn = false;
        }

        private void delayButton_Click(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
            {
                SelectedCharacter.delay = true;
                LogMessage(SelectedCharacter.name + " wartet.");
            }
            AdvanceTurn();
        }

    }
}
