using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Image fade;
    public AudioSource audioSource;
    public GameObject credits;

    void Start()
    {

    }

    public void Play()
    {
        audioSource.DOFade(0f, .45f);
        fade.DOFade(1f, .5f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void Return()
    {
        credits.SetActive(false);
    }
}
