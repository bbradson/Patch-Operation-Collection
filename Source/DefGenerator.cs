// Copyright (c) 2023 bradson
// This Source Code Form is subject to the terms of the MIT license.
// If a copy of the license was not distributed with this file,
// You can obtain one at https://opensource.org/licenses/MIT/.

namespace GeneratorOperation;

[UsedImplicitly]
public class DefGenerator : GeneratorBase
{
	public override string RootNodeName => "Defs";

	protected override bool ApplyResultingNode(XmlDocument xmlDocument, XmlNode matchNode, XmlNode valueChild)
	{
		var ownerDocument = matchNode.OwnerDocument;
		if (ownerDocument is null)
		{
			Log.Error($"'{matchNode}' lacks owner document at\n{matchNode.OuterXml}");
			return false;
		}

		var rootElement = ownerDocument.DocumentElement;
		if (rootElement is null)
		{
			Log.Error($"'{matchNode}' lacks root element at\n{matchNode.OuterXml}");
			return false;
		}

		rootElement.AppendChild(ownerDocument.ImportNode(valueChild, deep: true));
		return true;
	}
}