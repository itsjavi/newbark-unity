using UnityEngine;

namespace NewBark.Tilemap
{
    public class TileSwapScheduler : MonoBehaviour
    {
        public TileSwapSchedule[] schedules;
        private UnityEngine.Tilemaps.Tilemap[] _tileMaps;
        private TileSwapSchedule _currentSchedule;

        void Start()
        {
            _tileMaps = GetComponentsInChildren<UnityEngine.Tilemaps.Tilemap>();
        }

        void Update()
        {
            foreach (var schedule in schedules)
            {
                if (schedule.InSchedule() && (_currentSchedule != schedule))
                {
                    SwapTiles(schedule);
                    _currentSchedule = schedule;
                    return;
                }
            }
        }

        private void OnValidate()
        {
            _currentSchedule = null;
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
