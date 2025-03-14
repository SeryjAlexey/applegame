using UnityEngine;

public class MovableObject : MonoBehaviour, IInteractable
{
    private Rigidbody2D rb;
    private Collider2D collider2d;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }
    public void Interact(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)
        {
            collider2d.isTrigger = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
          Vector2 new_position = Camera.main.ScreenToWorldPoint(touch.position);
          transform.position = new Vector3(new_position.x, new_position.y, transform.position.z);
        }
        else if(touch.phase == TouchPhase.Ended)
        {
            collider2d.isTrigger = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
