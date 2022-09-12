using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Singleton<InputController>
{
    [Header("Camera")]
    [SerializeField] private new Camera camera;

    [Header("Swipe Parameters")]
    [SerializeField][Range(0, 1)] private float swipeSimillarityFactor;
    [SerializeField] private float minimumSwipeDistance;
    [SerializeField] private float minimumSwipeTime;


    private InputActions inputActions;
    private Vector3 swipeStartPos;
    private Vector3 swipeEndPos;
    private double swipeStartTime;

    private bool firstTouch = true;

    private new void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        inputActions.InGame.Enable();

        inputActions.InGame.PrimaryTouch.started += SwipeStarted;
        inputActions.InGame.PrimaryTouch.canceled += SwipeStopped;

        swipeEndPos = camera.transform.position;

    }


    private void SwipeStarted(InputAction.CallbackContext context)
    {
        if (firstTouch)
        {
            firstTouch = false;
            StartCoroutine(FirstTouchWait(context));
        }
        else 
        {
            swipeStartPos = GetWorldClickPos();
            swipeStartTime = context.startTime;
        }
    }

    private IEnumerator FirstTouchWait(InputAction.CallbackContext context)
    {
        yield return new WaitForEndOfFrame();
        SwipeStarted(context);
    }

    private void SwipeStopped(InputAction.CallbackContext context)
    {
        swipeEndPos = GetWorldClickPos();
        double swipeEndTime = Time.timeAsDouble;

        GameController.Direction? swipedDirection = TestForSwipes(swipeStartPos, swipeEndPos);
        if (swipedDirection != null)
        {
            GameController.Instance.BeginMoveSequence(swipedDirection.Value);
        }
    }

    private GameController.Direction? TestForSwipes(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        Vector2 swipeVector = (camera.WorldToScreenPoint(endWorldPos) - camera.WorldToScreenPoint(startWorldPos)).normalized;

        Debug.DrawLine(startWorldPos, endWorldPos, Color.white, 5);

        Debug.DrawLine(camera.transform.position, camera.transform.position + new Vector3(1, 0, 0), Color.red, 5, false);
        Debug.DrawLine(camera.transform.position, camera.transform.position + new Vector3(-1, 0, 0), Color.blue, 5, false);
        Debug.DrawLine(camera.transform.position, camera.transform.position + new Vector3(0, 0, 1), Color.yellow, 5, false);
        Debug.DrawLine(camera.transform.position, camera.transform.position + new Vector3(0, 0, -1), Color.green, 5, false);

        if (Vector2.Dot(swipeVector, GetXPScreenVector()) >= swipeSimillarityFactor)
        {
            return GameController.Direction.XPlus;
        }
        if (Vector2.Dot(swipeVector, GetZPScreenVector()) >= swipeSimillarityFactor)
        {
            return GameController.Direction.ZPlus;
        }
        if (Vector2.Dot(swipeVector, GetXMScreenVector()) >= swipeSimillarityFactor)
        {
            return GameController.Direction.XMinus;
        }
        if (Vector2.Dot(swipeVector, GetZMScreenVector()) >= swipeSimillarityFactor)
        {
            return GameController.Direction.ZMinus;
        }

        return null;

    }

    private Vector3 GetWorldClickPos()
    {
        Vector3 pos = inputActions.InGame.PrimaryPosition.ReadValue<Vector2>();
        pos.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(pos);
    }

    private Vector2 GetXPScreenVector()
    {
        Vector2 vector = camera.WorldToScreenPoint(camera.transform.position + new Vector3(1, 0, 0)) - camera.WorldToScreenPoint(camera.transform.position);
        return vector.normalized;
    }

    private Vector2 GetXMScreenVector()
    {
        Vector2 vector = camera.WorldToScreenPoint(camera.transform.position + new Vector3(-1, 0, 0)) - camera.WorldToScreenPoint(camera.transform.position);

        return vector.normalized;
    }

    private Vector2 GetZPScreenVector()
    {
        Vector2 vector = camera.WorldToScreenPoint(camera.transform.position + new Vector3(0, 0, 1)) - camera.WorldToScreenPoint(camera.transform.position);

        return vector.normalized;
    }

    private Vector2 GetZMScreenVector()
    {
        Vector2 vector = camera.WorldToScreenPoint(camera.transform.position + new Vector3(0, 0, -1)) - camera.WorldToScreenPoint(camera.transform.position);
        return vector.normalized;
    }



}
