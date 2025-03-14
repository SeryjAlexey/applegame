using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private List<IInteractable> active_interactables = new List<IInteractable>();
    void Update()
    {
        UpdateActiveObjectsList();
        InteractWithActiveObjects();
    }

    private void InteractWithActiveObjects()
    {
        for (int i = 0; i < active_interactables.Count; i++)
        {
            active_interactables[i].Interact(Input.touches[i]);
        }
    }

    private void UpdateActiveObjectsList()
    {
        foreach (Touch touch in Input.touches)
        {
            IInteractable touched_interactable = checkForInteractableColliders(touch);
            if (touch.phase == TouchPhase.Began && !active_interactables.Contains(touched_interactable))
            {
                active_interactables.Add(touched_interactable);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                active_interactables.Remove(touched_interactable);
                touched_interactable.Interact(touch);
            }
        }

    }
    IInteractable checkForInteractableColliders(Touch touch)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(worldPosition, Mathf.Min(touch.radius, 3));
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.GetComponent<IInteractable>() != null)
            {
                return collider.GetComponent<IInteractable>();
            }
        }
        return FindAnyObjectByType<MapMover>();
    }
}
