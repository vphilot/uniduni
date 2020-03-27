using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level10
{
    public class ProppellerTrigger : MonoBehaviour

    {
        private bool isActivated = false;
        private GameObject propellerParticleSystem;

        private void Start()
        {
            propellerParticleSystem = GameObject.Find("PropellerParticleSystem");
            propellerParticleSystem.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //only trigger if contacting from top (jumping on it)
            bool comingFromTop = collision.GetContact(0).normal.y < 0;

            if (collision.gameObject.layer == LayerMask.NameToLayer("Players") && comingFromTop)
            {
                isActivated = true;
                transform.localScale = new Vector3(1, 0.5f, 1);
                GameObject.Find("Propellers").GetComponent<Propeller>().isActive = true;
                if (propellerParticleSystem.activeSelf == false)
                {
                    propellerParticleSystem.SetActive(true);
                }
            }
        }
    }
}


