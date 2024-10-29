using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LoopAnim : MonoBehaviour
{
    [SerializeField] float delay = .3f;
    [SerializeField] Vector3 originSize;
    [SerializeField] float toRatio = 1.1f;
    [SerializeField] float startDuration = .25f, endDuration = .38f;
    [SerializeField] Ease startEase = Ease.Linear, endEase = Ease.Linear;
    [SerializeField] AnimationCurve startAC, endAC;
    [SerializeField] bool loopX = true, loopY = true;

    Vector3 toSize = Vector3.one;


    Tweener tweener;

    void OnValidate()
    {
        UIAppear uIAppear = GetComponent<UIAppear>();
        if (uIAppear)
            delay = uIAppear.appearDelay + uIAppear.duration;

        originSize = transform.localScale;
    }

    void OnEnable()
    {
        StartCoroutine(playScale());
    }



    IEnumerator playScale()
    {
        if (loopX)
            toSize.x = originSize.x * toRatio;
        else
            toSize.x = originSize.x;

        if (loopY)
            toSize.y = originSize.y * toRatio;
        else
            toSize.y = originSize.y;

        float t = delay;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }

        transform.localScale = originSize;
        bool wait;

        while (true)
        {
            wait = true;
            if (startEase != Ease.Unset)
                tweener = transform.DOScale(toSize, startDuration).SetEase(startEase).OnComplete(delegate { wait = false; });
            else
                tweener = transform.DOScale(toSize, startDuration).SetEase(startAC).OnComplete(delegate { wait = false; });

            while (wait)
                yield return null;

            wait = true;
            if (endEase != Ease.Unset)
                tweener = transform.DOScale(originSize, endDuration).SetEase(endEase).OnComplete(delegate { wait = false; });
            else
                tweener = transform.DOScale(originSize, endDuration).SetEase(endAC).OnComplete(delegate { wait = false; });

            while (wait)
                yield return null;
        }
    }



    void OnDisable()
    {
        tweener.Kill();
    }

}