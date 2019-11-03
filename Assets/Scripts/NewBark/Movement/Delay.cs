using System;
using UnityEngine;

namespace NewBark.Movement
{
    [Serializable]
    public class Delay
    {
        public float time;
        private float _countdown;
        private bool _init;
        
        public bool IsDelayed()
        {
            if (!_init)
            {
                _countdown = time;
                _init = true;
            }
            return _countdown > 0;
        }
        
        public void Reset()
        {
            _countdown = time;
        }

        public bool Tick()
        {
            if (!IsDelayed())
            {
                return false;
            }
            
            _countdown -= Time.deltaTime * 1000;
            if (_countdown > 0)
            {
                return true;
            }

            // it might be 0 or less
            _countdown = 0;

            return false;
        }
    }
}