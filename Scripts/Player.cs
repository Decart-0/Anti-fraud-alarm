using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private const string AxisHorizontal = "Horizontal";
    private const string AxisVertical = "Vertical";

    private void Update()
    {
        Move();
    }

    private void Move()
    {       
        Vector3 move = new Vector3(Input.GetAxis(AxisHorizontal), 0, Input.GetAxis(AxisVertical)).normalized;

        if (move.magnitude > 0)
        {
            transform.position += move * _speed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed);
        }
    }
}