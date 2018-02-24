
using UnityEngine;
using UnityEditor;
using Klak.Wiring;
using Klak.Wiring.Patcher;
using System.Collections.Generic;
using System;

[InitializeOnLoad]
public class EDDInitializer: NodeFactory.INodeFactory
{
	static EDDInitializer()
	{

		NodeFactory.AddFactory (new EDDInitializer ());
	}

	public Dictionary<Type,string> InitTypeAlias()
	{
		Dictionary<Type,string> _typeAlias = new Dictionary<Type, string> ();

		_typeAlias.Add (typeof(List<float>), "Float Array");
		_typeAlias.Add (typeof(List<int>), "Int Array");
		_typeAlias.Add (typeof(Quaternion), "Quaternion");
		_typeAlias.Add (typeof(Texture), "Texture");
		return _typeAlias;
	}
}

