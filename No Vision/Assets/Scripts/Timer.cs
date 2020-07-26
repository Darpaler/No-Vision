using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    // Variables
    public float time;
    public bool isSet = false;

    // Update is called once per frame
    void Update()
    {
        if (isSet)
        {
            time += Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        time = 0f;
        isSet = true;
    }
}
