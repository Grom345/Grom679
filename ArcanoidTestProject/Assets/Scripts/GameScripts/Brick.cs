using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] Material[] brickMaterials;
    //--
    private bool isDestriyable = true;
    private bool isActive = false;
    private int health = 1;
    private Transform parent;
    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (other.GetComponent<BallController>().ColisionCooldown)
            {
                other.GetComponent<BallController>().direction.x = other.GetComponent<BallController>().direction.x * -1;
                other.GetComponent<BallController>().direction.y = other.GetComponent<BallController>().direction.y * -1;
                other.GetComponent<BallController>().ColisionCooldown = false;
            }
            if (isDestriyable)
            {
                health--;
                GameManager.score += 10;
                if (health <= 0)
                {
                    if (isActive)
                    {
                        GameManager.score += 20;
                        clearTheBoard();
                    }
                    else
                    {
                        GameManager.score += 15;
                        clearTheBoard();
                    }

                }
            }
        }
    }
    public void Init(Transform transform,  int brickType)
    {
        parent = GameObject.Find("BricksContainer").transform;
        gameObject.transform.SetParent(parent);
        if (brickType == 1)
        {
            health = 1;
            mesh.material = brickMaterials[0];
        }
        else if (brickType == 2)
        {
            health = 3;
            mesh.material = brickMaterials[1];
        }
        else if (brickType == 3)
        {
            isDestriyable = false;
            mesh.material = brickMaterials[2];
        }
        else if (brickType == 4)
        {
            health = 1;
            isActive = true;
            mesh.material = brickMaterials[3];
        }
    }

    void clearTheBoard ()
    {
        Instantiate(particles, new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y, 0.0f), Quaternion.identity);
        destroyBrick();
    }

    void destroyBrick()
    {
        Destroy(gameObject);
        FindObjectOfType<BrickGenerator>().RemoveBrick(this);
    }

  
}
