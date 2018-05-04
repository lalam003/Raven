using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static GameObject singleton = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {  
                Debug.LogError("Object not found. Creating new object...");
                singleton = new GameObject();
                instance = singleton.AddComponent<T>();
                singleton.name = typeof(T).ToString();
            }

            return instance;
        }
    }

    public static GameObject GameObject
    {
        get { return Instance.gameObject; }
    }

    public static Transform Transform
    {
        get { return Instance.transform; }
    }
}
