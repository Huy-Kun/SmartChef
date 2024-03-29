using Dacodelaac.Core;

namespace Dacodelaac.UI.Tabs
{
    public class BaseTabButton : BaseMono
    {
        public event System.Action<int> OnClickedEvent = null;
        public int Index { get; set; }

        bool selected;

        public virtual void Setup()
        {
            selected = false;
        }

        public void OnClicked()
        {
            OnClickedEvent?.Invoke(Index);
        }

        public void DoSelected()
        {
            if (!selected)
            {
                selected = true;
                OnSelected();
            }
        }

        public void DoDeselected()
        {
            if (selected)
            {
                selected = false;
                OnDeselected();
            }
        }

        protected virtual void OnSelected()
        {
            
        }

        protected virtual void OnDeselected()
        {
            
        }
    }
}
