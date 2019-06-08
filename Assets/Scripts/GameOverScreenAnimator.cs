using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenAnimator : MonoBehaviour
{
    [SerializeField] private Transform inGameHud;
    void Awake()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnGameOver(null);
        }
    }
    
    public void OnGameOver(List<Player> playerScores)
    {
        Sequence animationSeq= DOTween.Sequence();
        animationSeq.Append(Camera.main.transform.DOMoveY(-.1f, .2f)).Join(inGameHud.DOScale(Vector3.one * .9f, .2f))
            .AppendInterval(.2f)
            .Append(GetComponent<RectTransform>().DOScale(Vector3.one, .3f).SetEase(Ease.OutBounce));
    }
    
    public void OnPlayAgainClicked()
    {
        GameManager.Instance.ReloadGame();
    }
}
