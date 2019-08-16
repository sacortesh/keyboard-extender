using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins
{
    public interface IBehavior
    {
        string Identifier { get; set; }

        int ModifierRange { get; set; }
        int CurrentModifier { get; set; }

        Keys TriggerKey { get; set; }

        byte[] ModifierArray { get; set; }

        void ResetModifier();

        void ExecuteBehavior();
        void Behavior(int modifier);
    }
}