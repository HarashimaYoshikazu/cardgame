using System;
using UnityEngine;

/// <summary>
/// SerializeReferenceの項目を表示してくれるEditor拡張用クラス
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class SubclassSelectorAttribute : PropertyAttribute
{
	bool _includeMono;

	public SubclassSelectorAttribute(bool includeMono = false)
	{
		_includeMono = includeMono;
	}

	public bool IsIncludeMono()
	{
		return _includeMono;
	}
}