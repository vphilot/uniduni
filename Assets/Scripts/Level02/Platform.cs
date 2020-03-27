using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level02
{
    public class Platform : MonoBehaviour
    {
        public GameObject otherPlatform;
        private Vector3 currentPos;
        private Vector3 endPos;
        private float maxY;

        private void Start()
        {
            currentPos = otherPlatform.transform.position;
            endPos = new Vector3(currentPos.x, currentPos.y, currentPos.z);
            maxY = 1.5f;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bool comingFromTop = collision.GetContact(0).normal.y < 0;

            if(comingFromTop && otherPlatform.transform.position.y < maxY)
            {
                currentPos = otherPlatform.transform.position;
                endPos = new Vector3(currentPos.x, currentPos.y + 1, currentPos.z);
                StartCoroutine(MovePlatform());
            }
        }

        IEnumerator MovePlatform()
        {
            float elapsedTime = 0;
            float waitTime = 1f;

            while(elapsedTime < waitTime)
            {
                otherPlatform.transform.position = Vector3.Lerp(currentPos, endPos, (elapsedTime/waitTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            otherPlatform.transform.position = endPos;

            yield return null;
        }
    }
}


  