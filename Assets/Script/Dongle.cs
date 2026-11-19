using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    private bool isMerg; //합치는 중 중복 처리
    private bool isDrop; //떨어뜨린 이후
    private float dropTime; //떨어뜨린 시각

    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Hold()
    {
        rb.simulated = false;
        isDrop = false;
    }

    public void Drop()
    {
        rb.simulated = true;
        isDrop = true;
        dropTime = Time.time;
    }
    
}
