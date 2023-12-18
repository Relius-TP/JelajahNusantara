using System.Collections;
using TMPro;
using UnityEngine;

public class TextTypeWriterAnim : MonoBehaviour
{
    //public AnimatorHandler animatorHandler;
    public TMP_Text _textMeshPro;
    public string stringArray;

    [SerializeField] float timeBtwnChars;
    //[SerializeField] float timeBtwnWords;

    int i = 0;

    void OnEnable()
    {
        stringArray = _textMeshPro.text;
        EndCheck(); //panggil ini
    }

    public void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            //mulai
            _textMeshPro.text = stringArray;
            StartCoroutine(TextVisible());
        }
    }

    public IEnumerator TextVisible()
    {

        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;
        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                if (i > stringArray.Length - 1) // cek apakah sudah sampai ke teks terakhir
                {
                    yield break; // hentikan coroutine
                }
                else
                {  
                    break;
                }
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }
}
