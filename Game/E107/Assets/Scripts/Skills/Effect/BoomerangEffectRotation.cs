using UnityEngine;

public class BoomerangEffectRotation : MonoBehaviour
{
    private const float ANGLULAR_VELOCITY = 4 * 360.0f;     // degrees per second

    private void Update()
    {
        Vector3 currentEuler = gameObject.transform.eulerAngles;
        Vector3 nextEuler = new Vector3(currentEuler.x, currentEuler.y + ANGLULAR_VELOCITY * Time.deltaTime, currentEuler.z);
        gameObject.transform.eulerAngles = nextEuler;
    }
}