using UnityEngine;

public class GroupAnimationController : MonoBehaviour
{
    private Transform[] items;

    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float xBoundLeft;

    [SerializeField] private float xBoundRight;

    private void Start()
    {
     
    }

    // Update is called once per frame
    private void Update()
    {
      
           
            if (transform.position.x < xBoundLeft) transform.position = new Vector2(xBoundRight, transform.position.y);

            transform.Translate(Vector2.left * speed);
     
    }
}