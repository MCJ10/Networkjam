using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Doll : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveY(transform.position.y + .5f, 1f).
            SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void Collected()
    {
        GameManager.inst.CollectedDoll();

        transform.DOKill();
        transform.DOScale(Vector3.zero, 1f);

        var sprite = GetComponent<SpriteRenderer>();
        sprite.DOFade(0f, 1f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
    }
}
