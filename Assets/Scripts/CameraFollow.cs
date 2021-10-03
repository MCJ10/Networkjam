using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offsetY;

    public float top, left, right, bottom;

    void Start()
    {
    }

    void Update()
    {
        var newPosition = Vector3.Lerp(transform.position, player.position, .5f);

        newPosition.y += offsetY;

        newPosition.x = Mathf.Clamp(newPosition.x, left, right);
        newPosition.y = Mathf.Clamp(newPosition.y, bottom, top);
        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }
}
