using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateRecycler : MonoBehaviour {

	public Transform template;
	public bool createNew;

	/// <summary> Current number of active entries for the template. </summary>
	public int Count { get { return list.Count; } }

	private List<Transform> list = new List<Transform>();
	private Stack<Transform> backup = new Stack<Transform>();


	private void Start() {
		template.gameObject.SetActive(false);
	}

	public void Clear() {
		if (createNew) {
			for (int i = 0; i < list.Count; i++) {
				Destroy(list[i].gameObject);
			}
		}
		else {
			for (int i = list.Count - 1; i >= 0; i--) {
				ReturnObject(list[i]);
			}
		}
		list.Clear();
	}

	public void Editor_Clear() {
		for (int i = 0; i < list.Count; i++) {
			DestroyImmediate(list[i].gameObject);
		}
		list.Clear();
	}

	public Transform CreateEntry() {
		Transform t;
		if (backup.Count > 0) {
			t = backup.Pop();
		}
		else {
			t = Instantiate(template, transform);
		}
		list.Add(t);
		t.gameObject.SetActive(true);
		return t;
	}

	public T CreateEntry<T>() where T : Component {
		Transform t;
		if (backup.Count > 0) {
			t = backup.Pop();
			t.SetAsLastSibling();
		}
		else {
			t = Instantiate(template, transform);
		}
		list.Add(t);
		t.gameObject.SetActive(true);
		return t.GetComponent<T>();
	}

	public void Delete(int index) {
		if (createNew) {
			Destroy(list[index].gameObject);
		}
		else {
			ReturnObject(list[index]);
		}
		list.RemoveAt(index);
	}

	public void Delete(Transform returnTransform) {
		list.Remove(returnTransform);
		if (createNew) {
			Destroy(returnTransform.gameObject);
		}
		else {
			ReturnObject(returnTransform);
			returnTransform.SetAsLastSibling();
		}
	}

	public Transform GetEntry(int index) {
		return list[index];
	}

	public T GetEntry<T>(int index) where T : Component {
		return list[index].GetComponent<T>();
	}

	public List<Transform> GetAllEntries() {
		return new List<Transform>(list);
	}

	public List<T> GetAllEntries<T>() where T : Component {
		List<T> entryList = new List<T>();
		for (int i = 0; i < list.Count; i++) {
			entryList.Add(list[i].GetComponent<T>());
		}
		return entryList;
	}

	public int GetIndex(Transform search) {
		for (int i = 0; i < list.Count; i++) {
			if (search == list[i])
				return i;
		}
		return -1;
	}

	public void Sort(System.Comparison<Transform> sorter) {
		list.Sort(sorter);
		for (int i = 0; i < list.Count; i++) {
			list[i].SetSiblingIndex(i);
		}
	}

	private void ReturnObject(Transform t) {
		backup.Push(t);
		t.gameObject.SetActive(false);
	}
}
