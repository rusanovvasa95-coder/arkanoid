using UnityEngine;

public class Platform : MonoBehaviour
{
    public float Speed;
    public Joystick Joystick;
    public float LeftX, RightX;

    private void Update()
    {
        float horizontal = Joystick.Horizontal;
        transform.position += horizontal * Vector3.right * Speed * Time.deltaTime;

        float positionX = transform.position.x;
        positionX = Mathf.Clamp(transform.position.x, LeftX, RightX);
        transform.position = new Vector3(positionX, transform.position.y);
    }
}
