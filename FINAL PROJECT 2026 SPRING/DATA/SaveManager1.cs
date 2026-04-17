using FinalBattler.Data;
using System;
using System.IO;
using System.Text.Json;

namespace FinalBattler.Data
{
    public class SaveManager
    {
        public static string SaveFilePath = "savegame";
        public static void SaveGame(FinalBattler.Data.GameData1 gameData)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(gameData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SaveFilePath, jsonString);
                Console.WriteLine("Game saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }

        }
        public static GameData1 LoadGame()
        {
            try
            {
                if (!File.Exists(SaveFilePath))
                {
                    Console.WriteLine("We didnt find files sorry");
                    return null;
                }
                string jsonString = File.ReadAllText(SaveFilePath);
                GameData1 loadedData = JsonSerializer.Deserialize<GameData1>(jsonString);
                return loadedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Loading Game :( ");
                return null;
            }
        }
    }
}