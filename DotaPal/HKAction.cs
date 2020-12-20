using System.Windows.Forms;

namespace DotaPal
{
    public class HKAction
    {
        public Action Action;
        public Slots Slot;
        public Keys Modifier;
        public Keys Key;
        public Label? Label;

        public HKAction(Action action, Slots slot, Keys modifier, Keys key, Label? label = null)
        {
            Action = action;
            Slot = slot;
            Modifier = modifier;
            Key = key;
            Label = label;
        }
    }
}