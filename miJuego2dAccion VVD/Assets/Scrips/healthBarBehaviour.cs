
using UnityEngine;
using UnityEngine.UIElements;

public class healthBarBehaviour : MonoBehaviour
{
    public Slider Slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    

    public void SetHealth(float health, float maxHealth)
    {
       
    }
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
