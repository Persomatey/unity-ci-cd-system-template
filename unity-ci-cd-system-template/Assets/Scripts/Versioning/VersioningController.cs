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

		versionTMP.text = $"v{data.version}";
		commitTMP.text = data.commit;
	}

	private void UpdateDefinesTMP()
	{
		if (Application.isEditor)
		{
			definesTMP.text = "EDI";
		}

#if DEV
		definesTMP.text = "DEV"; 
#endif

#if REL
		definesTMP.text = "REL"; 
#endif
	}

	[System.Serializable]
	public class VersionData
	{
		public string version;
		public string commit;
	}
}
