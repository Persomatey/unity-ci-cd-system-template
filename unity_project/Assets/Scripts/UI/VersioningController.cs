using TMPro;
using UnityEngine;

public class VersioningController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI projectNameTMP;
	[SerializeField] private TextMeshProUGUI versionTMP;
	[SerializeField] private TextMeshProUGUI commitTMP;
	[SerializeField] private TextMeshProUGUI definesTMP;
	[SerializeField] private TextAsset jsonFile;

	private VersionData data = new VersionData();

	private void Start()
	{
		JSONReader();
		UpdateTMPs();
	}

	private void JSONReader()
	{
		data = JsonUtility.FromJson<VersionData>(jsonFile.text);

		Debug.Log($"Loaded {jsonFile.name}'s contents:\n{jsonFile.text}");
	}

	private void UpdateTMPs()
	{
		// Get data from Application 
		projectNameTMP.text = $"Name: {Application.productName}";
		versionTMP.text = $"Version: v{Application.version}";

		// Get data from JSON 
		commitTMP.text = $"Commit: {data.commit}";

		// Get data from Build Profile 
		#if DEV
		definesTMP.text = "Defines: DEV";
#endif

#if REL
		definesTMP.text = "Defines: REL"; 
#endif

		// Extra info for if you're in editor 
		if (Application.isEditor)
		{
			definesTMP.text += " (Editor)";
		}
	}

	[System.Serializable]
	public class VersionData
	{
		public string commit;
	}
}
