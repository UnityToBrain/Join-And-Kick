using DG.Tweening;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private bool LeftRight;
    void Start()
    {
        if (LeftRight)
            transform.DORotate(new Vector3(0f, -90f, 0f), 1f).SetLoops(100000,LoopType.Yoyo).SetEase(Ease.InOutSine);
        else
            transform.DORotate(new Vector3(0f, 90f, 0f), 1f).SetLoops(100000,LoopType.Yoyo).SetEase(Ease.InOutSine);

    }

  
}
