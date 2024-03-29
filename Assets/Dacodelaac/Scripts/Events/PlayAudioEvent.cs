using System.Collections.Generic;
using UnityEngine;

namespace Dacodelaac.Events
{
    [CreateAssetMenu(menuName = "Event/Play Audio Event")]
    public class PlayAudioEvent : BaseEvent<AudioClip>, ISerializationCallbackReceiver
    {
        private Dictionary<AudioClip, float> _lastTimePlayDict = new Dictionary<AudioClip, float>();
        
        public override void Raise(AudioClip value)
        {
            _lastTimePlayDict.TryAdd(value, 0);
            if (Time.time - _lastTimePlayDict[value] < 0.1f) return;
            
            _lastTimePlayDict[value] = Time.time;
            base.Raise(value);
        }

        public void RaiseRandom(AudioClip[] audioClips)
        {
            Raise(audioClips[Random.Range(0, audioClips.Length)]);
        }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _lastTimePlayDict = new Dictionary<AudioClip, float>();
        }
    }
}