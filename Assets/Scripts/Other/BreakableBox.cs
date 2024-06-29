using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private AudioClip breakBoxsound;
    private Animator animator;
    private bool isBroken = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("FireBall") && !isBroken)
        {
            isBroken = true;
            SoundManager.instance.PlaySound(breakBoxsound);
            animator.SetTrigger("isBroken1");
            StartCoroutine(BreakBox());
        }
    }

    IEnumerator BreakBox()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject); 
    }
}
