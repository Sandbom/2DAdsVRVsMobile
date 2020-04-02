using System.Linq;
using UnityEditor;

namespace Adverty.Editor
{
    internal class AdvertyPrefabProcessor : AssetPostprocessor
    {
        private static string SdkLocation = EditorUtils.AdvertyDirectory;

        private const string UNIT_BASE_PATH = "Prefabs/Unit.prefab";
        private const string PRESET_PATH_BASE = "Prefabs/UnitPresets/";
        private const string BILLBOARD_PREFAB_NAME = "Billboard_Landscape.prefab";
        private const string BOX_STUTTER_SQUARE_PREFAB_NAME = "Box_Shutter_Box.prefab";
        private const string BOX_STUTTER_PORTRAIT_PREFAB_NAME = "Box_Shutter_Portrait.prefab";
        private const string BOX_STUTTER_LANDSCAPE_PREFAB_NAME = "Box_Shutter_Landscape.prefab";
        private const string POSTER_PREFAB_NAME = "Poster_Portrait.prefab";
        private const string FRAME_SQUARE_DARK_PREFAB_NAME = "Static_Frame_Box_Dark.prefab";
        private const string FRAME_SQUARE_LIGHT_PREFAB_NAME = "Static_Frame_Box_Light.prefab";
        private const string FRAME_PORTRAIT_DARK_PREFAB_NAME = "Static_Frame_Portrait_Dark.prefab";
        private const string FRAME_PORTRAIT_LIGHT_PREFAB_NAME = "Static_Frame_Portrait_Light.prefab";
        private const string FRAME_LANDSCAPE_DARK_PREFAB_NAME = "Static_Frame_Landscape_Dark.prefab";
        private const string FRAME_LANDSCAPE_LIGHT_PREFAB_NAME = "Static_Frame_Landscape_Light.prefab";

        private static readonly string[] advertyPresetsPaths = new[]
        {
            SdkLocation + UNIT_BASE_PATH,
            SdkLocation + PRESET_PATH_BASE + BILLBOARD_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + BOX_STUTTER_SQUARE_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + BOX_STUTTER_PORTRAIT_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + BOX_STUTTER_LANDSCAPE_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + POSTER_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + FRAME_SQUARE_DARK_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + FRAME_SQUARE_LIGHT_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + FRAME_PORTRAIT_DARK_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + FRAME_PORTRAIT_LIGHT_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + FRAME_LANDSCAPE_DARK_PREFAB_NAME,
            SdkLocation + PRESET_PATH_BASE + FRAME_LANDSCAPE_LIGHT_PREFAB_NAME
        };

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string assetPath in importedAssets)
            {
                if (advertyPresetsPaths.Contains(assetPath))
                {
                    AdvertyPrefabValidator.ValidatePrefab(assetPath, assetPath == advertyPresetsPaths[0]);
                }
            }
        }

    }
}
