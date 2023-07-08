using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.UI;
using GTA.Math;
using GTA.Native;
using System.Drawing;


namespace DieptidiUtility_SHVDN
{
    public static class Helper
    {

        #region IO
        public static List<string> LoadTextLinesFromGTABaseFolder(string filePath)
        {
            string path = Directory.GetCurrentDirectory() + filePath;
            if (File.Exists(path))
            {
                return File.ReadAllLines(path).ToList();
            }

            return new List<string>();
        }
        #endregion

        #region Drawing/Render/Input
        public static void Draw3dText(Vector3 position, string text)
        {
            try
            {
                Function.Call(Hash.SET_DRAW_ORIGIN, position.X, position.Y, position.Z, 0);
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 10.0f, 0.555f);
                Function.Call(Hash.SET_TEXT_COLOUR, 255, 255, 255, 255);
                Function.Call(Hash.SET_TEXT_CENTRE, true);
                Function.Call(Hash.SET_TEXT_DROPSHADOW, 0, 0, 0, 0, 0);
                Function.Call(Hash.SET_TEXT_EDGE, 0, 0, 0, 0, 0);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, 0, 0);
                Function.Call(Hash.CLEAR_DRAW_ORIGIN);
            }
            catch (Exception ex)
            {
                Notification.Show("~r~Error: ~w~" + ex.Message);
            }
        }
        public static void DrawTextOnScreen(string text, Color color, float x, float y, int shadowDistance)
        {
            try
            {
                Color black = Color.Black;
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 1f, 0.555f);
                Function.Call(Hash.SET_TEXT_COLOUR, color.R, color.G, color.B, color.A);
                Function.Call(Hash.SET_TEXT_CENTRE, false);
                Function.Call(Hash.SET_TEXT_DROPSHADOW, shadowDistance, black.R, black.G, black.B, black.A);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x, y);
            }
            catch (Exception ex)
            {
                Notification.Show("~r~Error: ~w~" + ex.Message);
            }
        }
        public static void DrawTextOnScreen(string text, Color color, float x, float y, float fontSize, Color shadowColor, int shadowDistance)
        {
            try
            {
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 1f, fontSize);
                Function.Call(Hash.SET_TEXT_COLOUR, color.R, color.G, color.B, color.A);
                Function.Call(Hash.SET_TEXT_CENTRE, false);
                Function.Call(Hash.SET_TEXT_DROPSHADOW, shadowDistance, shadowColor.R, shadowColor.G, shadowColor.B, shadowColor.A);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x, y);
            }
            catch (Exception ex)
            {
                Notification.Show("~r~Error: ~w~" + ex.Message);
            }
        }
        #endregion
    }
}
