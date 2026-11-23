using System.Runtime.CompilerServices;
using UnityEngine;

public class SetFow : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform MainPivot;

    [Header("Vector 3")]
    [SerializeField] private Vector3 Offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MainPivot.transform.position + Offset;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, MainPivot.eulerAngles.y, transform.eulerAngles.z);
     
      
    }
}
