
using UnityEngine;

public class RoomTransition : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform destination;

    public void Interact()
    {
        if (destination == null)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb == null)
            return;

        rb.linearVelocity = Vector2.zero;
        rb.position = new Vector2(
            destination.position.x,
            destination.position.y
        );
    }
}
