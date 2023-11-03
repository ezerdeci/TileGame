using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject tileUpwardMovementObject;
    public GameObject[] tiles;
    public GameObject money;
    public GameObject instantiateParent;
    public int tileVar;
    public int width;
    public int height;
    public int coinProb;
    public int selectionRequired;
    private float hGap = 0.8f*0.25f/0.324f;
    private float wGap = 0.8f*0.25f/0.324f;
    private Vector3 prePosition;
    private GameObject[,] tileGrid;
    private Vector3 cameraCenter;
    public float tileMoveSpeed;
    private GameOverScreen gameOverScript;
    private ScoreManager scoreManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        cameraCenter = Camera.main.transform.position;
        Tile.tileSelectionAmount = selectionRequired;
        Tile.width = width;
        TileUpwardMovement.boardSpeed = tileMoveSpeed;
        gameOverScript = GetComponent<GameOverScreen>(); 
        scoreManagerScript = GetComponent<ScoreManager>();

        tileGrid = new GameObject[100, 100];

        for (int i = 0; i < tileGrid.GetLength(0); i++)
        {
            for (int k = 0; k < tileGrid.GetLength(1); k++)
            {
                tileGrid[i, k] = null;
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k <= height + 1; k++)
            {
                if (width % 2 == 1)
                {
                    if (i % 2 == 0)
                    {
                        tileGrid[i, k] = createRandomTile((-i / 2) * (wGap * 6 / width) + cameraCenter.x, k * (hGap * 6 / width) - 4.12f);
                    }
                    else
                    {
                        tileGrid[i, k] = createRandomTile(((i + 1) / 2) * (wGap * 6 / width) + cameraCenter.x, k * (hGap * 6 / width) - 4.12f);
                    }
                } else {
                    if (i % 2 == 0)
                    {
                        tileGrid[i, k] = createRandomTile((-i / 2) * (wGap * 6 / width) + cameraCenter.x - ((wGap * 6 / width) /2), k * (hGap * 6 / width) - 4.12f);
                    }
                    else
                    {
                        tileGrid[i, k] = createRandomTile(((i + 1) / 2) * (wGap * 6 / width) + cameraCenter.x - ((wGap * 6 / width) /2), k * (hGap * 6 / width) - 4.12f);
                    }
                }

            }
        }
        prePosition = tileUpwardMovementObject.transform.position;
    }

    public GameObject createRandomTile(float x, float y)
    {

        GameObject newTile = GameObject.Instantiate(randomTilePick(), new Vector2(x, y), Quaternion.identity);
        newTile.transform.parent = instantiateParent.transform;
        newTile.transform.localScale = newTile.transform.localScale*0.25f/0.324f;
        int randomCoin = Random.Range(0, 100);
        if (randomCoin < coinProb)
        {
            GameObject coin = GameObject.Instantiate(money, new Vector2(x + 0.26f, y + 0.22f), Quaternion.identity);
            coin.transform.parent = newTile.transform;
        }
        newTile.transform.localScale = newTile.transform.localScale * 6 / width;
        return newTile;
    }
    public GameObject randomTilePick()
    {

        int random = Random.Range(0, tileVar);
        return tiles[random];
    }

    public void newTileRow()
    {

        for (int i = 0; i < width; i++)
        {
            for (int k = tileGrid.GetLength(1) - 2; k > -1; k--)
            {
                tileGrid[i, k + 1] = null;
                tileGrid[i, k + 1] = tileGrid[i, k];
            }
            tileGrid[i, 0] = null;
        }

        for (int i = 0; i < width; i++)
        {
            if (width % 2 == 1)
                {
                    if (i % 2 == 0)
                    {
                        tileGrid[i, 0] = createRandomTile((-i / 2) * (wGap * 6 / width) + cameraCenter.x, - 4.12f);
                    }
                    else
                    {
                        tileGrid[i, 0] = createRandomTile(((i + 1) / 2) * (wGap * 6 / width) + cameraCenter.x, - 4.12f);
                    }
                } else {
                    if (i % 2 == 0)
                    {
                        tileGrid[i, 0] = createRandomTile((-i / 2) * (wGap * 6 / width) + cameraCenter.x - ((wGap * 6 / width) /2), - 4.12f);
                    }
                    else
                    {
                        tileGrid[i, 0] = createRandomTile(((i + 1) / 2) * (wGap * 6 / width) + cameraCenter.x - ((wGap * 6 / width) /2), - 4.12f);
                    }
                }
        }

    }
    void dropTile(GameObject tileObject, int dropAmount)
    {
        StartCoroutine(DropTileWithAnimation(tileObject, 0.02f, dropAmount)); // 0.8f is the time it takes to complete the animation
    }

    IEnumerator DropTileWithAnimation(GameObject tileObject, float duration, int dropAmount)
    {
        Vector3 initialPosition = tileObject.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, -hGap * dropAmount * 6 / width, 0);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            tileObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tileObject.transform.position = targetPosition;
    }

    void calculateDropRange(int x, int y)
    {
        int count = 0;
        for (int i = y; i < tileGrid.GetLength(1); i++)
        {
            if (tileGrid[x, i] != null)
            {
                dropTile(tileGrid[x, i], count);
                tileGrid[x, i - count] = tileGrid[x, i];
                tileGrid[x, i] = null;
            }
            else
            {
                count++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < width; i++) {
            for (int k = 0; k < tileGrid.GetLength(1); k++) {
                if (tileGrid[i, k] != null) {
                    if (tileGrid[i, k].transform.position.y > 4.5f) {
                        if (scoreManagerScript.getScore() > PlayerPrefs.GetInt("HighScore")) {
                            PlayerPrefs.SetInt("HighScore", scoreManagerScript.getScore());
                        }
                        gameOverScript.GameOver();
                    }
                }
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < tileGrid.GetLength(1); k++)
            {
                if (tileGrid[i, k] == null)
                {
                    calculateDropRange(i, k);
                }
            }
        }

        if (tileUpwardMovementObject.transform.position.y - prePosition.y > hGap*6/width)
        {
            newTileRow();
            prePosition = tileUpwardMovementObject.transform.position;
        }
    }
}
