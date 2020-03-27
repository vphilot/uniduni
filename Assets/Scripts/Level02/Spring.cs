using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level02
{
    public class Spring : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //only trigger if contacting from top (jumping on it)
            bool comingFromTop = collision.GetContact(0).normal.y < 0;
            if (comingFromTop)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 25), ForceMode2D.Impulse);
            }
        }
    }

}

