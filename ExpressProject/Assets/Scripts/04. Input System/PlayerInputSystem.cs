using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed;

    public Rigidbody rigid;

    private void FixedUpdate()
    {
        Vector3 nextVec = new Vector3(inputVec.x, 0.0f, inputVec.y);
        rigid.MovePosition(rigid.position + nextVec * speed * Time.fixedDeltaTime);
    }
    
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

}
