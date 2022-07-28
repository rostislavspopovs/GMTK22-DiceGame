using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimUtils
{
    public static IEnumerator Translate(Transform transform, float time, Vector3 delta, System.Action callback = null, AnimationCurve curve = null)
    {
        if (curve == null) curve = AnimationCurve.Linear(0,0,1,1);

        float t = 0;
        Vector3 initPos = transform.localPosition;
        while (t < time && transform != null)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(initPos, initPos + delta, curve.Evaluate(t/time));
            yield return new WaitForEndOfFrame();
        }
        callback?.Invoke();
    }

    public static IEnumerator AnimateOpacity(CanvasGroup cg, float time, float goal, System.Action callback = null, AnimationCurve curve = null)
    {
        if (curve == null) curve = AnimationCurve.Linear(0, 0, 1, 1);

        float t = 0;
        float initParam = cg.alpha;
        while (t < time && cg != null)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(initParam, goal, curve.Evaluate(t/time));
            yield return new WaitForEndOfFrame();
        }
        callback?.Invoke();
    }

    public static IEnumerator TranslatePingPong(Transform transform, float time, Vector3 delta, System.Action callback = null, AnimationCurve curve = null)
    {
        if (curve == null) curve = AnimationCurve.Linear(0, 0, 1, 1);

        float t = 0;
        Vector3 initPos = transform.localPosition;
        while (t < time && transform != null)
        {
            t += Time.deltaTime;
            if (t < time / 2)
            {
                transform.localPosition = Vector3.Lerp(initPos, initPos + delta, curve.Evaluate(t / time*2));
            }
            else
            {
                transform.localPosition = Vector3.Lerp(initPos + delta, initPos, curve.Evaluate(t / time*2));
            }
            yield return new WaitForEndOfFrame();
        }

        callback?.Invoke();
    }
}
