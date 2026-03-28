using UnityEngine;

public class TopViewNew : MonoBehaviour
{
    public Transform target;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        if(target != null){
            transform.LookAt(target);
        }
    }
}
