using ShadowrunInitiative.Util;
using System;
using System.ComponentModel;

namespace ShadowrunInitiative.Core
{
    /// <summary>
    /// Stores data and stats on one character.
    /// </summary>
    public class Character : INotifyPropertyChanged
    {
        public string name;

        public MatrixLevel matrixLevel = MatrixLevel.AR;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool m_PC = false;
        private bool m_Incapacitated = false;
        public int edge, reaction, intuition, dataProcessing, iniWuerfel;
        private int m_Initiative = 0;
        public bool wentThisTurn = false;
        public bool seize = false;
        public bool delay = false;

        public bool PC
        {
            get { return m_PC; }
            set
            {
                m_PC = value;
                NotifyPropertyChanged("PC");
            }
        }
        
        public bool Incapacitated
        {
            get { return m_Incapacitated; }
            set
            {
                m_Incapacitated = value;
                NotifyPropertyChanged("Incapacitated");
            }
        }
        
        public int Initiative
        {
            get { return m_Initiative; }
            set
            {
                m_Initiative = Math.Max(0, value);
                NotifyPropertyChanged("Initiative");
            }
        }
        
        public void TurnReset()
        {
            wentThisTurn = false;
            seize = false;
            delay = false;
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public override string ToString()
        {
            return name;
        }

        public int getIni()
        {
            return reaction + intuition + D6.d6(iniWuerfel);
        }
    }
}
