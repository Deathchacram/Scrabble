using System.Collections.Generic;
using UnityEngine;

public class FallLetter_script : MonoBehaviour
{
    public Sprite[] sprites;
    public Vector3 respawnPos;
    public float speed = 6, rotareSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    void Start()
    {
        float randomFloat = Random.Range(1f, 1f);
        respawnPos = transform.position + new Vector3(randomFloat, randomFloat, 0);
        speed += Random.Range(-3f, 3f);
        rotareSpeed = Random.Range(-90, 90);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.down * speed;
        Physics2D.IgnoreLayerCollision(0, 0);

        sr = GetComponent<SpriteRenderer>();
        int randomSprite = Random.Range(0, 32);
        sr.sprite = sprites[randomSprite];
    }

    void Update()
    {
        Vector3 rotare = new Vector3(0, 0, rotareSpeed * Time.deltaTime);
        transform.Rotate(rotare);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randomSprite = Random.Range(0, 32);
        sr.sprite = sprites[randomSprite];
        transform.position = respawnPos;
        rb.velocity = Vector3.down * speed;
    }
}
