using System.Collections;
using TMPro;
using UnityEngine;

public class InteractedObjectTextUI : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] TextMeshPro textMesh;

    public void Initialize(string text, Color textColor, float textLifetime)
    {
        transform.position += new Vector3(0, 0.2f);

        lifetime = textLifetime;
        textMesh.color = textColor;
        textMesh.text = text;

        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifetime);


        Destroy(gameObject);
    }
}
