﻿using UnityEngine;

namespace Dacodelaac.Core
{
    public class TickerMono : MonoBehaviour
    {
        public Ticker Ticker { get; set; }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            Ticker.EarlyTick();
            Ticker.Tick();
        }

        void FixedUpdate()
        {
            Ticker.FixedTick();
        }

        void LateUpdate()
        {
            Ticker.LateTick();
        }
    }
}