using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    private float limitTime = 2f; // 선에 시간만큼 머물면 게임오버
    private float graceTime = 1f; // 막 떨어뜨린 동글은 이 시간 동안 무시
    
    private List<Dongle> donglesOnLine = new List<Dongle>(); // 지금 선에 걸쳐 있는 동글들
    private float overTimer;
    private bool isGameOver;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Dongle dongle)) return; // 동글이 아니면 무시

        if (!donglesOnLine.Contains(dongle))
        {
            donglesOnLine.Add(dongle);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Dongle dongle)) return;

        donglesOnLine.Remove(dongle);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        if(IsDongleOver())
        {
            overTimer += Time.deltaTime;
        }
        else
        {
            overTimer = 0.0f;
        }

        if (overTimer >= limitTime)
        {
            GameManager.Instance.GameOver();
        }
        
    }
    private bool IsDongleOver()
    {
        foreach (Dongle dongle in donglesOnLine)
        {
            if (!dongle.IsDrop) continue; // 들고 있는 동글은 제외

            float timeSinceDrop = Time.time - dongle.DropTime;
            if (timeSinceDrop > graceTime)
            {
                return true;
            }
        }
        return false;
    }
}