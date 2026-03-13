using UnityEngine;

public class Dummy : MonoBehaviour
{
    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;
    public Transform popupPoint;

    [Header("Hit Sound")]
    public AudioSource audioSource;   // ตัวเล่นเสียง
    public AudioClip hitSound;        // เสียงโดนฟัน

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            PlayerAttack player = other.GetComponentInParent<PlayerAttack>();

            if (player != null)
            {
                bool isCritical;
                int finalDamage = player.GetDamage(out isCritical);

                TakeDamage(finalDamage, isCritical);
            }
        }
    }

    void TakeDamage(int damage, bool isCritical)
    {
        if (anim != null)
            anim.SetTrigger("Hit");

        // 🔊 เล่นเสียงโดนฟัน
        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);

        if (GameManager.Instance != null)
            GameManager.Instance.AddCoins(damage);

        ShowDamage(damage, isCritical);
    }

    void ShowDamage(int damage, bool isCritical)
    {
        if (damagePopupPrefab == null || popupPoint == null)
            return;

        GameObject popup = Instantiate(
            damagePopupPrefab,
            popupPoint.position,
            Quaternion.identity
        );

        DamagePopup dp = popup.GetComponent<DamagePopup>();
        if (dp != null)
            dp.Setup(damage, isCritical);
    }
}