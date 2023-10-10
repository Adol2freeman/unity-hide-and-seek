using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float VerticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 p = new Vector3(HorizontalInput, 0, VerticalInput);

        transform.position += p;
    }
}
