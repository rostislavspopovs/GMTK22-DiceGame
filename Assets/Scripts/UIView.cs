using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{

    [SerializeField] private Text stepsLeftLabel;

    void Start()
    {
        GameController.Instance.AddOnStepEventAction(UpdateStepsLeft);
    }

    private void UpdateStepsLeft(int steps)
    {
        stepsLeftLabel.text = steps.ToString();
    }
}
