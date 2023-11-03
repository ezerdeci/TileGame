using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUpwardMovement : MonoBehaviour
{

    public static float boardSpeed = 0.2f;
    private float timeIncrease;
    void Start()
    {
        timeIncrease = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeIncrease += 0.00001f;
        transform.position += new Vector3(0, boardSpeed * Time.deltaTime + Time.deltaTime * timeIncrease, 0);

    }
}
