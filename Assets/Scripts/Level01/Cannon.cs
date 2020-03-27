using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level01
{
    public class Cannon : MonoBehaviour
    {

        private bool isPlayerInside;
        private GameObject playerInside;

        private void Start()
        {
            isPlayerInside = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isPlayerInside)
            {
                // avoiding double collisions
                isPlayerInside = true;
                playerInside = collision.gameObject;
                GrabPlayer(playerInside);
                playerInside.SetActive(false);
            }
        }

        private void GrabPlayer(GameObject _playerInside)
        {
            _playerInside.transform.position = transform.position;
            
        }

        private void TogglePlayerActive(GameObject _playerInside)
        {
            if (_playerInside.activeSelf)
            {
                _playerInside.SetActive(false);
            }
            else
            {
                _playerInside.SetActive(true);
            }
        }

        public void Push()
        {
            if(isPlayerInside)
            {
                playerInside.SetActive(!playerInside.activeSelf);
                playerInside.GetComponent<Rigidbody2D>().AddForce(transform.up * 25, ForceMode2D.Impulse);
                StartCoroutine(ResetPlayerState());
            }

        }

        IEnumerator ResetPlayerState()
        {
           
             yield return new WaitForSeconds(.1f);
            isPlayerInside = false;
           
        }
    }
}


