using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverFlip : MonoBehaviour
{
    public Color enabledColor = Color.green;
    public Color disabledColor = Color.red;

    public GameObject platformColliderTarget; // Assign the target object in the Inspector
    public GameObject platformSpriteGroup; // Assign the target group in the Inspector

    private bool collisionState = true;
    private Renderer leverRenderer;

    private bool isPlayerNearLever = false;

    // Start is called before the first frame update
    void Start()
    {
        // Cache the Renderer component
        leverRenderer = GetComponent<Renderer>();
        // Set collision to false
        ToggleCollision();

    }

    // Update is called once per frame
    void Update()
    {
        // Check for player input (e key)
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearLever)
        {
            ToggleCollision();
        }
    }

    private void ToggleCollision()
    {
        // Toggle collision state
        collisionState = !collisionState;

        // Enable/disable the collider
        Collider2D targetCollider = platformColliderTarget.GetComponent<Collider2D>();
        if (targetCollider != null)
        {
            targetCollider.enabled = collisionState;
        }
        // Change lever and platform color based on collision state
        foreach (SpriteRenderer platformRenderer in platformSpriteGroup.GetComponentsInChildren<SpriteRenderer>())
        {
            platformRenderer.color = collisionState ? enabledColor : disabledColor;
        }

        leverRenderer.material.color = collisionState ? enabledColor : disabledColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearLever = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearLever = false;
        }
    }
}