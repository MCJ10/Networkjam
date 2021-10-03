using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTrigger : MonoBehaviour
{
    void Start()
    {

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            GameManager.inst.Lose();
        }
        else if (col.CompareTag("Doll"))
        {
            col.transform.DOMove(transform.position, 1f);
            col.GetComponent<Doll>().Collected();
        }
        else if (col.CompareTag("Exit"))
        {
            if (GameManager.inst.dollCounter == 3)
                GameManager.inst.Win();
        }
    }
}
