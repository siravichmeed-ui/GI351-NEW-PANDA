
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Food Data")]
    public FoodData foodData;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Floating")]
    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;

    [Header("Sway")]
    public float swayAmount = 0.15f;
    public float swaySpeed = 1.5f;

    [Header("Rotation")]
    public float rotationSpeed = 20f;

    [Header("Destroy")]
    public float destroyX = -15f;

    private float startX;
    private float startY;

    // ตำแหน่ง X ที่ใช้เก็บการเคลื่อนที่จริง
    private float currentX;

    private float timeAlive;
    private float floatOffset;
    private float swayOffset;

    private float rotationDirection;

    private void Start()
    {
        // จำตำแหน่งตอน Spawn
        startX = transform.position.x;
        startY = transform.position.y;

        // ตำแหน่ง X ปัจจุบัน
        currentX = transform.position.x;

        // ทำให้อาหารแต่ละชิ้นลอยไม่พร้อมกัน
        floatOffset = Random.Range(0f, Mathf.PI * 2f);
        swayOffset = Random.Range(0f, Mathf.PI * 2f);

        // สุ่มทิศทางการหมุน
        rotationDirection = Random.value < 0.5f ? -1f : 1f;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        MoveAndFloat();
        Rotate();

        CheckDestroy();
    }

    private void MoveAndFloat()
    {
        // ความเร็วพื้นฐานของ Food
        float currentSpeed = moveSpeed;

        // รับโบนัสความเร็วจาก GameSpeedManager
        if (GameSpeedManager.Instance != null)
        {
            currentSpeed = GameSpeedManager.Instance.GetSpeed(moveSpeed);
        }

        // เคลื่อนที่จากขวาไปซ้ายแบบต่อเนื่อง
        currentX -= currentSpeed * Time.deltaTime;

        // ส่ายซ้ายขวาเล็กน้อย
        float sway = Mathf.Sin(
            timeAlive * swaySpeed + swayOffset
        ) * swayAmount;

        // ลอยขึ้นลง
        float y = startY +
                  Mathf.Sin(
                      timeAlive * floatSpeed + floatOffset
                  ) * floatHeight;

        transform.position = new Vector3(
            currentX + sway,
            y,
            transform.position.z
        );
    }

    private void Rotate()
    {
        // หมุนช้า ๆ
        transform.Rotate(
            0f,
            0f,
            rotationSpeed * rotationDirection * Time.deltaTime
        );
    }

    private void CheckDestroy()
    {
        // ถ้าอาหารออกนอกจอด้านซ้าย ให้ลบทิ้ง
        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ต้องชน Panda เท่านั้น
        if (!other.CompareTag("Panda"))
            return;

        PandaController panda = other.GetComponent<PandaController>();

        if (panda == null)
            return;

        // เช็กว่ามี FoodData หรือยัง
        if (foodData == null)
        {
            Debug.LogWarning(
                gameObject.name + " ยังไม่ได้ใส่ FoodData"
            );

            return;
        }

        // เพิ่มความอิ่มให้ Panda
        panda.Eat(foodData.fullnessValue);

        // ลบอาหารหลังจากกิน
        Destroy(gameObject);
    }
}

