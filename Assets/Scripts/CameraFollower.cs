using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform focus;
    [Range(0,0.2f)] [SerializeField] private float followSmoothingFactor;

    [SerializeField] private Vector3 camOffset = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate runs every frame but after the game logic, this is perfect for visual updates such as cameras.
    void LateUpdate()
    {
        Vector3 newPos = focus.position + camOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, followSmoothingFactor);
    }

    public void SetFocus(Transform focus)
    {
        this.focus = focus;
    }
}
