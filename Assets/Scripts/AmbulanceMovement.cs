using UnityEngine;

public class AmbulanceMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f; // Vitesse de déplacement de l'ambulance
    [SerializeField] private float _maxDistance = 50f; // Distance maximale que l'ambulance peut parcourir
    [SerializeField] private float _minDistance = 10f; // Distance minimale que l'ambulance peut parcourir
    [SerializeField] private bool _isFacingNorth = false; // Indique si l'ambulance se dirige vers le nord (true) ou le sud (false)

    private Rigidbody _rigidbody; // Référence au composant Rigidbody attaché à l'objet
    private AudioSource _audioSource; // Référence au composant AudioSource attaché à l'objet

    private float _normalSpeed; // Vitesse normale de l'ambulance
    //[SerializeField] private float _sprintSpeed = 20f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Récupère la référence au composant Rigidbody attaché à l'objet
        _audioSource = GetComponent<AudioSource>(); // Récupère la référence au composant AudioSource attaché à l'objet
        _normalSpeed = _speed; // Initialise la vitesse normale de l'ambulance à la vitesse initiale
        _audioSource.Play(); // Joue le son attaché à l'AudioSource
        _audioSource.loop = true; // Active la boucle de lecture du son
    }

    private void PushForward()
    {
        _rigidbody.velocity = transform.forward * _speed; // Applique une force de déplacement vers l'avant à l'objet
    }

    private void UTurn()
    {
        // Inverse le forward du transform en Z pour le retourner
        transform.forward = -transform.forward;
        // Inverse la valeur de la variable indiquant la direction actuelle de l'objet
        _isFacingNorth = !_isFacingNorth;
        // Fait demi-tour en appliquant une force de déplacement vers l'avant
        PushForward();
    }

    private void Update()
    {
        if (_isFacingNorth)
        {
            if (transform.position.z < _minDistance)
            {
                // Si l'objet est trop proche de sa position minimale, il doit faire demi-tour
                UTurn();
            }
        }
        else
        {
            if (transform.position.z > _maxDistance)
            {
                // Si l'objet est trop loin de sa position maximale, il doit faire demi-tour
                UTurn();
            }
        }

        if (_rigidbody.velocity.magnitude > 0)
        {
            // Si l'objet est en mouvement, applique un effet Doppler à son son
            _audioSource.dopplerLevel = 0.2f;
        }
        else
        {
            // Si l'objet est immobile, désactive l'effet Doppler sur son son
            _audioSource.dopplerLevel = 0f;
        }
    }
}
