using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAndOverlaysManager : Singleton<EffectsAndOverlaysManager>
{
    [SerializeField] private GameObject highlightPlanePrefab;

    private List<GameObject> currentHighlightObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateHighlights(TileMap tileMap, int floor, int originTileX, int originTileY, GameController.Direction direction, int length)
    {
        int tileX = originTileX;
        int tileY = originTileY;

        for (int i = 0; i<length; i++)
        {
            switch (direction)
            {
                case GameController.Direction.XPlus: tileX += 1; break;
                case GameController.Direction.XMinus: tileX -= 1; break;
                case GameController.Direction.ZPlus: tileY += 1; break;
                case GameController.Direction.ZMinus: tileY -= 1; break;
            }
            if (tileMap.IsSteppable(tileX, tileY))
            {
                Vector3 spawnPos = GameController.Instance.TileToWorldPosition(floor, tileX, tileY) + new Vector3(0, 0.01f, 0); // 0.01f above tile level
                currentHighlightObjects.Add(Instantiate(highlightPlanePrefab, spawnPos, new Quaternion(0, 0, 0, 0), this.transform));
            } else { break; }
        }
    }
     public void ClearHighlights()
    {
        foreach(GameObject highlight in currentHighlightObjects)
        {
            Destroy(highlight);
        }
        currentHighlightObjects.Clear();
    }

}
