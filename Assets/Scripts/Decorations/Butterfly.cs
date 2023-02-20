using UnityEngine;

public class Butterfly : MonoBehaviour
{
    Vector3 target;
    float timer;
    int sec;

    void Start()
    {
        target = ResetTarget();
        sec = ResetSec();
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > sec)
        {
            target = ResetTarget();
            sec = ResetSec();
        }

        transform.Translate(target * 1 * Time.deltaTime);
    }

    Vector3 ResetTarget()
    {
        return new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
    }

    int ResetSec()
    {
        timer = 0;
        return Random.Range(1, 3);
    }
}