using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using System.Text;


public class LoadSaveManager : Singleton<LoadSaveManager>
{
    public class GameData
    {
        public class HighScores
        {
            public int highestGoldAmount;
            public float heaviestWeight;
        }

        public class PlayerStats
        {
            public int gold, totalGold;
            public int commonBait, castersBait, anglersBait, enchantedBait;
            public bool gameReset = false;
        }

        public PlayerStats playerStats = new PlayerStats();
        public HighScores highScores = new HighScores();
    }

    private const string password = "PotatoSO";

    //Data path
    string dataPath;

    // Game data to save/load
    public GameData gameData = new GameData();

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "GameData.xml");
        Load();
    }

    public void Save()
    {
        Debug.Log("Saving Game File");
        // Save game data
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(dataPath, FileMode.Create);

        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = ASCIIEncoding.ASCII.GetBytes(password);
        DES.IV = ASCIIEncoding.ASCII.GetBytes(password);

        ICryptoTransform desencrypt = DES.CreateEncryptor();

        using (CryptoStream cStream = new CryptoStream(stream, desencrypt, CryptoStreamMode.Write))
        {
            serializer.Serialize(cStream, gameData);
        }

        stream.Close();
        stream.Dispose();
    }

    public void Load()
    {
        if (System.IO.File.Exists(dataPath))
        {
            Debug.Log("Loading Game File");
            Debug.Log("From: " + dataPath);
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream stream = new FileStream(dataPath, FileMode.Open);


            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(password);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(password);

            ICryptoTransform desDecrypt = DES.CreateDecryptor();

            using (CryptoStream cStream = new CryptoStream(stream, desDecrypt, CryptoStreamMode.Read))
            {
                gameData = serializer.Deserialize(cStream) as GameData;
            }

            stream.Close();
            stream.Dispose();
        }
        else
        {
            Debug.Log("File does not exist yet");
            Debug.Log("Creating file at: " + dataPath);
            ResetPlayerData();
            gameData.highScores.heaviestWeight = 0f;
            gameData.highScores.highestGoldAmount = 0;
            Save();
            PrintStats();
        }
    }

    void PrintStats()
    {
        Debug.Log("Gold: " + gameData.playerStats.gold + " Common Bait: " + gameData.playerStats.commonBait + " Casters Bait: " + gameData.playerStats.castersBait + " Anglers Bait: " + gameData.playerStats.anglersBait + " Enchanted Bait: " + gameData.playerStats.enchantedBait);
    }

    public void ResetPlayerData()
    {
        gameData.playerStats.gold = 15;
        gameData.playerStats.totalGold = 0;
        gameData.playerStats.commonBait = 3;
        gameData.playerStats.castersBait = 0;
        gameData.playerStats.anglersBait = 0;
        gameData.playerStats.enchantedBait = 0;
        gameData.playerStats.gameReset = false;
    }
}
