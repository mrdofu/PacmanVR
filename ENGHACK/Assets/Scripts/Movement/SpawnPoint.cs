using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    protected GameObject prefab;
    /**
     * Instantiates appropriate prefab
     */
    public virtual void InstantiatePrefab (Transform parent = null) {
        Instantiate(prefab, transform.position, transform.rotation, parent);
    }
}
