using UnityEngine.PlayerLoop;

public class SingletonBase<T> where T : class, new()
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }

            return instance;
        }
    }
    public static void Init()
    {
        if (instance == null)
        {
            instance = new T();
        }
    }
}