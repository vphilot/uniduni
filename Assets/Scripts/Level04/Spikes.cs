using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level04
{
    public class Spikes : MonoBehaviour
    {
        private EdgeCollider2D edgeCollider;

        private void Start()
        {
            edgeCollider = GetComponent<EdgeCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                collision.gameObject.GetComponent<PlayerController2D>().Die();
            }
        }
    }
}

