using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public int fuel = 100;
    public Text textFuel;

    bool moveForward = false;
    bool moveBackward = false;

    public WheelJoint2D frontWheel;
    public WheelJoint2D backWheel;
    JointMotor2D motor;

    private float speed = 0f;

    private bool isGrounded = false;
    private Rigidbody2D rb2D;

    private int startFuel;
    public Image fuelBar;
    void Start()
    {
        startFuel = fuel;
        StartCoroutine(FuelReducer());
        rb2D = GetComponent<Rigidbody2D>();
        motor.maxMotorTorque = 10000;
    }
    private IEnumerator FuelReducer()
    {
        while(fuel > 0)
        {
            fuel -= 1;
            textFuel.text = fuel.ToString();
            fuelBar.fillAmount = (float) fuel / startFuel;
            yield return new WaitForSeconds(0.5f);
        }
        GameOver();
    }
    private void MoveOnGround()
    {
        if(moveForward)
        {
            if(frontWheel.attachedRigidbody.angularVelocity > -2000)
            {
                speed += 40;
                motor.motorSpeed = speed;
            }
            
            frontWheel.motor = motor;
            backWheel.motor = motor;
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
        }
        else if(moveBackward)
        {
            if(frontWheel.attachedRigidbody.angularVelocity < 2000)
            {
                speed -= 40;
                motor.motorSpeed = speed;
            }
            frontWheel.motor = motor;
            backWheel.motor = motor;
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
        }
        else
        {
            speed = -frontWheel.attachedRigidbody.angularVelocity;
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
    }

    private void MoveInAir()
    {
        backWheel.useMotor = false;
        frontWheel.useMotor = false;
        if(moveForward)
        {
            if(rb2D.angularVelocity < 200f)
            rb2D.AddTorque(10f);
        }
        else if(moveBackward)
        {
            if (rb2D.angularVelocity > -200f)
                rb2D.AddTorque(-10f);
        }
    }
    private void CheckGameOver()
    {
        Vector2 rayDir = transform.up;
        RaycastHit2D[] hit =
            Physics2D.RaycastAll(transform.position, rayDir, 0.7f);
        Debug.DrawRay(transform.position, rayDir * 0.7f, Color.green);

        if(hit.Length > 1)
        {
            GameOver();
        }
        
    }
    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
    private void FixedUpdate()
    {
        if(backWheel.GetComponent<Collider2D>().IsTouchingLayers()
            || frontWheel.GetComponent<Collider2D>().IsTouchingLayers())
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        MoveOnGround();
        if (!isGrounded)
            MoveInAir();
    }

    void Update()
    {
        CheckGameOver();
        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveForward = true;
            moveBackward = false;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            moveBackward = true;
            moveForward = false;
        }
        else
        {
            moveForward = false;
            moveBackward = false;
        }
    }
}
