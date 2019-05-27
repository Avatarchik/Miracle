using UnityEngine;
using System.Collections;

public class RockerCotroller : MonoBehaviour
{
    public GameObject target;
    public UIJoystick joystick;
    Vector3 dir = Vector3.zero;
    private void Update()
    {
        if (joystick.InputAxis != Vector2.zero)
        {
            dir = new Vector3(joystick.InputAxis.normalized.x, 0, joystick.InputAxis.normalized.y);
            //向目标方向移动
            target.transform.Translate(dir * Time.deltaTime,Space.World);
            //旋转至目标方向
            Quaternion newRotation = Quaternion.LookRotation(dir);
            target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, newRotation, 130 * Time.deltaTime);
        }
    }


}
