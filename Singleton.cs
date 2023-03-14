using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance {
      get {
        if (_instance == null) {
           _instance = FindObjectOfType<T> ();
           if (_instance == null) {
             GameObject obj = new GameObject ();
             obj.name = typeof(T).Name;
             _instance = obj.AddComponent<T>();
           }
        }
       return _instance;
      }
    }
 
    public virtual void Awake ()
    {
       if (_instance == null) {
         this.transform.SetParent(null);
         _instance = this as T;
         DontDestroyOnLoad (this.gameObject);
    } else {
      Destroy (gameObject);
    }
  }
}