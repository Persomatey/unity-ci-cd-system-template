using TMPro;
using UnityEngine;

public class VersioningController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI versionTMP;
	[SerializeField] private TextMeshProUGUI commitTMP;
	[SerializeField] private TextMeshProUGUI definesTMP;
	[SerializeField] private TextAsset jsonFile;

	private VersionData data = new VersionData();

	private void Start()
	{
		JSONReader();
		UpdateDefinesTMP();
	}

	private void JSONReader()
	{
		data = JsonUtility.FromJson<VersionData>(jsonFile.text);

		Debug.Log($"Loading {jsonFile.name}'s contents:\n{jsonFile.text}"); 

		versionTMP.text = $"Version: v{data.version}";
		commitTMP.text = $"Commit: {data.commit}";
	}

	private void UpdateDefinesTMP()
	{
		if (Application.isEditor)
		{
			definesTMP.text = "Defines: EDI";
		}

#if DEV
		definesTMP.text = "Defines: DEV"; 
#endif

#if REL
		definesTMP.text = "Defines: REL"; 
#endif
	}

	[System.Serializable]
	public class VersionData
	{
		public string version;
		public string commit;
	}
}
