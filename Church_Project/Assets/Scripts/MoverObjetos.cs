using System.Collections;
using UnityEngine;

public class MoverObjetos : MonoBehaviour
{
    public string _tagObjects = "Movel";
    public float forcaDeArremeco = 100;
    [Space(10)]
    public Sprite texturaMaoFechada;
    public Sprite texturaMaoAberta;

    bool canMove, isMoving;
    float distance, rotXTemp, rotYTemp, tempDistance;

    RaycastHit tempHit;
    Rigidbody rbTemp;
    Vector3 rayEndPoint;
    Vector3 tempDirection;
    Vector3 tempSpeed;
    GameObject tempObject;

    Camera mainCamera;
    GameObject objClosedHand;
    GameObject objOpenHand;

    void Awake()
    {
        distance = 4;
        mainCamera = Camera.main;

        GameObject refTemp = transform.root.gameObject;
        refTemp.layer = 2;
        foreach (Transform trans in refTemp.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 2;
        }

        float tempDistance = 0.3f;
        float tempfloatNear = mainCamera.nearClipPlane;
        if (tempfloatNear >= tempDistance)
        {
            tempDistance = tempfloatNear + 0.05f;
        }

        if (texturaMaoFechada)
        {
            objClosedHand = new GameObject("objHandTextureClosed");
            objClosedHand.transform.parent = this.transform;
            objClosedHand.AddComponent<SpriteRenderer>().sprite = texturaMaoFechada;
            objClosedHand.transform.localPosition = new Vector3(0.0f, 0.0f, tempDistance);
            objClosedHand.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            objClosedHand.transform.localRotation = Quaternion.identity;
            objClosedHand.SetActive(false);
        }

        if (texturaMaoAberta)
        {
            objOpenHand = new GameObject("objHandTextureOpen");
            objOpenHand.transform.parent = this.transform;
            objOpenHand.AddComponent<SpriteRenderer>().sprite = texturaMaoAberta;
            objOpenHand.transform.localPosition = new Vector3(0.0f, 0.0f, tempDistance);
            objOpenHand.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            objOpenHand.transform.localRotation = Quaternion.identity;
            objOpenHand.SetActive(false);
        }
    }

    void Update()
    {
        rayEndPoint = transform.position + transform.forward * distance;
        if (Physics.Raycast(transform.position, transform.forward, out tempHit, 7))
        {
            if (Vector3.Distance(transform.position, tempHit.point) <= 6 && tempHit.transform.CompareTag(_tagObjects))
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && canMove)
            {
                if (tempHit.rigidbody)
                {
                    tempHit.rigidbody.useGravity = true;
                    distance = Vector3.Distance(transform.position, tempHit.point);
                    tempObject = tempHit.transform.gameObject;
                    isMoving = true;
                }
            }
        }
        else
        {
            canMove = false;
        }

        distance += Input.GetAxis("Mouse ScrollWheel") * 10.0f;
        distance = Mathf.Clamp(distance, 2.5f, 6);

        if (tempObject)
        {
            rbTemp = tempObject.GetComponent<Rigidbody>();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && tempObject)
        {
            rbTemp.useGravity = true;
            tempObject = null;
            rbTemp = null;
            isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && tempObject)
        {
            tempDirection = rayEndPoint - transform.position;
            tempDirection.Normalize();
            rbTemp.useGravity = true;
            rbTemp.AddForce(tempDirection * forcaDeArremeco);
            tempObject = null;
            rbTemp = null;
            isMoving = false;
        }

        if (tempObject)
        {
            if (Vector3.Distance(transform.position, tempObject.transform.position) > 6)
            {
                rbTemp.useGravity = true;
                tempObject = null;
                rbTemp = null;
                isMoving = false;
            }
        }

        if (tempObject && mainCamera)
        {
            if (Input.GetKey(KeyCode.R))
            {
                rotXTemp = Input.GetAxis("Mouse X") * 5.0f;
                rotYTemp = Input.GetAxis("Mouse Y") * 5.0f;
                tempObject.transform.Rotate(mainCamera.transform.up, -rotXTemp, Space.World);
                tempObject.transform.Rotate(mainCamera.transform.right, rotYTemp, Space.World);
            }
        }


        if (canMove && !isMoving && texturaMaoAberta)
        {
            objClosedHand.SetActive(false);
            objOpenHand.SetActive(true);
        }
        else if (isMoving && texturaMaoFechada)
        {
            objClosedHand.SetActive(true);
            objOpenHand.SetActive(false);
        }
        else
        {
            objClosedHand.SetActive(false);
            objOpenHand.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (tempObject)
        {
            rbTemp = tempObject.GetComponent<Rigidbody>();
            rbTemp.angularVelocity = new Vector3(0, 0, 0);
            tempSpeed = (rayEndPoint - rbTemp.transform.position);
            tempSpeed.Normalize();
            tempDistance = Vector3.Distance(rayEndPoint, rbTemp.transform.position);
            tempDistance = Mathf.Clamp(tempDistance, 0, 1);
            rbTemp.velocity = Vector3.Lerp(rbTemp.velocity, tempSpeed * 7.5f * tempDistance, Time.deltaTime * 12);
        }
    }
}
