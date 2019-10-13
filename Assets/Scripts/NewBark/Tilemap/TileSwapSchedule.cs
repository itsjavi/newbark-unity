using System;
using UnityEngine;

namespace NewBark.Tilemap
{
    [Serializable]
    public class TileSwapSchedule
    {
        public string m_Title;
        public int m_FromHour;
        public int m_ToHour;
        public TileSwap[] m_TileSwaps;
        public Color color = Color.white;
        
        [HideInInspector]
        public bool swapped;

        public bool CanSwap()
        {
            return !swapped && InSchedule();
        }
        
        public bool InSchedule()
        {
            return DateTime.Now.Hour >= m_FromHour && DateTime.Now.Hour < m_ToHour;
        }
    }
}
