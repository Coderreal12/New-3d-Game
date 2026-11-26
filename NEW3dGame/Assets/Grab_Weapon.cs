

using UnityEngine;

using UnityEngine.InputSystem;


public class Grab_Weapon : MonoBehaviour
{
    [Header("Raycast Variables")]
    [SerializeField] private Transform HoldLoc;
    [SerializeField] private float RaycastDis;

    [Header("Shader Variables")]
    [SerializeField] private GameObject Shader;
    [SerializeField] private Collider[] colliders;


    [SerializeField] private Vector3 AnglesOffset;
    //Raycasts
    private RaycastHit Hit;
    //Bools 
    bool Touching;
    bool Equip;

    //tempoary variables
    private Transform TempTransform;
    private Rigidbody TempRigidBody;


    private void Start()
    {
        
    }

    private void Update()
    {
        SendRay();
        MakeShader();
        Hold();

    }

    private void SendRay()
    {
        //Gets the mouse position.
        Vector3 pos = Mouse.current.position.ReadValue();
       //Makes the ray
        Ray ray = Camera.main.ScreenPointToRay(pos);

        
       
        Touching = Physics.Raycast(ray, out Hit, RaycastDis, LayerMask.GetMask("Gun"));
        Debug.DrawRay(ray.origin, ray.direction * RaycastDis, Color.red);

        if (Touching && Mouse.current.leftButton.wasPressedThisFrame)
        {
            SetVariables();
            Equip = true;
        }
        
       
        
       
    }

    private void MakeShader()
    {
        if (Touching)
        {
            Shader.SetActive(true);
        }
        else
        {
            Shader.SetActive(false);
        }
    }

    

    private void Hold()
    {
        if (Equip)
        {      
            SendToPos();
        }
         if ( Equip && Keyboard.current.fKey.isPressed)
        {
            Equip = false;
            ResetVariables();
            
        }
        
    }

    //START OF HOLDING FUNCTION.
private void SetVariables()
    {
        TempTransform = Hit.collider.GetComponent<Transform>();
        TempRigidBody = Hit.collider.GetComponent<Rigidbody>();
        colliders = Hit.collider.GetComponents<Collider>();
        

        
    }
    
    private void SendToPos()
    {
        foreach(Collider col in colliders)
        {
            col.isTrigger = true;
        }

        TempRigidBody.isKinematic = true;
        TempTransform.position = HoldLoc.position;
        TempTransform.SetParent(transform);
        TempTransform.eulerAngles = transform.eulerAngles + AnglesOffset;

    }



    private void ResetVariables()
    {

        //sets the collider
        foreach (Collider col in colliders)
        {
            col.isTrigger = false;
        }

        TempTransform.SetParent(null);
        TempRigidBody.isKinematic = false;


        TempTransform = null;
        TempRigidBody = null;
    }




}
