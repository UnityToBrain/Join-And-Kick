using DG.Tweening;
using UnityEngine;

public class blade : MonoBehaviour
{

    void Start()
    {
        transform.DOMoveY(-2f, Random.Range(1f, 1.3f)).SetLoops(1000000, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

}
