using System;
using System.IO;
using System.Text.Json;

namespace FINAL_Battler.Data
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
    }
}