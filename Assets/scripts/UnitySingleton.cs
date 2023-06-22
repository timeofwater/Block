using UnityEngine;

// 单例实现
public class UnitySingleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance) 
            {
                return _instance;
            }
            _instance = FindObjectOfType(typeof(T)) as T;
            if (_instance) 
            {
                return _instance;
            }
            GameObject obj = new GameObject();
            // obj.hideFlags = HideFlags.DontSave;
            // obj.hideFlags = HideFlags.HideAndDontSave;
            obj.name = typeof(T).FullName;
            _instance = (T)obj.AddComponent(typeof(T));
            return _instance;
        }
    }
    public virtual void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}