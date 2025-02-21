using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleTilemapController : MonoBehaviour
{
    public Tilemap obstacleTilemap;

    void Awake()
    {
        obstacleTilemap = GetComponent<Tilemap>();
    }
}
