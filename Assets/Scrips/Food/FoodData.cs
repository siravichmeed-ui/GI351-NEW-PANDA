using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "Panda Game/Food Data")]
public class FoodData : ScriptableObject
{
    [Header("ข้อมูลอาหาร")]
    public string foodName;

    public Sprite icon;

    [Header("ค่าความอิ่ม")]
    public float fullnessValue = 10f;
}