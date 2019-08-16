using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins
{
    public abstract class BehaviorBase : IBehavior
    {
       
        private string _identifier;
        private int _modifierRange;
        private int _currentModifier;
        private Keys _triggerKey;
        private byte[] _modifierArray;


        public string Identifier { get { return this._identifier; } set { this._identifier = value; } }
        public int ModifierRange { get { return this._modifierRange; } set { this._modifierRange = value; } }
        public int CurrentModifier { get { return this._currentModifier; } set { this._currentModifier = value; } }
        public Keys TriggerKey { get { return this._triggerKey; } set { this._triggerKey = value; } }
        ////Alt, AltGr, CtrlLeft, CtrlRight, ShiftLeft, ShiftRight, SuperLeft, SuperRight
        public byte[] ModifierArray { get { return this._modifierArray; } set { this._modifierArray = value; } }

        public virtual void Behavior(int modifier)
        {
            Console.WriteLine("Executing behavior in " + this.GetType() + " with modofier " + modifier);
        }

        public void ExecuteBehavior()
        {
            this.Behavior(this._currentModifier);

            _currentModifier = _currentModifier % _modifierRange;

            _currentModifier++;
        }

        public void ResetModifier()
        {
            this._currentModifier = 1;
        }

        public BehaviorBase(string identifier, int modifierRange = 1)
        {
            this._identifier = identifier;
            this._modifierRange = modifierRange;

            this._currentModifier  = 1;
            this._triggerKey = Keys.None;
            this._modifierArray = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        }

        public void Configure(Keys triggerKey, byte[] modifierArray)
        {
            this._triggerKey = triggerKey;
            this._modifierArray = modifierArray;
        }
    }
}
