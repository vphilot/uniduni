using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level01
{
    public class Button : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            GameObject.Find("Cannon").GetComponent<Cannon>().Push();
        }
    }
}


