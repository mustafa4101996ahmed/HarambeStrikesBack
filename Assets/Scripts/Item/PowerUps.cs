﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    void Start()
    {
        transform.Rotate(90, 0, 0);
    }

    void Update()
    {
        //spin();
    }

    private void spin()
    {
        transform.Rotate(3, 0, 0 * Time.deltaTime);
    }

}
