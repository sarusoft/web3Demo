using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Tweener sq;
    public GameObject Cube;
    public bool _buttonPressed;
    public Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MoveCube(int i)
    {
        
       

    }
    



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Cube.transform.position = new Vector3(Cube.transform.position.x -0.01f, Cube.transform.position.y, Cube.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Cube.transform.position = new Vector3(Cube.transform.position.x + 0.01f, Cube.transform.position.y, Cube.transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Cube.transform.position = new Vector3(Cube.transform.position.x , Cube.transform.position.y, Cube.transform.position.z + 0.01f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Cube.transform.position = new Vector3(Cube.transform.position.x , Cube.transform.position.y, Cube.transform.position.z - 0.01f);
        }
    }

    
}
