using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPlane : MonoBehaviour
{

    [SerializeField] private TextMesh textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHighlightColor(Color color)
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
