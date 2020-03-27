using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level01
{
    public class Catapult : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(15, 20), ForceMode2D.Impulse);
        }
    }
}

