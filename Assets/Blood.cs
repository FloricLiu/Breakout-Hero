/*blood script*/
/*liu chen han*/
using UnityEngine;

class Blood : MonoBehaviour
{
    private float memberTime = 1.0f;
    protected void Update()
    {
        memberTime -= Time.deltaTime;
        if (memberTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}