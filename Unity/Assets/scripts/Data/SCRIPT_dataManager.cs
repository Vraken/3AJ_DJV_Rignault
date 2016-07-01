using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class CharacterData
{
    public string character_name;

    public int health;
    public int stamina;
    public int strength;
}


public class SCRIPT_dataManager : MonoBehaviour {

    [SerializeField]
    private CharacterData characterData;

    public void saveProfile(PlayerStats character)
    {
        // Chemin vers un répertoire de données persistantes (même après fermeture du jeu), crossplatform et respectant les recommandations de chaque OS.
        var path = Application.persistentDataPath + "/deicide/profile.data";

        if (!File.Exists(path))
        {
            createProfile();
        }

        #region Serialization
        // Creation d'une structure de donnée plus complexe (ne pas utiliser les PlayerPrefs pour cela.
        var data = new CharacterData
        {
            character_name = character.getName(),
            health = character.getHealth(),
            stamina = character.getStamina(),
            strength = character.getStrength(),
        };

        // Ouverture du fichier en mode écriture
        var file = File.OpenWrite(path);

        // Initialization d'un serializer binaire
        var bf = new BinaryFormatter();

        // Serialization de notre structure dans le fichier
        bf.Serialize(file, data);

        // Ne pas oublier de fermer le flux après utilisation
        file.Close();
        #endregion
    }

    public PlayerStats loadProfile()
    {
        // Chemin vers un répertoire de données persistantes (même après fermeture du jeu), crossplatform et respectant les recommandations de chaque OS.
        var path = Application.persistentDataPath + "/deicide/profile.data";

        if (!File.Exists(path))
        {
            createProfile();
        }

        #region Deserialization
        // Ouverture du fichier en mode lecture
        var fileRead = File.OpenRead(path);

        // Initialization d'un serializer binaire
        var bf = new BinaryFormatter();

        // Reconstruction de la structure
        characterData = (CharacterData)bf.Deserialize(fileRead);

        // Ne pas oublier de fermer le flux après utilisation
        fileRead.Close();

        PlayerStats character = new PlayerStats(characterData.character_name, characterData.health, characterData.stamina, characterData.strength);
        return character;
        #endregion
    }

    public void createProfile()
    {
        var path = Application.persistentDataPath + "/deicide/profile.data";
        var fileCreate = File.Create(path);
        fileCreate.Close();
        PlayerStats characterStats = new PlayerStats();
        saveProfile(characterStats);
    }
}
