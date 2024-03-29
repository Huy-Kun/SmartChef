using Dacodelaac.DataType;
using TMPro;
using UnityEngine;

namespace Dacodelaac.UI.Buttons
{
    public class CostButton : GrayScaleButton
    {
        [SerializeField] TextMeshProUGUI cost;
        [SerializeField] RippleButton button; 
        
        public void Setup(string cost, bool gray)
        {
            this.cost.text = cost;
            enabled = gray;
            button.enabled = !gray;
        }
    }
}