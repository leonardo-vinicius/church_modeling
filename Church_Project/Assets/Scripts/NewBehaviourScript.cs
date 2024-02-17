using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{   
    [SerializeField]

    private float speed = 5f;

    private GameObject objSegurado;
    private Rigidbody objSeguradoRB;

    [SerializeField] Transform holdArea;
    [SerializeField] private float pickupRange = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            if (objSegurado == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {   //Pega o objeto

                    PickupObject(hit.transform.gameObject);

                }
            }
            else
            {
                //Solta o objeto
                DropObject();

            }
        }

        if (objSegurado != null)
        {
            //Move o objeto
            moveObject();


        }
        
    }

    void moveObject()
    {
        float x = 0, y = 0, z = 0;

        if(Input.GetKey(KeyCode.A)){ x = -1; }
        if(Input.GetKey(KeyCode.D)){ x = 1; }
        if(Input.GetKey(KeyCode.W)){ z = 1; }
        if(Input.GetKey(KeyCode.S)){ z = -1; }
        if(Input.GetKey(KeyCode.LeftShift)){ y = -1; }
        if(Input.GetKey(KeyCode.Space)){ y = 1; }

        Vector3 dir = new Vector3(x, y, z);
        transform.Translate(dir * speed * Time.deltaTime );
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            objSeguradoRB = pickObj.GetComponent<Rigidbody>();
            objSeguradoRB.useGravity = false;
            objSeguradoRB.drag = 10;

            objSeguradoRB.transform.parent = holdArea;
            objSegurado = pickObj;
        }
    }

    void DropObject()
    {
        objSeguradoRB.useGravity = true;
        objSeguradoRB.drag = 1;

        objSegurado.transform.parent = null;
        objSegurado = null;

    }
}
