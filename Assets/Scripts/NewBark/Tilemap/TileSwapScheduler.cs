using System;
using NewBark.Tilemap;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwapScheduler : MonoBehaviour
{
    public string m_Title;
    public int m_FromHour;
    public int m_ToHour;
    public bool m_RestoreOnFinish;
    public TileSwap[] m_Swaps;

    private bool _swapped = false;
    private Tilemap[] _tileMaps;

    void Awake()
    {
        _tileMaps = GetComponentsInChildren<Tilemap>();
    }

    void Update()
    {
        if (!_swapped && InSchedule())
        {
            SwapTiles();
            Debug.Log("Tiles changed to " + m_Title);
            return;
        }

        if (m_RestoreOnFinish && _swapped && !InSchedule())
        {
            SwapTilesBack();
        }
    }

    bool InSchedule()
    {
        return DateTime.Now.Hour >= m_FromHour && DateTime.Now.Hour <= m_ToHour;
    }

    void SwapTilesBack()
    {
        foreach (var tm in _tileMaps)
        {
            foreach (var sw in m_Swaps)
            {
                tm.SwapTile(sw.newTile, sw.changeTile);
            }
        }
        _swapped = false;
    }

    void SwapTiles()
    {
        foreach (var tm in _tileMaps)
        {
            foreach (var sw in m_Swaps)
            {
                tm.SwapTile(sw.changeTile, sw.newTile);
            }
        }
        _swapped = true;
    }
}
