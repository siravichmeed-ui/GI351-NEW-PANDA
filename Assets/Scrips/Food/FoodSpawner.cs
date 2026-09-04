
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Food ที่จะ Spawn")]
    public GameObject[] foodPrefabs;

    [Header("ตำแหน่ง Spawn")]
    public float spawnX = 12f;

    [Header("ช่วงความสูง")]
    public float minY = -2.5f;
    public float maxY = 2.5f;

    [Header("เวลา Spawn")]
    public float spawnInterval = 2f;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnFood();

            spawnTimer = 0f;
        }
    }

    private void SpawnFood()
    {
        // ถ้ายังไม่มี Food Prefab
        if (foodPrefabs == null || foodPrefabs.Length == 0)
        {
            Debug.LogWarning(
                "FoodSpawner ยังไม่ได้ใส่ Food Prefab"
            );

            return;
        }

        // สุ่ม Food
        int randomIndex = Random.Range(
            0,
            foodPrefabs.Length
        );

        GameObject foodPrefab =
            foodPrefabs[randomIndex];

        // สุ่มตำแหน่ง Y
        float randomY = Random.Range(
            minY,
            maxY
        );

        // ตำแหน่ง Spawn
        Vector3 spawnPosition = new Vector3(
            spawnX,
            randomY,
            0f
        );

        // สร้าง Food
        Instantiate(
            foodPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}

