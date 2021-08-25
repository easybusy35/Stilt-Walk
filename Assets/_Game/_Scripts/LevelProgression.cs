using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Transform Finish;
    [SerializeField] Slider slider;

    float maxDistance;


    // Start is called before the first frame update
    void Start()
    {
        maxDistance = getDistance();
    }
    // Update is called once per frame
    void Update()
    {
        StartFinishDistance();
    }

    private void StartFinishDistance()
    {
        if (Player.position.z <= maxDistance && Player.position.z <= Finish.position.z)
        {
            float distance = 1 - (getDistance() / maxDistance);
            setProgress(distance);
        }
    }

    float getDistance()
    {
        return Vector3.Distance(Player.position, Finish.position);
    }

    void setProgress(float p)
    {
        slider.value = p;
    }
}
