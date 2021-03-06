﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level04
{
    public class Key : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private Chest chest;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            chest = GameObject.Find("Chest").GetComponent<Chest>();
            chest.hasExternalCondition = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                //gameObject.SetActive(false);
                //one character has picked the key up so now we can made conditions complete
                chest.isExternalConditionSatisfied = true;
                gameObject.transform.parent = collision.gameObject.transform;
                gameObject.transform.localPosition = new Vector3(0, 0.75f, 0);

            }
        }
    }
}

