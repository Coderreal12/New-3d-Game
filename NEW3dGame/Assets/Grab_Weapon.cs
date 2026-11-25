
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab_Weapon : MonoBehaviour
{
    [Header("Raycast Variables")]

    [SerializeField] private float RaycastDis;



    //Raycasts
    private RaycastHit Hit;
    //Bools 
    bool Equip;

    private void Start()
    {
        
    }

    private void Update()
    {
        SendRay();
    }

    private void SendRay()
    {
        //Gets the mouse position.
        Vector3 pos = Mouse.current.position.ReadValue();
       //Makes the ray
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
 Equip =  Physics.Raycast(ray, RaycastDis, LayerMask.GetMask("Gun"));
        Debug.DrawRay(ray.origin, ray.direction *RaycastDis, Color.red, .1f); 
        }
       
    }

}
