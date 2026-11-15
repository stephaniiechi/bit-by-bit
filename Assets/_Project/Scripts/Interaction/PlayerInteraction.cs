
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1.5f;
    [SerializeField] private TMP_Text interactionPrompt;

    private IInteractable currentInteractable;

    private void Update()
    {
        FindInteractable();

        bool canInteract = currentInteractable != null;

        if (interactionPrompt != null)
            interactionPrompt.gameObject.SetActive(canInteract);

        if (canInteract &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    private void FindInteractable()
    {
        currentInteractable = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            interactionRadius
        );

        float closestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable =
                hit.GetComponent<IInteractable>();

            if (interactable == null)
                continue;

            float distance = Vector2.Distance(
                transform.position,
                hit.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentInteractable = interactable;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
