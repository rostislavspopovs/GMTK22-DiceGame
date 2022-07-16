using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Transform diceMesh;
    private Transform pivotSquare;
    private Transform XP;
    private Transform XM;
    private Transform ZP;
    private Transform ZM;
    // Start is called before the first frame update
    void Start()
    {
        diceMesh = transform.GetChild(0);
        pivotSquare =transform.GetChild(1);
        XP = pivotSquare.transform.GetChild(0);
        XM = pivotSquare.transform.GetChild(1);
        ZP = pivotSquare.transform.GetChild(2);
        ZM = pivotSquare.transform.GetChild(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(GameController.Direction dir)
    {
        switch (dir)
        {
            case GameController.Direction.XPlus:
                {
                    diceMesh.RotateAround(XP.position, Vector3.forward, 90);
                    diceMesh.transform.position += new Vector3(1, 0, 0);
                    transform.position += new Vector3(1, 0, 0);

                    break;
                }
        }
    }
}
