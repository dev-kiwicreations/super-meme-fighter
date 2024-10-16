using UnityEngine;
using UnityEditor;

namespace UFE3D
{
	public class UFEInputSetup : EditorWindow
	{
		// creates a menu entry that replaces a file.
		[MenuItem("Window/UFE/Project Settings/Override Input Manager")]
		static void ReplaceInputManagerAssetFile()
		{
			string path = Application.dataPath;
			path = path.Remove(path.Length - 6);

			string destPath = path + "ProjectSettings/";
			string sourcePath = path = "Assets/UFE/Engine/ProjectSettings/";
			bool exit = false;

			if (UnityEditor.EditorUtility.DisplayDialog("Replace InputManager.asset file", "Replace InputManager.asset file with one designed to work with UFE?"
														+ " (A backup will be made.)"
														+ "\n\nMake sure the InputManager settings are NOT open in the inspector or this won't work!", "OK", "Cancel"))
			{
				if (!System.IO.File.Exists(destPath + "InputManager.backup"))
				{
					// backup the old intputmanager.asset file
					FileUtil.CopyFileOrDirectory(destPath + "InputManager.asset", destPath + "InputManager.backup");
				}

				// copy the new inputmanager.asset file over the old version
				FileUtil.ReplaceFile(sourcePath + "UFEInput.asset", destPath + "InputManager.asset");
				exit = true;
			}

			if (exit && UnityEditor.EditorUtility.DisplayDialog("Restart Unity", "Inputmanager.asset successfully installed.\nUnity will have to restart now.\nClose the Editor?", "Yes", "No, I'll do it myself"))
			{
				EditorApplication.Exit(0);
			}
		}
	}
}