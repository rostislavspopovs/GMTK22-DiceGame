using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAndOverlaysManager : Singleton<EffectsAndOverlaysManager>
{
    [SerializeField] private HighlightPlane highlightPlanePrefab;

    private List<HighlightPlane> currentHighlightObjects = new List<HighlightPlane>();
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
        ClearHighlights();

        int tileX = originTileX;
        int tileY = originTileY;

        for (int i = 1; i<=length; i++)
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

                HighlightPlane highlight = Instantiate(highlightPlanePrefab, spawnPos, new Quaternion(0, 0, 0, 0), this.transform);
                
                if (i == length)
                {
                    highlight.SetHighlightColor(Color.blue);
                }
                else
                {
                    highlight.SetHighlightColor(Color.cyan);
                }

                highlight.SetText(i.ToString());

                currentHighlightObjects.Add(highlight);
            } 
            else if (tileMap.IsVoid(tileX, tileY))
            {
                Vector3 spawnPos = GameController.Instance.TileToWorldPosition(floor, tileX, tileY) + new Vector3(0, 0.01f, 0); // 0.01f above tile level

                HighlightPlane highlight = Instantiate(highlightPlanePrefab, spawnPos, new Quaternion(0, 0, 0, 0), this.transform);
                highlight.SetHighlightColor(Color.red);

                currentHighlightObjects.Add(highlight);
                break;
            }
            else { break; }
        }
    }
     public void ClearHighlights()
    {
        foreach(HighlightPlane highlight in currentHighlightObjects)
        {
            Destroy(highlight.gameObject);
        }
        currentHighlightObjects.Clear();
    }

}
