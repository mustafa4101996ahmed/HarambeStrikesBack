using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCoins : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(0, 0, 90);
    }

    void Update()
    {
        spin();
    }

    private void spin()
    {
        transform.Rotate(3, 0, 0 * Time.deltaTime);
    }
}


