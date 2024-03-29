using Dacodelaac.Core;
using Dacodelaac.DataType;
using Dacodelaac.DebugUtils;
using Dacodelaac.Utils;
using Dacodelaac.Variables;
using UnityEngine;
using Event = Dacodelaac.Events.Event;

namespace Dacodelaac.Offlines
{
    [CreateAssetMenu]
    public class OfflineReward : BaseSO, ISerializationCallbackReceiver
    {
        [SerializeField] IntegerVariable adventureLevel;
        [SerializeField] DoubleVariable lastTimeActive;
        [SerializeField] float minOfflineTime;
        [SerializeField] float maxOfflineTime;
        [SerializeField] float goldFactor;
        [SerializeField] Event showOfflinePopupEvent;

        public ShortDouble Bonus { get; set; }

        public void CheckLaunchingBonus()
        {
            Bonus += GetBonus();
            lastTimeActive.Value = TimeUtils.CurrentSeconds;
        }

        public void CheckResumeBonus()
        {
            Bonus += GetBonus();
            lastTimeActive.Value = TimeUtils.CurrentSeconds;
        }

        public void TryTriggerBonus()
        {
            if (Bonus > 0)
            {
                showOfflinePopupEvent.Raise();
            }
        }

        public void ResetBonus()
        {
            Bonus = 0;
        }

        ShortDouble GetBonus()
        {
            if (lastTimeActive.Value <= 0)
            {
                Dacoder.Log($"This is first time play, no offline reward");
                return 0;
            }
            var offlineTime = TimeUtils.CurrentSeconds - lastTimeActive.Value;
            if (offlineTime < minOfflineTime)
            {
                Dacoder.Log($"OfflineTime: {offlineTime} < {minOfflineTime}, no offline reward");
                return 0;
            }

            if (offlineTime > maxOfflineTime)
            {
                offlineTime = maxOfflineTime;
            }

            var bonus = offlineTime * goldFactor * adventureLevel.Value;
            Dacoder.Log($"OfflineTime: {offlineTime}, offline reward: {bonus:0.#}");

            return ((ShortDouble)bonus).Round;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            ResetBonus();
        }
    }
}