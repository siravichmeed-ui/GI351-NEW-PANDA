
using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager Instance;

    [Header("การเร่งความเร็ว")]
    [Tooltip("กี่วินาทีให้เพิ่มความเร็ว 1 ครั้ง")]
    public float increaseEvery = 10f;

    [Tooltip("หยุดเพิ่มความเร็วเมื่อเกมถึงกี่วินาที")]
    public float stopIncreasingAt = 60f;

    [Tooltip("เพิ่มความเร็วครั้งละเท่าไหร่")]
    public float speedIncrease = 1f;

    private float gameTime = 0f;
    private float increaseTimer = 0f;

    // ค่าความเร็วที่เพิ่มขึ้นจาก Base Speed
    private float speedBonus = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        // ถ้ายังไม่ถึงเวลาหยุดเพิ่ม
        if (gameTime < stopIncreasingAt)
        {
            increaseTimer += Time.deltaTime;

            if (increaseTimer >= increaseEvery)
            {
                increaseTimer -= increaseEvery;

                // เพิ่มทีละค่าที่กำหนด
                speedBonus += speedIncrease;

                Debug.Log("Speed Bonus เพิ่มเป็น: " + speedBonus);
            }
        }
    }

    // ให้ Script อื่นเรียกใช้ความเร็วปัจจุบัน
    public float GetSpeed(float baseSpeed)
    {
        return baseSpeed + speedBonus;
    }

    // ดูเวลาที่เกมเล่นไป
    public float GetGameTime()
    {
        return gameTime;
    }

    // ดู Bonus ปัจจุบัน
    public float GetSpeedBonus()
    {
        return speedBonus;
    }
}

