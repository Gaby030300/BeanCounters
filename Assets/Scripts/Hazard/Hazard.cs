using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Hazard Properties")]
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private bool fade;
    [SerializeField] private float deadTime;

    private Vector2 moveAngle;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MoveObject();
        deadTime = 0.5f;
        fade = false;
    }

    void Update()
    {
        VerifyObjectCollision();
    }

    private void VerifyObjectCollision()
    {
        if (fade == true)
        {
            if (deadTime <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                deadTime -= Time.deltaTime;
            }
        }
    }

    private void MoveObject()
    {
        Vector2 moveAngle = new Vector2(Random.Range(-800f, -250f), Random.Range(250f, 600f));
        rb.AddForce(moveAngle, ForceMode2D.Force);
    }

    private void HandleGroundCollision()
    {
        if(rb != null)
        {
            Destroy(rb);
            spriteRenderer.sprite = brokenSprite;
            fade = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;

            case "Ground":
                HandleGroundCollision();
                break;
        }
    }
}
