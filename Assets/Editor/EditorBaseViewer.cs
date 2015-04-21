using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EditorBaseViewer : EditorWindow {

	static public List<BaseGameStructure> sl_bsBases;
	static public List<KeyValuePair<string, bool>> sl_mapsbShowTrains;
	static private int s_iWindowCount;
	static private GUIStyle s_guiStyleDisabled;
	static private GUIStyle s_guiStyleAlert;
	static private bool s_bCleanRuntime;

	//Menu items
	[MenuItem("Window/Base Viewer %&b")]
	private static void NewMenuOption() {
		Init();
	}

	[MenuItem("CONTEXT/BaseGameStructure/Base Viewer")]
	private static void MenuBaseContext() {
		Init();
	}

	//Window
	public static void Init() {
		EditorBaseViewer window = (EditorBaseViewer)EditorWindow.GetWindow(typeof (EditorBaseViewer));

		ReloadBases();

		s_guiStyleDisabled = new GUIStyle();
		s_guiStyleDisabled.normal.textColor = Color.grey;

		s_guiStyleAlert = new GUIStyle();
		s_guiStyleAlert.normal.textColor = Color.red;

		window.Show();
	}

	public void OnGUI() {
		if (sl_bsBases != null) {
			if (sl_bsBases.Count > 0) {
				foreach (BaseGameStructure curbase in sl_bsBases) {
					//========= GUI BEGIN 
					GUILayout.BeginVertical("box");

					//=========== GUI BEGIN Base Name
					GUILayout.Label(curbase.name, EditorStyles.boldLabel);
					//=========== GUI END   Base Name

					//=========== GUI BEGIN Jump To
					if (GUILayout.Button("Focus Scene View")) {
						EditorCameraLookAt(curbase.transform.position);
					}
					//=========== GUI END   Jump To

					//=========== GUI BEGIN Corestats
					GUILayout.BeginHorizontal("box");
					GUILayout.Label(curbase.fHealthCurrent.ToString() + "/" + curbase.fHealthMax.ToString());
					if (GUILayout.Button("Fix")) {
						curbase.fHealthCurrent = curbase.fHealthMax;
					}
					if (GUILayout.Button("Upgrade") && EditorApplication.isPlaying) {
						curbase.UpgradeIntegrity();
						curbase.UpgradeWindows();
						curbase.UpgradeTrains();
					}
					GUILayout.EndHorizontal();
					//=========== GUI END   Corestats

					//=========== GUI BEGIN Upgrades
					GUILayout.BeginVertical("box");
					GUILayout.BeginHorizontal();
					GUILayout.Label("HP Lv. " + curbase.iIntegrityLevel);
					GUILayout.Label("Window Lv. " + curbase.iWindowLevel);
					GUILayout.Label("Trains Lv. " + curbase.iTrainsLevel);
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					//=========== GUI END   Upgrades

					//=========== GUI BEGIN Attackers
					GUILayout.BeginHorizontal();
					GUILayout.Label(curbase.l_euAttackers.Count.ToString() + " attacker(s)");
					if (curbase.l_euAttackers.Count > 0) {
						GUILayout.Label("Alert!", s_guiStyleAlert);
					}
					GUILayout.EndHorizontal();
					//=========== GUI END   Attackers

					//=========== GUI BEGIN Windows
					GUILayout.BeginVertical("box");
					s_iWindowCount = 1;

					//Windows are made dynamically, so not useful when not playing
					if (curbase.l_windows == null || curbase.l_windows.Count == 0 || !EditorApplication.isPlaying) {
						GUILayout.Label("No window(s) found.", s_guiStyleDisabled);
					}
					else {
						foreach (Window win in curbase.l_windows) {
							GUILayout.BeginHorizontal();
							//GUILayout.Label("Window #" + s_iWindowCount.ToString() + ": " + s_sWindowInfo);
							GUILayout.Label("Window #" + s_iWindowCount.ToString() + ": ");
							if (win.bIsActive) {
								GUILayout.Label("Act");
							}
							else {
								GUILayout.Label("act", s_guiStyleDisabled);
							}

							if (win.bIsManned) {
								GUILayout.Label("Man");
							}
							else {
								GUILayout.Label("man", s_guiStyleDisabled);
							}

							if (win.bIsTargeted) {
								GUILayout.Label("Tar");
							}
							else {
								GUILayout.Label("tar", s_guiStyleDisabled);
							}
							GUILayout.EndHorizontal();

							s_iWindowCount++;
						}
					}
					GUILayout.EndVertical();
					//=========== GUI END   Windows

					//========= GUI END
					GUILayout.EndVertical();
				}
			}
			else {
				GUILayout.Label("No BaseGameStructure object(s) found!", EditorStyles.boldLabel);
			}
		}
		else {
			//Reload and style the GUI when switching from edit to play
			if (Event.current.type == EventType.Repaint) {
				Init();
			}
		}
	}

	public void OnInspectorUpdate() {
		//Keep data up to date while running
		if (EditorApplication.isPlaying) {
			this.Repaint();
			s_bCleanRuntime = true;
		}
		//Return to default, editor state after a play
		else if (s_bCleanRuntime) {
			ReloadBases();
		}
	}

	public static void ReloadBases() {
		if (sl_bsBases == null) {
			sl_bsBases = new List<BaseGameStructure>();
		}

		if (sl_bsBases != null) {
			sl_bsBases.Clear();
			//We'd like them alphabetical please Unity
			sl_bsBases = GameObject.FindObjectsOfType<BaseGameStructure>().OrderBy(go=>go.name).ToList();
		}

		s_bCleanRuntime = false;
	}

	private static void EditorCameraLookAt(Vector3 target) {
		if (EditorWindow.GetWindow<EditorWindow>("Scene")) {
			SceneView.lastActiveSceneView.LookAt(target);
			SceneView.lastActiveSceneView.Repaint();
		}
		else {
			Debug.Log("Can't find the Scene view.");
		}
	}
}
