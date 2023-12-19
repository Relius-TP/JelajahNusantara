using System.Collections;
using TMPro;
using UnityEngine;

public class TextTypeWriterAnim : MonoBehaviour
{
    public TMP_Text _textMeshPro;

    [TextArea(10, 20)]
    public string stringArray;

    [SerializeField] float timeBtwnChars;

    void Start()
    {
        EndCheck();
    }

    public void EndCheck()
    {
        StartCoroutine(TextVisible());
    }

    public IEnumerator TextVisible()
    {
        foreach(char c in stringArray)
        {
            _textMeshPro.text += c;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

    public void Close()
    {
        StopCoroutine(TextVisible());
    }
}
