using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType { HealthPotion, ManaPotion, Money }

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.Money;

    SpriteRenderer sprite;

    CircleCollider2D itemCollider;

    bool hasBeenCollected = false;

    public int value = 1;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        itemCollider = GetComponent<CircleCollider2D>();
    }

    void Show()
    {
        sprite.enabled = true;

        itemCollider.enabled = true;

        hasBeenCollected = false;
    }

    void Hide()
    {
        sprite.enabled = false;

        itemCollider.enabled = false;
    }

    void Collect()
    {
        Hide();

        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.HealthPotion:

                break;

            case CollectableType.ManaPotion:

                break;

            case CollectableType.Money:
                GameManager.sharedInstance.CollectObject(this);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenCollected)
        {
            Collect();
        }
    }
}
