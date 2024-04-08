using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Rendering.PostProcessing;

public class GraphicalOptions : MonoBehaviour
{
   public TMPro.TMP_Dropdown TextureDropDown;
    public TMPro.TMP_InputField FPSCapInputField;
    public Toggle VSyncTog;
    public TMPro.TMP_Dropdown BloomDropDown;
    public Toggle DepthOfField;
    public Toggle ScreenSpaceRef;
    public Toggle Vignette;
    public bool IsDuringGame = false;
    public PostProcessLayer PostProcessLayer;
    bool DOF;
    int Bloom;
    bool SSR;
    bool Vig;

     
    private void Start()
    {
        string path = Application.persistentDataPath + "/graphics.txt";
        if (File.Exists(path))
        {
            QualitySettings.globalTextureMipmapLimit = int.Parse(ReadString(0));
            TextureDropDown.value = QualitySettings.globalTextureMipmapLimit;
            Application.targetFrameRate = int.Parse(ReadString(2));
            Bloom = int.Parse(ReadString(3));
            Debug.Log(ReadString(4)+"Start");
            if (ReadString(4) == "True")
            {
                DOF = true;
            }
            else if (ReadString(4) == "False")
            {
                DOF = false;
            }
            if (ReadString(5) == "True")
            {
                SSR = true;
            }
            else
            {
                SSR = false;
            }
            if (ReadString(6) == "True")
            {
                Vig = true;
                Debug.Log("Yeessss.. good.. good..");
            }
            else
            {
                Vig = false;
                Debug.Log("NOOooooo...");
            }

            BloomDropDown.value = Bloom;
            DepthOfField.isOn = DOF;
            ScreenSpaceRef.isOn = SSR;
            Vignette.isOn = Vig;
            if (ReadString(1) == "0")
            {
                
                if (Application.targetFrameRate != -1)
                {
                    FPSCapInputField.text = Application.targetFrameRate.ToString();

                }

                VSyncTog.isOn = false;
            }
            else
            {
                VSyncTog.isOn = true;
            }
            QualitySettings.vSyncCount = int.Parse(ReadString(1));
            
            
            
        }
    }
    public void TextureChanged()
   {
        QualitySettings.globalTextureMipmapLimit = TextureDropDown.value;
        WriteString();
   }
    public void OnChangedVsyncFps()
    {
        if (VSyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = int.Parse(FPSCapInputField.text);
        }
        WriteString();
    }
    public void BloomChanged()
    {
        Bloom = BloomDropDown.value;     
        WriteString();
    }
    public void DOFChanged()
    {
        DOF = DepthOfField.isOn;
        Debug.Log("DOFs has changed...");
        WriteString();
    }
    public void SSRChanged()
    {
        SSR = ScreenSpaceRef.isOn;
        WriteString();

    }
    public void VigChanged()
    {
        Vig = Vignette.isOn;
        WriteString();
    }
    public void WriteString()
    {
        string path = Application.persistentDataPath + "/graphics.txt";

        
        
        
        File.WriteAllText(path, "");
        
        StreamWriter writer = new StreamWriter(path, true);
        
        writer.WriteLine(QualitySettings.globalTextureMipmapLimit.ToString());
        writer.WriteLine(QualitySettings.vSyncCount);
        writer.WriteLine(Application.targetFrameRate);
        writer.WriteLine(BloomDropDown.value);
       // writer.WriteLine(DepthOfField.isOn);
        writer.WriteLine(SSR);
        writer.WriteLine(Vig);
        writer.Close();
       
    }
    public string ReadString(int Line)
    {
        string path = Application.persistentDataPath + "/graphics.txt";
        
        StreamReader reader = new StreamReader(path);
        string[] lines = File.ReadAllLines(path);
        reader.Close();
        return lines[Line];
        
    }
}
