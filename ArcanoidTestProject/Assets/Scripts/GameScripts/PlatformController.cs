using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float AngleScaler;
    //--
    public Vector2 screenSize;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Awake()
    {
        calculateScreenSize();
    }

    private void Update()
    {
        moving();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);//create borders
    }

    private void moving()
    {
        if ((Input.mousePosition.x >= Screen.width / 2) && (Input.GetMouseButton(0)) || (Input.GetAxisRaw("Horizontal") > 0))
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }
        else if (((Input.mousePosition.x < Screen.width / 2) && Input.GetMouseButton(0)) || (Input.GetAxisRaw("Horizontal") < 0))
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            float deltaX = transform.position.x - other.GetComponent<BallController>().transform.position.x;
            other.GetComponent<BallController>().direction.x = deltaX * AngleScaler;
            if (other.GetComponent<BallController>().direction.x >= 0f)
            {
                other.GetComponent<BallController>().direction.y = 1 / other.GetComponent<BallController>().direction.x;
            }
            if (other.GetComponent<BallController>().direction.x < 0f)
            {
                other.GetComponent<BallController>().direction.y = -1 / other.GetComponent<BallController>().direction.x;
            }
        }
    }

    private void calculateScreenSize ()
    {
        screenSize.y = Camera.main.fieldOfView;
        screenSize.x = Camera.main.fieldOfView * Screen.width / Screen.height;
        minX = -screenSize.x / 2 + transform.localScale.x;
        maxX = screenSize.x / 2 - transform.localScale.x;
        minY = -screenSize.y / 2 + transform.localScale.x;
        maxY = screenSize.y / 2 - transform.localScale.x;
        GameManager.X = maxX;
        GameManager.Y = maxY;
    }
}
