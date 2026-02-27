using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float destroyTime = 2f;

    private TextMeshPro textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void Setup(int damage, bool isCritical)
    {
        textMesh.text = damage.ToString();

        if (isCritical)
        {
            textMesh.color = Color.red;
            textMesh.fontSize *= 1.3f;
        }
    }
}