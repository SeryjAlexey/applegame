
using UnityEngine;

public class MapMover : MonoBehaviour, IInteractable
{
    [SerializeField] Transform left_border;
    [SerializeField] Transform right_border;
    private Vector2 start_position = Vector2.zero;
    void IInteractable.Interact(Touch touch)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
        if(touch.phase == TouchPhase.Began)
        {
            start_position = worldPosition - (Vector2)transform.position;
        } else if(touch.phase == TouchPhase.Moved)
        {
            float new_position_x = Mathf.Min(Mathf.Max(worldPosition.x - start_position.x, left_border.position.x), right_border.position.x);
            transform.position = new Vector3(new_position_x,
            transform.position.y,
            Camera.main.nearClipPlane);
        }
    }
}
