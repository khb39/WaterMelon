using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("레벨 순서대로")]
    [SerializeField] private Dongle[] donglePrefabs;

    [Header("동글 스폰")]
    [SerializeField] private int maxSpawnLevel = 4; // 새 동글은 0~4 레벨만

    private bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    public int MaxLevel
    {
        get { return donglePrefabs.Length - 1; }
    }

    private void Awake()
    {
        Instance = this;
    }

    //동글 생성
    public Dongle SpawnNewDongle(Vector3 position)
    {
        int randomLevel = Random.Range(0, maxSpawnLevel + 1);
        Dongle newDongle = CreateDongle(randomLevel, position);
        newDongle.Hold();
        return newDongle;
    }

    //동글 합성
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

    //게임 오버
    public void GameOver()
    {
        if (isGameOver) return; // 중복 호출 방지
        isGameOver = true;

        Debug.Log("게임메니저 게임오버 실행");

        Time.timeScale = 0f; // 게임 정지
    }
}