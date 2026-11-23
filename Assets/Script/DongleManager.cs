using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleManager : MonoBehaviour
{
    public static DongleManager Instance { get; private set; }

    [Header("레벨 순서대로")]
    [SerializeField] private Dongle[] donglePrefabs;

    [Header("동글 스폰")]
    [SerializeField] private Transform dropPoint;   // 동글이 생성될 위치
    [SerializeField] private int maxSpawnLevel = 4; // 새 동글은 0~4 레벨만

    private Dongle currentDongle; // 지금 들고 있는 동글

    public int MaxLevel
    {
        get { return donglePrefabs.Length - 1; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnNewDongle();
    }

    public void SpawnNewDongle()
    {
        int randomLevel = Random.Range(0, maxSpawnLevel + 1);
        currentDongle = CreateDongle(randomLevel, dropPoint.position);
        currentDongle.Hold();
    }

    public Dongle CreateMergedDongle(int level, Vector3 position)
    {
        Dongle merged = CreateDongle(level, position);
        merged.Drop();
        return merged;
    }

    private Dongle CreateDongle(int level, Vector3 position)
    {
        return Instantiate(donglePrefabs[level], position, Quaternion.identity);
    }
}
