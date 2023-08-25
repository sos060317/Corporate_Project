using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_j : MonoBehaviour, IFireRater
{
    [SerializeField]
    private float time = 0;

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
    }
}
