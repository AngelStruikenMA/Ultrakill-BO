using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPoints : MonoBehaviour
{
    [SerializeField] DraggablePoint A;
    [SerializeField] DraggablePoint B;
    [SerializeField] LineRenderer lineRenderer;
    public LinearFunction f = new LinearFunction(1,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f.SetFunctionWithTwoPoints(A.transform.position, B.transform.position);
        lineRenderer.SetPosition(0, new Vector3(-10, f.getY(-10), 0));
        lineRenderer.SetPosition(1, new Vector3(10, f.getY(10), 0));

    }
}