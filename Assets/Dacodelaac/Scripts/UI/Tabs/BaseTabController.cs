using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.UI.Popups;
using UnityEngine;

namespace Dacodelaac.UI.Tabs
{
    public class BaseTabController : BaseMono
    {
        [SerializeField] private RectTransform tabContainer;
        [SerializeField] private RectTransform tabButtonContainer;
        
        private BaseTabButton[] _tabButtons;
        private BaseTab[] _tabs;
        private int _currentTab;
        public int CurrentTabIndex => _currentTab;
        public BasePopup Controller { get; set; }
        public BaseTab CurrentTab => _tabs[_currentTab];

        public void Setup(BasePopup tabPopupController)
            => Controller = tabPopupController;

        BaseTab[] GetTabs()
        {
            var _tabs = new List<BaseTab>();
            foreach (RectTransform t in tabContainer.transform)
            {
                t.TryGetComponent<BaseTab>(out var tab);
                if (tab != null) _tabs.Add(tab);
            }
            return _tabs.ToArray();
        }

        BaseTabButton[] GetTabButtons()
        {
            var _tabButtons = new List<BaseTabButton>();
            foreach (RectTransform t in tabButtonContainer.transform)
            {
                t.TryGetComponent<BaseTabButton>(out var button);
                if(button != null) _tabButtons.Add(button);
            }

            return _tabButtons.ToArray();
        }

        public override void Initialize()
        {
            // tabButtons = tabButtonContainer.GetComponentsInChildren<BaseTabButton>(true);
            // tabs = tabContainer.GetComponentsInChildren<BaseTab>(true);

            _tabs = GetTabs();
            _tabButtons = GetTabButtons();
            
            for (var i = 0; i < _tabButtons.Length; i++)
            {
                _tabButtons[i].Index = i;
                _tabButtons[i].Setup();
                _tabs[i].Controller = this;
                _tabs[i].Index = i;
                _tabs[i].Setup();
            }
            OnChangeTab(-1);
        }

        public void Show(int tabIndex)
        {
            for (var i = 0; i < _tabButtons.Length; i++)
            {
                _tabButtons[i].OnClickedEvent += OnChangeTab;
            }

            _currentTab = tabIndex;
            OnChangeTab(_currentTab);
        }

        public void Resume()
        {
            foreach (var tab in _tabs)
            {
                tab.OnResume();
            }
        }

        public void Dismiss()
        {
            for (int i = 0; i < _tabButtons.Length; i++)
            {
                _tabButtons[i].OnClickedEvent -= OnChangeTab;
            }
            OnChangeTab(-1);
        }

        void OnChangeTab(int tabIndex)
        {
            if (tabIndex != -1)
            {
                _currentTab = tabIndex;
            }

            foreach (var button in _tabButtons)
            {
                if (button.Index == tabIndex)
                {
                    button.DoSelected();
                }
                else
                {
                    button.DoDeselected();
                }
            }

            foreach (var tab in _tabs)
            {
                if (tab.Index != tabIndex && tab.gameObject.activeSelf)
                {
                    tab.OnBeginDismiss();
                    tab.gameObject.SetActive(false);
                    tab.OnDismissed();
                }
                else if (tab.Index == tabIndex && !tab.gameObject.activeSelf)
                {
                    tab.OnBeginShow();
                    tab.gameObject.SetActive(true);
                    tab.OnShown();
                }
            }
        }
    }
}
