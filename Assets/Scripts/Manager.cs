using DG.Tweening;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public bool canPlay = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            canPlay = true;
        });
    }
}