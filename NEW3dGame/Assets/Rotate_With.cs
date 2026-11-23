
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate_With : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Player_Movent PlayerScript;

    [Header("Rotation variables")]
    [SerializeField] float sens;
    [SerializeField] float uppperPOS;
    [SerializeField] float lowerNEG;
    [Header("Offset For Placement")]
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform Player;
    


    Vector2 MouseValues;
    float y;
    float z;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position = Player.transform.position + Offset;
        //If Right Mouse button Is Pressed
       if(Mouse.current.rightButton.isPressed && PlayerScript.flip)
        {
SetValues();
        Rotate();
        }
       else if (!PlayerScript.flip)
        {
            SetValues();
            Rotate();
        }
       else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        




    }

    private void SetValues()
    {

        //Setting y and Z Values for rotation.
        
y += Mouse.current.delta.x.ReadValue();

 z += Mouse.current.delta.y.ReadValue();
        
      
       


    if (z * sens * -1 >= uppperPOS)
        {
            z = z - Mouse.current.delta.y.ReadValue();
        }
    else if (z * sens * -1 <= lowerNEG)
        {
            
            z = z - Mouse.current.delta.y.ReadValue();
        }

            Cursor.lockState = CursorLockMode.Locked;
      
    }
    private void Rotate()
    {
        //Rotation Angles.
        transform.eulerAngles = new Vector3(-1 * z * sens, y * sens, 0);

       
        
        

    }


   
}
