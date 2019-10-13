using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace NewBark.Tilemap
{
    public class TileSwapScheduler : MonoBehaviour
    {
        public TileSwapSchedule[] schedules;
        private UnityEngine.Tilemaps.Tilemap[] _tileMaps;

        void Awake()
        {
            _tileMaps = GetComponentsInChildren<UnityEngine.Tilemaps.Tilemap>();
        }

        void Update()
        {
            foreach (var schedule in schedules)
            {
                if (schedule.CanSwap())
                {
                    SwapTiles(schedule);
                    schedule.swapped = true;
                    return;
                }

                schedule.swapped = false;
            }
        }

        void SwapTiles(TileSwapSchedule schedule)
        {
            foreach (var tm in _tileMaps)
            {
                tm.color = schedule.color;

                foreach (var sw in schedule.m_TileSwaps)
                {
                    tm.SwapTile(sw.changeTile, sw.newTile);
                }
            }
        }
    }
}
