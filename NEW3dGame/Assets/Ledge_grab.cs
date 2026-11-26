
using UnityEngine;
using UnityEngine.InputSystem;

public class Ledge_grab : MonoBehaviour
{
    [SerializeField] Slide sliding;
    [SerializeField] Jumping PlayerScript;
    [SerializeField] private Jumping jump;


    [Header("Player Oparations")]
    [SerializeField] private Transform Player;
    [SerializeField] private Rigidbody PlayerRb;

    [Header("Unfixed Variables")]
    [SerializeField] private float Jump_Force;
    [SerializeField] private Vector3 BoxCheck;
    [Header("Raycasts")]

    [SerializeField] private Transform GrabPoint;
    [SerializeField] private Transform NullPoint;
    [SerializeField] private string[] Ledges;
    [SerializeField] private float RayDis;


    Ray ray;
    RaycastHit[] Hits = new RaycastHit[2];

    //Bools
    bool[] Touching = new bool[2];

    //Temporary variabels
    float temp;
    private void Update()
    {

        SendRay();
        Check_Jump();



    }


    private void SendRay()
    {

        //Sets a new ray for each point

        //Ray 1
        
        //Changes the touhcing[0] variable to a CheckBox
        Touching[0] = Physics.CheckBox(GrabPoint.position, BoxCheck, GrabPoint.transform.rotation,LayerMask.GetMask(Ledges));
        

        //Ray 2
        ray = new Ray(NullPoint.transform.position, Player.forward);
        Touching[1] = Physics.Raycast(ray, RayDis, LayerMask.GetMask(Ledges));
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        // * FOR DEBUGGING * displays the bool; (Since it is an array and therse 2 you have it run twice
     /*   for (int i = 0; i<= 1; i++)
        {
Debug.Log("Bool number " + i + " " + Touching[i]);
        } */
        
    
        

    }
        
    private void Check_Jump()
    {
        //If grounded then set the jumprforce (tempp) to the nomral jump force.
        if (PlayerScript.Grounded)
        {
            temp = Jump_Force;
        }


        //If Square cast is not null, and the box cast is null, and space key was pressed, then jump; 
            if (Touching[0] && !Touching[1] && Keyboard.current.spaceKey.wasPressedThisFrame && !sliding.done)
        {
          
            PlayerRb.linearVelocity = new Vector3(PlayerRb.linearVelocity.x, temp, PlayerRb.linearVelocity.z);
            //subtracts the jump force by X/jumpforce amount of times so it allows you to jump more
            temp -= temp/4;
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(GrabPoint.position, BoxCheck);
    }


}
