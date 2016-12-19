using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//! -(belum paham)-
public class TextProcessor : MonoBehaviour
{

    public string TextName;/*!<text*/
    private string rawString;
    private string[] unsortedString;
    private string[] ButtonString;

    [System.Serializable]
    //!text
    public class SortedTextAsset
    {
        public string[] textAsset;/*!<text*/
        /**
         * text
         * */
        public void RemoveNullString()
        {
            int totalNull = 0;
            int i = 0;
            string[] tempString;
            
            for (i = 0; i < textAsset.Length; i++)
            {
                if (textAsset[i].Length == 0 || textAsset[i].Length == 1)
                {
                    totalNull++;
                }
            }

            tempString = new string[textAsset.Length - totalNull];

            for (i = 0; i < textAsset.Length; i++)
            {
                if (textAsset[i].Length == 0 || textAsset[i].Length == 1)
                    continue;

                tempString[i-totalNull] = textAsset[i];
            }

            textAsset = tempString;
        }
    }

    [System.Serializable]
    //!text
    public class ProcessedText
    {
        public VariableSoal[]soal;/*!<text*/
    }
    [System.Serializable]
    //!text
    public class VariableSoal
    {
        public string minVal, maxVal, delta;/*!<text*/
    }


    public SortedTextAsset[] sortedTextAsset;/*!<text*/
    public ProcessedText[] processedText;/*!<text*/

    void LoadGlobalText()
    {
        rawString = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/" + TextName);
        //rawString = rawString.Replace(System.Environment.NewLine, "");
#if UNITY_EDITOR
        Debug.Log(rawString.ToString());
#endif
    }

    void Awake()
    {
        LoadGlobalText();
        ParseString();
    }
    void ParseString()
    {

        int i = 0;
        int totalNull = 0;
        int fixIndex = 0;
        rawString = rawString.Replace(System.Environment.NewLine, "");
        unsortedString = rawString.Split('#');

        for (i = 0; i < unsortedString.Length; i++)
        {
            if (unsortedString[i].Length == 0 || unsortedString[i].Length == 1)
            {
                totalNull++;
            }
        }
        //sort string
        sortedTextAsset = new SortedTextAsset[unsortedString.Length - totalNull];
        for (i = 0; i < unsortedString.Length; i++)
        {

            if (unsortedString[i].Length == 0 || unsortedString[i].Length == 1)
                continue;

            //Split depends of functionality
            sortedTextAsset[fixIndex] = new SortedTextAsset();
            sortedTextAsset[fixIndex].textAsset = unsortedString[i].Split('@');
            sortedTextAsset[fixIndex].RemoveNullString();
            fixIndex++;
        }
        
        //assing string to ProcessedText
        processedText = new ProcessedText[sortedTextAsset.Length];

        for (i = 0; i < sortedTextAsset.Length; i++)
        {
            processedText[i] = new ProcessedText();
            processedText[i].soal = new VariableSoal[sortedTextAsset[i].textAsset.Length];

            for (int j = 0; j < sortedTextAsset[i].textAsset.Length; j++)
            {
                string[] splittedText = sortedTextAsset[i].textAsset[j].Split(',');
                processedText[i].soal[j] = new VariableSoal();
                processedText[i].soal[j].minVal = splittedText[0];
                processedText[i].soal[j].maxVal = splittedText[1];
                processedText[i].soal[j].delta = splittedText[2];
            }

        }
         

    }

    public string getStringValByTitleAndLang(string label)/*!<text*/
    {
        string retVal = "Bahasa yang diinginkan tidak ada";
#if UNITY_EDITOR
        //Debug.Log(HashLabel.LANGUAGE);
#endif
        for (int i = 0; i < processedText.Length; i++)
        {
#if UNITY_EDITOR
           // Debug.Log(processedText[i].lang);
#endif
            
            /*if (processedText[i].lang == HashLabel.LANGUAGE)
            {

                return processedText[i].getValueByLabel(label);
            }*/
        }
        return retVal;
    }

    public string[] GetAllLanguage()/*!<text*/
    {
        string[] retVal = new string[processedText.Length];


        for (int i = 0; i < processedText.Length; i++)
        {
            //retVal[i] = processedText[i].lang;
        }
        return retVal;
    }
}
