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

    public void CreateHighlights(int floor, int originTileX, int originTileY, GameController.Direction direction, int length)
    {
        Vector3 spawnPos = GameController.Instance.TileToWorldPosition(floor, originTileX, originTileY) + new Vector3(0, 0.01f, 0); // 0.01f above tile level

        for (int i = 0; i<length; i++)
        {
            switch (direction)
            {
                case GameController.Direction.XPlus: spawnPos += new Vector3(1, 0, 0); break;
                case GameController.Direction.XMinus: spawnPos += new Vector3(-1, 0, 0); break;
                case GameController.Direction.ZPlus: spawnPos += new Vector3(0, 0, 1); break;
                case GameController.Direction.ZMinus: spawnPos += new Vector3(0, 0, -1); break;
            }
            currentHighlightObjects.Add(Instantiate(highlightPlanePrefab, spawnPos, new Quaternion(0,0,0,0), this.transform));
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
