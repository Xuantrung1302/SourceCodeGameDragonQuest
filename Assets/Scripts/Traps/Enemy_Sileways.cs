using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sileways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip silewaySound;

    public GameObject pos1, pos2;
    private Vector2 Target;
    void Start()
    {
        Target = pos1.transform.position;
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(silewaySound);
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        if (collision.CompareTag("pos1"))
        {
            Target = pos2.transform.position;
            transform.localScale = new Vector2(5, 5);
        }
        else if (collision.CompareTag("pos2"))
        {
            Target = pos1.transform.position;
            transform.localScale = new Vector2(-5, 5);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1.transform.position, pos2.transform.position);
    }
}
