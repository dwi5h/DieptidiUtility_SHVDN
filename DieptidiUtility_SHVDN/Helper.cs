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
using System.Globalization;

namespace DieptidiUtility_SHVDN
{
    public static class Helper
    {
        #region Formatting
        public static string DollarFormat(decimal price)
        {
            return price.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }
        #endregion

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
        public static List<string> LoadTextLinesFromLogTxt()
        {
            string _path = @"E:\PC\GTAV\log.txt";
            if (File.Exists(_path))
            {
                return File.ReadAllLines(_path).ToList();
            }

            return new List<string>();
        }
        public static void LoggingToLogTxt(string text)
        {
            string _path = @"E:\PC\GTAV\log.txt";
            File.WriteAllText(_path, text);
            Notification.Show("~g~Log ~w~Inputed");
        }
        public static void LoggingToLogTxt(string[] text)
        {
            string _path = @"E:\PC\GTAV\log.txt";
            File.WriteAllLines(_path, text);
            Notification.Show("~g~Log Lines ~w~Inputed");
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
        public static void DrawMarkerFromDistance(Vector3 position, float distanceX = 0f, float distanceY = 0f, float distanceZ = 0f)
        {
            var newPosition = new Vector3(position.X + distanceX, position.Y + distanceY, position.Z + distanceZ);
            World.DrawMarker(MarkerType.VerticalCylinder, newPosition, new Vector3(), new Vector3(), new Vector3(1, 1, 1), Color.Red);
        }
        #endregion

        #region Entity/Object/Blip
        public static Vehicle GetVehicleInFrontPlayer()
        {
            var frontPosition = Game.Player.Character.GetOffsetPosition(new Vector3(0, 0.5f, 0));
            return World.GetClosestVehicle(frontPosition, 1.5f);
        }
        public static Vehicle[] GetNearbyVehiclesInFrontPlayer(float radius)
        {
            var frontPosition = Game.Player.Character.GetOffsetPosition(new Vector3(0, 0.5f, 0));
            return World.GetNearbyVehicles(frontPosition, radius);
        }
        public static Entity GetEntityInFrontPlayer()
        {
            var frontPosition = Game.Player.Character.GetOffsetPosition(new Vector3(0, 0.5f, 0));
            //return World.GetClosestVehicle(frontPosition, 1.5f);
            return World.GetClosestProp(frontPosition, 1.5f);
        }
        public static float HeadingToEntity(Entity entity1, Entity entity2)
        {
            var p1 = Function.Call<Vector3>(Hash.GET_ENTITY_COORDS, entity1, false);
            var p2 = Function.Call<Vector3>(Hash.GET_ENTITY_COORDS, entity2, false);

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            return Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, dx, dy);

            return Game.Player.Character.Heading;
        }
        public static void ToggleEnableDoorOpen(uint hash, Vector3 doorPosition, bool isOpen)
        {
            if (isOpen)
            {
                Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                    hash,
                    doorPosition.X, doorPosition.Y, doorPosition.Z,
                    0);
            }
            else
            {
                Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                    hash,
                    doorPosition.X, doorPosition.Y, doorPosition.Z,
                    1);
            }
        }
        #endregion

        #region Interior
        public static void ActivatingInterior(int interiorId)
        {
            Function.Call(Hash.SET_INTERIOR_ACTIVE, interiorId, true);
            Function.Call(Hash.DISABLE_INTERIOR, interiorId, false);
        }
        public static int GetCurrentInteriorId()
        {
            var position = Game.Player.Character.Position;
            return Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, position.X, position.Y, position.Z);
        }
        public static uint GetCurrentRoomKey()
        {
            return Function.Call<uint>(Hash.GET_ROOM_KEY_FROM_ENTITY, Game.Player.Character);
        }
        public static void RefreshCurrentRoom(int interiorId, uint roomKey)
        {
            Function.Call(Hash.FORCE_ROOM_FOR_ENTITY, Game.Player.Character, interiorId, roomKey);
        }
        public static void RefreshCurrentRoom(int interiorId, uint roomKey, Entity entity)
        {
            Function.Call(Hash.FORCE_ROOM_FOR_ENTITY, entity, interiorId, roomKey);
        }
        #endregion

        #region Config
        public static void SetConfigValue<T>(string configName, string section, string name, T value)
        {
            string _file = $"{configName}.ini";
            string _path = $"{Directory.GetCurrentDirectory()}/scripts/{_file}";
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "");
                var config = ScriptSettings.Load(_path);
                config.SetValue(section, name, value);
                config.Save();
            }
        }
        public static T GetConfigValue<T>(string configName, string section, string name, T defaultvalue)
        {
            string _file = $"{configName}.ini";
            string _path = $"{Directory.GetCurrentDirectory()}/scripts/{_file}";
            if (File.Exists(_path))
            {
                var config = ScriptSettings.Load(_path);
                return config.GetValue<T>(section, name, defaultvalue);
            }

            return defaultvalue;
        }
        #endregion
    }
}
