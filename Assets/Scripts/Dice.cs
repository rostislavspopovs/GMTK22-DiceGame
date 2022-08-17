using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;

    private Transform diceMesh;
    private Transform pivotSquare;
    private Transform XP;
    private Transform XM;
    private Transform ZP;
    private Transform ZM;

    private Animator anim;

    private bool moving;

    private float waitTime = 0.005f;

    void OnEnable()
    {
        diceMesh = transform.GetChild(0);
        pivotSquare = transform.GetChild(1);
        XP = pivotSquare.transform.GetChild(0);
        XM = pivotSquare.transform.GetChild(1);
        ZP = pivotSquare.transform.GetChild(2);
        ZM = pivotSquare.transform.GetChild(3);

        anim = GetComponent<Animator>();
        rb = diceMesh.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryMove(GameController.Direction dir)
    {
        if (!moving)
        {
            ToggleAnimator(false);
            StartCoroutine(MoveEnumerator(dir));
            return true;
        }

        return false;
    }

    private IEnumerator MoveEnumerator(GameController.Direction dir)
    {
        moving = true;
        switch (dir)
        {
            case GameController.Direction.XPlus:
                {
                    for (int d = 0; d < 30; d++)
                    {
                        diceMesh.RotateAround(XP.position, Vector3.forward, -3);
                        yield return new WaitForSeconds(waitTime);
                    }
                    diceMesh.transform.position -= new Vector3(1, 0, 0);
                    transform.position += new Vector3(1, 0, 0);

                    break;
                }

            case GameController.Direction.XMinus:
                {
                    for (int d = 0; d < 30; d++)
                    {
                        diceMesh.RotateAround(XM.position, Vector3.forward, 3);
                        yield return new WaitForSeconds(waitTime);
                    }
                    diceMesh.transform.position -= new Vector3(-1, 0, 0);
                    transform.position += new Vector3(-1, 0, 0);

                    break;
                }

            case GameController.Direction.ZPlus:
                {
                    for (int d = 0; d < 30; d++)
                    {
                        diceMesh.RotateAround(ZP.position, Vector3.right, 3);
                        yield return new WaitForSeconds(waitTime);
                    }
                    diceMesh.transform.position -= new Vector3(0, 0, 1);
                    transform.position += new Vector3(0, 0, 1);

                    break;
                }

            case GameController.Direction.ZMinus:
                {
                    for (int d = 0; d < 30; d++)
                    {
                        diceMesh.RotateAround(ZM.position, Vector3.right, -3);
                        yield return new WaitForSeconds(waitTime);
                    }
                    diceMesh.transform.position -= new Vector3(0, 0, -1);
                    transform.position += new Vector3(0, 0, -1);

                    break;
                }
        }
        moving = false;
        GameController.Instance.CallNextStep(dir);
        //toggleAnimator(true);
    }

    private void ToggleAnimator(bool toggle)
    {
        anim.enabled = toggle;
    }


    public void Ragdoll(Vector3 forceDir)
    {
        rb.isKinematic = false;
        //Vector3 forcePos = (diceMesh.transform.position - forceDir) + new Vector3(Random.Range(-0.5f, 0.5f), -1, Random.Range(-0.5f, 0.5f));
        //Debug.DrawLine(forcePos, forcePos + forceDir, Color.red, 20, false);
        rb.AddExplosionForce(2, transform.position-forceDir, 5, 0.5f, ForceMode.VelocityChange);
        Debug.DrawLine(transform.position - forceDir, transform.position, Color.red, 20, false);
    }

    public Transform getDiceMesh() 
    {
        return diceMesh;
    }

}
