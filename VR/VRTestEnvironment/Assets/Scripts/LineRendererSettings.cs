using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineRendererSettings : MonoBehaviour
{
    
    [SerializeField] LineRenderer rend;

    Vector3[] points;
    public GameObject panel;
    public Image img;
    public Button btn;


    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<LineRenderer>();

        points = new Vector3[2];

        points[0] = Vector3.zero;

        //set the end point 20 units away from the GO on the z axis(pointing forward)
        points[1] = transform.position + new Vector3(0, 0, 20);

        rend.SetPositions(points);
        rend.enabled = true;
        img = panel.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        AlignLineRenderer(rend);
        if(AlignLineRenderer(rend) && Input.GetAxis("Submit") > 0)
        {
            btn.onClick.Invoke();
        }
        
    }
    public LayerMask layerMask;
    public bool AlignLineRenderer(LineRenderer rend)
    {
        bool hitbtn = false;
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, layerMask))
        {
            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            btn = hit.collider.gameObject.GetComponent<Button>();
            hitbtn = true;
        }
        else
        {
            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;
            hitbtn = false;
        }

        rend.SetPositions(points);
        rend.material.color = rend.startColor;
        return hitbtn;
    }


    public void ColorChangeOnClick() 
    {
        if(btn != null)
        {
            if(btn.name == "Button")
            {
                img.color = Color.red;
            }
        }
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SceneWithAds");
    }

}
