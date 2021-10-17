using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ElderScroll : MonoBehaviour
{
    [SerializeField] Text missionCompleteText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.isScrollFound = true;
            Debug.Log("Fus-Ro-Dah!!!");
            Destroy(gameObject);
            missionCompleteText.enabled = true;
        }
    }
    
}
