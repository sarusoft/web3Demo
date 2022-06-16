using System;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;

public class Metaverse : MonoBehaviour
{
    // Start is called before the first frame update

    public Text verifyAddress;

    public string message = "hello";


    private void Start()
    {
//        OnHashMessage();
    }
    public void OnSignOut()
    {
        // Clear Account
        PlayerPrefs.SetString("Account", "0x0000000000000000000000000000000000000001");
        // go to login scene
        SceneManager.LoadScene(0);
        
    }
    async public void OnHashMessage()
    {
        try
        {
            string hashedMessage = await Web3GL.Sha3(message);
            Debug.Log("Hashed Message: " + hashedMessage);
            string signHashed = await Web3GL.Sign(hashedMessage);
            Debug.Log("Signed Hashed: " + signHashed);
            ParseSignatureFunction(signHashed);
            Task<string> verify = EVM.Verify(hashedMessage, signHashed);
            verifyAddress.text = await verify;
            Debug.Log("Verify Address: " + verifyAddress.text);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
        StartCoroutine(WalletHasParcelOrNot());
    }
    public void ParseSignatureFunction(string sig)
    {
        string signature = sig;
        string r = signature.Substring(0, 66);
        Debug.Log("R:" + r);
        string s = "0x" + signature.Substring(66, 64);
        Debug.Log("S: " + s);
        int v = int.Parse(signature.Substring(130, 2), System.Globalization.NumberStyles.HexNumber);
        Debug.Log("V: " + v);
    }


    //Check parcel data
    IEnumerator WalletHasParcelOrNot()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.sandvault.dev/owner/" + verifyAddress.text);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            verifyAddress.text = "error";
        }
        else
        {
            verifyAddress.text = www.downloadHandler.text;

        }
    }

}
