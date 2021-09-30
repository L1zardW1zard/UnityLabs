using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shroom : MonoBehaviour
{
    [SerializeField] private LayerMask RayCastIgnoreLayer;
    [SerializeField] private Player playerObjReference;
    [SerializeField] private SpriteRenderer arrowAbove;

    // private float arrowStarterPosition;
    // private float arrowCurrentPosition;
    
    private bool inRange;

    private bool arrowMovingUp;
    //private bool timerStarted = false;
    //public float timeLeft = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        //arrowStarterPosition = transform.position.y;
        arrowAbove.enabled = false;
        InvokeRepeating(nameof(ChangeArrowMoveDirection),1f,0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inRange = true;
    }


    private void Update()
    {
        if (inRange)
        {
            //arrowCurrentPosition = arrowAbove.transform.position.y;
            arrowAbove.enabled = true;

            // if (arrowAbove.transform.position.y > arrowStarterPosition + 1f)  // Не очень удачная попытка сделать анимацию стрелки
            //     arrowCurrentPosition = arrowStarterPosition;
            //     
            //
            // if (arrowMovingUp)
            // {
            //     arrowAbove.transform.Translate(0, 0.005f, 0);
            // }
            // else
            // {
            //     arrowAbove.transform.Translate(0,-0.005f,0);
            // }
            
            Debug.Log("InRange");
            Player player = playerObjReference.GetComponent<Player>();
            if (player != null && Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.transform.position.x - 1,player.transform.position.y-0.35f), player.transform.TransformDirection(Vector2.right), 1.5f,~RayCastIgnoreLayer);
            
                //Debug.DrawRay(new Vector3(transform.position.x - 1,transform.position.y-0.35f,transform.position.z),transform.TransformDirection(Vector2.right),Color.red);
                if (hit.collider.name == "shrooms")
                {
                    Debug.Log("Player ate shroom");
                    Debug.Log("Player hp before: " + player.HP);
                    Destroy(gameObject);
                    float shroomHPEffect = Random.Range(-15, 15);
                    player.HP += shroomHPEffect;
                    Debug.Log("Player hp after: " + player.HP);
                }
            }
        }
    }

    private void ChangeArrowMoveDirection()
    {
        arrowMovingUp = !arrowMovingUp;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        arrowAbove.enabled = false;
    }
}
