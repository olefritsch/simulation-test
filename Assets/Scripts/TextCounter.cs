using UnityEngine;
using UnityEngine.UI;

public class TextCounter : MonoBehaviour {

	[SerializeField] TransformRuntimeSet runtimeSet;
	[SerializeField] float updateDelay;

	private Text textComponent;
	private float timeSinceLastUpdate;

	void Awake() 
	{
		textComponent = GetComponent<Text>();
		//runtimeSet.OnCountChanged += UpdateText;

		//UpdateText ();
	}

	void Update()
	{ 
		// Limit the amount updates to avoid flickering
		if (Time.time - timeSinceLastUpdate > updateDelay) 
		{
			textComponent.text = runtimeSet.Count + " Entities";
			timeSinceLastUpdate = Time.time;
		}
	}

	void OnDestroy() 
	{
		//runtimeSet.OnCountChanged -= UpdateText;
	}
}
