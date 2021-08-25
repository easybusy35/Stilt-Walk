using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int diamondAmount;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        diamondAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = diamondAmount.ToString();
    }
}
