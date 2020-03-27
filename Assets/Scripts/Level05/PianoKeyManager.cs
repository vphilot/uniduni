using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level05
{
    public class PianoKeyManager : MonoBehaviour
    {

        private Queue<string> keyOrder;
        private string[] keys;

        public AudioClip errorAudioClip;
        public AudioClip successAudioClip;
        private AudioSource audioSource;

        private Chest chest;

        void Start()
        {
            keyOrder = new Queue<string>();
            keys = new string[8] { "a1", "b1", "a2", "b2", "a3", "b3", "a4", "b4"};
            foreach (string id in keys)
            {
                keyOrder.Enqueue(id);
            }

            Debug.Log(keyOrder);

            audioSource = GetComponent<AudioSource>();

            chest = GameObject.Find("Chest").GetComponent<Chest>();
            chest.hasExternalCondition = true;

        }

        public void checkNotes(string keyID, AudioClip audioClip)
        {
            if (keyOrder.Count == 1 && keyOrder.Peek() == keyID)
            {
                TriggerSuccess();
                return;
            }

            //if correct key inside key order, dequeue and wait for next press
            if (keyOrder.Peek() == keyID)
            {
                string nextKey = keyOrder.Dequeue();
                audioSource.PlayOneShot(audioClip);
            } else
            {
                //else, the sequence is wrong and triggers reset function
                TriggerError();
            }

           

        }

        private void TriggerSuccess()
        {
            chest.isExternalConditionSatisfied = true;
            audioSource.PlayOneShot(successAudioClip);
        }

        private void TriggerError()
        {
            //reset queue
            keyOrder.Clear();
            foreach (string id in keys)
            {
                keyOrder.Enqueue(id);
            }
            //play error message
            audioSource.PlayOneShot(errorAudioClip);
        }
    }
}


