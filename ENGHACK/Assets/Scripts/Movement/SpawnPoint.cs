using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    protected GameObject prefab;
    /**
     * Instantiates appropriate dot prefab
     */
    public virtual void InstantiatePrefab () {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
