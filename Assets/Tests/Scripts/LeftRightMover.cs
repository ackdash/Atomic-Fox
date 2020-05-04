using UnityEngine;

public class LeftRightMover : MonoBehaviour
{
    [SerializeField] [InspectorName("Speed")]
    private float speed;

    private void Update()
    {
        var h = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(Vector2.left * h, Space.World);
    }
}