using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleDroper : MonoBehaviour
{
    [Header("스폰 포인터 오브젝트")]
    [SerializeField] private GameObject spawnPointer;

    private Dongle currentDongle; // 지금 들고 있는 동글
    private Camera mainCamera;

    private float leftWallX = -2.75f;
    private float rightWallX = 2.75f;

    private float spawnDropCooldown = 0.5f;  // 다음 동글 스폰 대기시간

    private void Start()
    {
        mainCamera = Camera.main;
        SpawnNextDongle();
    }

    private void Update()
    {
        if (currentDongle == null) return; //들고있는 동글이 없을 경우

        FollowPointer();

        if (Input.GetMouseButtonUp(0)) // 왼쪽 클릭을 뗀 순간
        {
            DropDongle();
        }
    }

    private void FollowPointer()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        // 동글 반지름만큼 안쪽까지만 이동
        float radius = GetRadius(currentDongle);
        float clampedX = Mathf.Clamp(mouseWorldPos.x, leftWallX + radius, rightWallX - radius);

        Vector3 newPos = new Vector3(clampedX, spawnPointer.transform.position.y, 0f);
        spawnPointer.transform.position = newPos;
        currentDongle.transform.position = newPos; // 들고 있는 동글도 함께 이동
    }

    private void DropDongle()
    {
        currentDongle.Drop();
        currentDongle = null;
        StartCoroutine(SpawnAfterDelay());
    }

    private IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnDropCooldown);
        SpawnNextDongle();
    }

    private void SpawnNextDongle()
    {
        currentDongle = GameManager.Instance.SpawnNewDongle(spawnPointer.transform.position);
    }

    private float GetRadius(Dongle dongle)
    {
        CircleCollider2D col = dongle.GetComponent<CircleCollider2D>();
        return col.radius * dongle.transform.lossyScale.x;
    }
}
