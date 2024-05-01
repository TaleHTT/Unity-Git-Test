using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyRandomGenerator : MonoBehaviour
{
    public List<GameObject> EnemyList;
    //private List<GameObject> EnemyListInScene;
    public Tilemap tilemap;

    private void Awake()
    {
        SpawnEnemiesInsideTilemap();
    }

    public void SpawnEnemiesInsideTilemap()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            SpawnEnemyInsideTilemap(EnemyList[i]);
        }
    }

    public void SpawnEnemyInsideTilemap(GameObject enemyPrefab)
    {
        BoundsInt bounds = tilemap.cellBounds; // 获取Tilemap的边界

        // 遍历Tilemap的边界内部
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Vector3 spawnPosition = tilemap.CellToWorld(tilePosition) + new Vector3(0.5f, 0.5f, 0f); // 获取Tile的世界坐标

                // 检查Tile的世界坐标是否在Tilemap内部
                if (IsPointInsideTilemap(spawnPosition))
                {
                    // 在Tile的世界坐标位置实例化敌人预制体
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    private bool IsPointInsideTilemap(Vector3 point)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(point);
        Vector3 tileCenter = tilemap.CellToWorld(tilePosition) + new Vector3(0.5f, 0.5f, 0f); // 获取Tile的中心世界坐标

        // 检查点是否在Tilemap内部
        return tilemap.HasTile(tilePosition) && point == tileCenter;
    }
}
