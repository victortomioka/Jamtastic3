using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DashMovement : MonoBehaviour
{
    public float distance;
    public float speed;
    public float cooldown;

    public TrailRenderer trail;

    private bool coolingDown;
    private Rigidbody rb;

    public bool IsDashing { get; private set; }
    public bool IsDashAllowed { get{ return !IsDashing && !coolingDown; } }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Dash(Vector3 direction)
    {
        if (IsDashAllowed)
            StartCoroutine("DashCoroutine", direction);
    }

    public void Interrupt()
    {
        StopCoroutine("DashCoroutine");

        IsDashing = false;
        SendMessage("DashEnded", null, SendMessageOptions.DontRequireReceiver);

        SetTrailEnabled(false);
    }

    private IEnumerator DashCoroutine(Vector3 direction)
    {
        IsDashing = true;
        SendMessage("DashStarted", null, SendMessageOptions.DontRequireReceiver);

        SetTrailEnabled(true);

        float travelled = 0;
        Vector3 startPosition = rb.position;

        while(travelled <= distance)
        {
            yield return new WaitForEndOfFrame();

            Vector3 movement = direction * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            travelled = Vector3.Distance(startPosition, rb.position);
        }

        IsDashing = false;
        SendMessage("DashEnded", null, SendMessageOptions.DontRequireReceiver);

        SetTrailEnabled(false);

        coolingDown = true;
        yield return new WaitForSeconds(cooldown);
        coolingDown = false;
    }

    private void SetTrailEnabled(bool enabled)
    {
        if(trail != null)
            trail.enabled = enabled;
    }
}
