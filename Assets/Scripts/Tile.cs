using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public static int tileSelectionAmount;
    public GameObject selectionTool;
    public static int width;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void tileMatch()
    {
        // Destroy the objects with a delay
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("selection"))
        {
            if (obj.transform.parent != null)
            {
                StartCoroutine(DestroyObjectsWithDelay(obj.transform.parent.gameObject, 0.4f));
            }
        }
    }

    private IEnumerator DestroyObjectsWithDelay(GameObject obj, float delay)
    {
        ScoreManager.increaseScore(10);
        foreach (Transform child in obj.transform)
        {
            if (child.tag == "coin")
            {
                ScoreManager.increaseEarnedCoin(10);
            }
        }

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);


        Destroy(obj);
    }

    void OnMouseDown()
    {
        if (!PauseMenu.gamePaused && !GameOverScreen.gameOver){
            bool contains = false;
            foreach (Transform childTransform in transform)
            {
                GameObject childObject = childTransform.gameObject;

                // Check if the child has the desired tag
                if (childObject.CompareTag("selection"))
                {
                    contains = true;
                }
            }

            if (!contains)
            {
                GameObject newSelection = GameObject.Instantiate(selectionTool, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                newSelection.transform.localScale = newSelection.transform.localScale * 6 / width * 0.25f / 0.324f; ;
                newSelection.transform.parent = transform;
                int count = 0;
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("selection"))
                {
                    if (obj.transform.parent != null)
                    {
                        if (obj.transform.parent.gameObject.tag != gameObject.tag)
                        {

                            Destroy(obj);
                        }
                        else
                        {
                            count++;
                            if (count == tileSelectionAmount)
                            {
                                tileMatch();
                            }
                        }
                    }
                }
            }
        }
    }
}
