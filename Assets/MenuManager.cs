using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image hand;
    [SerializeField] private TextMeshProUGUI tp;
    [SerializeField] private GameObject TapMenu;
    void Start()
    {
        hand.transform.DOLocalMoveX(200f,0.8f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutSine);
        tp.transform.DOScale(1.2f, 0.5f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public void StartTheGame()
    {
        PlayerManager.PlayerManagerCls.gameState = true;
        TapMenu.SetActive(false);
    }

}
