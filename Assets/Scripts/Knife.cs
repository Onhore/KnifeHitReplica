using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Knife : MonoBehaviour
{
    [Header("Throw options")]
    [SerializeField] private Vector2 throwForce;
   
    [Header("Reflection options")]
    [SerializeField] private ParticleSystem WoodFX;
    [SerializeField] private float ReflectionForce = 5f;
    [SerializeField] private float DestroyDelay;

    private bool isActive = true;

    private Rigidbody2D rigidBody;
    new private BoxCollider2D collider;

    private void Awake()
    {
       rigidBody = GetComponent<Rigidbody2D>();
       collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
       if (Input.GetMouseButtonDown(0) && isActive)
       {
           rigidBody.gravityScale = 1;
           rigidBody.AddForce(throwForce, ForceMode2D.Impulse);
           GameManager.Instance.ThrowKnife?.Invoke();
       }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isActive)
            return;

        isActive = false;

        if (other.collider.CompareTag("Log"))
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(other.collider.transform);
            WoodFX.Play();

            collider.offset = new Vector2(collider.offset.x, -0.4f);
            collider.size = new Vector2(collider.size.x, 1.2f);

            GameManager.Instance.KnifeHit?.Invoke();
        }
        else if (other.collider.CompareTag("Knife"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -ReflectionForce);
            Destroy(gameObject, DestroyDelay);
            collider.isTrigger = true;
            GameManager.Instance.KnifeHit?.Invoke();
        }
    }

    
}
