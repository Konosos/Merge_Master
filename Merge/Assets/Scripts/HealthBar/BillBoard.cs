using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Transform modelTrans;
    [SerializeField] private RectTransform healthBarTrans;
    private Camera cam;
    [SerializeField] private float hight = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        healthBarTrans.localScale = Vector3.one / modelTrans.localScale.x ;
        //transform.LookAt(transform.position + new Vector3(0, 15, -19));
        transform.LookAt(transform.position + cam.transform.position);
    }
}
