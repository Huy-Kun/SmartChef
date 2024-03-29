using System.Linq;
using Dacodelaac.Attributes;
using Dacodelaac.ObjectPooling;
#if UNITY_EDITOR
using Dacodelaac.Utils;
using UnityEditor;
#endif
using UnityEngine;

namespace Dacodelaac.Core
{
    public abstract class BaseMono : MonoBehaviour, IEntity
    {
        [Header("Base")]
        [SerializeField, NamedId] string id;
        [SerializeField] public Pools pools;
        [SerializeField] public Ticker ticker;
        [SerializeField] bool earlyTick;
        [SerializeField] bool tick;
        [SerializeField] bool lateTick;
        [SerializeField] bool fixedTick;

        public string Id => id;

#if UNITY_EDITOR
        [ContextMenu("ResetId")]
        public void ResetId()
        {
            id = NamedIdAttributeDrawer.ToSnakeCase(name);
            EditorUtility.SetDirty(this);
        }
#endif

        void OnEnable()
        {
            BindVariable();
            ListenEvents();
            SubTick();
            DoEnable();
        }

        void OnDisable()
        {
            DoDisable();
            UnsubTick();
            StopListenEvents();
            UnbindVariable();
        }

        public virtual void BindVariable()
        {
        }

        public virtual void ListenEvents()
        {
        }
        
        void SubTick()
        {
            if (earlyTick) ticker.SubEarlyTick(this);
            if (tick) ticker.SubTick(this);
            if (lateTick) ticker.SubLateTick(this);
            if (fixedTick) ticker.SubFixedTick(this);
        }

        public virtual void DoEnable()
        {
        }

        public virtual void Initialize()
        {
        }
        
        public virtual void EarlyTick()
        {
        }

        public virtual void Tick()
        {
        }

        public virtual void LateTick()
        {
        }

        public virtual void FixedTick()
        {
        }
        
        public virtual void CleanUp()
        {
        }
        
        public virtual void DoDisable()
        {
        }

        void UnsubTick()
        {
            if (earlyTick) ticker.UnsubEarlyTick(this);
            if (tick) ticker.UnsubTick(this);
            if (lateTick) ticker.UnsubLateTick(this);
            if (fixedTick) ticker.UnsubFixedTick(this);
        }

        public virtual void StopListenEvents()
        {
        }

        public virtual void UnbindVariable()
        {
        }

#if UNITY_EDITOR
        void Reset()
        {
            ticker = AssetUtils.FindAssetAtFolder<Ticker>(new string[] {"Assets"}).FirstOrDefault();
            pools = AssetUtils.FindAssetAtFolder<Pools>(new string[] {"Assets"}).FirstOrDefault();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}