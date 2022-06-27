using UnityEngine;

/// <summary>
/// MonoBehaviorを継承しないSingletonクラス
/// </summary>
/// <typeparam name="T">instanceを作成する派生クラス</typeparam>
public class Singleton<T>where T : Singleton<T>, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            return GetOrCreateInstance<T>();
        }
    }

    protected static InheritSingletonType GetOrCreateInstance<InheritSingletonType>()
        where InheritSingletonType : class, T, new()
    {
        if (IsCreated)
        {
            // 基底クラスから呼ばれた後に継承先から呼ばれるとエラーになる。先に継承先から呼ぶ
            if (!typeof(InheritSingletonType).IsAssignableFrom(_instance.GetType()))
            {
                Debug.LogErrorFormat(
                "{1}が{0}を継承していません",
                typeof(InheritSingletonType),
                _instance.GetType()
            );
            }
        }
        else
        {
            _instance = new InheritSingletonType();
        }
        return _instance as InheritSingletonType;
    }

    public static bool IsCreated
    {
        get { return _instance != null; }
    }

    /// <summary>
    /// コンストラクタ（外部からの呼び出し禁止）
    /// </summary>
    protected Singleton() { }
}