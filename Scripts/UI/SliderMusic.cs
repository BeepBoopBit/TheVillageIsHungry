using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour
{
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener((v) =>
        {
            GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
            musicObj[musicObj.Length - 1].GetComponent<AudioSource>().volume = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
