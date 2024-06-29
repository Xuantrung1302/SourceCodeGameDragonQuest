using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] private AudioClip enemydamgeSound;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                SoundManager.instance.PlaySound(enemydamgeSound);
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
