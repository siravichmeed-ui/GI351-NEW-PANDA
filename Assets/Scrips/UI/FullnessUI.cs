
using UnityEngine;
using UnityEngine.UI;

public class FullnessUI : MonoBehaviour
{
    [Header("Panda")]
    public PandaController panda;

    [Header("UI")]
    public Image fillImage;

    [Header("Shake")]
    public float maxShakeAmount = 15f;
    public float shakeSpeed = 20f;

    private RectTransform barRect;
    private Vector2 originalPosition;

    private void Start()
    {
        barRect = GetComponent<RectTransform>();

        if (barRect != null)
        {
            originalPosition = barRect.anchoredPosition;
        }
    }

    private void Update()
    {
        if (panda == null)
            return;

        if (fillImage == null)
            return;

        UpdateFill();
        UpdateShake();
    }

    private void UpdateFill()
    {
        // คำนวณเปอร์เซ็นต์ความอิ่ม
        float fullnessPercent =
            panda.currentFullness / panda.maxFullness;

        // หลอดแสดงได้สูงสุดแค่ 100%
        // แต่ currentFullness จริงสามารถเกิน 100 ได้
        fillImage.fillAmount =
            Mathf.Clamp01(fullnessPercent);
    }

    private void UpdateShake()
    {
        if (barRect == null)
            return;

        // ถ้ายังไม่เกิน 100%
        if (panda.currentFullness <= panda.maxFullness)
        {
            // ค่อย ๆ กลับไปตำแหน่งเดิม
            barRect.anchoredPosition = Vector2.Lerp(
                barRect.anchoredPosition,
                originalPosition,
                Time.deltaTime * 10f
            );

            return;
        }

        // คำนวณว่าล้นเกินมาเท่าไหร่
        float overfill =
            panda.currentFullness - panda.maxFullness;

        // 100 = ล้น 100%
        // เช่น
        // 110 = 0.1
        // 150 = 0.5
        // 200 = 1
        float shakePercent =
            Mathf.Clamp01(overfill / panda.maxFullness);

        // คำนวณความแรงของการสั่น
        float shakeAmount =
            shakePercent * maxShakeAmount;

        // สร้างการสั่น
        float x =
            Mathf.Sin(Time.time * shakeSpeed) *
            shakeAmount;

        float y =
            Mathf.Cos(Time.time * shakeSpeed * 1.3f) *
            shakeAmount;

        // ขยับหลอดจากตำแหน่งเดิม
        barRect.anchoredPosition =
            originalPosition +
            new Vector2(x, y);
    }
}

