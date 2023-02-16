using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 5f;
    private IUsable _target;

    private void Update()
    {
        FindTarget();
        UseTarget();
        ChangeCrossHairState();
    }

    private void FindTarget()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, _maxDistance);
        if (hit && hitInfo.collider.gameObject.TryGetComponent<IUsable>(out var usable))
        {
            _target = usable;
        }
        else
        {
            _target = null;
        }
    }

    private void UseTarget()
    {
        if (Input.GetButtonDown("Use") && _target != null)
        {
            _target.Use();
        }
    }

    private void ChangeCrossHairState()
    {
        if (_target != null)
        {
            // Changer la couleur du réticule pour indiquer que la cible peut être utilisée
            Debug.Log("Target found!");
        }
        else
        {
            // Réinitialiser la couleur du réticule
            Debug.Log("No target found.");
        }
    }
}
