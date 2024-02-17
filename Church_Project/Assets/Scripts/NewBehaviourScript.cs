using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{   
    [SerializeField]

    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
