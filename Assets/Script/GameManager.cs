using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("레벨 순서대로")]
    [SerializeField] private Dongle[] donglePrefabs;

    [Header("동글 스폰")]
    [SerializeField] private int maxSpawnLevel = 4; // 새 동글은 0~4 레벨만

    public int MaxLevel
    {
        get { return donglePrefabs.Length - 1; }
    }

    private void Awake()
    {
        Instance = this;
    }

    public Dongle SpawnNewDongle(Vector3 position)
    {
        int randomLevel = Random.Range(0, maxSpawnLevel + 1);
        Dongle newDongle = CreateDongle(randomLevel, position);
        newDongle.Hold();
        return newDongle;
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