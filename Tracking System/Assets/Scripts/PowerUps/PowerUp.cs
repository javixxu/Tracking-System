using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField]
    float rotacion = 0.1f;
    Vector3 ejeRotacion = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        RotatePowerUp();
    }
    public void RotatePowerUp()
    {
        transform.Rotate(ejeRotacion, rotacion);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShipController>() != null)
        {
            Power(other.gameObject);
            this.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
    public virtual void Power(GameObject player) { }
}
