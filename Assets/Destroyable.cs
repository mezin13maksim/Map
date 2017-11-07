using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

    void OnMouseUp()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Destroy(this);
        }


    }


}
