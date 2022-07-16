using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;


    public static T Instance => instance;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Multiple instances of the singleton type '" + instance.GetType() + "' were found. Deleting this instance");
            Destroy(gameObject);
        }
    }
}
