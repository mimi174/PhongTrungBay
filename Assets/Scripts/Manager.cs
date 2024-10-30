using DG.Tweening;
using UnityEngine;

//gán cho obj Manager
//Tạo ra một lớp quản lý trong Unity, đảm bảo rằng chỉ có một thể hiện của nó tồn tại và giữ trạng thái cho phép chơi

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