using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    // Start is called before the first frame update
    private void FinishAnim()
    {
        // Debug.Log("Finish anim");
        Destroy(gameObject);
    }
}
