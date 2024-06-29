using UnityEngine;
using System.Collections;
using UnityEditor;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header ("FireTrap Times")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    [SerializeField] private AudioClip firetrapSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!triggered)
                StartCoroutine(ActiveFiretrap());
        }
    }
    private IEnumerator ActiveFiretrap()
    {
        //turn the sprite red  to notify the player  and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;

        //Wait for delay, activate trap, turn on  animtion, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //Wait until X second, deativate  trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);


    }
}