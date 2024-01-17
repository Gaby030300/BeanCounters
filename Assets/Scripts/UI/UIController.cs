using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private LoadSceneManager loadSceneManager;
    void Start()
    {
        playButton.onClick.AddListener(loadSceneManager.LoadSceneAsync);
    }
}
