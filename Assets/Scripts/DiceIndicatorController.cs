using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceIndicatorController : Singleton<DiceIndicatorController>
{

    [SerializeField] private Transform dotOverlayTransform;
    [SerializeField] private List<GameObject> faceDotPrefabs;
    private List<RawImage> currentDots = new List<RawImage>(6);

    void Start()
    {
        //GameController.Instance.AddOnStepEventAction(DecrementDot);
        currentDots = new List<RawImage>(dotOverlayTransform.GetChild(0).GetComponentsInChildren<RawImage>());
    }

    public void ResetDots(int face)
    {
        foreach(RawImage currentDot in currentDots.ToArray())
        {
            RemoveDot(currentDot);
        }

        Destroy(dotOverlayTransform.GetChild(0).gameObject);

        GameObject dots = Instantiate(faceDotPrefabs[face - 1], dotOverlayTransform);
        currentDots = new List<RawImage>(dots.GetComponentsInChildren<RawImage>());

        Debug.Log("Reset Dots: " + currentDots.Count);
    }

    public void DecrementDot(int stepsLeft)
    {
        if (stepsLeft == 0) return;
        Debug.Log("Decremented Dots: " + currentDots.Count);
        RemoveDot(currentDots[0]);
    }

    private void RemoveDot(RawImage dot)
    {
        currentDots.Remove(dot);
        Destroy(dot.gameObject);
    }


}
