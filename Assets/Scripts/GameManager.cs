using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public Text dollCounterText;
    public Image fade;
    public Player player;
    public Image lost, won;

    public AudioSource audioSource;
    public AudioClip dollCollectedClip;


    public float timescale
    {
        get
        {
            return Time.timeScale;
        }
        set
        {
            Time.timeScale = value;
        }
    }


    [NonSerialized] public int dollCounter;


    void Awake()
    {
        inst = this;

        dollCounter = 0;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        fade.color = Color.black;
        fade.DOFade(0f, 2f).SetEase(Ease.OutExpo);
    }

    void Update()
    {

    }

    public void CollectedDoll()
    {
        dollCounter++;
        dollCounterText.text = dollCounter.ToString();
        audioSource.PlayOneShot(dollCollectedClip);
    }

    public void Lose()
    {
        player.Die();
        fade.DOFade(1f, 2f).SetEase(Ease.OutExpo);
        DOTween.To(()=> timescale, x=> timescale = x, .5f, 2f).SetEase(Ease.InSine).OnComplete(() =>
        {
            lost.DOFade(1f, 1f).SetUpdate(true).OnComplete(() =>
            {
                DOVirtual.DelayedCall(5f, () =>
                {
                    lost.DOFade(0f, 1f).SetUpdate(true).OnComplete(() =>
                        SceneManager.LoadScene("MainMenu"));
                });
            });
        });
    }

    public void Win()
    {
        player.Die();
        fade.DOFade(1f, 2f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            won.DOFade(1f, 1f).SetUpdate(true).OnComplete(() =>
            {
                DOVirtual.DelayedCall(5f, () =>
                {
                    won.DOFade(0f, 1f).SetUpdate(true).OnComplete(() =>
                        SceneManager.LoadScene("MainMenu"));
                });
            });
        });
    }
}
