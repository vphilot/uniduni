using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool hasPlayer1Collided = false;
    private bool hasPlayer2Collided = false;
    private GameObject canvas;

    public bool hasExternalCondition = false;
    public bool isExternalConditionSatisfied = false;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
    }

    void Update()
    {

        if (hasExternalCondition)
        {
            //check what needs to be checked by external condition
            if (hasPlayer1Collided && hasPlayer2Collided && isExternalConditionSatisfied)
            {
                StartCoroutine(LoadNextScene());
            }
        } else {
            //no external condition means we only check for player collision
            if (hasPlayer1Collided && hasPlayer2Collided)
            {
                StartCoroutine(LoadNextScene());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player1")
        {
            hasPlayer1Collided = true;
        } else if (collision.gameObject.name == "Player2")
        {
            hasPlayer2Collided = true;
        }
    }

    private IEnumerator LoadNextScene() {
        canvas.SetActive(true);
        yield return new WaitForSeconds(3);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadNextScene();
    }

}
