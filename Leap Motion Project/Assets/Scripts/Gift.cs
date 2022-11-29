using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using TMPro;

public class Gift : MonoBehaviour
{
    private Material currentMaterial;

    public Material redMaterial, greenMaterial, blueMaterial;

    private TextMeshProUGUI redText, greenText, blueText;

    private InteractionBehaviour interactionBehaviour;

    private GameObject cube;

    private GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("Wall");
        cube = transform.Find("Cube").gameObject;
        currentMaterial = cube.GetComponent<Renderer>().material;
        interactionBehaviour = GetComponent<InteractionBehaviour>();
        redText = GameObject.Find("Red Text (TMP)").GetComponent<TextMeshProUGUI>();
        greenText = GameObject.Find("Green Text (TMP)").GetComponent<TextMeshProUGUI>();
        blueText = GameObject.Find("Blue Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void OnCollisionEnter(Collision other)
    {
        if (interactionBehaviour.isGrasped && other.gameObject.name == "Wall")
            return;
        
        if (other.gameObject.name.Contains("Gift"))
            return;

        if (other.gameObject.name.Contains("Contact"))
            return;

        if (other.gameObject.name == "Table")
            return;
        
        if (other.gameObject.layer == 7)
            return;

        Debug.Log(other.gameObject.name);

        if (other.gameObject.GetComponent<Renderer>().material.color == currentMaterial.color)
        {
            Debug.Log("Correct");
            Debug.Log(other.gameObject.GetComponent<Renderer>().material);
            if (currentMaterial.color == redMaterial.color)
            {
                Debug.Log("Red");
                redText.text = (int.Parse(redText.text) + 1).ToString("D2");
            }
            else if (currentMaterial.color == greenMaterial.color)
            {
                Debug.Log("Green");
                greenText.text = (int.Parse(greenText.text) + 1).ToString("D2");
            }
            else if (currentMaterial.color == blueMaterial.color)
            {
                Debug.Log("Blue");
                blueText.text = (int.Parse(blueText.text) + 1).ToString("D2");
            }

            // Animate the gift shrinking and then destroying itself
            StartCoroutine(ShrinkAndDestroy());
        }
        else
        {
            Debug.Log("Incorrect");
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ShrinkAndDestroy()
    {
        float shrinkTime = 0.5f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(0.01f, 0.01f, 0.01f);

        while (elapsedTime < shrinkTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, (elapsedTime / shrinkTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
