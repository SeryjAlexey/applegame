using UnityEngine;

public class MovableObject : MonoBehaviour, IInteractable
{
    private Rigidbody2D rb;
    [SerializeField]private Collider2D lower_collider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Interact(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)
        {
            lower_collider.isTrigger = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
          Vector2 new_position = Camera.main.ScreenToWorldPoint(touch.position);
          transform.position = new Vector3(new_position.x, new_position.y, transform.position.z);
        }
        else if(touch.phase == TouchPhase.Ended)
        {
            if(Physics2D.OverlapAreaAll(lower_collider.bounds.min, lower_collider.bounds.max).Length <= 2)
            {
              lower_collider.isTrigger = false;
              rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
