using UnityEngine;
using System.Collections;

/* Discription: ObjectComponent class for rotating items in fixed position in inspect mode
 * 
 * Created by: Robert Datum: 02/04-14
 * Modified by: Jimmy 03-04-14
 * 				Jon Wahlström 2014-04-14
 * 
 */

public class Inspect : ObjectComponent
{
    #region PublicMemberVariables
    public float m_Sensitivity = 20.0f;
    public float m_InspectionViewDistance = 2.0f;
    public float m_LerpSpeed = 5f;
    public string m_Input = "Fire2";
    #endregion

    #region PrivateMemberVariables
    private Vector3 m_OriginalPosition;
    private Quaternion m_OriginalRotation;
    private int m_DeActivateCounter = 0;
    private float m_ActualViewDistance = 0;
    private bool m_PickUp = false;
    private bool m_IsInspecting = false;
    private bool m_IsLerping = false;
    #endregion

    public bool IsInspecting
    {
        get { return m_IsInspecting; }
        set { m_IsInspecting = value; }
    }

    void Update()
    {
        if (gameObject.GetComponent<PickUp>())
        {
            m_PickUp = gameObject.GetComponent<PickUp>().HoldingObject;
        }

        if (m_IsInspecting && !m_IsLerping)
        {
            float m_moveX = Input.GetAxis("Mouse X") * m_Sensitivity;
            float m_moveY = Input.GetAxis("Mouse Y") * m_Sensitivity;

            transform.RotateAround(collider.bounds.center, Vector3.left, m_moveY);
            transform.RotateAround(collider.bounds.center, Vector3.up, m_moveX);
        }
    }

    public void StartInspecting()
    {
        if (!m_IsLerping)
        {
            m_IsInspecting = true;

            Camera.main.transform.gameObject.GetComponent<FirstPersonCamera>().LockCamera();
            Camera.main.transform.parent.GetComponent<FirstPersonController>().LockPlayerMovement();

            m_OriginalPosition = transform.position;
            m_OriginalRotation = transform.rotation;

            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                rigidbody.useGravity = false;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
            Cast();
            StartCoroutine("LerpOutObject", Camera.main.transform.position + (Camera.main.transform.forward * m_ActualViewDistance));

            if (!m_PickUp)
            {
                Camera.main.gameObject.GetComponent<Raycasting>().m_HoldingObject = true;
                Camera.main.gameObject.GetComponent<Raycasting>().HoldObject = gameObject;
            }
        }
    }

    public override void Interact()
    {
        if (!m_IsInspecting)
            StartInspecting();
    }

    public void StopInspecting()
    {
        if (!m_IsLerping)
        {
            StartCoroutine("LerpInObject");
        }
    }

    public void Drop()
    {
        if (!m_IsLerping && m_PickUp)
        {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                rigidbody.useGravity = true;
            }

            if (!m_PickUp)
            {
                Camera.main.gameObject.GetComponent<Raycasting>().m_HoldingObject = false;
                Camera.main.gameObject.GetComponent<Raycasting>().HoldObject = null;
            }
            Camera.main.transform.gameObject.GetComponent<FirstPersonCamera>().UnLockCamera();
            Camera.main.transform.parent.GetComponent<FirstPersonController>().UnLockPlayerMovement();

            m_IsInspecting = false;
//            gameObject.GetComponent<PickUp>().DropInspect();
        }
    }

    public IEnumerator LerpOutObject(Vector3 outPosition)
    {
        m_IsLerping = true;
        while (Vector3.Distance(transform.position, outPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, outPosition, m_LerpSpeed / 10.0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        m_IsLerping = false;
        yield return null;
    }

    public IEnumerator LerpInObject()
    {
        m_IsLerping = true;
        while (Vector3.Distance(transform.position, m_OriginalPosition) > 0.01f || Quaternion.Angle(transform.rotation, m_OriginalRotation) > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, m_OriginalPosition, m_LerpSpeed / 10.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, m_OriginalRotation, m_LerpSpeed / 10.0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        m_IsLerping = false;

        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            rigidbody.useGravity = true;
        }

        if (!m_PickUp)
        {
            Camera.main.gameObject.GetComponent<Raycasting>().m_HoldingObject = false;
            Camera.main.gameObject.GetComponent<Raycasting>().HoldObject = null;
        }
        Camera.main.transform.gameObject.GetComponent<FirstPersonCamera>().UnLockCamera();
        Camera.main.transform.parent.GetComponent<FirstPersonController>().UnLockPlayerMovement();

        m_IsInspecting = false;
        yield return null;
    }

    void Cast()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.transform.position, Camera.main.transform.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * m_InspectionViewDistance, Color.magenta);

        if (Physics.Raycast(ray, out hit, m_InspectionViewDistance))
        {
            m_ActualViewDistance = Vector3.Distance(hit.point, Camera.main.transform.position);
            m_ActualViewDistance *= 0.88f;
        }
        else
        {
            m_ActualViewDistance = m_InspectionViewDistance;
        }
    }
}