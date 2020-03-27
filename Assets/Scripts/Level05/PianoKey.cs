using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level05
{
    public class PianoKey : MonoBehaviour
    {
        private Vector3 initialLocalScale;
        private bool canCollide;
        public string keyID;
        public AudioClip keyAudioClip;
        // Start is called before the first frame update
        void Start()
        {
            initialLocalScale = transform.localScale;
            canCollide = true;

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bool comingFromTop = collision.GetContact(0).normal.y < 0;

            if (comingFromTop && canCollide)
            {
                transform.localScale = new Vector3(initialLocalScale.x, initialLocalScale.y/2, initialLocalScale.z);
                //triger note order check manager
                GameObject.Find("pianoKeyManager").GetComponent<PianoKeyManager>().checkNotes(keyID, keyAudioClip);
                canCollide = false;
            }

        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            //Debug.Log("exited");
            StartCoroutine(ResetScale());
            
        }

        IEnumerator ResetScale()
        {
            yield return new WaitForSeconds(1);
            transform.localScale = initialLocalScale;
            canCollide = true;
            StopAllCoroutines();
        }
    }
}

