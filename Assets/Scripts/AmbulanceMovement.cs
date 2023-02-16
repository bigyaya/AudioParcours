using UnityEngine;

public class AmbulanceMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 50f;
    [SerializeField] private float _minDistance = 10f;
    [SerializeField] private bool _isFacingNorth = false;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private float _normalSpeed;
    //[SerializeField] private float _sprintSpeed = 20f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _normalSpeed = _speed;
        _audioSource.Play();
        _audioSource.loop = true;
    }

    private void PushForward()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void UTurn()
    {
        // On inverse le forward du transform en Z pour le retourner
        transform.forward = -transform.forward;
        // On inverse la valeur de la variable indiquant notre direction actuelle (aller 0 ou retour 1)
        _isFacingNorth = !_isFacingNorth;
        // On fait demi-tour
        PushForward();
    }

    private void Update()
    {
        if (_isFacingNorth)
        {
            if (transform.position.z < _minDistance)
            {
                UTurn();
            }
        }
        else
        {
            if (transform.position.z > _maxDistance)
            {
                UTurn();
            }
        }

        if (_rigidbody.velocity.magnitude > 0)
        {
            _audioSource.dopplerLevel = 0.2f;
        }
        else
        {
            _audioSource.dopplerLevel = 0f;
        }
    }
}