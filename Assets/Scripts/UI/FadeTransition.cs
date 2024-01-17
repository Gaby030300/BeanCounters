using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] float timeToFade;
    [SerializeField] Image blackScreen;

    private void OnEnable()
    {
        blackScreen.DOColor(new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 0), timeToFade);
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }
}
