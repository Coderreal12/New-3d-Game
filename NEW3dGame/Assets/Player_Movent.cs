


using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Movent : MonoBehaviour
{
    //Old pos Vector3(0,27,-60)
    //New Pos 

    float add;

    [SerializeField] private Jumping jump;

    [Header("UnFixed Variables For PLayer")]
    [SerializeField] private Vector3 OldPos;
    [SerializeField] private Vector3 NewPos;

    [Header("Input For Charecter")]
    [SerializeField] private InputAction Controller;
    private Vector2 movement;

    [Header("Movement For Charceter")]
    //Rigidbodies/transfroms
    [SerializeField] private Rigidbody PlayerRigid;
    [SerializeField] private Transform pivot;

    [Header("HeadRotate")]
    [SerializeField] Transform headbone;

    //floats
    [SerializeField] private float Speed;
    [SerializeField] private float offfff;
    [SerializeField] private float rospeed;
    [SerializeField] Animator Animator;

    [SerializeField] private float Sprint;
    [SerializeField] private float temp;
    [SerializeField] private float AirMult;

    //Animation bools.

    bool walking;
   public  bool flip = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void Update()
    {

        Playeranimation();
        GoFast();
        flop();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        movement = Controller.ReadValue<Vector2>();

        if (flip)
        {
            Walking();
            Move();
        }
        else
        {
            ShiftLock();
            MoveShift();
        }

    }



    private void Walking()
    {
        Camera.main.transform.localPosition = new Vector3(0, 27, -60);

        //If A and S is pressed (Backward direction)
        if (Keyboard.current.aKey.isPressed && Keyboard.current.sKey.isPressed)
        {
            //Set the rotatoin to the back left
            Quaternion Dire = Quaternion.Euler(new Vector3(transform.eulerAngles.x, pivot.eulerAngles.y + offfff, transform.eulerAngles.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Dire, rospeed);
        }
        //If S and D is pressed
        else if (Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            //Set the rotation to the back right
            Quaternion Dire = Quaternion.Euler(new Vector3(transform.eulerAngles.x, pivot.eulerAngles.y + offfff * -1f, transform.eulerAngles.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Dire, rospeed);
        }
        else if (movement.x != 0)
        {
            //Set the Reotation to left or right + 90 times this value
            Quaternion Dire = Quaternion.Euler(new Vector3(transform.eulerAngles.x, pivot.eulerAngles.y + 90 * movement.x, transform.eulerAngles.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Dire, rospeed);


        }
        else if (movement.y != 0)
        {



            if (movement.y == -1)
            {
                add = 180f;
            }
            else if (movement.y == 1)
            {
                add = 0f;
            }

            Quaternion Dire = Quaternion.Euler(new Vector3(transform.eulerAngles.x, pivot.eulerAngles.y + add, transform.eulerAngles.z));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Dire, rospeed);

        }






        /*else if (movement.x != 0 && !jump.Grounded || movement.y != 0 && !jump.Grounded)
        {
            PlayerRigid.AddRelativeForce(new Vector3(0, 0, 1 * Speed * AirMult));
        }*/




    }



    private void Playeranimation()
    {

        Animator.SetFloat("Speed", temp);

        //If On the grounded then set jump to false
        if (jump.jump)
        {
            Animator.SetBool("Jump", false);
        }
        //If In the air set the Jump to true
        else
        {
            Animator.SetBool("Jump", true);
        }
        //If it is not grounded or no movements Set walking to false
        if (jump.Grounded == false || movement.x == 0 && movement.y == 0)
        {
            Animator.SetBool("Walking", false);
        }
        //Other than that just start walking!
        else
        {
            Animator.SetBool("Walking", true);

        }



    }
    private void GoFast()
    {
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            temp = Sprint;
        }
        else
        {
            temp = 1;
        }
    }
    private void ShiftLock()
    {
        //The player angels is equal to the pivot angels Y value and the regular x and z values.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, pivot.eulerAngles.y, transform.eulerAngles.z);
        Camera.main.transform.localPosition = new Vector3(20, 19, -41.5f);
    }



    private void flop()
    {

        if (Keyboard.current.altKey.wasPressedThisFrame)
        {
            flip = !flip;
        }

    }

    private void OnEnable()
    {
        Controller.Enable();
    }

    private void OnDisable()
    {
        Controller.Disable();
    }

    private void Move()
    {
        if (movement.x != 0 && jump.Grounded || movement.y != 0 && jump.Grounded)
        {
            PlayerRigid.AddRelativeForce(new Vector3(0, 0, 1 * Speed * temp));
        }
        //If not add the new speed.
        else if (movement.x != 0 && !jump.Grounded || movement.y != 0 && !jump.Grounded)
        {
            Vector3 tempo = new Vector3(PlayerRigid.linearVelocity.x, 0, PlayerRigid.linearVelocity.z);
            PlayerRigid.AddRelativeForce(new Vector3(0, 0, 1 * Speed * AirMult * temp));
            PlayerRigid.linearVelocity = PlayerRigid.linearVelocity - tempo;
        }
    }

    private void MoveShift()
    {
        //If On the ground and moving
        if (movement.x != 0 && jump.Grounded || movement.y != 0 && jump.Grounded)
        {
            Debug.Log(movement);
            //Add a force times the W A S D values.
            PlayerRigid.AddRelativeForce(new Vector3(movement.x * Speed * temp, 0,  movement.y * Speed * temp));
        }
        //If not add the new speed.
        else if (movement.x != 0 && !jump.Grounded || movement.y != 0 && !jump.Grounded)
        {
            Vector3 tempo = new Vector3(PlayerRigid.linearVelocity.x, 0, PlayerRigid.linearVelocity.z);
            PlayerRigid.AddRelativeForce(new Vector3(movement.x * Speed * AirMult * temp, 0, movement.y * Speed * AirMult * temp));
            PlayerRigid.linearVelocity = PlayerRigid.linearVelocity - tempo;
        }
    }
}
