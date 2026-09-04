
using UnityEngine;

public class PandaController : MonoBehaviour
{
    [Header("Jump")]
    public Rigidbody2D rb;
    public float jumpForce = 8f;

    [Header("Fullness")]
    public float currentFullness = 0f;
    public float maxFullness = 100f;

    [Header("Growth")]
    public float maxGrowthMultiplier = 1.5f;
    public float growthSmoothSpeed = 5f;

    [Header("Burst")]
    public float burstFullness = 200f;

    private Vector3 originalScale;

    private bool hasBurst = false;

    private void Start()
    {
        // จำขนาด Panda ตอนเริ่มเกม
        originalScale = transform.localScale;

        // เริ่มต้นที่ 0%
        currentFullness = 0f;
    }

    private void Update()
    {
        // คลิกเพื่อกระโดด
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // ระบบตัวใหญ่ขึ้น
        UpdateGrowth();

        // ตรวจสอบว่าถึงจุดระเบิดหรือยัง
        CheckBurst();
    }

    // =========================================================
    // JUMP
    // =========================================================

    private void Jump()
    {
        if (rb == null)
        {
            Debug.LogWarning(
                "PandaController ยังไม่ได้ใส่ Rigidbody2D"
            );

            return;
        }

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );
    }

    // =========================================================
    // EAT
    // =========================================================

    public void Eat(float fullnessAmount)
    {
        if (hasBurst)
            return;

        // เพิ่มความอิ่ม
        // สามารถเกิน 100% ได้
        currentFullness += fullnessAmount;

        Debug.Log(
            "Panda กินอาหาร | Fullness: " +
            currentFullness +
            " / " +
            maxFullness
        );
    }

    // =========================================================
    // GROWTH
    // =========================================================

    private void UpdateGrowth()
    {
        // ถ้า Fullness ยังไม่เกิน 100%
        // Panda จะกลับไปขนาดปกติ
        if (currentFullness <= maxFullness)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale,
                Time.deltaTime * growthSmoothSpeed
            );

            return;
        }

        // =====================================================
        // คำนวณว่าล้นเกินมาเท่าไหร่
        // =====================================================

        float overfill =
            currentFullness - maxFullness;

        // แปลงเป็น 0 - 1
        //
        // 100% = 0
        // 150% = 0.5
        // 200% = 1
        float growthPercent =
            Mathf.Clamp01(
                overfill / maxFullness
            );

        // =====================================================
        // คำนวณขนาด Panda
        // =====================================================

        float multiplier =
            Mathf.Lerp(
                1f,
                maxGrowthMultiplier,
                growthPercent
            );

        Vector3 targetScale =
            originalScale * multiplier;

        // ค่อย ๆ ขยายตัว
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * growthSmoothSpeed
        );
    }

    // =========================================================
    // CHECK BURST
    // =========================================================

    private void CheckBurst()
    {
        if (hasBurst)
            return;

        if (currentFullness >= burstFullness)
        {
            hasBurst = true;

            Burst();
        }
    }

    // =========================================================
    // BURST
    // =========================================================

    private void Burst()
    {
        Debug.Log("PANDA BURST!");

        // ตอนนี้หยุดเกมไว้ก่อน
        // เดี๋ยวค่อยทำ Animation ระเบิด + Game Over
        Time.timeScale = 0f;
    }
}

