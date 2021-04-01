using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class MenuController : MonoBehaviour
{
    public Texture2D MouseClickTexture, MouseTexture;
    public float speed;

    private Vector2 CursorDir;
  
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(MouseTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(Sinput.GetButtonDown("Select"))
        {
            Cursor.SetCursor(MouseClickTexture, Vector2.zero, CursorMode.Auto);
            CursorControl.SimulateLeftClick();
        }
        else if(Sinput.GetButtonUp("Select"))
        {
            Cursor.SetCursor(MouseTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    void FixedUpdate()
    {
            if (Sinput.GetAxis("Horizontal") != 0 || Sinput.GetAxis("Vertical") != 0)
        {
            CursorDir = new Vector2(Sinput.GetAxis("Horizontal"), Sinput.GetAxis("Vertical"));
           // CursorDir.Normalize();
            CursorControl.SetLocalCursorPos((Vector2)Input.mousePosition + (CursorDir * speed));      
        }
    }


}
