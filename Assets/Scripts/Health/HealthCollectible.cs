using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                SoundManager.instance.PlaySound(pickupSound);
                health.AddHealth(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}
