using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : GameController
{
    [SerializeField] private Animator animator;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
