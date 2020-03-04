using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider;
    private LayerMask playersLayerMask;
    public bool isActive = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playersLayerMask = (1 << LayerMask.NameToLayer("Players"));
    }

    private void FixedUpdate()
    {
        if (IsTargetingPlayer())
        {
            MakePlayerFloat(IsTargetingPlayer());
        }
    }

    private GameObject IsTargetingPlayer()
    {
        float distance = 5f;
        //RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, distance, playersLayerMask);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, distance, playersLayerMask);
        if (raycastHit.collider != null)
        {
            return raycastHit.collider.gameObject;
        }  else
        {
            return null;
        }
    }

    void MakePlayerFloat(GameObject player)
    {
        if(isActive)
        {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 150), ForceMode2D.Force);
        }
    }
}
