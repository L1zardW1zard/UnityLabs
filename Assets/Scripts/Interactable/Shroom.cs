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

    private bool inRange;

    private bool arrowMovingUp;

    // Start is called before the first frame update
    void Start()
    {
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
            arrowAbove.enabled = true;

            Debug.Log("InRange");
            IDamagable gamagableObject = playerObjReference.GetComponent<IDamagable>();
            Player player = playerObjReference.GetComponent<Player>();
            if (player != null && Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.transform.position.x - 1,player.transform.position.y-0.35f), player.transform.TransformDirection(Vector2.right), 1.5f,~RayCastIgnoreLayer);
            
                //Debug.DrawRay(new Vector3(transform.position.x - 1,transform.position.y-0.35f,transform.position.z),transform.TransformDirection(Vector2.right),Color.red);
                if (hit.collider.name == "shrooms")
                {
                    Debug.Log("Player ate shroom");
                    player.TakeDamage(5,0,0);
                    Destroy(gameObject);
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
