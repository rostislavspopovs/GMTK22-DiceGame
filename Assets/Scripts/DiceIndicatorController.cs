using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceIndicatorController : Singleton<DiceIndicatorController>
{

    [SerializeField] private Transform dotOverlayTransform;
    [SerializeField] private List<GameObject> faceDotPrefabs;

    [SerializeField] private Transform XPSelector;
    [SerializeField] private Transform XMSelector;
    [SerializeField] private Transform ZPSelector;
    [SerializeField] private Transform ZMSelector;

    [SerializeField] private AnimationCurve dotFadeOutCurve;
    [SerializeField] private float dotFadeOutTime;
    private List<RawImage> currentDots = new List<RawImage>(6);

    void Start()
    {
        //GameController.Instance.AddOnStepEventAction(DecrementDot);
        currentDots = new List<RawImage>(dotOverlayTransform.GetChild(0).GetComponentsInChildren<RawImage>());
    }

    public void ResetDots(int face, float delay)
    {
        StartCoroutine(IResetDots(face, delay));
    }

    private IEnumerator IResetDots(int face, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (RawImage currentDot in currentDots.ToArray())
        {
            RemoveDot(currentDot);
        }

        Destroy(dotOverlayTransform.GetChild(0).gameObject);

        GameObject dots = Instantiate(faceDotPrefabs[face - 1], dotOverlayTransform);
        currentDots = new List<RawImage>(dots.GetComponentsInChildren<RawImage>());

        Debug.Log("Reset Dots: " + currentDots.Count);
    }

    public void DecrementDot(int stepsLeft, GameController.Direction dir)
    {
        if (stepsLeft == 0) return;
        Debug.Log("Decremented Dots: " + currentDots.Count);
        RemoveDot(currentDots[0]);
        AnimateSelector(dir);
    }

    private void RemoveDot(RawImage dot)
    {
        currentDots.Remove(dot);
        StartCoroutine(AnimUtils.AnimateOpacity(dot.gameObject.GetComponent<CanvasGroup>(), dotFadeOutTime, 0.15f, curve: dotFadeOutCurve));
    }

    private void AnimateSelector(GameController.Direction dir)
    {
        Transform transform = null;
        switch (dir)
        {
            case GameController.Direction.XPlus: transform = XPSelector; break;
            case GameController.Direction.XMinus: transform = XMSelector; break;
            case GameController.Direction.ZPlus: transform = ZPSelector; break;
            case GameController.Direction.ZMinus: transform = ZMSelector; break;
        }
        StartCoroutine(AnimUtils.TranslatePingPong(transform, 0.2f, transform.forward*0.1f, curve: AnimationCurve.EaseInOut(0, 0, 1, 1)));
    }


}
